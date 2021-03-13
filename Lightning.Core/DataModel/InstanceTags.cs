using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning Core
    /// 
    /// Instance Tagging Services
    /// 
    /// Holds attributes that are instance-wide.
    /// </summary>
    public enum InstanceTags
    {
        /// <summary>
        /// This instance is instantiable from ESX2.
        /// </summary>
        Instantiable = 1,

        /// <summary>
        /// This Instance is shown in the IDE.
        /// </summary>
        ShownInIDE = 2,

        /// <summary>
        /// This Instance is serialisable.
        /// </summary>
        Serialisable = 4,

        /// <summary>
        /// This instance is saveable.
        /// </summary>
        Archivable = 8,

        /// <summary>
        /// This instance is destroyable.
        /// </summary>
        Destroyable = 16,

    }
}
