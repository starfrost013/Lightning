using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LoadScriptResult
    /// 
    /// April 27, 2021 
    /// </summary>
    public class LoadScriptResult : IResult
    {
        /// <summary>
        /// The script that has been loaded. Will only be valid if <see cref="FailureReason"/> is null and <see cref="Successful"/> is true. 
        /// </summary>
        public Script Script { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }

    }
}
