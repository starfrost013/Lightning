using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{

    /// <summary>
    /// Manifold [Non-DataModel]
    /// 
    /// July 26, 2021
    /// 
    /// Defines a collision manifold.
    /// </summary>
    public class Manifold 
    {

        /// <summary>
        /// The first object to implement.
        /// </summary>
        public PhysicalInstance PhysicalInstanceA { get; set; }

        /// <summary>
        /// The second object to implement.
        /// </summary>
        public PhysicalInstance PhysicalInstanceB { get; set; }

        /// <summary>
        /// The amount penetrated as a part of the collision.
        /// </summary>
        public double PenetrationAmount { get; set; }

        /// <summary>
        /// The normal vector to move to resolve the AABB vs AABB collision.
        /// </summary>
        public Vector2 NormalVector { get; set; }
    }
}
