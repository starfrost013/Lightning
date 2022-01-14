using NuCore.Utilities; 
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
    /// January 9, 2022 (modified January 10, 2022: fully working)
    /// 
    /// Implements an accelerated rendering pipeline for lighting.
    /// Builds a screen-space lightmap and renders it to a image (not nurender currently until RenderTarget is implemented) for rendering. 
    /// </summary>
    public class LightingService : Service
    {

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "LightingService";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override ServiceImportance Importance => ServiceImportance.Low;

        #region TEMP
        private IntPtr ImageTexture { get; set; }

        private Vector2 WindowSize { get; set; } 

        #endregion

        public override void OnDataSent(ServiceMessage Data)
        {
            return; 
        }

        public override ServiceStartResult OnStart()
        {
            // Subscribe to the pre-rendering event.
            PreRender += DoPreRender;
            OnRender += DoRender; 

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

            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

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

            WindowSize = new Vector2(WindowWidth, WindowHeight);

            // todo: make these enums
            ImageTexture = SDL.SDL_CreateTexture(MainWindow.Settings.RenderingInformation.RendererPtr, SDL.SDL_PIXELFORMAT_RGBA8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, WindowWidth, WindowHeight);
            
            foreach (Light Light in InstanceList)
            {
                Light.Render(PEEA.SDL_Renderer, null, ImageTexture);
            }

            return;
        }

        private List<Instance> DoPreRender_GetLights()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Light");

            if (!GMIR.Successful)
            {
                ErrorManager.ThrowError(ClassName, "ErrorAcquiringLightsException");
                return null;
            }
            else
            {
                return GMIR.Instances;
            }
        }

        private void DoRender(object Sender, RenderEventArgs PEEA)
        {
            Window MainWindow = PEEA.SDL_Renderer.GetMainWindow();

            SDL.SDL_Rect SrcRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = (int)WindowSize.X,
                h = (int)WindowSize.Y
            };

            SDL.SDL_Rect DstRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = (int)WindowSize.X,
                h = (int)WindowSize.Y
            };

            // Render the image texture to the display.
            SDL.SDL_RenderCopy(MainWindow.Settings.RenderingInformation.RendererPtr, ImageTexture, ref SrcRect, ref DstRect);

        }

        public override void Poll()
        {
            return;
        }
    }
}
