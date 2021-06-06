﻿using Lightning.Core.SDL2;
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

        public override void Render(Renderer Renderer, Texture Tx)
        {
            if (!INITIALISED)
            {
                // OnCreate not used as it is ran before the object is loaded - change this?
                // do this later
                if (Size == null) Size = Renderer.WindowSize;
                if (Position == null) Position = new Vector2(0, 0);
                if (ForceToBack) ZIndex = -2147483647; // low z-index = drawn earlier

                GetInstanceResult GIR = GetFirstChildOfType("Texture");

                if (!GIR.Successful
                    || GIR.Instance == null)
                {
                    // fatal for now
                    ErrorManager.ThrowError(ClassName, "SkyObjectMustHaveTextureException");
                    return; 
                }
                else
                {
                    // Texture verified
                    INITIALISED = true;
                    return;
                }
            }
            else
            {
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
                SDL.SDL_RenderCopy(Renderer.SDLRenderer, Tx.SDLTexturePtr, ref SourceRect, ref DestinationRect);
            }
            
        }
    }
}