using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Token
    /// 
    /// April 16, 2021 (modified June 2, 2021)
    /// 
    /// <para>Defines a token for parsing scripts.</para>
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Determines if this token is the centre of an AST pattern.
        /// </summary>
        public bool IsCentralToken { get; set; }

        /// <summary>
        /// Determines if a particular token is at the end of a pattern
        /// </summary>
        public bool IsEndOfPattern { get; set; }
        public List<char> Characters { get; set; }

        /// <summary>
        /// The logical AST token tree of this element.
        /// </summary>
        public List<Token> Children { get; set; }
    }
}
