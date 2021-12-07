using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Result class used for GameSettings.
    /// </summary>
    public class GetGameSettingsResult : IResult 
    {
        public GameSettings GameSettings { get; set; }
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }
    }
}
