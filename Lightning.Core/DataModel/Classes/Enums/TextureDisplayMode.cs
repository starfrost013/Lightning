using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TextureDisplayMode
    /// 
    /// April 10, 2021 (modified August 15, 2021)
    /// 
    /// Defines display modes for ImageBrushes.
    /// </summary>
    public enum TextureDisplayMode
    {
        /// <summary>
        /// The texture will tile to its defined size from the size of its image.
        /// </summary>
        DisplayAsIs = 0,

        /// <summary>
        /// The texture will stretch to its defined size.
        /// </summary>
        Stretch = 1,

        /// <summary>
        /// The texture will be displayed as is. 
        /// </summary>
        Tile = 2,

        /// <summary>
        /// The texture will be blended.
        /// </summary>
        Blended = 3
    }
}
