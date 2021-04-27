using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{ 
    /// <summary>
    /// ScriptMethod
    /// 
    /// April 24, 2021
    /// 
    /// Defines a method that has been exposed to scripting. Must be public, but not all public methods may be exposed; is obtained from InstanceInformation.
    /// </summary>
    public class ScriptMethod
    {
        /// <summary>
        /// The full name of the method, including namespace.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The list of parameters of this method.
        /// </summary>
        public List<ScriptMethodParameter> Parameters { get; set; }

        public ScriptMethod()
        {
            Parameters = new List<ScriptMethodParameter>(); 
        }
    }
}
