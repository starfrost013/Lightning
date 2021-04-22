using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// StatementToken
    /// 
    /// April 22, 2021
    /// 
    /// Defines a statement. Woo!
    /// </summary>
    public class StatementToken : Token 
    {
        public StatementTokenType Type { get; set; }
    }
}
