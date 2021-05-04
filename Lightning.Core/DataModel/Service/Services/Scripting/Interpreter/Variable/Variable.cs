using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Variable
    /// 
    /// May 3, 2021
    /// 
    /// Defines a script variable.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of this variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of this variable.
        /// </summary>
        public object Value { get; set; }
    }
}
