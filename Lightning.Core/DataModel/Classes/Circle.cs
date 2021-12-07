using NuRender.SDL2;
using NuCore.Utilities;
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

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            IntPtr SDL_RendererPtr = SDL_Renderer.RendererPtr;

            SDL.SDL_SetRenderDrawBlendMode(SDL_RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);

            if (Colour != null)
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 255, 255, 255, 255);
            }


            // Midpoint ellipse algorithm
            // June 5, 2021
            // Old algo worked but was thousands of times (multiple orders of magnitude) slower

            double StartX = 0;
            double StartY = Position.Y;

            // Decision parameter for the next stage of calculations

            double DecisionParam1 = (Size.Y * Size.Y) - (Size.X * Size.X * Size.Y) + (0.25 * Size.X * Size.X);

            // faster than Math.Pow()?
            // why not use it 
            double DecisionParamX = 2 * Size.Y * Size.Y * StartX;
            double DecisionParamY = 2 * Size.X * Size.X * StartY;

            // Initial points
            Render_PlotPoints(SDL_Renderer, StartX, StartY);

            while (DecisionParamX < DecisionParamY)
            {
                if (DecisionParam1 < 0)
                {
                    StartX++;
                    DecisionParamX = DecisionParamX + (2 * Size.Y * Size.Y);
                    DecisionParam1 = DecisionParam1 + DecisionParamX + (Size.Y * Size.Y);
                }
                else
                {
                    StartX++;
                    StartY--;
                    DecisionParamX = DecisionParamX + (2 * Size.Y * Size.Y);
                    DecisionParamY = DecisionParamY - (2 * Size.X * Size.X);
                    DecisionParam1 = DecisionParam1 + DecisionParamX - DecisionParamY + (Size.Y * Size.Y);
                }

                // Region 1
                Render_PlotPoints(SDL_Renderer, StartX, StartY);
            }

            // PERFORM MATHEMATICS 
            double DecisionParam2 = ((Size.Y * Size.Y) * ((StartX + 0.5) * (StartX + 0.5)))
                + (Size.X * Size.X) * ((StartY - 1) * (StartY - 1))
                - (Size.X * Size.X * Size.Y * Size.Y);

            while (StartY >= 0)
            {
                // Region 2
                Render_PlotPoints(SDL_Renderer, StartX, StartY);

                if (DecisionParam2 > 0)
                {
                    StartY--;
                    DecisionParamY = DecisionParamY - (2 * Size.X * Size.X);
                    DecisionParam2 = DecisionParam2 + (Size.X * Size.X) - DecisionParamY;
                }
                else
                {
                    StartY--;
                    StartX++;
                    DecisionParamX = DecisionParamX + (2 * Size.Y * Size.Y);
                    DecisionParamY = DecisionParamY + (2 * Size.X * Size.X);
                    DecisionParam2 = DecisionParam2 + DecisionParamX - DecisionParamY + (Size.X * Size.X); 
                }
            }
        }

        /// <summary>
        /// Plots the points of each region of a midpoint drawn circle. 
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="StartX">X position to start drawing for each regio.n</param>
        /// <param name="StartY">Y position to start drawing for each region.</param>
        private void Render_PlotPoints(Renderer SDL_Renderer, double StartX, double StartY)
        {
            // draw the points
            if (!Fill)
            {
                // Set up the points we must draw for the circle.
                SDL.SDL_FPoint[] FPointSet = new SDL.SDL_FPoint[]
                {
                        new SDL.SDL_FPoint
                        {
                            x = ((float)StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X,
                            y = ((float)StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y,
                        },
                        new SDL.SDL_FPoint
                        {
                            x = ((float)-StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X,
                            y = ((float)StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y,
                        },
                        new SDL.SDL_FPoint
                        {
                            x = ((float)StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X,
                            y = ((float)-StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y,
                        },
                        new SDL.SDL_FPoint
                        {
                            x = ((float)-StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X,
                            y = ((float)-StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y,
                        },
                };

                SDL.SDL_RenderDrawPointsF(SDL_Renderer.RendererPtr, FPointSet, 4);
            }
            else
            {
                SDL.SDL_RenderDrawLineF(SDL_Renderer.RendererPtr, ((float)StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X, ((float)StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y, ((float)-StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X, ((float)-StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y);
                SDL.SDL_RenderDrawLineF(SDL_Renderer.RendererPtr, ((float)-StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X, ((float)StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y, ((float)StartX + (float)Position.X) - (float)SDL_Renderer.CCameraPosition.X, ((float)-StartY + (float)Position.Y) - (float)SDL_Renderer.CCameraPosition.Y);
            }

        }

    }
}
