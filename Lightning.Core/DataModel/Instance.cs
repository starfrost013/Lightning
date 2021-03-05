using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning DataModel
    /// 
    /// Instance
    /// 
    /// Provides the root for all objects provided in Lightning.
    /// </summary>
    public abstract class Instance
    {
        /// <summary>
        /// The class name of this object.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attributes of this object.
        /// </summary>
        public InstanceTag InstanceTag { get; set; }

        public void OnSpawn()
        {

        }


    }
}
