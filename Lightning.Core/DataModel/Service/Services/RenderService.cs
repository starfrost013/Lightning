﻿using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics; 
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
    /// 2021-04-10; Added event loop
    /// 
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
            Logging.Log(ClassName, "Temp?: Shutting down SDL...");

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

            SDLInitialisationResult SDIR = OnStart_InitSDL();

            if (!SDIR.Successful)
            {
                string ErrorString = $"Failed to initialise RenderService: {SDIR.FailureReason}";

                // Return a failureon error. 
                ErrorManager.ThrowError(ClassName, "RenderServiceInitialisationFailedException", ErrorString);

                //todo: crash the service here
                ServiceNotification SN = new ServiceNotification { ServiceClassName = ClassName, NotificationType = ServiceNotificationType.Crash };

                return;
            }
            else
            {
                Renderer = SDIR.Renderer;

                RENDERER_INITIALISED = true;

                return;
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
                    case SDL.SDL_EventType.SDL_QUIT:
                        // Less temp
                        ServiceNotification SN = new ServiceNotification { ServiceClassName = ClassName, NotificationType = ServiceNotificationType.Shutdown_ShutDownEngine };
                        ServiceNotifier.NotifySCM(SN); 
                        
                        return;
                }
            }
        }

        public override ServiceShutdownResult OnUnexpectedShutdown()
        {
            throw new NotImplementedException();
        }
    }
}
