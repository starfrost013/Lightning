using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public class Rectangle : Primitive
    {
        public override string ClassName => "Rectangle";


        /// <summary>
        /// The border size of this rectangle (relative to the <see cref="Size"/> property)
        /// </summary>
        public Vector2Internal BorderSize { get; set; }

        /// <summary>
        /// The size of this rectangle.
        /// </summary>
        public Vector2Internal Size { get; set; }

        /// <summary>
        /// Radius of the border of this rectangle. If above 0, this rectangle will be rendered as a rounded rectangle. 
        /// </summary>
        public int BorderRadius { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Start(WindowRenderingInformation RenderInfo)
        {
            return; 
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            if (Bordered) DrawRectangle(BorderSize, RenderInfo);
            DrawRectangle(Size, RenderInfo);
        }

        private void DrawRectangle(Vector2Internal RSize, WindowRenderingInformation RenderInfo)
        {

            SDL.SDL_SetRenderDrawColor(RenderInfo.RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);

            if (BorderRadius > 0)
            {
                if (Filled)
                {
                    SDL_gfx.boxRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)RSize.X, (int)Position.Y, (int)RSize.Y, Colour.R, Colour.G, Colour.B, Colour.A);
                }
                else
                {
                    SDL_gfx.rectangleRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)RSize.X, (int)Position.Y, (int)RSize.Y, Colour.R, Colour.G, Colour.B, Colour.A);
                }
            }
            else
            {
                if (Filled)
                {
                    SDL_gfx.roundedBoxRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)RSize.X, (int)Position.Y, (int)RSize.Y, BorderRadius, Colour.R, Colour.G, Colour.B, Colour.A);
                }
                else
                {
                    SDL_gfx.roundedRectangleRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)RSize.X, (int)Position.Y, (int)RSize.Y, BorderRadius, Colour.R, Colour.G, Colour.B, Colour.A);
                }
            }

            SDL.SDL_SetRenderDrawColor(RenderInfo.RendererPtr, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.R, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.G, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.B, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.A);
        }
    }
}
