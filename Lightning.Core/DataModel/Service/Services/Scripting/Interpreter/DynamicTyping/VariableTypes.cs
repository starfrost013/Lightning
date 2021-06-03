using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// VariableTypes
    /// 
    /// June 3, 2021
    /// 
    /// Defines the valid types of a variable. Used for LightningScript's dynamic typing.
    /// </summary>
    public enum VariableTypes
    {
        /// <summary>
        /// Integer
        /// </summary>
        Int = 0,

        /// <summary>
        /// Double-precision floating-point number
        /// </summary>
        Double = 1,
        
        /// <summary>
        /// String
        /// </summary>
        String = 2,

        /// <summary>
        /// Boolean (
        /// </summary>
        Boolean = 3
    }
}
