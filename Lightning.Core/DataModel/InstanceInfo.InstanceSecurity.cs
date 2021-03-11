using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning.Core
    /// 
    /// DataModel
    /// 
    /// Instance Security
    /// 
    /// Determines the security of an instance
    /// </summary>
    public enum InstanceSecurity
    {
        /// <summary>
        /// Can be used from scripts and the engine.
        /// </summary>
        Public = 0,

        /// <summary>
        /// Can be called into by any component of the engine.
        /// </summary>
        Private = 1,

        /// <summary>
        /// Hidden from all enumerations.
        /// </summary>
        LightningLocked = 2,
    }
}
