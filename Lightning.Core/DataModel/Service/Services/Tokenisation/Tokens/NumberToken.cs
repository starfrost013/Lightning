using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// NumberToken
    /// 
    /// April 22, 2021
    /// 
    /// Defines a number. "I can do my 123s..."
    /// </summary>
    public class NumberToken : Token
    {
        public int Value { get; set; }
    }
}
