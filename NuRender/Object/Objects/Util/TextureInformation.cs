using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// TextureInformation
    /// 
    /// December 5, 2021 (modified December 11, 2021)
    /// 
    /// Defines texture information.
    /// </summary>
    public class TextureInformation
    {

        /// <summary>
        /// Determines the blending mode used for this texture.
        /// </summary>
        public SDL.SDL_BlendMode BlendingMode { get; set; }

        /// <summary>
        /// Determines if this texture is loaded.
        /// </summary>
        public bool Loaded { get; set; }

        /// <summary>
        /// The path to this file.
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// Raw pointer to the texture data of this Image in memory.
        /// </summary>
        public IntPtr TexPtr { get; set; }

    }
}
