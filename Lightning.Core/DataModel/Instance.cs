﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning DataModel
    /// 
    /// Instance Ver0.2.2
    /// 
    /// Provides the root for all objects provided in Lightning.
    /// 
    /// 2020-03-04  Created
    /// 2020-03-06  Refactored: renamed InstanceTag to attributes, made ClassName virtual and read-only.
    /// 2020-03-09  Added InstanceInfo. Possibly merge InstanceTag and InstanceInfo?
    /// 2020-03-11  Made InstanceTag an enum - InstanceTags
    /// 2020-03-12  Made InstanceInfo 
    /// 2020-03-18  DataModel.State only contains first-level instances; Instances store parent and child
    /// 2020-03-23  (need to move this comment block to DataModel.cs): worked on Standard Instance Library
    /// 2020-03-26  Added the ability to set an instance's parent at instantiation time. 
    /// </summary>
    public abstract class Instance
    {
        public static int INSTANCEAPI_VERSION_MAJOR = 0;
        public static int INSTANCEAPI_VERSION_MINOR = 2;
        public static int INSTANCEAPI_VERSION_REVISION = 2;

        /// <summary>
        /// Backing field for <see cref="Parent"/>
        /// </summary>
        private Instance _parent { get; set; }

        /// <summary>
        /// The parent of this instance.
        /// </summary>
        public Instance Parent { get
            {
                if (_parent != null)
                {
                    return _parent;

                }
                else
                {
                    // todo: throw error
                    Logging.Log("Error: Attempted to acquire nonexistent parent!", "Lightning DataModel", MessageSeverity.Error);
                    return null; 
                }
                 
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
        public InstanceCollection Children { get; set; }

        /// <summary>
        /// Attributes of this object. OVERRIDE OPTIONAL!
        /// </summary>
        public virtual InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE | InstanceTags.Destroyable); }

        /// <summary>
        /// The properties and methods of this object.
        /// </summary>
        public InstanceInfo Info { get; set; }

        /// <summary>
        /// The class name of this object. MUST OVERRIDE! -- READONLY
        /// </summary>
        public virtual string ClassName { get; }

        /// <summary>
        /// The user-name of this object. MUST OVERRIDE!
        /// </summary>
        public string Name { get; set; }

        public Instance()
        {
            Name = "Instance";
            Children = new InstanceCollection();
        }

        public void GenerateInstanceInfo()
        {
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

        /// <summary>
        /// Lightning Instance Standard Library
        /// 
        /// GetChild()
        /// 
        /// Gets a child of this Instance.
        /// </summary>
        /// <param name="Name">The name of this instance to acquire.</param>
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

        /// <summary>
        /// Lightning Instance Standard Library
        /// 
        /// Get a child with the ID <paramref name="Id"/>of this instance.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public GetInstanceResult GetChildAt(int Id)
        {
            GetInstanceResult GIR = new GetInstanceResult();

            if (Id < 0 || Id > Children.Instances.Count)
            {
                // Successful is false by default
                GIR.FailureReason = "Cannot find instance";
                return GIR; 

            }
            else
            {
                if (Children.Instances.Count < Id)
                {
                    GIR.FailureReason = "Attempted to acquire invalid instance ID";
                    return GIR; 
                }
                else
                {
                    GIR.Instance = Children.Instances[Id];
                    GIR.Successful = true;
                    return GIR;
                }

            }
        }

        /// <summary>
        /// Lightning Instance Standard Library
        /// 
        /// Get the first child of this instance.
        /// </summary>
        /// <returns></returns>
        public GetInstanceResult GetFirstChild()
        {

            GetInstanceResult GIR = new GetInstanceResult();

            if (Children.Instances.Count <= 0)
            {
                GIR.FailureReason = "There are no instances to get the first child of!";
                return GIR; 
            }
            else
            {
                GIR.Instance = Children.Instances[0];
                GIR.Successful = true;
                return GIR;
            }

        }

        /// <summary>
        /// Lightning Instance Standard Library
        /// 
        /// Gets the last child of this Instance.
        /// </summary>
        /// <returns></returns>
        public GetInstanceResult GetLastChild()
        {
            GetInstanceResult GIR = new GetInstanceResult();

            if (Children.Instances.Count <= 0)
            {
                GIR.FailureReason = "There are no instances to get the first child of!";
                return GIR; 
            }
            else
            {
                int ChildrenCount = Children.Instances.Count - 1;

                GIR.Instance = Children.Instances[ChildrenCount];
                GIR.Successful = true;
                return GIR;
            }
        }

        public Instance GetParent() => Parent;

        public void RemoveAllChildren() => Children.Instances.Clear(); 


    }
}
