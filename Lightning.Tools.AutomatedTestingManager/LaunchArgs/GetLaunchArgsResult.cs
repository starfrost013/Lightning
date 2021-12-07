using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Tools.AutomatedTestingManager
{
    /// <summary>
    /// GetLaunchArgsResult
    /// 
    /// July 29, 2021
    /// 
    /// Defines the result class for <see cref="LaunchArgs"/> (Lightning.Tools.AutomatedTestingManager)
    /// </summary>
    public class GetLaunchArgsResult : IResult
    {
        /// <summary>
        /// The launch arguments - see <see cref="LaunchArgs"/>
        /// </summary>
        public LaunchArgs LaunchArguments { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }

        public GetLaunchArgsResult()
        {
            LaunchArguments = new LaunchArgs();
        }
    }
}
