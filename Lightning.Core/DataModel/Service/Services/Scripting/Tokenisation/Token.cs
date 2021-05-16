using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Token
    /// 
    /// April 16, 2021
    /// 
    /// <para>Defines a token for parsing scripts.</para>
    /// </summary>
    public class Token
    {
        public List<char> Characters { get; set; }

        public List<Token> ASTTokenTree { get; set; }
    }
}
