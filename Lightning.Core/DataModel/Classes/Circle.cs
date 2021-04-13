using Lightning.Core.SDL2;
using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Circle
    /// 
    /// April 12, 2021
    /// 
    /// Renders a circle.
    /// </summary>
    public class Circle : Primitive
    {
        public override string ClassName => "Circle";

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            IntPtr SDL_RendererPtr = SDL_Renderer.SDLRenderer;

            SDL.SDL_SetRenderDrawBlendMode(SDL_RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 255, 255, 255, 255);
            }

            // This isn't particularly efficient.
            // There's better ways to do this but this is the simplest for now. 
            for (int i = 0; i < 360; i++)
            {
                double X = Size.X * Math.Cos(MathUtil.DegreesToRadians(i)) + Position.X;
                double Y = Size.Y * Math.Sin(MathUtil.DegreesToRadians(i)) + Position.Y;

                // draw a point. 
                SDL.SDL_RenderDrawPointF(SDL_RendererPtr, (float)X - (float)SDL_Renderer.CCameraPosition.X, (float)Y - (float)SDL_Renderer.CCameraPosition.Y);
                
            }
        }


    }
}
