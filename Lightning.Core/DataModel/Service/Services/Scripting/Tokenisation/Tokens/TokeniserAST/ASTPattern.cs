using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ASTPattern
    /// 
    /// June 1, 2021
    /// 
    /// Defines an individual AST toeknisation pattern.
    /// </summary>
    public class ASTPattern
    {
        public string PatternName { get; set; }
        public List<Token> TokenList { get; set; }
        public ASTPattern()
        {
            TokenList = new List<Token>(); 
        }
    }
}
