using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Tools.AutomatedTestingManager
{
    /// <summary>
    /// LaunchArgs
    /// 
    /// July 29, 2021 (modified July 30, 2021: Add LightningDirectory and ProcessLifetime)
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
        /// The directory that contains the Lightning installation.
        /// </summary>
        public string LightningDirectory { get; set; }

        /// <summary>
        /// The process lifetime in milliseconds. Default = 30 seconds.
        /// </summary>
        public int ProcessLifetime { get; set; }

        /// <summary>
        /// Determines if recursive search will be used.
        /// </summary>
        public bool Recurse { get; set; }


    }
}
