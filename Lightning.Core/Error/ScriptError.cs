using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// ScriptError
    /// 
    /// April 17, 2021
    /// 
    /// Defines an error in a script. 
    /// </summary>
    public class ScriptError : Error
    {

        public string Line { get; set; }
        public string ScriptName { get; set; }
    }
}
