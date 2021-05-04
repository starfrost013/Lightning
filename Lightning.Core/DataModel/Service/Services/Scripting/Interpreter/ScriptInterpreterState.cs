using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptInterpreterState
    /// 
    /// May 3, 2021
    /// </summary>
    public class ScriptInterpreterState
    {
        /// <summary>
        /// A list of variables
        /// </summary>
        public List<Variable> Variables { get; set; }

        public ScriptInterpreterState()
        {
            Variables = new List<Variable>();
        }
    }
}
