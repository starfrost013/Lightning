using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// TextureDisplayMode
    /// 
    /// April 10, 2021
    /// 
    /// Defines display modes for Textures.
    /// </summary>
    public enum TextureDisplayMode
    {
        /// <summary>
        /// The texture will tile to its defined size from the size of its image.
        /// </summary>
        Tile = 0,

        /// <summary>
        /// The texture will stretch to image.
        /// </summary>
        Stretch = 1,

        /// <summary>
        /// The texture will be displayed as is. 
        /// </summary>
        DisplayAsIs = 2
    }
}
