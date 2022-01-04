using Lightning.Core.API; 
using NuCore.NativeInterop;
#if WINDOWS 
using NuCore.NativeInterop.Win32;
#endif
using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// BootWindow
    /// 
    /// January 2, 2022
    /// 
    /// Defines the Lightning boot progress window. Replaces SplashScreen
    /// 
    /// Creates a special SDL window (using SDL2 directly) instead of calling through NuRender.
    /// </summary>
    public class BootWindow
    {
        private double _progress { get; set; }

        private Scene BWScene { get; set; }

        public BootWindow()
        {
            BWScene = new Scene();
        }

        public double Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (value < 0.0) value = 0.0;
                if (value > 1.0) value = 1.0;
                _progress = value;
                
            }

        }

        private string CurrentProgressString { get; set; }
        
        public void SetProgress(double NProgress, string NProgressString)
        {
            Progress = NProgress;
            CurrentProgressString = NProgressString;
        }

        private void Init()
        {
            // no nurender, so create a window
            // (this runs pre-init)

            // only initialise the subsystems we need to reduce singlethreaded loading time
            SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_EVENTS | SDL.SDL_InitFlags.SDL_INIT_VIDEO);

#if WINDOWS
            BWScene.AddWindow(new WindowSettings { ApplicationName = "BootWindow (temporary)",
                IsMainWindow = true,
                WindowSize = new Vector2Internal(960, 480),
                WindowPosition = new Vector2Internal(NativeMethodsWin32.GetSystemMetrics(SystemMetric.SM_CXSCREEN), NativeMethodsWin32.GetSystemMetrics(SystemMetric.SM_CYSCREEN)),
                WindowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN });
            #else
                Logging.Log("BootWindow not implemented on Linux/OSX yet!");
#endif

        
        }

        private void Init_LoadBootSplash()
        {
            GlobalSettings GS = DataModel.GetGlobalSettings();

            if (File.Exists(GS.BootSplashPath)
            {
                IntPtr BootSplash = SDL_image.IMG_Load(GS.BootSplashPath);
            }
            
        }
        private void Render()
        {

        }

    }
}
