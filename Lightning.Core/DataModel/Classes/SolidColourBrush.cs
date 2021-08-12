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


        private bool SOLIDCOLOURBRUSH_INITIALISED { get; set; }
        
        internal void Init()
        {
            if (Position == null) Position = new Vector2(0, 0);
            if (Size == null)
            {
                ErrorManager.ThrowError(ClassName, "BrushMustHaveDefinedSizeException");
                Parent.RemoveChild(this);
                return; // will get gc'd
            }

            if (BackgroundColour == null) BackgroundColour = new Color4(255, 255, 255, 255);

            SOLIDCOLOURBRUSH_INITIALISED = true; 
        }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!SOLIDCOLOURBRUSH_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
           
        }

        private void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            SDL.SDL_Rect DstRect = new SDL.SDL_Rect();
            
            if (SDL_Renderer.CCameraPosition == null)
            {
                DstRect.x = (int)Position.X;
                DstRect.y = (int)Position.Y;
            }
            else
            {
                DstRect.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
                DstRect.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;
            }



            if (DisplayViewport != null)
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

            if (BorderThickness > 0) RenderBorder(SDL_Renderer, Tx);

            if (!Fill) // todo: shape system
            {
                SDL.SDL_RenderDrawRect(SDL_Renderer.RendererPtr, ref DstRect);
            }
            else
            {
                SDL.SDL_RenderFillRect(SDL_Renderer.RendererPtr, ref DstRect);
            }


            SDL.SDL_SetRenderDrawColor(SDL_Renderer.RendererPtr, 0, 0, 0, 0);
        }

        private void RenderBorder(Renderer SDL_Renderer, ImageBrush Tx)
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
