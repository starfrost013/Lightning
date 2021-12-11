using NuCore.Utilities;
using NuRender;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Rectangle
    /// 
    /// April 12, 2021 (modified December 11, 2021: Initial NR port)
    /// 
    /// Defines a rectangle.
    /// </summary>
    public class Rectangle : Line
    {
        internal override string ClassName => "Rectangle";
        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            Brush Brush = GetBrush();

            if (Brush != null)
            {
                
                Brush.Render(SDL_Renderer, Tx); 
            }
            else
            {
                Window MainWindow = SDL_Renderer.GetMainWindow();

                IntPtr SDL_RendererPtr = MainWindow.Settings.RenderingInformation.RendererPtr;

                if (BorderColour == null) BorderColour = new Color4(0, 0, 0, 0);

                SDL.SDL_Rect SR1 = new SDL.SDL_Rect();

                if (!ForceToScreen)
                {
                    SR1.x = (int)Position.X - (int)MainWindow.Settings.RenderingInformation.CCameraPosition.X;
                    SR1.y = (int)Position.Y - (int)MainWindow.Settings.RenderingInformation.CCameraPosition.Y;
                }
                else
                {
                    SR1.x = (int)Position.X;
                    SR1.y = (int)Position.Y;
                }

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

        /// <summary>
        /// Renders the border of this Rectangle.
        /// </summary>
        /// <param name="SDL_Renderer"><inheritdoc/></param>
        /// <param name="Tx"><inheritdoc/></param>
        private void RenderBorder(Scene SDL_Renderer, ImageBrush Tx)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            IntPtr SDL_RendererPtr = MainWindow.Settings.RenderingInformation.RendererPtr;

            Vector2 BorderSize = new Vector2(Size.X + (BorderThickness * 2), Size.Y + (BorderThickness * 2));
            SDL.SDL_Rect SR2 = new SDL.SDL_Rect();

            SR2.x = (int)(Position.X - BorderThickness); // todo: position => int?
            SR2.y = (int)(Position.Y - BorderThickness); // todo: position => int?
            SR2.w = (int)BorderSize.X;
            SR2.h = (int)BorderSize.Y;

            if (!ForceToScreen)
            {
                SR2.x -= (int)MainWindow.Settings.RenderingInformation.CCameraPosition.X;
                SR2.y -= (int)MainWindow.Settings.RenderingInformation.CCameraPosition.Y;
            }


            BorderFill = true; 

            SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, BorderColour.R, BorderColour.G, BorderColour.B, BorderColour.A);

            int Result = 0;

            if (!BorderFill)
            {
                Result = SDL.SDL_RenderDrawRect(SDL_RendererPtr, ref SR2);
            }
            else
            {
                Result = SDL.SDL_RenderFillRect(SDL_RendererPtr, ref SR2);
            }

            SDL.SDL_SetRenderDrawColor(SDL_RendererPtr, 0, 0, 0, 0);


#if DEBUG
            if (Result < 0) Logging.Log(Result.ToString());
#endif
        }
    }
}
