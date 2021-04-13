using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
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
        public override string ClassName => "Line";
        public Vector2 Begin { get; set; }
        public Vector2 End { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Render"></param>
        /// <param name="Tx"></param>
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            IntPtr SDL_RendererPtr = SDL_Renderer.SDLRenderer;

            // todo: FX api 
            SDL.SDL_SetRenderDrawBlendMode(SDL_RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);
                SDL.SDL_RenderDrawLineF(SDL_RendererPtr, (float)Begin.X - (float)SDL_Renderer.CCameraPosition.X, (float)Begin.Y - (float)SDL_Renderer.CCameraPosition.Y, (float)End.X - (float)SDL_Renderer.CCameraPosition.X, (float)End.Y - (float)SDL_Renderer.CCameraPosition.Y);
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 0, 0, 0, 0);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 255, 255, 255, 255);
                SDL.SDL_RenderDrawLineF(SDL_RendererPtr, (float)Begin.X, (float)Begin.Y, (float)End.X, (float)End.Y);
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 0, 0, 0, 0);
            }

        }
    }
}
