using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Test
{
    /// <summary>
    /// Result class for testworld tests
    /// </summary>
    public class TestResult : IResult
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FailureReason { get; set; }

        public MessageSeverity Severity { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }   
    }
}
