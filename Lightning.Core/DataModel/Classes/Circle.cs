using Lightning.Core.SDL2;
using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
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
        internal override string ClassName => "Circle";

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

                if (Fill)
                {
                    // 360 - 180 = 180


                    double T1 = i - 180;
                    double T2 = 180 - i;


                    double FillLine1X = X;
                    double FillLine1Y = Y;

                    double FillLine2X;
                    double FillLine2Y;

                    if (i > 180)
                    {
                        FillLine2X = Size.X * Math.Cos(MathUtil.DegreesToRadians(Math.Abs(T1))) + Position.X;
                        FillLine2Y = Size.Y * Math.Cos(MathUtil.DegreesToRadians(Math.Abs(T1))) + Position.Y;
                    }
                    else
                    {
                        FillLine2X = Size.X * Math.Cos(MathUtil.DegreesToRadians(Math.Abs(T2))) + Position.X;
                        FillLine2Y = Size.Y * Math.Cos(MathUtil.DegreesToRadians(Math.Abs(T2))) + Position.Y;
                    }

                    SDL.SDL_RenderDrawLineF(SDL_RendererPtr, (float)FillLine1X - (float)SDL_Renderer.CCameraPosition.X, (float)FillLine1Y - (float)SDL_Renderer.CCameraPosition.Y, (float)FillLine2X - (float)SDL_Renderer.CCameraPosition.X, (float)FillLine2Y - (float)SDL_Renderer.CCameraPosition.Y);

                }

                // draw a point. 
                SDL.SDL_RenderDrawPointF(SDL_RendererPtr, (float)X - (float)SDL_Renderer.CCameraPosition.X, (float)Y - (float)SDL_Renderer.CCameraPosition.Y);
                
            }
        }


    }
}
