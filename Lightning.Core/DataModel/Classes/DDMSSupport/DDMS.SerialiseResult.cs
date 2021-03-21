using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class DDMSSerialisationResult : IResult
    {
        public DataModel DataModel { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
