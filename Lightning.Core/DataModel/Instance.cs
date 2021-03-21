using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning DataModel
    /// 
    /// Instance Ver0.2.0
    /// 
    /// Provides the root for all objects provided in Lightning.
    /// 
    /// 2020-03-04  Created
    /// 2020-03-06  Refactored: renamed InstanceTag to attributes, made ClassName virtual and read-only.
    /// 2020-03-09  Added InstanceInfo. Possibly merge InstanceTag and InstanceInfo?
    /// 2020-03-11  Made InstanceTag an enum - InstanceTags
    /// 2020-03-12  Made InstanceInfo 
    /// 2020-03-18  DataModel.State only contains first-level instances; Instances store parent and child
    /// 
    /// </summary>
    public abstract class Instance
    {
        public static int INSTANCEAPI_VERSION_MAJOR = 0;
        public static int INSTANCEAPI_VERSION_MINOR = 2;
        public static int INSTANCEAPI_VERSION_REVISION = 0;

        /// <summary>
        /// Backing field for <see cref="Parent"/>
        /// </summary>
        private Instance _parent { get; set; }

        /// <summary>
        /// The parent of this instance.
        /// </summary>
        public Instance Parent { get
            {
                return _parent; 
            }

            set
            {
                // Check that a candidate parent is a superclass of this instance
                Type TypeOfCParent = this.GetType();
                Type TypeOfVParent = value.GetType();

                if (TypeOfCParent == TypeOfVParent)
                {
                    _parent = value; 
                }
                else
                {
                    if (TypeOfCParent.IsSubclassOf(TypeOfVParent))
                    {
                        _parent = value;
                    }
                    else
                    {
                        // TODO - THROW ERROR [SERIALISATION/ERRORS.XML]
                        return; 
                    }
                }
            }

        }

        /// <summary>
        /// The children of this instance.
        /// 
        /// Children must be classes that derive from this class.  (2021-03-21)
        /// </summary>
        public virtual InstanceCollection Children { get; set; }

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

            Children = new InstanceCollection();

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

        /// <summary>
        /// Lightning Instance Standard Library
        /// 
        /// GetChild()
        /// 
        /// Gets a child of this Instance.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public GetInstanceResult GetChild(string Name)
        {
            GetInstanceResult GIR = new GetInstanceResult();
            
            foreach (Instance Ins in Children)
            {
                if (Ins.Name == Name)
                {
                    GIR.Successful = true;
                    GIR.Instance = Ins;
                    return GIR;
                }
            }

            GIR.FailureReason = "Cannot find instance";
            return GIR;
            //todo throw error
        }

        public void OnSpawn()
        {
            throw new NotImplementedException();
        }


    }
}
