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
    public class InstanceTag
    {
        /// <summary>
        /// This instance is instantiable from ESX2 and Instance.CreateInstance();
        /// </summary>
        public bool Instantiable { get; set; }

        /// <summary>
        /// Is this Instance private?
        /// 
        /// If so, it cannot be called in scripts.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// IDE-visible
        /// </summary>
        public bool ShownInIDE { get; set; }

        public bool Serialisable { get; set; }
    }
}
