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

        public AABB AABB
        {
            get
            {
                if (Position == null
                || Size == null)
                {
                    return null;
                }
                else
                {
                    return new AABB(Position, Size);
                }

            }
        }


        /// <summary>
        /// Determines if this object is anchored - if so, gravity is not active on it. (<see cref="PhysicsEnabled"/> must be set to true.)
        /// </summary>
        public bool Anchored { get; set; }

        /// <summary>
        /// Backing field for <see cref="_mass"/>
        /// </summary>
        private double _mass { get; set; }

        /// <summary>
        /// The mass of this object. (kg)
        /// </summary>
        public double Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                if (value == 0)
                {
                    InverseMass = 0; // prevents objects spazzing off to infinity...
                }
                else 
                {
                    InverseMass = 1 / value;
                }
                _mass = value;
            }

        }

        /// <summary>
        /// Inverse mass of this object.
        /// </summary>
        internal double InverseMass { get; private set; }

        /// <summary>
        /// The speed of this object. (m/s)
        /// </summary>
        internal Vector2 Velocity { get; set; }

        /// <summary>
        /// The elasticity of this object.
        /// </summary>
        public double Elasticity { get; set; }

        /// <summary>
        /// The maximum force that this object can tolerate before breaking. A random modifier will be applied to simulate real world effects.
        /// </summary>
        public double MaximumForce { get; set; }

        /// <summary>
        /// Determines if physics is enabled.
        /// </summary>
        public bool PhysicsEnabled { get; set; }


        /// <summary>
        /// The PhysicsController of this PhysicsObject - see <see cref="PhysicsController"/>.
        /// </summary>
        public PhysicsController PhysicsController { get; set; }

        public override void OnCreate()
        {
            PhysicsController = new DefaultPhysicsController(); 
            if (Velocity == null) Velocity = new Vector2(0, 0);
        }

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

        /// <summary>
        /// Applies an instantenous impulse force to this object.
        /// </summary>
        /// <param name="Impulse">A <see cref="Vector2"/> containing the impulse force to apply to this PhysicalObject.</param>
        public void ApplyImpulse(Vector2 Impulse)
        {
            if (Impulse == null)
            {
                ErrorManager.ThrowError(ClassName, "AttemptedToApplyInvalidImpulseException");
            }
            else
            {
                Velocity += Impulse; 
            }
        }

    }
}
