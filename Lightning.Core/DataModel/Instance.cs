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
    /// 2020-03-09  Added InstanceInfo. Possibly merge InstanceTag and InstanceInfo?
    /// 2020-03-11  Made InstanceTag an enum - InstanceTags
    /// </summary>
    public abstract class Instance
    {
        public static int INSTANCEAPI_VERSION_MAJOR = 0;
        public static int INSTANCEAPI_VERSION_MINOR = 1;
        public static int INSTANCEAPI_VERSION_REVISION = 3;

        /// <summary>
        /// Attributes of this object. OVERRIDE OPTIONAL!
        /// </summary>
        public virtual InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE); }

        /// <summary>
        /// The properties and methods of this object.
        /// </summary>
        public static InstanceInfo Info { get; set; }

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
            Name = "Instance";
        }

        public void OnSpawn()
        {
            throw new NotImplementedException();
        }

        public void GetMethods()
        {
            // implement: 2021-03-09
            throw new NotImplementedException();
        }

    }
}
