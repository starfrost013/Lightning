using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// TextStyle
    /// 
    /// December 16, 2021
    /// 
    /// Defines text style masks.
    /// </summary>
    public enum TextStyle
    {
        /// <summary>
        /// Text is bold if this flag is set.
        /// </summary>
        Bold = 1,

        /// <summary>
        /// Text is italic if this flag is set.
        /// </summary>
        Italic = 2,

        /// <summary>
        /// Text is underline if this flag is set.
        /// </summary>
        Underline = 4,

        /// <summary>
        /// Text is strikethrough if this flag is set.
        /// </summary>
        Strikethrough = 8
    }
}
