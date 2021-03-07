using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class InstantiationResult : IResult
    {
        public object Instance { get; set; }
        public bool Successful { get; set; }
    }
}
