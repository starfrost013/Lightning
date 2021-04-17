using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class TokenListResult : IResult
    {
        public List<Token> TokenList { get; set; }
        public bool Successful { get; set; }
        public string FailureReason { get; set; }
    }
}
