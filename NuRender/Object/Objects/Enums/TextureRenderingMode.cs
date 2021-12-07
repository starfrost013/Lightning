using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// TextureRenderingMode
    /// 
    /// December 6, 2021
    /// 
    /// Defines texture rendering modes.
    /// </summary>
    public enum TextureRenderingMode
    {
        /// <summary>
        /// Normal texture rendering mode.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// The texture is not rendered.
        /// </summary>
        NotRendered = 1,

        /// <summary>
        /// The texture is tiled.
        /// </summary>
        Tile = 2,

        /// <summary>
        /// The texture is blended.
        /// </summary>
        Blended = 3
    }
}
