using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptStatus
    /// 
    /// October 15, 2021
    /// 
    /// Defines the script status.
    /// </summary>
    public class ScriptStatus
    {
        /// <summary>
        /// The line to resume from when unpausing.
        /// </summary>
        public int LineToResumeFrom { get; set; }
    }
}
