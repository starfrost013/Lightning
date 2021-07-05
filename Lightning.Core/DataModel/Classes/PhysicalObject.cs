using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
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
        internal override string ClassName => "PhysicalObject";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override InstanceTags Attributes => base.Attributes;

        /// <summary>
        /// Can this object collide?
        /// </summary>
        public bool CanCollide { get; set; }

        /// <summary>
        /// The position of this object in the world. 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The size of this object.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// The Z-index of this object.
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// The colour of this object. Not actually used by this class *specifically* but IS used by classes that inherit from this.
        /// </summary>
        public Color4 Colour { get; set; }

        // Texture is a child. 

        /// <summary>
        /// Ran on the spawning of an object, before it is rendered for the first time and after the initialisation of the renderer.
        /// </summary>
        public virtual void OnSpawn()
        {
            return; 
        }


        /// <summary>
        /// This is called on each frame by the RenderService to tell this object to 
        /// render itself.
        /// 
        /// It has already been loaded, so the object is not required to load textures or anything similar.
        /// </summary>
        public virtual void Render(Renderer SDL_Renderer, Texture Tx)
        {
            IntPtr SDL_RendererPtr = SDL_Renderer.SDLRenderer;
            // requisite error checking already done

            // create the source rect
            SDL.SDL_Rect SourceRect = new SDL.SDL_Rect();

            // x,y = point on texture, w,h = size to copy
            SourceRect.x = 0;
            SourceRect.y = 0;

            SourceRect.w = (int)Size.X;
            SourceRect.h = (int)Size.Y;

            SDL.SDL_Rect DestinationRect = new SDL.SDL_Rect();

            DestinationRect.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
            DestinationRect.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;

            DestinationRect.w = (int)Size.X;
            DestinationRect.h = (int)Size.Y;

            SDL.SDL_RenderCopy(SDL_RendererPtr, Tx.SDLTexturePtr, ref SourceRect, ref DestinationRect);
        }
    }
}
