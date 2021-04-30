using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CallToken
    /// 
    /// April 30, 2021
    /// 
    /// Defines a function call. 
    /// </summary>
    public class CallToken : Token 
    {
        /// <summary>
        /// The name of the function that is being called.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// String values of all parameters in the function. Converted at runtime to their actual values. 
        /// </summary>
        public List<string> ParameterValues { get; set; }

        public CallToken()
        {
            ParameterValues = new List<string>();
        }
    }
}
