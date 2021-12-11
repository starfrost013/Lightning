using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Line
    /// 
    /// April 12, 2021
    /// 
    /// Implements...a line. Drawn using the SDL2 line API methods. Woo!
    /// </summary>
    public class Line : Primitive
    {
        internal override string ClassName => "Line";

        internal override InstanceTags Attributes => base.Attributes; // TEMP
        public Vector2 Begin { get; set; }
        public Vector2 End { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Render"></param>
        /// <param name="Tx"></param>
        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow(); 

            IntPtr SDL_RendererPtr = MainWindow.Settings.RenderingInformation.RendererPtr;

            // todo: FX api 
            SDL.SDL_SetRenderDrawBlendMode(SDL_RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);
                SDL.SDL_RenderDrawLineF(SDL_RendererPtr, (float)Begin.X - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.X, (float)Begin.Y - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.Y, (float)End.X - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.X, (float)End.Y - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 0, 0, 0, 0);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 255, 255, 255, 255);
                SDL.SDL_RenderDrawLineF(SDL_RendererPtr, (float)Begin.X - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.X, (float)Begin.Y - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.Y, (float)End.X - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.X, (float)End.Y - (float)MainWindow.Settings.RenderingInformation.CCameraPosition.Y);
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 0, 0, 0, 0);
            }

        }
    }
}
