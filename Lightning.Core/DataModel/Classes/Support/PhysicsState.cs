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

        /// <summary>
        /// The current physics mode - see <see cref="GravityState"/>.
        /// </summary>
        public GravityState GravityState { get; set; }

        internal static readonly Vector2 GravityDefaultValue = new Vector2(0, -5.5);

        /// <summary>
        /// The boundary under which any object will be destroyed by the PhysicsService.
        /// </summary>
        public Vector2 ObjectKillBoundary { get; set; }

        internal static readonly Vector2 ObjectKillBoundaryDefaultValue = new Vector2(2147483647, -1000);
    }
}
