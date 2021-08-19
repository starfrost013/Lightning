using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{ 
    /// <summary>
    /// GradientDirection
    /// 
    /// August 17, 2021
    /// 
    /// Defines a gradient direction
    /// </summary>
    public enum GradientDirection
    {
        /// <summary>
        /// Defines a left to right gradient.
        /// </summary>
        Left = 0,

        /// <summary>
        /// Defines a right to left gradient.
        /// </summary>
        Right = 1,

        /// <summary>
        /// Defines a top to bottom gradient.
        /// </summary>
        Up = 2,

        /// <summary>
        /// Defines a bottom to top gradient.
        /// </summary>
        Down = 3,

        /// <summary>
        /// Defines a top left to bottom right gradient.
        /// </summary>
        TopLeft = 4,

        /// <summary>
        /// Defines a top right to bottom left gradient.
        /// </summary>
        TopRight = 5,

        /// <summary>
        /// Defines a bottom left to top right gradient.
        /// </summary>
        BottomLeft = 6,

        /// <summary>
        /// Defines a bottom right to top left gradient.
        /// </summary>
        BottomRight = 7
    }
}
