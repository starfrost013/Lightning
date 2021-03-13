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
    /// 2020-03-12  Made InstanceInfo 
    /// </summary>
    public abstract class Instance
    {
        public static int INSTANCEAPI_VERSION_MAJOR = 0;
        public static int INSTANCEAPI_VERSION_MINOR = 1;
        public static int INSTANCEAPI_VERSION_REVISION = 4;

        /// <summary>
        /// Attributes of this object. OVERRIDE OPTIONAL!
        /// </summary>
        public virtual InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE | InstanceTags.Destroyable); }

        /// <summary>
        /// The properties and methods of this object.
        /// </summary>
        public InstanceInfo Info { get; set; }

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
            
            InstanceInfoResult IIR = InstanceInfo.FromType(typeof(Instance));


            if (IIR.Successful)
            {
                Info = IIR.InstanceInformation;
            }
            else
            {
                // TODO - SERIALISATION - THROW ERROR
                return; 
                // TODO - SERIALISATION - THROW ERROR
            }

        }

        public void OnSpawn()
        {
            throw new NotImplementedException();
        }


    }
}
