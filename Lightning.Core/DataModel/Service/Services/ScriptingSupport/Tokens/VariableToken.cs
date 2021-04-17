using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Defines a variable token. Paired with an OperatorToken and a ValueToken.
    /// </summary>
    public class VariableToken : Token
    {
        public string Name { get; set; }
    }
}
