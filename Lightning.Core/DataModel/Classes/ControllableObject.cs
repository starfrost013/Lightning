using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ControllableObject
    /// 
    /// April 13, 2021
    /// 
    /// Defines an object that can be controlled.
    /// </summary>
    public class ControllableObject : PhysicalObject
    {
        internal override string ClassName => "ControllableObject";
        internal override InstanceTags Attributes => 0;

        public AABB AABB
        {
            get
            {
                if (Position == null
                || Size == null)
                {
                    return null;
                }
                else
                {
                    return new AABB(Position, Size);
                }
                
            }
        }

        /// <summary>
        /// Backing field for <see cref="_mass"/>
        /// </summary>
        private double _mass { get; set; }

        /// <summary>
        /// The mass of this object. (kg)
        /// </summary>
        public double Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                InverseMass = 1 / value;
                _mass = value;
            }

        }

        /// <summary>
        /// Inverse mass of this object.
        /// </summary>
        internal double InverseMass { get; private set; }


        /// <summary>
        /// The speed of this object. (m/s)
        /// </summary>
        public Vector2 Velocity { get; set; }

        public double Elasticity { get; set; }
        public bool PhysicsEnabled { get; set; }
        internal object PhysicsController { get; set; }
    }
}
