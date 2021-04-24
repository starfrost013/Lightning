using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptScope
    /// 
    /// April 23, 2021
    /// 
    /// Defines a scope. A scope is where variables etc will be defined. 
    /// </summary>
    public class ScriptScope
    {
        /// <summary>
        /// The type of the current scope. 
        /// </summary>
        public ScriptScopeType Type { get; set; }
    }
}
