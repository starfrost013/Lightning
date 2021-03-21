using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// The result for getting an instance.
    /// </summary>
    public class GetInstanceResult : IResult 
    {
        /// <summary>
        /// The instance; polymorphically will be the type we want
        /// </summary>
        public object Instance { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
