using NuCore.Utilities;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Texture
    /// 
    /// December 3, 2021 (modified December 11, 2021: Implement file not found check and add constructor to initialise TextureInfo at instantiation)
    /// 
    /// Defines a texture.
    /// </summary>
    public class Image : NRObject
    {
        public override string ClassName => "Texture";

        /// <summary>
        /// A <see cref="Vector2Internal"/> holding the size of this Image.
        /// </summary>
        public Vector2Internal Size { get; set; }

        /// <summary>
        /// A <see cref="Vector2Internal"/> holding the viewport of this image. 
        /// </summary>
        public Vector2Internal Viewport { get; set; }

        /// <summary>
        /// A <see cref="Vector2Internal"/> holding the viewport anchor of this image.
        /// 
        /// A viewport anchor is where the viewport for this image starts. Default is (0,0). 
        /// </summary>
        public Vector2Internal ViewportAnchor { get; set; }

        /// <summary>
        /// Information about this texture - see <see cref="TextureInfo"/>.
        /// </summary>
        public TextureInformation TextureInfo { get; set; }

        /// <summary>
        /// The rendering mode of this texture - see <see cref="TextureRenderingMode"/>.,
        /// </summary>
        public TextureRenderingMode RenderMode { get; set; }

        public Image()
        {
            TextureInfo = new TextureInformation(); 
        }

        public bool Load(WindowRenderingInformation WRI)
        {
            if (!File.Exists(TextureInfo.Path))
            {
                ErrorManager.ThrowError(ClassName, "NRFailedToLoadTextureException", $"Error loading texture: The file {TextureInfo.Path} does not exist!");
                return false;
            }

            // Ccahe this texture       
            foreach (Image Img in WRI.ImageCache)
            {
                if (Img.TextureInfo.Path == TextureInfo.Path)
                {
                    // cache texture and return
                    TextureInfo.TexPtr = Img.TextureInfo.TexPtr;
                    return true;
                }
            }

            IntPtr Surface = SDL_image.IMG_Load(TextureInfo.Path);

            if (Surface == IntPtr.Zero) 
            {
                ErrorManager.ThrowError(ClassName, "NRFailedToLoadTextureException", $"Error loading texture (NuRender.SDL2.SDL_image.IMG_Load(Path) or C++ failed: {SDL.SDL_GetError()}).");
                return false;
            }

            TextureInfo.TexPtr = SDL.SDL_CreateTextureFromSurface(WRI.RendererPtr, Surface);

            if (TextureInfo.TexPtr == IntPtr.Zero)
            {
                ErrorManager.ThrowError(ClassName, "NRFailedToLoadTextureException", $"Error loading texture (error converting SDL_Surface to SDL_Texture): {SDL.SDL_GetError()}");
                return false;
            }

            // prevent memory leaks

            SDL.SDL_FreeSurface(Surface);

            return true; 

        }

        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            if (TextureInfo.Path != null) Load(RenderingInformation); 
            return; 
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            SDL.SDL_SetTextureBlendMode(RenderInfo.RendererPtr, RenderInfo.BlendingMode);
            
            switch (RenderMode)
            {
                case TextureRenderingMode.NotRendered:
                    return;
                case TextureRenderingMode.Normal:
                    Render_Normal(RenderInfo);
                    return;
                case TextureRenderingMode.Tile:
                    Render_Tiled(RenderInfo);
                    return;
            
            }
        }

        private void Render_Normal(WindowRenderingInformation RenderInfo, Vector2Internal OverridePosition = null)
        {


            SDL.SDL_Rect SrcRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = (int)Size.X,
                h = (int)Size.Y
            };

            // Set up the viewport anchor.
            if (ViewportAnchor != null)
            {
                SrcRect.x = (int)ViewportAnchor.X;
                SrcRect.y = (int)ViewportAnchor.Y;
            }

            // Set up the viewport.
            if (Viewport != null)
            {
                SrcRect.w = (int)Viewport.X;
                SrcRect.h = (int)Viewport.Y;
            }

            // Set up destination rect

            SDL.SDL_FRect DstRect = new SDL.SDL_FRect
            {
                x = (float)Position.X,
                y = (float)Position.Y,
                w = (float)Size.X,
                h = (float)Size.Y
            };

            if (OverridePosition != null) // override position  used for tiling 
            {
                DstRect.x = (int)OverridePosition.X;
                DstRect.y = (int)OverridePosition.Y;
            }

            SDL.SDL_RenderCopyF(RenderInfo.RendererPtr, TextureInfo.TexPtr, ref SrcRect, ref DstRect);
        }

        private void Render_Tiled(WindowRenderingInformation RenderInfo)
        {
            
            int TileCountX = (int)Viewport.X / (int)Size.X;
            int TileCountY = (int)Viewport.Y / (int)Size.Y;

            if (TileCountX < 1 || TileCountY < 1) return; 

            Vector2Internal TilePosition = Position;

            int IncrementX = (int)Viewport.X / TileCountX;
            int IncrementY = (int)Viewport.Y / TileCountY; 


            for (int y = 0; y < TileCountY; y++)
            {
                for (int x = 0; x < TileCountX; x++)
                {
                    Render_Normal(RenderInfo, TilePosition);

                    TilePosition += IncrementX; 
                }

                TilePosition += IncrementY;
            }
        }
    }
}
