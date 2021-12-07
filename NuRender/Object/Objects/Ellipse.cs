using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public class Ellipse : Primitive
    {
        public override string ClassName => "Ellipse";


        /// <summary>
        /// The border size of this rectangle (relative to the <see cref="Size"/> property)
        /// </summary>
        public Vector2 BorderSize { get; set; }

        /// <summary>
        /// The size of this rectangle.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Start(WindowRenderingInformation RenderInfo)
        {
            return; 
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            if (Bordered) DrawEllipse(BorderSize, RenderInfo);
            DrawEllipse(Size, RenderInfo);
        }

        private void DrawEllipse(Vector2 RSize, WindowRenderingInformation RenderInfo)
        {
            SDL.SDL_FRect Rectangle = new SDL.SDL_FRect
            {
                w = (float)RSize.X,
                h = (float)RSize.Y,
                x = (float)Position.X,
                y = (float)Position.Y
            };

            // todo: colour

            SDL.SDL_SetRenderDrawColor(RenderInfo.RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);

            // todo: Filled Anti-Aliased (in c++)

            if (Antialiased && !Filled)
            {
                SDL_gfx.aaellipseRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)Position.Y, (int)RSize.X, (int)RSize.Y, Colour.R, Colour.G, Colour.B, Colour.A);
            }
            else
            {
                if (Filled)
                {
                    SDL_gfx.filledEllipseRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)Position.Y, (int)RSize.X, (int)RSize.Y, Colour.R, Colour.G, Colour.B, Colour.A);
                }
                else
                {
                    SDL_gfx.ellipseRGBA(RenderInfo.RendererPtr, (int)Position.X, (int)Position.Y, (int)RSize.X, (int)RSize.Y, Colour.R, Colour.G, Colour.B, Colour.A);
                }
            }


            SDL.SDL_SetRenderDrawColor(RenderInfo.RendererPtr, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.R, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.G, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.B, NuRender.NURENDER_DEFAULT_SDL_DRAW_COLOUR.A);
        }
    }
}
