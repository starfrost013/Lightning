using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptScopeType
    /// 
    /// April 23, 2021
    /// 
    /// Defines types of script scopes.
    /// </summary>
    public enum ScriptScopeType
    {
        /// <summary>
        /// Global scope
        /// </summary>
        Global = 0,

        /// <summary>
        /// Function scope
        /// </summary>
        Function = 1,

        /// <summary>
        /// Statement scope. 
        /// </summary>
        Statement = 2
    }
}
