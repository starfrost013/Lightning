using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Light
    /// 
    /// January 8, 2022
    /// 
    /// Defines a light source in Lightning
    /// </summary>
    public class Light : PhysicalObject
    {
        /// <summary>
        /// Determines if this Light will bounce off objects.
        /// </summary>
        public bool BounceOffObjects { get; set; }

        /// <summary>
        /// The intensity of this lighting
        /// Implemented as a maximum alpha value 1-255
        /// </summary>
        public byte Intensity { get; set; }

        /// <summary>
        /// Range of the maximum extent of this lighting
        /// </summary>
        public double Range { get; set; }


        public override void OnCreate()
        {
            // enable physics
            // so we can detect if we are colliding
            PhysicsEnabled = true; 
        }

    }
}
