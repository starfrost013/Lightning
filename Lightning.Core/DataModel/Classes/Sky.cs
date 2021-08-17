﻿using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Sky (needs to be rewritten)
    /// 
    /// May 26, 2021 (modified August 17, 2021)
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

        private bool SKY_INITIALISED { get; set; }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!SKY_INITIALISED)
            {
                Init(SDL_Renderer);
                
            }
            else
            {
                Brush CurBrush = GetBrush();

                CurBrush.Render(SDL_Renderer, Tx);

              
            }
            
        }

        private void Init(Renderer SDL_Renderer)
        {
            // OnCreate not used as it is ran before the object is loaded - change this?
            // do this later
            if (Size == null) Size = SDL_Renderer.WindowSize;
            if (Position == null) Position = new Vector2(0, 0);
            if (ForceToBack) ZIndex = -2147483647; // low z-index = drawn earlier

            GetInstanceResult GIR = GetFirstChildOfTypeT(typeof(ImageBrush), true);

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
                SKY_INITIALISED = true;
                return;
            }

        }
    }
}
