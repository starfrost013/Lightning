using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Tools.AutomatedTestingManager
{
    /// <summary>
    /// LaunchArgs
    /// 
    /// July 29, 2021
    /// 
    /// Defines launch arguments for the Automated Testing Manager.
    /// </summary>
    public class LaunchArgs
    {
        /// <summary>
        /// The directory to search - defaults to "Test"
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Determines if recursive search will be used.
        /// </summary>
        public bool Recurse { get; set; }
    }
}
