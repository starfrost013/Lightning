using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Rectangle
    /// 
    /// April 12, 2021
    /// 
    /// Defines a rectangle.
    /// </summary>
    public class Rectangle : Line
    {
        public override string ClassName => "Rectangle";
        /// <summary>
        /// Is this rectangle filled?
        /// </summary>
        public bool Fill { get; set; }
        public override void Render(IntPtr SDL_Renderer, Texture Tx)
        {
            // todo: FX api 
            SDL.SDL_SetRenderDrawBlendMode(SDL_Renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, Colour.R, Colour.G, Colour.B, Colour.A);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, 255, 255, 255, 255);
            }


            SDL.SDL_Rect SR1 = new SDL.SDL_Rect();

            SR1.x = (int)Position.X;
            SR1.y = (int)Position.Y;
            SR1.w = (int)Size.X;
            SR1.h = (int)Size.Y;

            if (!Fill)
            {
                SDL.SDL_RenderDrawRect(SDL_Renderer, ref SR1);
            }
            else
            {
                SDL.SDL_RenderFillRect(SDL_Renderer, ref SR1);
            }

            SDL.SDL_SetRenderDrawColor(SDL_Renderer, 0, 0, 0, 0);
        }
    }
}
