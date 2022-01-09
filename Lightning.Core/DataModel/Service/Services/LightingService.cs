using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LightingService
    /// 
    /// January 9, 2022 
    /// 
    /// Implements an accelerated rendering pipeline for lighting.
    /// Builds a screen-space lightmap and renders it to a NuRender Image for rendering. 
    /// </summary>
    public class LightingService : Service
    {
        internal override ServiceImportance Importance => ServiceImportance.Low;

        public override void OnDataSent(ServiceMessage Data)
        {
            return; 
        }

        public override ServiceStartResult OnStart()
        {
            // Subscribe to the pre-rendering event.
            PreRender += DoPreRender; 
            return new ServiceStartResult { Successful = true };
        }

        public override ServiceShutdownResult OnShutdown()
        {
            return new ServiceShutdownResult { Successful = true };
        }

        private void DoPreRender(object Sender, PreRenderEventArgs PEEA)
        {
            Window MainWindow = PEEA.SDL_Renderer.GetMainWindow();

            List<Instance> InstanceList = DoPreRender_GetLights();

            GetInstanceResult GIR = DataModel.GetFirstChildOfType("GameSettings");

            // we already checked for gamesettings so we don't need to check them
            //
            GameSettings GS = (GameSettings)GIR.Instance;

            GetGameSettingResult GGSR_Width = GS.GetSetting("WindowWidth");
            GetGameSettingResult GGSR_Height = GS.GetSetting("WindowHeight");

            // as these settings are required by renderservice,
            // they are already set so we don't need to check again

            GameSetting GS_Width = GGSR_Width.Setting;
            GameSetting GS_Height = GGSR_Height.Setting;

            // we've already checked they are valid values, so get the values

            int WindowWidth = Convert.ToInt32(GS_Width.SettingValue);
            int WindowHeight = Convert.ToInt32(GS_Height.SettingValue);

            // create a new Vector2 for the window size
            // and a new texture for the screen space lightmap

            Vector2 WindowSize = new Vector2(WindowWidth, WindowHeight);

            // todo: make these enums
            IntPtr ImageTexture = SDL.SDL_CreateTexture(MainWindow.Settings.RenderingInformation.RendererPtr, SDL.SDL_PIXELFORMAT_RGBA8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, WindowWidth, WindowHeight);
            return;
        }

        private List<Instance> DoPreRender_GetLights()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Light");

            if (!GMIR.Successful)
            {
                //todo: throw error message
                return null;
            }
            else
            {
                return GMIR.Instances;
            }
        }

        public override void Poll()
        {
            return;
        }
    }
}
