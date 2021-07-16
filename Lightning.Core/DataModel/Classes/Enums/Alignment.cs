using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TextAlignment
    /// 
    /// July 16, 2021
    /// 
    /// Defines an enumeration with the valid types of text alignment.
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// No text alignment - Position value treated as position.
        /// </summary>
        None = 0,

        /// <summary>
        /// Left text alignment
        /// </summary>
        Left = 1,

        /// <summary>
        /// Centre text alignment
        /// </summary>
        Centre = 2,

        /// <summary>
        /// Right text alignment
        /// </summary>
        Right = 3,

        /// <summary>
        /// Justify text
        /// </summary>
        Justify = 4
    }
}
