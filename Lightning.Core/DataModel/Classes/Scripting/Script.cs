using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Script
    /// 
    /// April 16, 2021
    /// 
    /// Defines a LightningScript script. 
    /// </summary>
    public class Script : SerialisableObject
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "Script";

        /// <summary>
        /// A list of the script's lines.
        /// </summary>
        internal List<string> ScriptContent { get; set; }
        
        /// <summary>
        /// Used for seamless DataModel serialisation. 
        /// </summary>
        public string Content { get; set; }
    }
}
