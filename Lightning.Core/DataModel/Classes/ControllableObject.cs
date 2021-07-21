﻿using System;
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

        public bool PhysicsEnabled { get; set; }
        internal object PhysicsController { get; set; }
    }
}
