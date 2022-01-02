using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Validation result class for DDMS Validation.
    /// </summary>
    public class DDMSValidateResult : IResult
    {
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
