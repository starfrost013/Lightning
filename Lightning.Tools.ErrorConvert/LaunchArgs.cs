using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Tools.ErrorConvert
{
    /// <summary>
    /// LaunchArgs
    /// 
    /// July 2, 2021
    /// 
    /// Launch arguments for the VERY quick and dirty tool to convert from old to new error system
    /// </summary>
    public class LaunchArgs
    {
        /// <summary>
        /// The old error xml file.
        /// </summary>
        public string OldFile { get; set; }

        /// <summary>
        /// The new error xml file.
        /// </summary>
        public string NewFile { get; set; }

        /// <summary>
        /// The namespace to use for the converted file. Must be a valid .NET namespace.
        /// </summary>
        public string Namespace { get; set; }
    }
}
