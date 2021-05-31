using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class ASTTreeSectionResult : IResult
    {
        public List<Token> TokenList { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }

        public ASTTreeSectionResult()
        {
            TokenList = new List<Token>(); 
        }
    }
}
