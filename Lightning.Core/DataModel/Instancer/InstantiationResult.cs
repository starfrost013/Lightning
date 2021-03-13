using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Result class for DataModel instantiation.
    /// </summary>
    public class InstantiationResult : IResult
    {
        public string Error { get; set; }
        public object Instance { get; set; }
        public bool Successful { get; set; }
    }
}
