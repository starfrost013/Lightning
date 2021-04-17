using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// StartOfFileToken
    /// 
    /// April 16, 2021
    /// 
    /// Defines the start of a LightningScript file. A LightningScript file must start with a StartOfFileToken and end with an EndOfFile token.
    /// </summary>
    public class StartOfFileToken : Token
    {
        public string ScriptName { get; set; }
    }
}
