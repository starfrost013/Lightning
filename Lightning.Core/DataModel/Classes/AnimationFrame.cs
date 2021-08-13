using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AnimationFrame
    /// 
    /// August 10, 2021
    /// 
    /// Defines an animation frame
    /// </summary>
    public class AnimationFrame : PhysicalObject // may change
    {
        internal override string ClassName => "AnimationFrame";

        /// <summary>
        /// Path to the animation.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Default time this animation will be played. CAN BE CHANGED BY SCRIPTING!
        /// </summary>
        public int DefaultTiming { get; set; }

        private bool NotCameraAware { get; set; }
        public void Load()
        {

        }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            IntPtr SDL_RendererPtr = SDL_Renderer.RendererPtr;
            // requisite error checking already done

            // create the source rect
            SDL.SDL_Rect SourceRect = new SDL.SDL_Rect();

            // x,y = point on texture, w,h = size to copy
            SourceRect.x = 0;
            SourceRect.y = 0;

            SourceRect.w = (int)Size.X;
            SourceRect.h = (int)Size.Y;

            SDL.SDL_Rect DestinationRect = new SDL.SDL_Rect();


            if (SDL_Renderer.CCameraPosition != null && !NotCameraAware)
            {
                DestinationRect.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
                DestinationRect.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;
            }
            else
            {
                DestinationRect.x = (int)Position.X;
                DestinationRect.y = (int)Position.Y;
            }

            if (DisplayViewport == null)
            {
                DestinationRect.w = (int)Size.X;
                DestinationRect.h = (int)Size.Y;
            }
            else
            {
                DestinationRect.w = (int)DisplayViewport.X;
                DestinationRect.h = (int)DisplayViewport.Y;
            }

            SDL.SDL_RenderCopy(SDL_RendererPtr, Tx.SDLTexturePtr, ref SourceRect, ref DestinationRect);
        }
    }
    }
}
