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
        /// The mass of this object. (kg)
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// The acceleration of this object. (m/s2)
        /// </summary>
        public Vector2 Acceleration { get; set; }

        /// <summary>
        /// The speed of this object. (m/s)
        /// </summary>
        public Vector2 Speed { get; set; }
        public bool PhysicsEnabled { get; set; }
        internal object PhysicsController { get; set; }
    }
}
