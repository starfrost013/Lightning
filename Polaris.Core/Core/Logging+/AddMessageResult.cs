using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Polaris.Core
{
    public class AddMessageResult : IResult
    {
        public LoggingMessage LoggingMessage { get; set; }
        public bool Successful { get; set; }
        public string FailureReason { get; set; }
    }
}
