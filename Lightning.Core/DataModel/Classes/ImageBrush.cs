using NuCore.Utilities;
using NuRender;
using NuRender.SDL2;
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

        private Image NRImage { get; set; }

        public override void OnCreate()
        {
            Type ParentType = Parent.GetType();

            if (ParentType != typeof(PhysicalInstance)
            && !ParentType.IsSubclassOf(typeof(PhysicalInstance))) 
            {
                ErrorManager.ThrowError(ClassName, "BrushMustHavePhysicalInstanceParentException");
                Parent.RemoveChild(this);
            }
        }


        public override void Render(Scene SDL_Renderer, ImageBrush Tx, IntPtr RenderTarget)
        {
            base.PO_Init();

            if (!TEXTURE_INITIALISED)
            {
                Init(SDL_Renderer);
            }
            else
            {
                DoRender(SDL_Renderer, Tx); 
            }

        }

        internal void Init(Scene SDL_Renderer)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            ServiceNotification SN = new ServiceNotification();
            SN.ServiceClassName = "RenderService";
            SN.NotificationType = ServiceNotificationType.MessageSend;
            SN.Data.Name = "LoadTexture";
            SN.Data.Data.Add((PhysicalInstance)Parent); // todo: messagedatacollection
            SN.Data.Data.Add(this);

            ServiceNotifier.NotifySCM(SN);

            TEXTURE_INITIALISED = true;
            return; 
        }


        private void DoRender(Scene SDL_Renderer, ImageBrush Tx) //todo: remove second parameter
        {
            SnapToParent();

            switch (TextureDisplayMode)
            {
                case TextureDisplayMode.DisplayAsIs:
                case TextureDisplayMode.Stretch:
                    RenderUntiledTexture(SDL_Renderer, Tx);
                    return;
                case TextureDisplayMode.Tile:
                    RenderTiledTexture(SDL_Renderer, Tx);
                    return;
                 
            }
        }

        private void RenderUntiledTexture(Scene SDL_Renderer, ImageBrush Tx)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            IntPtr SDL_RendererPtr = MainWindow.Settings.RenderingInformation.RendererPtr;

            // requisite error checking already done

            // create the source rect
            SDL.SDL_Rect SourceRect = new SDL.SDL_Rect
            // x,y = point on texture, w,h = size to copy
            {
                x = 0,
                y = 0,
                w = (int)Size.X,
                h = (int)Size.Y
            };

            SDL.SDL_Rect DestinationRect = new SDL.SDL_Rect();


            if (MainWindow.Settings.RenderingInformation.CCameraPosition != null && !ForceToScreen)
            {
                DestinationRect.x = (int)Position.X - (int)MainWindow.Settings.RenderingInformation.CCameraPosition.X;
                DestinationRect.y = (int)Position.Y - (int)MainWindow.Settings.RenderingInformation.CCameraPosition.Y;
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

        private void RenderTiledTexture(Scene SDL_Renderer, ImageBrush Tx)
        {
            Window MainWindow = SDL_Renderer.GetMainWindow();

            Vector2 TileCount = DisplayViewport / Size;

            if (TileCount.X < 1 || TileCount.Y < 1)
            {
                ErrorManager.ThrowError(ClassName, "DisplayViewportMustBeLargerThanSizeForTilingException");
                Parent.RemoveChild(this); // DO NOT RENDER
                return;
            }
            else
            {
                // point in original texture
                SDL.SDL_Rect SrcRect = new SDL.SDL_Rect
                {
                    x = 0,
                    y = 0,
                    w = (int)Size.X,
                    h = (int)Size.Y
                };

                SDL.SDL_Rect DstRect = new SDL.SDL_Rect
                {
                    x = (int)Position.X,
                    y = (int)Position.Y,
                    w = (int)Size.X / (int)TileCount.X,
                    h = (int)Size.Y / (int)TileCount.Y
                };


                // DRAW IT
                for (int i = 0; i < TileCount.X; i++)
                {
                    DstRect.x = (int)Position.X + (int)(Size.X / TileCount.X) * (i + 1);

                    if (!ForceToScreen) DstRect.x -= (int)MainWindow.Settings.RenderingInformation.CCameraPosition.X;

                    // obsolete code
                    SDL.SDL_RenderCopy(MainWindow.Settings.RenderingInformation.RendererPtr, Tx.SDLTexturePtr, ref SrcRect, ref DstRect);

                    for (int j = 0; j < TileCount.Y; j++)
                    {
                        DstRect.y = (int)Position.Y + (int)(Size.Y / TileCount.Y) * j + 1;

                        if (!ForceToScreen) DstRect.y -= (int)MainWindow.Settings.RenderingInformation.CCameraPosition.Y;

                        SDL.SDL_RenderCopy(MainWindow.Settings.RenderingInformation.RendererPtr, Tx.SDLTexturePtr, ref SrcRect, ref DstRect);
                    }
                }
            }
        }

        internal void SnapToParent()
        {
            // Very temporary code.
            // In the future you will only set these on the brushes,
            // this is temp until ~Aug 20 2021
            PhysicalInstance PParent = (PhysicalInstance)Parent;
            if (PParent.Position != null) Position = PParent.Position;
            if (PParent.Size != null) Size = PParent.Size;
            if (PParent.BorderColour != null) BorderColour = PParent.BorderColour;
            if (PParent.BackgroundColour != null) BackgroundColour = PParent.BackgroundColour;
            if (PParent.DisplayViewport != null) DisplayViewport = PParent.DisplayViewport;
            if (PParent.Colour != null) Colour = PParent.Colour;
        }
    }
}
