using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class ServiceStartResult : IResult
    {
        public string Information { get; set; }
        public bool Successful { get; set; }
    }
}
