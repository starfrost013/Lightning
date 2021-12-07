using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Result class for acquiring GlobalSettings.
    /// </summary>
    public class GlobalSettingsResult : IResult
    {
        /// <summary>
        /// Assists in debugging.
        /// </summary>
        public Exception BaseException { get; set; }
        public string FailureReason { get; set; }
        public GlobalSettings Settings { get; set; }
        public bool Successful { get; set; }
    }
}
