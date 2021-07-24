using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// PhysicsState
    /// 
    /// July 23, 2021
    /// 
    /// Defines the physics state.
    /// </summary>
    public class PhysicsState
    {
        /// <summary>
        /// The current gravitational pull
        /// </summary>
        public Vector2 Gravity { get; set; }

        public GravityState GravityState { get; set; }
    }
}
