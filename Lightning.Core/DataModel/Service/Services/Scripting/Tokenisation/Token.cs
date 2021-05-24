using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Token
    /// 
    /// April 16, 2021 (modified May 24, 2021)
    /// 
    /// <para>Defines a token for parsing scripts.</para>
    /// </summary>
    public class Token
    {
        public List<char> Characters { get; set; }

        /// <summary>
        /// The logical AST token tree of this element.
        /// </summary>
        public List<Token> TokenTree { get; set; }
    }
}
