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
        /// Gets or sets the colour of the border of this GuiElement. 
        /// </summary>
        public Color4 BorderColour { get; set; }

        /// <summary>
        /// Gets or sets the border width of this GuiElement - if it is set to zero, border drawing will be skipped as there is nothing to draw.
        /// </summary>
        public int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the border fill state. If true, the border will be filled. If false, it will not.
        /// </summary>
        public bool BorderFill { get; set; }
        /// <summary>
        /// Ran on the spawning of an object, before it is rendered for the first time and after the initialisation of the renderer.
        /// </summary>
        public virtual void OnSpawn()
        {
            return; 
        }

        /// <summary>
        /// Key down event handler.
        /// 
        /// Default event handler may be implemented by any Lightning class.
        /// 
        /// Scripts may modify the event handler function. 
        /// </summary>
        public KeyDownEvent OnKeyDownHandler { get; set; }

        /// <summary>
        /// Click event handler.
        /// 
        /// Default event handler may be implemented by any Lightning class.
        /// 
        /// Scripts may modify the event handler function. 
        /// </summary>

        public MouseDownEvent Click { get; set; }

        /// <summary>
        /// Mouse enter event handler.
        /// 
        /// Default event handler may be implemented by any Lightning class.
        /// 
        /// Scripts may modify the event handler function. 
        /// </summary>
        public MouseEnterEvent OnMouseEnter { get; set; }

        /// <summary>
        /// Mouse leave event handler.
        /// 
        /// Default event handler may be implemented by any Lightning class.
        /// 
        /// Scripts may modify the event handler function. 
        /// </summary>
        public MouseLeaveEvent OnMouseLeave { get; set; }

        /// <summary>
        /// Mouse up event handler.
        /// 
        /// Default event handler may be implemented by any Lightning class.
        /// 
        /// Scripts may modify the event handler function. 
        /// </summary>
        public MouseUpEvent OnMouseUp { get; set; }

        /// <summary>
        /// Engine shutdown event handler.
        /// 
        /// Called on engine shutdown.
        /// </summary>
        public ShutdownEvent OnShutdown { get; set; }

        /// <summary>
        /// This is called on each frame by the RenderService to tell this object to 
        /// render itself.
        /// 
        /// It has already been loaded, so the object is not required to load textures or anything similar.
        /// </summary>
        public virtual void Render(Renderer SDL_Renderer, Texture Tx)
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

            DestinationRect.x = (int)Position.X - (int)SDL_Renderer.CCameraPosition.X;
            DestinationRect.y = (int)Position.Y - (int)SDL_Renderer.CCameraPosition.Y;

            DestinationRect.w = (int)Size.X;
            DestinationRect.h = (int)Size.Y;

            SDL.SDL_RenderCopy(SDL_RendererPtr, Tx.SDLTexturePtr, ref SourceRect, ref DestinationRect);
        }



        public virtual void OnClick(object Sender, MouseEventArgs EventArgs)
        {
            return; 
        }

        public virtual void OnKeyDown(Control Control)
        {
            // Remove old placeholder code (May 26, 2021)
            //MessageBox.Show($"You pressed {Control.KeyCode.ToString()}!");
        }

        /// <summary>
        /// Runs on a key stopping being pressed.
        /// </summary>
        /// <param name="Control"></param>
        public virtual void OnKeyUp(Control Control)
        {

        }
    }
}
