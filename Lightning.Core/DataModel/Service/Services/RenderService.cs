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
    /// 2021-12-10: Rewritten for NuRender
    /// 2021-12-11: LoadTexture stuff rewritten for NuRender.
    /// 2021-12-18: Moved font loading and unloading to here from UIService
    /// 2022-01-09: Implemented PreRender
    /// 2022-01-13: Reimplemented OnRender
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

                    WS.SetWindowID(1);

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
                    
                    Logging.Log("Initialising window...", ClassName);

                    // Create a fullscreen window if fullscreen is false.
                    if (Fullscreen)
                    {
                        WS.WindowMode = WindowMode.Fullscreen;
 
                    }
                    else
                    {
                        WS.WindowMode = WindowMode.Windowed;
                    }

                    // Make sure this is the main window.
                    WS.IsMainWindow = true; 

                    MainScene.AddWindow(WS);

                    SDIR.Successful = true;
                    return SDIR;
                }
            }
        }

        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("Unloading fonts...");

            Rendering_UnloadAllFonts();

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
                    InitRendering_GetBlendMode();

                    InitRendering_LoadAndCacheTextures();

                    Window MainWindow = MainScene.GetMainWindow();

                    Rendering_LoadAllFonts(MainWindow.Settings.RenderingInformation);

                    Rendering_TriggerOnSpawn(); // todo: onspawn event

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
        

        /// <summary>
        /// Private: Gets and sets up the rendering blend mode for this game.
        /// </summary>
        private void InitRendering_GetBlendMode()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            Window MainWindow = MainScene.GetMainWindow();

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
                    MainWindow.Settings.RenderingInformation.BlendingMode = SDL.SDL_BlendMode.SDL_BLENDMODE_NONE;
                    return; 
                }
                else
                {
                    // if it has been successfully loaded...
                    GameSetting BlendModeSetting = GGSR.Setting;

                    if (BlendModeSetting.SettingValue != null) // check for a valid blending mode
                    {
                        MainWindow.Settings.RenderingInformation.BlendingMode = (SDL.SDL_BlendMode)BlendModeSetting.SettingValue;
                    }
                    else
                    {
                        // set a default and return if invalid value
                        MainWindow.Settings.RenderingInformation.BlendingMode = SDL.SDL_BlendMode.SDL_BLENDMODE_NONE; // .default? 
                    }

                    return;
                }
            }
        }

        private void Rendering_TriggerOnSpawn()
        {
            List<PhysicalInstance> ObjectsToLoad = BuildListOfPhysicalInstances();

            foreach (PhysicalInstance PO in ObjectsToLoad)
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

            List<PhysicalInstance> ObjectsToLoad = BuildListOfPhysicalInstances();

            Logging.Log("Built list of object textures to load. Loading object textures...", ClassName);
            // Load the object textures from the PhysicalInstances we have acquired. 
            //LoadObjectTextures(ObjectsToLoad);
        }

        private List<PhysicalInstance> Rendering_BuildListOfCurrentlyRenderablePhysicalInstances()
        {
            Workspace Ws = DataModel.GetWorkspace();

            Window MainWindow = MainScene.GetMainWindow();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("PhysicalInstance");

            List<PhysicalInstance> RenderableObjects = new List<PhysicalInstance>();

            if (!GMIR.Successful)
            {
                ErrorManager.ThrowError(ClassName, "ErrorObtainingListOfPhysicalInstancesToRenderException", $"Error acquiring list of PhysicalInstances to render: {GMIR.Successful}");
                return null;
            }
            else // if successful...
            {
                foreach (PhysicalInstance PI in GMIR.Instances)
                {
                    
                    // move this optimisation to nurender probably
                    if (PI.ForceToScreen)
                    {
                        if (PI.Position.X >= 0
                        && PI.Position.Y >= 0
                        && PI.Position <= (Vector2)MainWindow.Settings.WindowSize)
                        {
                            RenderableObjects.Add(PI);
                        }
                    }
                    else
                    {
                        // todo: remove ccameraposition
                        if (PI.Position == null)
                        {
                            // sky etc:
                            // null by default
                            // fix this bug

                            RenderableObjects.Add(PI); // HACK
                            continue; 
                        }

                        Vector2 CameraRelativePosition = PI.Position - (Vector2)MainWindow.Settings.RenderingInformation.CCameraPosition;

                        if (CameraRelativePosition.X >= 0
                        && CameraRelativePosition.Y >= 0
                        && CameraRelativePosition <= (Vector2)MainWindow.Settings.WindowSize)
                        {
                            RenderableObjects.Add(PI);
                        }
                    }
                }

                return RenderableObjects;
            }
        }

        private List<PhysicalInstance> BuildListOfPhysicalInstances()
        {
            Workspace Ws = DataModel.GetWorkspace();

            InstanceCollection Children = Ws.Children;

            List<PhysicalInstance> ObjectsToLoad = new List<PhysicalInstance>();

            foreach (Instance WsChild in Children)
            {
                Type ChiType = WsChild.GetType();

                if (ChiType == typeof(PhysicalInstance))
                {
                    ObjectsToLoad.Add((PhysicalInstance)WsChild);
                }
                else
                {
                    // If it's a subclass of PhysicalInstance, add it. 
                    if (ChiType.IsSubclassOf(typeof(PhysicalInstance)))
                    {
                        ObjectsToLoad.Add((PhysicalInstance)WsChild);
                    }
                }
            }

            return ObjectsToLoad;

        }

        private void LoadTexture(PhysicalInstance PO, ImageBrush Tx)
        {
            Window MainWindow = MainScene.GetMainWindow();

            if (!File.Exists(Tx.Path))
            {
                ErrorManager.ThrowError(ClassName, "CannotLoadNonexistentTextureException", $"Attempted to load a Texture at {Tx.Path} that does not exist!");
                ServiceNotification SN2 = new ServiceNotification { NotificationType = ServiceNotificationType.Crash, ServiceClassName = ClassName };
                ServiceNotifier.NotifySCM(SN2);

            }
            else
            {
                // this bit rewritten for nurender
                // as nurender has its own texture cache

                Image Image = new Image();

                Image.TextureInfo.Path = Tx.Path;

                // set imagebrush stuff for compat
                Image.Load(MainWindow.Settings.RenderingInformation);

                Tx.SDLTexturePtr = Image.TextureInfo.TexPtr;
            }
        }

        private void UpdateRendering()
        {
            // Get the current SDL event.
            SDL.SDL_Event CurEvent;

            if (SDL.SDL_PollEvent(out CurEvent) > 0)
            {
                // Since January 4, 2022
                // Window IDs are hardcoded.
                // Window ID 0 is BootWindow.
                // Window ID 1 is the main window.
                if (CurEvent.window.windowID == 1)
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

            }
            else
            {
                // Render the objects.
                Rendering_PreRender(MainScene);
                Rendering_RenderPhysicalInstances();
            }
        }

        private void Rendering_PreRender(Scene SDL_Renderer)
        {
            InstanceCollection InsList = Rendering_BuildListOfAllObjects();

            foreach (Instance Ins in InsList)
            {
                if (Ins.PreRender != null)
                {
                    PreRenderEventArgs PEEA = new PreRenderEventArgs();
                    PEEA.SDL_Renderer = SDL_Renderer;

                    Ins.PreRender(this, PEEA);
                }
            }
        }

        private void Rendering_RenderPhysicalInstances()
        {
            Window MainWindow = MainScene.GetMainWindow();

            SDL.SDL_RenderClear(MainWindow.Settings.RenderingInformation.RendererPtr);

            // Get the workspace.
            Workspace Ws = DataModel.GetWorkspace();

            // Acquire all PhysicalInstances currently being displayed
            List<PhysicalInstance> RenderablePIs = Rendering_BuildListOfCurrentlyRenderablePhysicalInstances();

            // Call Render events for objects that subscribe to the OnRender event.
            Rendering_CallOnRender();
            // Render each object.
            Rendering_DoRenderPhysicalInstances(RenderablePIs);

            // call nurender to do the legwork of scene rendering

            MainScene.Render(false); // render nurender.

        }

        private void Rendering_CallOnRender()
        {
            InstanceCollection Instances = Rendering_BuildListOfAllObjects();

            foreach (Instance Instance in Instances)
            {
                if (Instance.OnRender != null)
                {
                    RenderEventArgs REA = new RenderEventArgs();
                    REA.SDL_Renderer = MainScene;
                    Instance.OnRender(this, REA);
                }
            }
        }

        private void Rendering_DoRenderPhysicalInstances(List<PhysicalInstance> PhysicalInstances)
        {
            PhysicalInstances = PhysicalInstances.OrderBy(PO => PO.ZIndex).ToList();

            Window MainWindow = MainScene.GetMainWindow();

            
            foreach (PhysicalInstance PO in PhysicalInstances)
            {
                GetInstanceResult GIR = PO.GetFirstChildOfType("ImageBrush");
                
                if (PO.Attributes.HasFlag(InstanceTags.UsesCustomRenderPath)) continue; // object uses custom render path? (do we need this for NR?)

                if (!GIR.Successful
                    && !PO.Invisible) // check for custom render path being used (i.e. render() is not being called by something else) 
                {
                    //todo (January 10, 2022):
                    //remove default render method, only call render method for objects that subscribe to OnRender.
                    if (PO.OnRender == null)
                    {
                        RenderEventArgs REA = new RenderEventArgs();
                        REA.SDL_Renderer = MainScene;

                        PO.Render(MainScene, null, IntPtr.Zero);
                        continue;
                    }
                    // January 13, 2022: onrender already called here

                }
                else
                {

                    if (PO.Invisible) return;

                    ImageBrush Tx = (ImageBrush)GIR.Instance;

                    // HACK: Until we actually have a proper loader.
                    // might need to get rid of this one
                    if (!Tx.TEXTURE_INITIALISED)
                    {
                        PO.GetBrush();
                        Tx.Init(MainScene);
                    }

                    // Made this code a bit less hackish (December 11, 2021):
                    if (PO.OnRender == null)
                    {
                        PO.Render(MainScene, Tx, IntPtr.Zero);
                    }
                    else
                    {
                        RenderEventArgs REA = new RenderEventArgs();
                        REA.SDL_Renderer = MainScene;
                        REA.Tx = Tx;
                        PO.OnRender(MainScene, REA);
                    }

                    

                }
                    
            }
        }

        private InstanceCollection Rendering_BuildListOfAllObjects() // cahnge this to List<Instance> when i feel less lazy
        {
            GetMultiInstanceResult GMIR = DataModel.GetChildren(true); // will always be successful 

            return new InstanceCollection(GMIR.Instances);
        }

        private void HandleKeyDown(SDL.SDL_Event CurEvent)
        {
            
            InstanceCollection AllObjects = Rendering_BuildListOfAllObjects();

            HandleKeyDown_NotifyAllPhysicalInstances(AllObjects, CurEvent);
        }

        private List<ControllableObject> Rendering_BuildListOfControllableObjects()
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

        private void HandleKeyDown_NotifyAllPhysicalInstances(InstanceCollection AllObjects, SDL.SDL_Event CurEvent)
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
            List<PhysicalInstance> POList = BuildListOfPhysicalInstances();

            HandleMouseDown_NotifyAllPhysicalInstances(POList, Event); 
        }

        /// <summary>
        /// Private: Invokes the click event on all PhysicalInstances that hold it.
        /// </summary>
        /// <param name="PhysicalInstances"></param>
        /// <param name="CurEvent"></param>
        private void HandleMouseDown_NotifyAllPhysicalInstances(List<PhysicalInstance> PhysicalInstances, SDL.SDL_Event CurEvent)
        {
            foreach (PhysicalInstance PO in PhysicalInstances)
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
            List<PhysicalInstance> PhysicalInstances = BuildListOfPhysicalInstances();

            HandleMouseUp_NotifyAllPhysicalInstances(CurEvent, PhysicalInstances);
        }

        private void HandleMouseUp_NotifyAllPhysicalInstances(SDL.SDL_Event CurEvent, List<PhysicalInstance> PhysicalInstances)
        {
            foreach (PhysicalInstance PhysicalInstance in PhysicalInstances)
            {
                if (PhysicalInstance.OnMouseUp != null)
                {
                    MouseEventArgs EventArgs = new MouseEventArgs();
                    EventArgs.Button = (MouseButton)CurEvent.button.button;
                    EventArgs.ClickCount = CurEvent.button.clicks;
                    EventArgs.RelativePosition = new Vector2(CurEvent.button.x, CurEvent.button.y);

                    PhysicalInstance.OnMouseUp(this, EventArgs);
                }
            }
        }

        private void HandleMouseEnter(SDL.SDL_Event CurEvent)
        {
            List<PhysicalInstance> PhysicalInstances = BuildListOfPhysicalInstances();
            HandleMouseEnter_NotifyAllPhysicalInstances(CurEvent, PhysicalInstances);
        }

        private void HandleMouseEnter_NotifyAllPhysicalInstances(SDL.SDL_Event CurEvent, List<PhysicalInstance> PhysicalInstances)
        {
            foreach (PhysicalInstance PhysicalInstance in PhysicalInstances)
            {
                if (PhysicalInstance.OnMouseEnter != null)
                {
                    PhysicalInstance.OnMouseEnter(this, new EventArgs());
                }
            }
        }

        private void HandleMouseLeave(SDL.SDL_Event CurEvent)
        {
            List<PhysicalInstance> PhysicalInstances = BuildListOfPhysicalInstances();

            HandleMouseLeave_NotifyAllPhysicalInstances(CurEvent, PhysicalInstances);
        }

        private void HandleMouseLeave_NotifyAllPhysicalInstances(SDL.SDL_Event CurEvent, List<PhysicalInstance> PhysicalInstances)
        {
            foreach (PhysicalInstance PhysicalInstance in PhysicalInstances)
            {
                if (PhysicalInstance.OnMouseLeave != null)
                {
                    PhysicalInstance.OnMouseLeave(this, new EventArgs());
                }
            }
        }

        private void ThrowQuitEvent(SDL.SDL_Event CurEvent, bool Expected = false) // not good need to change asap
        {
            List<PhysicalInstance> PhysicalInstances = BuildListOfPhysicalInstances();

            ThrowQuitEvent_NotifyAllPhysicalInstances(CurEvent, PhysicalInstances, Expected);

        }

        private void ThrowQuitEvent_NotifyAllPhysicalInstances(SDL.SDL_Event CurEvent, List<PhysicalInstance> PhysicalInstances, bool Expected = false)
        {
            foreach (PhysicalInstance PO in PhysicalInstances)
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
            InstanceCollection AllObjects = Rendering_BuildListOfAllObjects();

            HandleKeyUp_NotifyAllPhysicalInstances(CurEvent, AllObjects);
        }

        private void HandleKeyUp_NotifyAllPhysicalInstances(SDL.SDL_Event CurEvent, InstanceCollection PhysicalInstances)
        {
            foreach (Instance PhysicalInstance in PhysicalInstances)
            {
                if (PhysicalInstance.KeyUp != null)
                {
                    KeyEventArgs KEA = GetKeyEventArgs(CurEvent);

                    PhysicalInstance.KeyUp(this, KEA);
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
                            PhysicalInstance PO = (PhysicalInstance)Data.Data[0];
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

        private void Rendering_LoadAllFonts(WindowRenderingInformation RenderInfo)
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Font");

            Logging.Log("Loading fonts...", ClassName);

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfFontsException");
                return;
            }
            else
            {
                for (int i = 0; i < GMIR.Instances.Count; i++)// collection is being modified
                {
                    Instance Instance = GMIR.Instances[i];

                    Font Fnt = (Font)Instance;

                    if (Fnt.Name != null)
                    {
                        Logging.Log($"Loading the font {Fnt.Name}...", ClassName);

                        Fnt.Load(RenderInfo);

                        if (!Fnt.FONT_LOADED)
                        {
                            // font failed to load
                            GMIR.Instances.Remove(Instance);
                        }
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "FontMustDeclareNameException");
                        GMIR.Instances.Remove(Instance);
                    }

                }
            }

        }

        private void Rendering_UnloadAllFonts()
        {

            Logging.Log($"Unloading all fonts...", ClassName);

            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Font");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfFontsException");
                return;
            }
            else
            {
                foreach (Instance Ins in GMIR.Instances)
                {
                    Font Fnt = (Font)Ins;

                    Logging.Log($"Unloading the font {Fnt.Name}...", ClassName);
                    Fnt.Unload();
                }
            }
        }




    }
}
