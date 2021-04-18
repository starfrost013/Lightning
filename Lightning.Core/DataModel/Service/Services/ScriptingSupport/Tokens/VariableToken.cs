using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// VariableToken
    /// 
    /// April 16, 2021 (modified April 17, 2021)
    /// 
    /// Defines a variable token. Paired with an OperatorToken and a ValueToken.
    /// </summary>
    public class VariableToken : Token
    {
        public string Name { get; set; }
        public ValueToken Value { get; set; }
    }
}
