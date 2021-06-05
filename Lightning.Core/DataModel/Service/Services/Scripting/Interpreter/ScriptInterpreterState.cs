using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptInterpreterState
    /// 
    /// May 3, 2021 (modified June 5, 2021: Lua)
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
