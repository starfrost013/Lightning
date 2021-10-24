using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// RandomStringFlags
    /// 
    /// October 19, 2021
    /// 
    /// Defines flags for the random string generation API.
    /// </summary>
    public enum RandomStringFlags
    {
        /// <summary>
        /// Lowercase alphabetical characters.
        /// </summary>
        AlphaLowercase = 1,

        /// <summary>
        /// Uppercase alphabetical characters
        /// </summary>
        AlphaUppercase = 2,

        /// <summary>
        /// Numerical characters.
        /// </summary>
        Numeric = 4,

        /// <summary>
        /// Special characters.
        /// </summary>
        Special = 8,

        /// <summary>
        /// All other characters.
        /// </summary>
        All = 16
    }
}
