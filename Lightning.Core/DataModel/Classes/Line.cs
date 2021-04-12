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
        public override void Render(IntPtr SDL_Renderer, Texture Tx)
        {
            // todo: FX api 
            SDL.SDL_SetRenderDrawBlendMode(SDL_Renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, Colour.R, Colour.G, Colour.B, Colour.A);
                SDL.SDL_RenderDrawLineF(SDL_Renderer, (float)Begin.X, (float)Begin.Y, (float)End.X, (float)End.Y);
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, 0, 0, 0, 0);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, 255, 255, 255, 255);
                SDL.SDL_RenderDrawLineF(SDL_Renderer, (float)Begin.X, (float)Begin.Y, (float)End.X, (float)End.Y);
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, 0, 0, 0, 0);
            }

        }
    }
}
