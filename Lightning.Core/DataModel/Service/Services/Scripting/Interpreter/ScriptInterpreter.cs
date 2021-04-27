using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LightningScript
    /// 
    /// A dynamically typed scripting language for Lightning
    /// 
    /// April 27, 2021
    /// </summary>
    public class ScriptInterpreter
    {
        /// <summary>
        /// All methods that have been exposed.
        /// </summary>
        public List<ScriptMethod> ExposedMethods { get; set; }

        /// <summary>
        /// A list of currently running scripts.
        /// </summary>
        public List<Script> RunningScripts { get; set; }

        public ScriptInterpreter()
        {   
            ExposedMethods = new List<ScriptMethod>();
            RunningScripts = new List<Script>();
        }

        public List<Script> ScX { get; set; }
    }
}
