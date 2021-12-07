using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Texture
    /// 
    /// December 3, 2021 (modified December 5, 2021)
    /// 
    /// Defines a texture.
    /// </summary>
    public class Image : NRObject
    {
        public override string ClassName => "Texture";

        /// <summary>
        /// The path to this file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// A <see cref="Vector2"/> holding the size of this Image.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// A <see cref="Vector2"/> holding the viewport of this image. 
        /// </summary>
        public Vector2 Viewport { get; set; }

        /// <summary>
        /// A <see cref="Vector2"/> holding the viewport anchor of this image.
        /// 
        /// A viewport anchor is where the viewport for this image starts. Default is (0,0). 
        /// </summary>
        public Vector2 ViewportAnchor { get; set; }

        /// <summary>
        /// Information about this texture - see <see cref="TextureInfo"/>.
        /// </summary>
        public TextureInformation TextureInfo { get; set; }

        /// <summary>
        /// The rendering mode of this texture - see <see cref="TextureRenderingMode"/>.,
        /// </summary>
        public TextureRenderingMode RenderMode { get; set; }

        public bool Load(WindowRenderingInformation WRI)
        {
            IntPtr Surface = SDL_image.IMG_Load(Path);

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

            return true; 

        }

        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            if (Path != null) Load(RenderingInformation); 
            return; 
        }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
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

        private void Render_Normal(WindowRenderingInformation RenderInfo, Vector2 OverridePosition = null)
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

            Vector2 TilePosition = Position;

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
