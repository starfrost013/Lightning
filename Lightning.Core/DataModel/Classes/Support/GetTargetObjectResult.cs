using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class GetTargetObjectResult : IResult
    {
        public PhysicalObject TargetObject { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
