using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Sky
    /// 
    /// May 26, 2021 (modified May 27, 2021)
    /// 
    /// Defines a background. The <see cref="PhysicalObject.Size"/> property is optional for this object. It will stretch to the size of the game window.
    /// </summary>
    public class Sky : PhysicalObject
    {
        internal override string ClassName => "Sky";

        /// <summary>
        /// Force the background to the back.
        /// </summary>
        public bool ForceToBack { get; set; }

        private bool INITIALISED { get; set; }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!INITIALISED)
            {
                // OnCreate not used as it is ran before the object is loaded - change this?
                // do this later
                if (Size == null) Size = SDL_Renderer.WindowSize;
                if (Position == null) Position = new Vector2(0, 0);
                if (ForceToBack) ZIndex = -2147483647; // low z-index = drawn earlier

                GetInstanceResult GIR = GetFirstChildOfType("ImageBrush");

                if (!GIR.Successful
                    || GIR.Instance == null)
                {
                    // fatal for now
                    ErrorManager.ThrowError(ClassName, "SkyObjectMustHaveTextureException");
                    return; 
                }
                else
                {
                    Brush GBrush = (ImageBrush)GIR.Instance;
                    GBrush.NotCameraAware = true; 

                    // I ACTUALLY HAVE NO IDEA WHAT THE FUCK IS GOING ON HERE SO WE ARE DOING THIS STUPID SHIT INSTEAD
                    GBrush.BRUSH_INITIALISED = false;
                    // END I ACTUALLY HAVE NO IDEA WHAT THE FUCK IS GOING ON HERE SO WE ARE DOING THIS STUPID SHIT INSTEAD
                    
                    GetBrush(); 
                    // Texture verified
                    INITIALISED = true;
                    return;
                }

                
            }
            else
            {
                Brush CurBrush = GetBrush();

                CurBrush.Render(SDL_Renderer, Tx);

                /*
                // Does not move with camera pos
                // create the source rect
                SDL.SDL_Rect SourceRect = new SDL.SDL_Rect();

                // x,y = point on texture, w,h = size to copy
                SourceRect.x = 0;
                SourceRect.y = 0;

                SourceRect.w = (int)Size.X;
                SourceRect.h = (int)Size.Y;

                SDL.SDL_Rect DestinationRect = new SDL.SDL_Rect();

                DestinationRect.x = (int)Position.X;
                DestinationRect.y = (int)Position.Y;

                DestinationRect.w = (int)Size.X;
                DestinationRect.h = (int)Size.Y;

                // Do not move with camera
                SDL.SDL_RenderCopy(Renderer.RendererPtr, Tx.SDLTexturePtr, ref SourceRect, ref DestinationRect);
                */ 
            }
            
        }
    }
}
