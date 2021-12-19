using NuRender.SDL2;
using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// UIService
    /// 
    /// June 20, 2021
    /// 
    /// Implements a 2D UI system for Lightning.
    /// </summary>
    public class UIService : Service
    {
        internal override string ClassName => "UIService";
        internal override ServiceImportance Importance => ServiceImportance.Low;

#if DEBUG

        /// <summary>
        /// DEBUG only - funny string to be displayed with debug text
        /// </summary>
        private string FunnyHahaDebugString { get; set; }

#endif
        public override ServiceStartResult OnStart()
        {
            ServiceStartResult SSR = new ServiceStartResult();

            Logging.Log("Initialising SDL2_ttf...", ClassName);

            if (SDL_ttf.TTF_Init() < 0)
            {
                SSR.FailureReason = $"Failed to initialise SDL2_ttf: {SDL.SDL_GetError()}";
            }
            else
            {
                SSR.Successful = true;
                return SSR; 
            }

            
            return SSR; 
        }

        private static bool UISERVICE_INITIALISED { get; set; }

        private void Init()
        {
            UISERVICE_INITIALISED = true; 
        }

        public override void Poll()
        {
            if (!UISERVICE_INITIALISED)
            {
                Init();
            }
            //main loop goes here
            //else 
            //{
            //
            //}

            return;
        }

        public override ServiceShutdownResult OnShutdown()
        {
            ServiceShutdownResult SSR = new ServiceShutdownResult();
            
            ShutdownSDL2TTF();

            SSR.Successful = true;

            return SSR;
        }

        private void ShutdownSDL2TTF()
        {
            Logging.Log("Shutting down SDL2_ttf...", ClassName);
            SDL_ttf.TTF_Quit();
            return; 
        }

       


        public override void OnDataSent(ServiceMessage Data)
        {
            return;
        }


    }
}
