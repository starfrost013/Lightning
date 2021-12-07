using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class GetMultiInstanceResult : IResult
    {
        public List<Instance> Instances { get; set; }
        public bool Successful { get; set; }
        public string FailureReason { get; set; }

        public GetMultiInstanceResult()
        {
            Instances = new List<Instance>(); 
        }
    }
}
