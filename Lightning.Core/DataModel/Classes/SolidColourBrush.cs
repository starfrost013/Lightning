using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// SolidColourBrush
    /// 
    /// August 6, 2021
    /// 
    /// Defines a solid colour brush.
    /// </summary>
    public class SolidColourBrush : Brush 
    {
        internal override string ClassName => "SolidColourBrush";

        public bool Fill { get; set; }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {

            SDL.SDL_Rect DstRect = new SDL.SDL_Rect();
            DstRect.x = (int)Position.X;
            DstRect.y = (int)Position.Y;


            if (DisplayViewport == null)
            {
                DstRect.w = (int)DisplayViewport.X;
                DstRect.h = (int)DisplayViewport.Y;
            }
            else
            {
                DstRect.w = (int)Size.X;
                DstRect.h = (int)Size.Y;
            }

            SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, BackgroundColour.R, BackgroundColour.G, BackgroundColour.B, BackgroundColour.A);

            if (!Fill)
            {
                SDL.SDL_RenderDrawRect(SDL_Renderer.RendererPtr, ref DstRect);
            }
            else
            {
                SDL.SDL_RenderFillRect(SDL_Renderer.RendererPtr, ref DstRect);
            }
            

            SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, 0, 0, 0, 0);
            base.Render(SDL_Renderer, Tx);
        }
    }
}
