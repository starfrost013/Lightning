using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Result class for SDL2 renderer initialisation.
    /// </summary>
    public class SDLInitialisationResult : IResult
    {

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
