using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Lightning   Extensible Physics Engine
    /// 
    /// July 20, 2021 00:49
    /// 
    /// Defines a physics controller. A physics controller is a code-only API class that defines the physics behaviour
    /// for a particular class. It is designed to be natively extensible and additionally extensible through Lua
    /// </summary>
    public abstract class PhysicsController : PhysicalInstance
    {
        internal override string ClassName => "PhysicsController";
        internal override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable;

        /// <summary>
        /// Ran on the initialisation of the physics controller.
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// Ran each frame.
        /// </summary>
        public abstract void OnTick(PhysicalInstance Object, PhysicsState PS); // TODO: TEMP VERY VERY BAD DO NOT USE FOR LONGER THAN LIKE A DAY

    }
}
