using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// FunctionToken
    /// 
    /// April 16, 2021
    /// 
    /// Defines a function 
    /// </summary>
    public class FunctionToken : Token
    {
        /// <summary>
        /// FunctionName
        /// 
        /// Defines the name of the function to call
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// FunctionParameters
        /// 
        /// A list of strings defining the parameters for the function that is going to be called.
        /// </summary>
        public List<string> FunctionParameters { get; set; }
    }
}
