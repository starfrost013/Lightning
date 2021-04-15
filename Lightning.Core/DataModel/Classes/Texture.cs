using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Texture
    /// 
    /// April 9, 2021 (modified April 11, 2021)
    /// 
    /// Defines an image that can be displayed on the screen. Non-animated 
    /// </summary>
    public class Texture : PhysicalObject
    {
        /// <summary>
        /// <inheritdoc/> -- set to Texture.
        /// </summary>
        internal override string ClassName => "Texture";

        /// <summary>
        /// The path to the image of this non-animated texture.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The display mode of this texture - see <see cref="TextureDisplayMode"/>.
        /// </summary>
        public TextureDisplayMode TextureDisplayMode { get; set; }
        
        /// <summary>
        /// INTERNAL: A pointer to the SDL2 hardware-accelerated texture used by this object./>
        /// </summary>
        internal IntPtr SDLTexturePtr { get; set; }
    }
}
