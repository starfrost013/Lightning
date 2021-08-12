using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AnimationType
    /// 
    /// August 10, 2021
    /// 
    /// Defines an animation type.
    /// </summary>
    public enum AnimationType
    {
        /// <summary>
        /// Continuous animation
        /// </summary>
        Continuous = 0,

        /// <summary>
        /// Animations played on a specific trigger method being called
        /// </summary>
        OnTrigger = 1,

        /// <summary>
        /// Completely custom behaviour - defined in Lua
        /// </summary>
        Custom = 2
    }
}
