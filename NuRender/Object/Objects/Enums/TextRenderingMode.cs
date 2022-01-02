using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// TextRenderingMode
    /// 
    /// December 6, 2021
    /// 
    /// Defines the mode of text rendering. 
    /// </summary>
    public enum TextRenderingMode
    {
        /// <summary>
        /// Normal, antialiased text.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Non-antialiased, "rough" text.
        /// </summary>
        NoAntialias = 1, 

        /// <summary>
        /// Shaded text. WILL BE RENDERED AS ANTIALIASED IF THE BGCOLOUR IS NOT SET!
        /// </summary>
        Shaded = 2
    }
}
