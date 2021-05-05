using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class GetMultiInstanceResult : IResult
    {
        public List<Instance> InstanceList { get; set; }
        public bool Successful { get; set; }
        public string FailureReason { get; set; }
    }
}
