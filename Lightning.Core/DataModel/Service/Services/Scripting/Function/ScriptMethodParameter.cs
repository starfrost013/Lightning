using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptMethodParameter
    /// 
    /// April 24, 2021
    /// 
    /// Defines a parameter in a scripting-callable method
    /// </summary>
    public class ScriptMethodParameter
    {
        public string Name { get; set; }
        public Type Type { get; set; }
    }
}
