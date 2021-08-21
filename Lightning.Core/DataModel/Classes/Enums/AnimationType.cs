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
        /// Animation that plays from a script with an optional repeat
        /// </summary>
        FromScript = 1,

        /// <summary>
        /// Animations played on a specific event being called to their parent object
        /// </summary>
        OnEvent = 2,

        /// <summary>
        /// Completely custom behaviour - defined in Lua
        /// </summary>
        Custom = 3
    }
}
