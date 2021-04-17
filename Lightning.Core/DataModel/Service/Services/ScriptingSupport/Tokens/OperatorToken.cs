using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// OperatorToken
    /// 
    /// April 16, 2021
    /// 
    /// Defines an operator token.
    /// An operator token is a token specific to an individual operator.
    /// </summary>
    public class OperatorToken : Token 
    {
        public OperatorTokenType Type { get; set; }
    }
}
