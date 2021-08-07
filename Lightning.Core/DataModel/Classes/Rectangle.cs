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
            Brush Brush = base.GetBrush();

            if (Brush != null)
            {
                Brush.Render(SDL_Renderer, Tx); 
            }
            else
            {
                IntPtr SDL_RendererPtr = SDL_Renderer.RendererPtr;

                SDL_Renderer.SetCurBlendMode();

                if (BorderColour == null) BorderColour = new Color4(0, 0, 0, 0);

                SDL.SDL_Rect SR1 = new SDL.SDL_Rect();

                SR1.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
                SR1.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;
                SR1.w = (int)Size.X;
                SR1.h = (int)Size.Y;

                if (BorderThickness > 0) RenderBorder(SDL_Renderer, Tx);

                if (Colour != null)
                {
                    SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, Colour.R, Colour.G, Colour.B, Colour.A);
                }
                else
                {
                    SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 255, 255, 255, 255);
                }

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

        private void RenderBorder(Renderer SDL_Renderer, Texture Tx)
        {
            Vector2 BorderSize = new Vector2(Size.X + (BorderThickness * 2), Size.Y + (BorderThickness * 2));
            SDL.SDL_Rect SR2 = new SDL.SDL_Rect();

            SR2.x = (int)(Position.X - BorderThickness); // todo: position => int?
            SR2.y = (int)(Position.Y - BorderThickness); // todo: position => int?
            SR2.w = (int)BorderSize.X;
            SR2.h = (int)BorderSize.Y;

            SR2.x -= (int)SDL_Renderer.CCameraPosition.X;
            SR2.y -= (int)SDL_Renderer.CCameraPosition.Y;

            BorderFill = true; 

            SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, BorderColour.R, BorderColour.G, BorderColour.B, BorderColour.A);

            int Result = 0;

            if (!BorderFill)
            {
                Result = SDL.SDL_RenderDrawRect(SDL_Renderer.RendererPtr, ref SR2);
            }
            else
            {
                Result = SDL.SDL_RenderFillRect(SDL_Renderer.RendererPtr, ref SR2);
            }

            SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, 0, 0, 0, 0);


#if DEBUG
            if (Result < 0) Logging.Log(Result.ToString());
#endif
        }
    }
}
