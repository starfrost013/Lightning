using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// WindowMode
    /// 
    /// September 25, 2021
    /// 
    /// Defines the current window mode.
    /// </summary>
    public enum WindowMode
    {
        /// <summary>
        /// Defines a windowed window.
        /// </summary>
        Windowed = 0,

        /// <summary>
        /// Defines a fullscreen window.
        /// </summary>
        Fullscreen = 1,

        /// <summary>
        /// Defines a windowed borderless window (sometimes called "fake fullscreen"). Less of the jank you get with "true fullscreen".
        /// </summary>
        WindowedBorderless = 2
    }
}
