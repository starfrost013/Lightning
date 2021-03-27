using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Result class for service shutdown
    /// </summary>
    public class ServiceShutdownResult : IResult
    {
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
