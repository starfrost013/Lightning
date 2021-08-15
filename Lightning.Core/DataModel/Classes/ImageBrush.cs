using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ImageBrush (Texture 2.0)
    /// 
    /// August 7, 2021 (original: April 9, 2021, modified August 15, 2021)
    /// 
    /// Defines a brush used for non-animated images that can be displayed on the screen.
    /// </summary>
    public class ImageBrush : Brush
    {
        /// <summary>
        /// <inheritdoc/> -- set to ImageBrush.
        /// </summary>
        internal override string ClassName => "ImageBrush";

        internal override InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE | InstanceTags.Destroyable | InstanceTags.ParentCanBeNull); }


        /// <summary>
        /// The path to the image of this non-animated texture.
        /// </summary>
        public string Path { get; set; }
 

        /// <summary>
        /// The display mode of this texture - see <see cref="TextureDisplayMode"/>.
        /// </summary>
        public TextureDisplayMode TextureDisplayMode { get; set; }
        
        /// <summary>
        /// INTERNAL: A pointer to the SDL2 hardware-accelerated texture used by this object.
        /// </summary>
        internal IntPtr SDLTexturePtr { get; set; }

        /// <summary>
        /// PRIVATE: Determines if this texture is initialised.
        /// </summary>
        internal bool TEXTURE_INITIALISED { get; set; }

        public override void OnCreate()
        {
            Type ParentType = Parent.GetType();

            if (ParentType != typeof(PhysicalObject)
            && !ParentType.IsSubclassOf(typeof(PhysicalObject))) 
            {
                ErrorManager.ThrowError(ClassName, "BrushMustHavePhysicalObjectParentException");
                Parent.RemoveChild(this);
            }
        }


        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            base.PO_Init();

            if (!TEXTURE_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx); 
            }

        }

        internal void Init()
        {
            ServiceNotification SN = new ServiceNotification();
            SN.ServiceClassName = "RenderService";
            SN.NotificationType = ServiceNotificationType.MessageSend;
            SN.Data.Name = "LoadTexture";
            SN.Data.Data.Add((PhysicalObject)Parent); // todo: messagedatacollection
            SN.Data.Data.Add(this);

            ServiceNotifier.NotifySCM(SN);

            TEXTURE_INITIALISED = true;
            return; 
        }


        private void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            SnapToParent();

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

        internal void SnapToParent()
        {
            // Very temporary code.
            // In the future you will only set these on the brushes,
            // this is temp until ~Aug 20 2021
            PhysicalObject PParent = (PhysicalObject)Parent;
            if (PParent.Position != null) Position = PParent.Position;
            if (PParent.Size != null) Size = PParent.Size;
            if (PParent.BorderColour != null) BorderColour = PParent.BorderColour;
            if (PParent.BackgroundColour != null) BackgroundColour = PParent.BackgroundColour;
            if (PParent.DisplayViewport != null) DisplayViewport = PParent.DisplayViewport;
            if (PParent.Colour != null) Colour = PParent.Colour;

        }
    }
}
