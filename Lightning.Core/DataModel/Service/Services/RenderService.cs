using NuCore.Utilities;
using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// RenderService (NuRender version)
    /// 
    /// Handles the render loop using the NuRender API.
    ///
    /// 2021-03-14: Created
    /// 2021-04-07: Added first functionality
    /// 2021-04-08: Added test code.
    /// 2021-04-09: Added Renderer class and SDL renderer.
    /// 2021-04-10: Added event loop
    /// 2021-04-11: Added nonanimated texture rendering.
    /// 2021-04-1x: Refactoring
    /// 2021-04-17: Moved to Lightning.Core.API
    /// 2021-04-29: Removed RunningServices; all services are now Children of the SCM
    /// 2021-05-08: Minor changes
    /// 2021-05-13: Implemented ZIndex
    /// 2021-05-27: ACTUALLY implemented ZIndex (????)
    /// 2021-06-05: Add an FPS meter
    /// 2021-07-13: Add blend mode changing, various other stuff
    /// 2021-07-19: Implemented many events
    /// 2021-07-21: KeyDown now an event
    /// 2021-08-15: Animation loading, even MORE events
    /// 2021-12-10: Rweritten for NuRender
    /// 
    /// </summary>
    public class RenderService : Service
    {
        internal override string ClassName => "RenderService";
        internal override ServiceImportance Importance => ServiceImportance.High;
        private static bool RENDERER_INITIALISED { get; set; }

        /// <summary>
        /// The NuRender scene. 
        /// </summary>
        internal Scene MainScene { get; set; }

        public override ServiceStartResult OnStart()
        {
            // TEST code
            ServiceStartResult SSR = new ServiceStartResult();

            Logging.Log("Initialising RenderService...", ClassName);

            OnStart_InitNR();

            SSR.Successful = true;
            return SSR; 
        
        }

        /// <summary>
        /// Initialises the Lightning SDL2 renderer.
        /// </summary>
        /// <returns></returns>
        private SDLInitialisationResult OnStart_InitNR()
        {
            SDLInitialisationResult SDIR = new SDLInitialisationResult();

            // Initialises SDL.
            Logging.Log("Initialising NuRender...", ClassName);

            if (!NuRender.NuRender.NuRender_Init())
            {
                SDIR.FailureReason = "NuRender initialisation failed";
                return SDIR;
            }

            // if NuRender_Init returned true, initialise scene 
            MainScene = new Scene();
            SDIR.Successful = true;
            return SDIR;

            
        }

        private SDLInitialisationResult OnStart_InitNRWindow()
        {
            Logging.Log("Preparing to create NuRender window...", ClassName);

            SDLInitialisationResult SDIR = new SDLInitialisationResult();

            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
                return SDIR; // this is a fatal error so it will not run.
            }
            else
            {
                // Acquire the GameSettings around the window positioning. 
                GameSettings Settings = (GameSettings)GIR.Instance;
                GetGameSettingResult GGSR_WindowTitle = Settings.GetSetting("WindowTitle");
                GetGameSettingResult GGSR_WindowHeight = Settings.GetSetting("WindowHeight");
                GetGameSettingResult GGSR_WindowWidth = Settings.GetSetting("WindowWidth");
                GetGameSettingResult GGSR_DefaultWindowX = Settings.GetSetting("DefaultWindowPositionX");
                GetGameSettingResult GGSR_DefaultWindowY = Settings.GetSetting("DefaultWindowPositionY");
                GetGameSettingResult GGSR_FullScreen = Settings.GetSetting("Fullscreen");

                // Check that the settings were acquired.
                if (!GGSR_WindowTitle.Successful
                    || !GGSR_WindowWidth.Successful
                    || !GGSR_WindowHeight.Successful
                    || !GGSR_DefaultWindowX.Successful
                    || !GGSR_DefaultWindowY.Successful)
                {
                    ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "WindowTitle, WindowHeight, WindowWidth, DefaultWindowPositionX, and DefaultWindowPositionY must all be set");
                    return SDIR; // fatal error - will not run. 
                }
                else
                {
                    GameSetting WindowTitle_Setting = GGSR_WindowTitle.Setting;
                    GameSetting WindowWidth_Setting = GGSR_WindowWidth.Setting;
                    GameSetting WindowHeight_Setting = GGSR_WindowHeight.Setting;
                    GameSetting DefaultWindowX_Setting = GGSR_DefaultWindowX.Setting;
                    GameSetting DefaultWindowY_Setting = GGSR_DefaultWindowY.Setting;

                    // because we have to do this apparently
                    GameSetting Fullscreen_Setting = null;

                    if (GGSR_FullScreen.Successful)
                    {
                        Fullscreen_Setting = GGSR_FullScreen.Setting;
                    }

                    // Set up the window using the stuff we acquired from GameSettings. 
                    string WindowTitle = (string)WindowTitle_Setting.SettingValue;
                    int WindowWidth = (int)WindowWidth_Setting.SettingValue;
                    int WindowHeight = (int)WindowHeight_Setting.SettingValue;
                    int DefaultWindowX = (int)DefaultWindowX_Setting.SettingValue;
                    int DefaultWindowY = (int)DefaultWindowY_Setting.SettingValue;
                    bool Fullscreen = false;

                    // TODO: REPLACE WITH WINDOWMODE SETTING
                    if (Fullscreen_Setting != null)
                    {
                        Fullscreen = (bool)GGSR_FullScreen.Setting.SettingValue;
                    }
                    // TODO: REPLACE WITH WINDOWMODE SETTING

                    Logging.Log("Initialising renderer...", ClassName);

                    // initialise the NuRender window settings.
                    WindowSettings WS = new WindowSettings();

                    if (WindowWidth == 0 || WindowHeight == 0)
                    {
                        //todo: convert from vector2 to vector2internal
                        WS.WindowSize = new Vector2Internal(DefaultWindowX, DefaultWindowY);
                    }
                    else
                    {
                        WS.WindowSize = new Vector2Internal(WindowWidth, WindowHeight);
                    }

                    WS.ApplicationName = WindowTitle;
                    WS.WindowPosition = new Vector2Internal(DefaultWindowX, DefaultWindowY);

                    Logging.Log("Calling SDL_CreateWindow to initialise window...", ClassName);

                    // Create a fullscreen window if fullscreen is false.
                    if (Fullscreen)
                    {
                        WS.WindowMode = WindowMode.Fullscreen;
 
                    }
                    else
                    {
                        WS.WindowMode = WindowMode.Windowed;
                    }

                    MainScene.AddWindow(WS);

                    SDIR.Successful = true;
                    return SDIR;
                }
            }
        }

#if DEBUG
        public void ATest_RenderServiceQuerySettings()
        {
            Logging.Log("Query GameSettings Test:", "Automated Testing");

            Workspace Wks = DataModel.GetWorkspace();
           
            GetInstanceResult GS = Wks.GetFirstChildOfType("GameSettings");

            // It should always be loaded at this point.
            Debug.Assert(GS.Successful && GS.Instance != null);

            GameSettings Settings = (GameSettings)GS.Instance;

            GetGameSettingResult GGSR = Settings.GetSetting("MaxFPS");

            // assert - this means the setting failed to load previously.
            Debug.Assert(GGSR.Successful && GGSR.Setting != null);

            GameSetting Setting = GGSR.Setting;

            Logging.Log($"MaxFPS: {Setting.SettingValue}");

        }
#endif
        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("Shutting down SDL...", ClassName);

            SDL.SDL_Quit();

            ServiceShutdownResult SSR = new ServiceShutdownResult();

            SSR.Successful = true; 

            return SSR; // do nothing for now
        }

        public override void Poll()
        {
            // This initialises the SDL code.
            if (!RENDERER_INITIALISED)
            {
                InitRendering();
               
                return; 
            }
            else
            {
                UpdateRendering();
                return; 
            }

            
        }

        private void InitRendering()
        {
            Logging.Log("Initialising SDL renderer...", ClassName);

            try
            {
                SDLInitialisationResult SDIR = OnStart_InitNRWindow();

                if (!SDIR.Successful)
                {

                    // Return a failureon error. 
                    ErrorManager.ThrowError(ClassName, "RenderServiceInitialisationFailedException", SDIR.FailureReason);

                    //todo: crash the service here
                    ServiceNotification SN = new ServiceNotification { ServiceClassName = ClassName, NotificationType = ServiceNotificationType.Crash };

                    return;
                }
                else
                {
                    InitRendering_DisplaySplash();

                    InitRendering_GetBlendMode();

                    InitRendering_LoadAndCacheTextures();

                    TriggerOnSpawn();

                    Workspace Ws = DataModel.GetWorkspace();

                    RENDERER_INITIALISED = true;

                    return;
                }
            }
            catch (DllNotFoundException err)
            {
                ErrorManager.ThrowError(ClassName, "ErrorLoadingSDL2DllException", err);
                ServiceNotification SN4 = new ServiceNotification { NotificationType = ServiceNotificationType.Crash, ServiceClassName = ClassName };
                ServiceNotifier.NotifySCM(SN4);
            }


        }

        private void InitRendering_DisplaySplash()
        {
            SplashScreen SS = (SplashScreen)DataModel.CreateInstance("SplashScreen");

            GetInstanceResult GIR = SS.GetFirstChildOfType("Texture");

            if (!GIR.Successful
                || GIR.Instance == null)
            {
                return;
            }
            else
            {
                ImageBrush Tx = (ImageBrush)GIR.Instance;

                Tx.Position = new Vector2(0, 0);
                Tx.Size = new Vector2(MainScene.GetMainWindow().Settings.WindowSize.X, MainScene.GetMainWindow().Settings.WindowSize.Y);

                Tx.SDLTexturePtr = SDL_image.IMG_Load(Tx.Path);

                SS.Render(Renderer, Tx);

                SDL.SDL_RenderPresent(Renderer.RendererPtr); 
            }

            
        }

        /// <summary>
        /// Private: Gets and sets up the rendering blend mode for this game.
        /// </summary>
        private void InitRendering_GetBlendMode()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful
                || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
            }
            else
            {
                GameSettings GS = (GameSettings)GIR.Instance;

                GetGameSettingResult GGSR = GS.GetSetting("RenderingBlendMode");
                
                if (!GGSR.Successful) // set a default if we do not work
                {
                    Renderer.BlendMode = RenderingBlendMode.None;
                    return; 
                }
                else
                {
                    // if it has been successfully loaded...
                    GameSetting BlendModeSetting = GGSR.Setting;

                    if (BlendModeSetting.SettingValue != null) // check for a valid logo
                    {
                        Renderer.BlendMode = (RenderingBlendMode)BlendModeSetting.SettingValue;
                    }
                    else
                    {
                        // set a default and return if invalid value
                        Renderer.BlendMode = RenderingBlendMode.None; // .default? 
                    }

                    return;
                }
            }
        }

        private void TriggerOnSpawn()
        {
            List<PhysicalObject> ObjectsToLoad = BuildListOfPhysicalObjects();

            foreach (PhysicalObject PO in ObjectsToLoad)
            {
                PO.OnSpawn();
            }
        }
        /// <summary>
        /// Loads amd caches SDL_Textures.
        /// </summary>
        private void InitRendering_LoadAndCacheTextures()
        {
            Logging.Log("Building list of object textures to load...", ClassName);

            List<PhysicalObject> ObjectsToLoad = BuildListOfPhysicalObjects();

            Logging.Log("Built list of object textures to load. Loading object textures...", ClassName);
            // Load the object textures from the physicalobjects we have acquired. 
            //LoadObjectTextures(ObjectsToLoad);
        }

        private List<PhysicalObject> BuildListOfPhysicalObjects()
        {
            Workspace Ws = DataModel.GetWorkspace();

            InstanceCollection Children = Ws.Children;

            List<PhysicalObject> ObjectsToLoad = new List<PhysicalObject>();

            foreach (Instance WsChild in Children)
            {
                Type ChiType = WsChild.GetType();

                if (ChiType == typeof(PhysicalObject))
                {
                    ObjectsToLoad.Add((PhysicalObject)WsChild);
                }
                else
                {
                    // If it's a subclass of PhysicalObject, add it. 
                    if (ChiType.IsSubclassOf(typeof(PhysicalObject)))
                    {
                        ObjectsToLoad.Add((PhysicalObject)WsChild);
                    }
                }
            }

            return ObjectsToLoad;

        }

        private void LoadTexture(PhysicalObject PO, ImageBrush Tx)
        {

            if (!File.Exists(Tx.Path))
            {
                ErrorManager.ThrowError(ClassName, "CannotLoadNonexistentTextureException", $"Attempted to load a Texture at {Tx.Path} that does not exist!");
                ServiceNotification SN2 = new ServiceNotification { NotificationType = ServiceNotificationType.Crash, ServiceClassName = ClassName };
                ServiceNotifier.NotifySCM(SN2);

            }
            else
            {
                Logging.Log($"Loading texture at {Tx.Path}...", ClassName);
                // Load an image to a surface and create a texture from it
                IntPtr Surface = SDL_image.IMG_Load(Tx.Path);

                if (Surface == IntPtr.Zero)
                {
                    ErrorManager.ThrowError(ClassName, "ErrorLoadingTextureException", $"An error occurred loading the Texture at {Tx.Path}: {SDL.SDL_GetError()}");
                    ServiceNotification SN3 = new ServiceNotification { NotificationType = ServiceNotificationType.Crash, ServiceClassName = ClassName };
                    ServiceNotifier.NotifySCM(SN3);
                }

                IntPtr Texture = SDL.SDL_CreateTextureFromSurface(Renderer.RendererPtr, Surface);

                // Do we add this texture to the cache?
                bool AddToCache = true;

                List<PhysicalObject> ObjectsToLoad = BuildListOfPhysicalObjects(); 

                // Add a texture to the cache.
                foreach (PhysicalObject PO2 in ObjectsToLoad)
                {
                    // don't check ourselves.
                    if (PO2 == PO) continue;

                    GetInstanceResult GIR2 = PO2.GetFirstChildOfType("Texture");

                    if (!GIR2.Successful)
                    {
                        continue;
                    }
                    else
                    {
                        ImageBrush TX2 = (ImageBrush)GIR2.Instance;

                        foreach (ImageBrush CachedTx in Renderer.TextureCache)
                        {
                            if (Tx.Path == TX2.Path)
                            {
                                AddToCache = false;
                            }
                        }

                    }
                }

                if (AddToCache)
                {
                    Logging.Log($"Caching texture at {Tx.Path}...", ClassName);
                    Tx.SDLTexturePtr = Texture;
                    Renderer.TextureCache.Add(Tx);
                }
                else
                {
                    // destroy textures we don't want
                    // saves memory 
                    SDL.SDL_FreeSurface(Surface);
                    SDL.SDL_DestroyTexture(Texture);
                }
            }
        }

        private void UpdateRendering()
        {
            // Get the current SDL event.
            SDL.SDL_Event CurEvent;
            
            if (SDL.SDL_PollEvent(out CurEvent) > 0)
            {
                switch (CurEvent.type)
                {
                    case SDL.SDL_EventType.SDL_KEYUP:
                        HandleKeyUp(CurEvent);
                        return; 
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        HandleKeyDown(CurEvent); 
                        return;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        HandleMouseUp(CurEvent);
                        return;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        HandleMouseDown(CurEvent);
                        return;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        switch (CurEvent.window.windowEvent)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                                HandleMouseEnter(CurEvent);
                                return;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                                HandleMouseLeave(CurEvent);
                                return;
                            default:
                                return; 
                        }

                    case SDL.SDL_EventType.SDL_QUIT:
                        ThrowQuitEvent(CurEvent); 
                        // Less temp
                        ServiceNotification SN = new ServiceNotification { ServiceClassName = ClassName, NotificationType = ServiceNotificationType.Shutdown_ShutDownEngine };
                        ServiceNotifier.NotifySCM(SN); 
                        
                        return;
                }
            }
            else
            {
                // Render the objects.
                Rendering_RenderPhysicalObjects(); 
            }
        }

        private void Rendering_RenderPhysicalObjects()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(Renderer.RendererPtr);

            // Get the workspace.
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("PhysicalObject");

            if (GMIR.Instances != null
                && GMIR.Successful)
            {
                // perhaps this works?
                List<PhysicalObject> ObjectsToRender = ListTransfer<Instance, PhysicalObject>.TransferBetweenTypes(GMIR.Instances); 
                // Render each object.
                Rendering_DoRenderPhysicalObjects(ObjectsToRender);

                SDL.SDL_RenderPresent(Renderer.RendererPtr);
            }
            else
            {
                ErrorManager.ThrowError(ClassName, "ErrorOccurredAcquiringPhysicalObjectListException");
                return; 
            }

            

        }

        private void Rendering_DoRenderPhysicalObjects(List<PhysicalObject> PhysicalObjects)
        {
            PhysicalObjects = PhysicalObjects.OrderBy(PO => PO.ZIndex).ToList();



            foreach (PhysicalObject PO in PhysicalObjects)
            {
                GetInstanceResult GIR = PO.GetFirstChildOfType("ImageBrush");
                
                if (PO.Attributes.HasFlag(InstanceTags.UsesCustomRenderPath)) continue;

                if (!GIR.Successful
                    && !PO.Invisible) // check for custom render path being used (i.e. render() is not being called by something else) 
                {
                    if (PO.OnRender == null)
                    {
                        RenderEventArgs REA = new RenderEventArgs();
                        REA.SDL_Renderer = Renderer;

                        PO.Render(Renderer, null);
                        continue;
                    }
                    else
                    {
                        RenderEventArgs REA = new RenderEventArgs();
                        REA.SDL_Renderer = Renderer;
                        PO.OnRender(this, REA);
                    }

                }
                else
                {

                    if (PO.Invisible) return;

                    ImageBrush Tx = (ImageBrush)GIR.Instance;

                    // HACK: Until we actually have a proper loader.
                    if (!Tx.TEXTURE_INITIALISED)
                    {
                        PO.GetBrush();
                        Tx.Init();
                        
                    }

                    // END VERY BAD HACK

                    // Set the tiling mode and then render the texture.
                    for (int i = 0; i < Renderer.TextureCache.Count; i++)
                    {
                        ImageBrush CachedTx = Renderer.TextureCache[i];

                        if (CachedTx.Path == Tx.Path)
                        {
                            IntPtr TexturePointer = CachedTx.SDLTexturePtr;

                            Tx.SDLTexturePtr = CachedTx.SDLTexturePtr;

                            if (PO.OnRender == null)
                            {
                                PO.Render(Renderer, Tx);
                            }
                            else
                            {
                                RenderEventArgs REA = new RenderEventArgs();
                                REA.SDL_Renderer = Renderer;
                                REA.Tx = Tx;
                                PO.OnRender(this, REA);
                            }

                            
                        }
                    }
                    

                }
                    
            }
        }

        private InstanceCollection BuildListOfAllObjects()
        {
            Workspace Ws = DataModel.GetWorkspace();

            return Ws.Children;
        }

        private void HandleKeyDown(SDL.SDL_Event CurEvent)
        {
            
            InstanceCollection AllObjects = BuildListOfAllObjects();

            HandleKeyDown_NotifyAllPhysicalObjects(AllObjects, CurEvent);
        }

        private List<ControllableObject> BuildListOfControllableObjects()
        {
            List<ControllableObject> ControllableObjects = new List<ControllableObject>();

            Workspace Ws = DataModel.GetWorkspace();

            foreach (Instance Ins in Ws.Children)
            {
                Type InsType = Ins.GetType();

                if (InsType.IsSubclassOf(typeof(ControllableObject)))
                {
                    ControllableObjects.Add((ControllableObject)Ins);
                }
            }

            return ControllableObjects;
        }

        private void HandleKeyDown_NotifyAllPhysicalObjects(InstanceCollection AllObjects, SDL.SDL_Event CurEvent)
        {
            foreach (Instance PO in AllObjects)
            {
                if (PO.OnKeyDownHandler != null)
                {
                    KeyEventArgs KEA = GetKeyEventArgs(CurEvent);

                    PO.OnKeyDownHandler(this, KEA);
                }
                
            }
        }

        private void HandleMouseDown(SDL.SDL_Event Event)
        {
            List<PhysicalObject> POList = BuildListOfPhysicalObjects();

            HandleMouseDown_NotifyAllPhysicalObjects(POList, Event); 
        }

        /// <summary>
        /// Private: Invokes the click event on all physicalobjects that hold it.
        /// </summary>
        /// <param name="PhysicalObjects"></param>
        /// <param name="CurEvent"></param>
        private void HandleMouseDown_NotifyAllPhysicalObjects(List<PhysicalObject> PhysicalObjects, SDL.SDL_Event CurEvent)
        {
            foreach (PhysicalObject PO in PhysicalObjects)
            {
                if (PO.Click != null)
                {
                    MouseEventArgs CEA = new MouseEventArgs();

                    CEA.Button = (MouseButton)CurEvent.button.button;
                    CEA.ClickCount = CurEvent.button.clicks;
                    CEA.RelativePosition = new Vector2(CurEvent.button.x, CurEvent.button.y);

                    PO.Click(this, CEA);
                }
                else
                {
                    continue; 
                }
                
            }
        }

        private void HandleMouseUp(SDL.SDL_Event CurEvent)
        {
            List<PhysicalObject> PhysicalObjects = BuildListOfPhysicalObjects();

            HandleMouseUp_NotifyAllPhysicalObjects(CurEvent, PhysicalObjects);
        }

        private void HandleMouseUp_NotifyAllPhysicalObjects(SDL.SDL_Event CurEvent, List<PhysicalObject> PhysicalObjects)
        {
            foreach (PhysicalObject PhysicalObject in PhysicalObjects)
            {
                if (PhysicalObject.OnMouseUp != null)
                {
                    MouseEventArgs EventArgs = new MouseEventArgs();
                    EventArgs.Button = (MouseButton)CurEvent.button.button;
                    EventArgs.ClickCount = CurEvent.button.clicks;
                    EventArgs.RelativePosition = new Vector2(CurEvent.button.x, CurEvent.button.y);

                    PhysicalObject.OnMouseUp(this, EventArgs);
                }
            }
        }

        private void HandleMouseEnter(SDL.SDL_Event CurEvent)
        {
            List<PhysicalObject> PhysicalObjects = BuildListOfPhysicalObjects();
            HandleMouseEnter_NotifyAllPhysicalObjects(CurEvent, PhysicalObjects);
        }

        private void HandleMouseEnter_NotifyAllPhysicalObjects(SDL.SDL_Event CurEvent, List<PhysicalObject> PhysicalObjects)
        {
            foreach (PhysicalObject PhysicalObject in PhysicalObjects)
            {
                if (PhysicalObject.OnMouseEnter != null)
                {
                    PhysicalObject.OnMouseEnter(this, new EventArgs());
                }
            }
        }

        private void HandleMouseLeave(SDL.SDL_Event CurEvent)
        {
            List<PhysicalObject> PhysicalObjects = BuildListOfPhysicalObjects();

            HandleMouseLeave_NotifyAllPhysicalObjects(CurEvent, PhysicalObjects);
        }

        private void HandleMouseLeave_NotifyAllPhysicalObjects(SDL.SDL_Event CurEvent, List<PhysicalObject> PhysicalObjects)
        {
            foreach (PhysicalObject PhysicalObject in PhysicalObjects)
            {
                if (PhysicalObject.OnMouseLeave != null)
                {
                    PhysicalObject.OnMouseLeave(this, new EventArgs());
                }
            }
        }

        private void ThrowQuitEvent(SDL.SDL_Event CurEvent, bool Expected = false) // not good need to change asap
        {
            List<PhysicalObject> PhysicalObjects = BuildListOfPhysicalObjects();

            ThrowQuitEvent_NotifyAllPhysicalObjects(CurEvent, PhysicalObjects, Expected);

        }

        private void ThrowQuitEvent_NotifyAllPhysicalObjects(SDL.SDL_Event CurEvent, List<PhysicalObject> PhysicalObjects, bool Expected = false)
        {
            foreach (PhysicalObject PO in PhysicalObjects)
            {
                if (PO.OnShutdown != null)
                {
                    ShutdownEventArgs SEA = new ShutdownEventArgs();

                    SEA.Expected = Expected;

                    PO.OnShutdown(this, SEA);
                }
            }
        }

        private void HandleKeyUp(SDL.SDL_Event CurEvent)
        {
            InstanceCollection AllObjects = BuildListOfAllObjects();

            HandleKeyUp_NotifyAllPhysicalObjects(CurEvent, AllObjects);
        }

        private void HandleKeyUp_NotifyAllPhysicalObjects(SDL.SDL_Event CurEvent, InstanceCollection PhysicalObjects)
        {
            foreach (Instance PhysicalObject in PhysicalObjects)
            {
                if (PhysicalObject.KeyUp != null)
                {
                    KeyEventArgs KEA = GetKeyEventArgs(CurEvent);

                    PhysicalObject.KeyUp(this, KEA);
                }
            }
        }

        private KeyEventArgs GetKeyEventArgs(SDL.SDL_Event CurEvent)
        {
            KeyEventArgs KEA = new KeyEventArgs();
            KEA.Key = new Control();
            KEA.Key.KeyCode = CurEvent.key.keysym;

            KEA.Repeat = (CurEvent.key.repeat > 0);
            KEA.RepeatCount = CurEvent.key.repeat;

            return KEA;
        }

        public override void OnDataSent(ServiceMessage Data)
        {
            switch (Data.Name)
            {
                case "LoadTexture":
                    if (Data.Data.Count != 2)
                    {
                        ErrorManager.ThrowError(ClassName, "InvalidTextureLoadMessageException", "A texture load message must have two components!");
                        return; 
                    }
                    else
                    {
                        try 
                        {
                            PhysicalObject PO = (PhysicalObject)Data.Data[0];
                            ImageBrush Tx = (ImageBrush)Data.Data[1];

                            LoadTexture(PO, Tx);
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            ErrorManager.ThrowError(ClassName, "InvalidTextureLoadMessageException", $"An error occurred loading a texture: Invalid LoadTexture message sent!\n\nException: {ex}");
#else
                            ErrorManager.ThrowError(ClassName, "InvalidTextureLoadMessageException", $"An error occurred loading a texture: Invalid LoadTexture message sent");
#endif
                            return; 
                        }
                    }
                    return; 
            }

            return;
        }


    }
}
