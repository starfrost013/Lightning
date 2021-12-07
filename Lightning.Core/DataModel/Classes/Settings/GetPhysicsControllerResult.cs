using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GetPhysicsControllerResult
    /// 
    /// July 21, 2021
    /// 
    /// Defines a result class for obtaining physics controllers.
    /// </summary>
    public class GetPhysicsControllerResult : IResult
    {
        /// <summary>
        /// The physics controller set by the object.
        /// </summary>
        public PhysicsController PhysController { get; set; }


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
