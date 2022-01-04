using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Solidity
    /// 
    /// January 3, 2022
    /// 
    /// Defines a flag for solid objects in the engine. 
    /// </summary>
    public enum Solidity
    {
        /// <summary>
        /// This object is only solid on top.
        /// </summary>
        Top = 0,

        /// <summary>
        /// This object is solid on the sides.
        /// </summary>
        Sides = 1,

        /// <summary>
        /// This object is solid at the bottom.
        /// </summary>
        Bottom = 2,

        /// <summary>
        /// Default - this object is solid on all sides.
        /// </summary>
        Default = (Top | Sides | Bottom)
    }
}
