using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// OperatorTokenType
    /// 
    /// April 16, 2021
    /// 
    /// Defines the type of an OperatorToken. The type in this context is the specific operator it is.
    /// </summary>
    public enum OperatorTokenType
    {
        /// <summary>
        /// = (assignment)
        /// </summary>
        Assignment = 0,

        /// <summary>
        /// == (equality)
        /// </summary>
        Equality = 1,

        /// <summary>
        /// != (inequality)
        /// </summary>
        Inequality = 2,

        /// <summary>
        /// + (add)
        /// </summary>
        Plus = 3,

        /// <summary>
        /// - (Minus)
        /// </summary>
        Minus = 4,

        /// <summary>
        /// * (Multiply)
        /// </summary>
        Multiply = 5,

        /// <summary>
        /// / (divide)
        /// </summary>
        Divide = 6,

        /// <summary>
        /// % (Modulus)
        /// </summary>
        Modulus = 7,


    }
}
