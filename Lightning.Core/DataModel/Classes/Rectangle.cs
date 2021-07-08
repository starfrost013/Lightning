using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
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
        internal override string ClassName => "Rectangle";

        
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            IntPtr SDL_RendererPtr = SDL_Renderer.RendererPtr;

            // todo: FX api 
            SDL.SDL_SetRenderDrawBlendMode(SDL_RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 255, 255, 255, 255);
            }


            SDL.SDL_Rect SR1 = new SDL.SDL_Rect();

            SR1.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
            SR1.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;
            SR1.w = (int)Size.X;
            SR1.h = (int)Size.Y;

            if (!Fill)
            {
                SDL.SDL_RenderDrawRect(SDL_RendererPtr, ref SR1);
            }
            else
            {
                SDL.SDL_RenderFillRect(SDL_RendererPtr, ref SR1);
            }

            SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 0, 0, 0, 0);
        }
    }
}
