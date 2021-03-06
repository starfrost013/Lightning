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
    /// 
    /// 2020-03-04  Created
    /// 2020-03-06  Refactored: renamed InstanceTag to attributes, made ClassName virtual and read-only. 
    /// </summary>
    public abstract class Instance
    {
        public static int INSTANCEAPI_VERSION_MAJOR = 0;
        public static int INSTANCEAPI_VERSION_MINOR = 1;
        public static int INSTANCEAPI_VERSION_REVISION = 1;

        /// <summary>
        /// Attributes of this object.
        /// </summary>
        public static InstanceTag Attributes { get; set; }

        /// <summary>
        /// The class name of this object. MUST OVERRIDE!
        /// </summary>
        public virtual string ClassName { get; }

        /// <summary>
        /// The user-name of this object. MUST OVERRIDE!
        /// </summary>
        public string Name { get; set; }

        public Instance()
        {
            Name = "Object";
        }

        public void OnSpawn()
        {

        }


    }
}
