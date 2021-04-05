using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class GlobalSettingsResult : IResult
    {
        public string FailureReason { get; set; }
        public GlobalSettings Settings { get; set; }
        public bool Successful { get; set; }
    }
}
