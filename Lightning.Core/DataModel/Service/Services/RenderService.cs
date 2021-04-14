using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; 
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// RenderService
    /// 
    /// Handles rendering of all PhysicalInstances for Lightning using SDL2. 
    ///
    /// 2021-03-14: Created
    /// 2021-04-07: Added first functionality
    /// 2021-04-08: Added test code.
    /// 2021-04-09: Added Renderer class and SDL renderer.
    /// 2021-04-10: Added event loop
    /// 2021-04-11: Added nonanimated texture rendering
    /// </summary>
    public class RenderService : Service
    {
        public override string ClassName => "RenderService";
        public override ServiceImportance Importance => ServiceImportance.High;
        public Renderer Renderer { get; set; }

        public static bool RENDERER_INITIALISED { get; set; }
        public override ServiceStartResult OnStart()
        {
            // TEST code
            ServiceStartResult SSR = new ServiceStartResult();

            Logging.Log("RenderService Init", ClassName);

            SSR.Successful = true;
            return SSR; 
        
        }

        /// <summary>
        /// Initialises the Lightning SDL2 renderer.
        /// </summary>
        /// <returns></returns>
        private SDLInitialisationResult OnStart_InitSDL()
        {
            SDLInitialisationResult SDIR = new SDLInitialisationResult();

            // Initialises SDL.
            Logging.Log("Initialising SDL2 Video, Audio, and Events subsystems...", ClassName);
            int SDLErr = SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO | SDL.SDL_INIT_EVENTS);

            
            if (SDLErr < 0)
            {
                SDIR.FailureReason = $"Failed to initialise an SDL subsystem: {SDL.SDL_GetError()}";
                return SDIR; 
            }
            else
            {

                Logging.Log("Initialising SDL2_image...");

                int SDL_ImageErr = SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

                if (SDL_ImageErr < 0)
                {
                    SDIR.FailureReason = $"Failed to initialise SDL2_image: {SDL.SDL_GetError()}";
                    return SDIR; 
                }
                else
                {
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

                            if (Fullscreen_Setting != null)
                            {
                                Fullscreen = (bool)GGSR_FullScreen.Setting.SettingValue;
                            }

                            Logging.Log("Initialising SDL Window...");
                            SDIR.Renderer = new Renderer();

                            // Create a fullscreen window if fullscreen is false.
                            if (Fullscreen)
                            {
                                SDIR.Renderer.Window = SDL.SDL_CreateWindow(WindowTitle, DefaultWindowX, DefaultWindowY, WindowWidth, WindowHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
                            }
                            else
                            {
                                SDIR.Renderer.Window = SDL.SDL_CreateWindow(WindowTitle, DefaultWindowX, DefaultWindowY, WindowWidth, WindowHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                            }

                            if (SDIR.Renderer.Window == IntPtr.Zero)
                            {
                                SDIR.FailureReason = $"Failed to initialise window: {SDL.SDL_GetError()}";
                                return SDIR;

                            }
                            else
                            {
                                SDIR.Renderer.SDLRenderer = SDL.SDL_CreateRenderer(SDIR.Renderer.Window, 0, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

                                if (SDIR.Renderer.SDLRenderer == IntPtr.Zero)
                                {
                                    SDIR.FailureReason = $"Failed to initialise renderer: {SDL.SDL_GetError()}";
                                    return SDIR;
                                }
                                else
                                {
                                    SDIR.Successful = true;
                                    return SDIR;
                                }
                            }
                        }
                    }
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
                SDLInitialisationResult SDIR = OnStart_InitSDL();

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
                    Renderer = SDIR.Renderer;

                    LoadAndCacheTextures();
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
        /// Loads amd caches SDL_Textures.
        /// </summary>
        private void LoadAndCacheTextures()
        {
            Logging.Log("Building list of object textures to load...", ClassName);

            Workspace Ws = DataModel.GetWorkspace();

            List<PhysicalObject> ObjectsToLoad = new List<PhysicalObject>();

            foreach (Instance WsChild in Ws.Children)
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

            Logging.Log("Built list of object textures to load. Loading object textures...", ClassName);
            // Load the object textures from the physicalobjects we have acquired. 
            LoadObjectTextures(ObjectsToLoad);
        }

        private void LoadObjectTextures(List<PhysicalObject> ObjectsToLoad)
        {
            // The goal of this method is to load the textures for each object.
            // Additionally, the goal is to only load each texture once. 
            foreach (PhysicalObject PO in ObjectsToLoad)
            {
                GetInstanceResult GIR = PO.GetFirstChildOfType("Texture");

                if (!GIR.Successful)
                {
                    // these don't have a texture *as a child* (physicalobject contains texture)
                    continue;
                }
                else
                {
                    Texture Tx = (Texture)GIR.Instance;
                    
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

                        IntPtr Texture = SDL.SDL_CreateTextureFromSurface(Renderer.SDLRenderer, Surface);

                        // Do we add this texture to the cache?
                        bool AddToCache = true;

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
                                Texture TX2 = (Texture)GIR2.Instance;

                                foreach (Texture CachedTx in Renderer.TextureCache)
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
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        Control Ctl = new Control();
                        Ctl.KeyCode = CurEvent.key.keysym;
                        HandleKeyDown(Ctl); 
                        return; 
                    case SDL.SDL_EventType.SDL_QUIT:
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
            SDL.SDL_RenderClear(Renderer.SDLRenderer);

            // Get the workspace.
            Workspace Ws = DataModel.GetWorkspace();

            List<PhysicalObject> ObjectsToRender = new List<PhysicalObject>(); 

            foreach (Instance Ins in Ws.Children)
            {
                Type Tt = Ins.GetType();

                if (Tt == typeof(PhysicalObject))
                {
                    ObjectsToRender.Add((PhysicalObject)Ins);
                }
                else
                {
                    if (Tt.IsSubclassOf(typeof(PhysicalObject)))
                    {
                        ObjectsToRender.Add((PhysicalObject)Ins);
                    }
                }
            }

            // Render each object.
            Rendering_DoRenderPhysicalObjects(ObjectsToRender);

            SDL.SDL_RenderPresent(Renderer.SDLRenderer);

        }

        private void Rendering_DoRenderPhysicalObjects(List<PhysicalObject> PhysicalObjects)
        {
            
            foreach (PhysicalObject PO in PhysicalObjects)
            {
                GetInstanceResult GIR = PO.GetFirstChildOfType("Texture");

                if (!GIR.Successful)
                {
                    PO.Render(Renderer, null);
                    continue; 
                }
                else
                {
                    Texture Tx = (Texture)GIR.Instance;
                    
                    // Set the tiling mode and then render the texture.
                    foreach (Texture CachedTx in Renderer.TextureCache)
                    {
                        if (CachedTx.Path == Tx.Path)
                        {
                            Tx.SDLTexturePtr = CachedTx.SDLTexturePtr;
                            PO.Render(Renderer, Tx); 
                        }
                    }

                }
                    
            }
        }

        public override void OnKeyDown(Control KeyCode) => throw new NotImplementedException();

        private void HandleKeyDown(Control Ctl)
        {
            List<ControllableObject> ControllableObjects = BuildListOfControllableObjects();

            HandleKeyDown_NotifyAllControllableObjects(ControllableObjects, Ctl);
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

        private void HandleKeyDown_NotifyAllControllableObjects(List<ControllableObject> ControllableObjects, Control Ctl)
        {
            foreach (ControllableObject CO in ControllableObjects)
            {
                CO.OnKeyDown(Ctl);
            }
        }
    }
}
