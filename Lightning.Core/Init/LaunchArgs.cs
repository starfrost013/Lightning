using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// LaunchArgs
    /// 
    /// April 8, 2021 (modified May 16, 2021: Add AppName for Polaris)
    /// 
    /// Defines launch arguments for the DataModel.
    /// </summary>
    public class LaunchArgs
    {

        /// <summary>
        /// Application name used for 
        /// </summary>
        public string AppName { get; set; }
        public string GameXMLPath { get; set; }
        public bool InitServices { get; set; }


    }
}
