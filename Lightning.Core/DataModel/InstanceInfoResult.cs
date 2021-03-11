using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class InstanceInfoResult : IResult 
    {
        public string FailureReason { get; set; }
        public InstanceInfo InstanceInformation { get; set; }
        public bool Successful { get; set; }

        public InstanceInfoResult()
        {
            InstanceInformation = new InstanceInfo();
        }

    }
}
