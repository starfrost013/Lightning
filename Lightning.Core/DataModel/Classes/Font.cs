using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Font
    /// 
    /// June 30, 2021
    ///  
    /// Defines a font
    /// </summary>
    public class Font
    {
        public string FontFamily { get; set; }

        /// <summary>
        /// Unmanaged ptr to SDL2_ttf font structure
        /// </summary>
        internal IntPtr FontPointer { get; set; }
    }
}
