using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CollisionResult
    /// 
    /// July 26, 2021
    /// 
    /// Defines a result class for a collision.
    /// </summary>
    public class CollisionResult : IResult 
    {
        /// <summary>
        /// The calculated collision manifold as a result of the collision operation returning this class - see <see cref="Manifold"/>.
        /// </summary>
        public Manifold Manifold { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }

        public CollisionResult()
        {
            Manifold = new Manifold();
        }
    }
}
