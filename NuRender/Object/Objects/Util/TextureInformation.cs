using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// TextureInformation
    /// 
    /// December 5, 2021
    /// 
    /// Defines texture information.
    /// </summary>
    public class TextureInformation
    {
        public IntPtr TexPtr { get; set; }

        /// <summary>
        /// Determines if this texture is loaded.
        /// </summary>
        public bool Loaded { get; set; }
    }
}
