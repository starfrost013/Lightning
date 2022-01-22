using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LoadGameDLLResult
    /// 
    /// January 15, 2022
    /// 
    /// Defines a result class for C# scripting GameDLL loading.
    /// </summary>
    public class LoadGameDLLResult : IResult
    {

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// The assembly containing the GameDLL.
        /// </summary>
        public Assembly GameDLL { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }
    }
}
