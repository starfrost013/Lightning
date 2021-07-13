using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Renderer (Non-DataModel)
    /// 
    /// April 9, 2021 (modified July 13, 2021: Add current blending mode)
    /// 
    /// Holds information about the renderer and SDL2. 
    /// </summary>
    public class Renderer
    {

        private RenderingBlendMode _blendmode { get; set; }

        /// <summary>
        /// The current rendering blend mode. 
        /// </summary>
        public RenderingBlendMode BlendMode
        {
            get
            {
                return _blendmode;
            }
            set
            {
                _blendmode = value;
                SetCurBlendMode();
            }
        }

        /// <summary>
        /// The current Camera position. 
        /// </summary>
        public Vector2 CCameraPosition { get; set; }

        /// <summary>
        /// The SDL window.
        /// </summary>
        public IntPtr Window { get; set; }

        /// <summary>
        /// The SDL renderer.
        /// </summary>
        public IntPtr RendererPtr { get; set; }

        /// <summary>
        /// A cache of textures.
        /// </summary>
        public List<Texture> TextureCache { get; set; }

        /// <summary>
        /// The window size.
        /// </summary>
        public Vector2 WindowSize { get; set; }

        public Renderer()
        {
            TextureCache = new List<Texture>();
            // not added to the datamodel
            CCameraPosition = new Vector2();
        }
        
        /// <summary>
        /// Sets the blend mode to the current blend mode.
        /// 
        /// Originally we had this in the set accessor, but it needs to be set each frame therefore we have to set it at least once each frame.
        /// </summary>
        public void SetCurBlendMode()
        {

            switch (BlendMode)
            {

                case RenderingBlendMode.None:
                    SDL.SDL_SetRenderDrawBlendMode(RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_NONE);
                    return;
                case RenderingBlendMode.AdditiveBlending:
                    SDL.SDL_SetRenderDrawBlendMode(RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_ADD);
                    return;
                case RenderingBlendMode.AlphaBlending:
                    SDL.SDL_SetRenderDrawBlendMode(RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
                    return;
                case RenderingBlendMode.ColourModulation:
                    SDL.SDL_SetRenderDrawBlendMode(RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_MOD);
                    return;
                case RenderingBlendMode.ColourMultiplication:
                    SDL.SDL_SetRenderDrawBlendMode(RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_MUL); // not documented in the sdl wiki??????
                    return;
            }

        }
    }
}
