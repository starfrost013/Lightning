﻿using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// PhysicalObject
    /// 
    /// April 9, 2021 (modified April 11, 2021)
    /// 
    /// Defines a physically rendered object in Lightning, with a Position, Size, and a Texture (stored as a logical child). Rendered every frame by RenderService.
    /// </summary>
    public class PhysicalObject : SerialisableObject
    {
        /// <summary>
        /// <inheritdoc/> -- set to PhysicalObject.
        /// </summary>
        public override string ClassName => "PhysicalObject";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override InstanceTags Attributes => base.Attributes;

        /// <summary>
        /// The position of this object in the world. 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The size of this object.
        /// </summary>
        public Vector2 Size { get; set; }


        // Texture is a child. 

        /// <summary>
        /// This is called on each frame by the RenderService to tell this object to 
        /// render itself.
        /// 
        /// It has already been loaded, so the object is not required to load textures or anything similar.
        /// </summary>
        public void Render(IntPtr SDL_Renderer, Texture Tx)
        {
            // requisite error checking already done

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

            SDL.SDL_RenderCopy(SDL_Renderer, Tx.SDLTexturePtr, ref SourceRect, ref DestinationRect);
        }
    }
}
