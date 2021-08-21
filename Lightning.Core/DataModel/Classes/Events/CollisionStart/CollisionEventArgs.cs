using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CollisionEventArgs
    /// 
    /// August 9, 2021 (modified August 20, 2021: add comment block)
    /// 
    /// Defines the event arguments for Collision events.
    /// </summary>
    public class CollisionEventArgs : EventArgs
    {
        /// <summary>
        /// The Manifold of this collision - see <see cref="Manifold"/>.
        /// </summary>
        public Manifold Manifold { get; set; }
    }
}
