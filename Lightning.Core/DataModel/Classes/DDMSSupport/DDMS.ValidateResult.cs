﻿using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Validation result class for DDMS Validation.
    /// </summary>
    public class DDMSValidateResult : IResult
    {
        public string FailureMessage { get; set; }
        public bool Successful { get; set; }
    }
}