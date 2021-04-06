using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning DataModel
    /// 
    /// DataModel/Instance Ver0.4.0
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
    /// 2020-04-02  Error handling, implemented InstanceCollection.Add(); 
    /// 2020-04-05  Actually implemented InstanceCollection.Add(); and InstanceCollection.Clear(); - DataModel class itself now stores GlobalSettings.
    /// 2020-04-06  Added Workspace; made parent/child addition actually work...
    /// 
    /// </summary>
    public abstract class Instance
    {
        // These all should always be the same, so I made it so.
        // 2021-04-02
        public static int INSTANCEAPI_VERSION_MAJOR = DataModel.DATAMODEL_VERSION_MAJOR;
        public static int INSTANCEAPI_VERSION_MINOR = DataModel.DATAMODEL_VERSION_MINOR;
        public static int INSTANCEAPI_VERSION_REVISION = DataModel.DATAMODEL_VERSION_REVISION;

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
                    // for workspace etc - if instancetags.parentcanbennull allow a null parent 
                    if (!Attributes.HasFlag(InstanceTags.ParentCanBeNull))
                    {
                        ErrorManager.ThrowError(ClassName, "CannotAcquireNullParentException");
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                   
                }
                 
            }

            set
            {
                // Check that a candidate parent is a superclass of this instance or is of the same class of this instance.
                Type TypeOfCParent = this.GetType();
                Type TypeOfVParent = value.GetType();

                if (TypeOfCParent == TypeOfVParent)
                {
                    _parent = value; 
                }
                else // If it isn't the same...
                {
                    // is it a subclass?
                    if (TypeOfCParent.IsSubclassOf(TypeOfVParent))
                    {
                        _parent = value;
                    }
                    else // Throw an error and return if it isn't.
                    {

                        ErrorManager.ThrowError(ClassName, "InstanceCannotBeParentException", $"{TypeOfCParent} is not {TypeOfVParent} and does not inherit from it. As a result of this, {ClassName} cannot be a parent of {value.ClassName}.");
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
        /// The user-friendly name of this object. OPTIONAL OVERRIDE
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
                ErrorManager.ThrowError(ClassName, "InstanceInfoGenerationFailedException", $"Failed to generate InstanceInfo: {IIR.FailureReason}. This instance will not appear in the Explorer.");
                return;
                
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

        /// <summary>
        /// Gets the first child of this Instance with ClassName <see cref="ClassName"/>
        /// </summary>
        /// <returns>A <see cref="GetInstanceResult"/> object. The Instance is <see cref="GetInstanceResult.Instance"/>.</returns>
        public GetInstanceResult GetFirstChildOfType(string ClassName) => Children.GetFirstChildOfType(ClassName);

        /// <summary>
        /// Gets the last child of this Instance with Class Name <see cref="ClassName"/>
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public GetInstanceResult GetLastChildOfType(string ClassName) => Children.GetLastChildOfType(ClassName);
        

        public Instance GetParent() => Parent;

        public void RemoveAllChildren() => Children.Clear();
        public void AddChild(Instance Chl) => Children.Add(Chl);
        public GenericResult RemoveChild(Instance Chl)
        {
            GenericResult GR = new GenericResult();
            
            if (Children.Instances.Contains(Chl))
            {
                Children.Instances.Remove(Chl);
                GR.Successful = true;
                return GR; 
            }
            else
            {
                // Successful is false by default
                GR.FailureReason = "Attempted to remove an Instance that is either not in the DataModel or not a child of this Instance.";
                return GR; 
            }
        }

        public GenericResult RemoveChildAt(int Id)
        {
            GenericResult GR = new GenericResult();

            if (Id < 0 || Id > Children.Instances.Count - 1)
            {
                GR.FailureReason = $"Attempted to remove invalid child with ID {Id}, when there are only {Children.Instances.Count - 1} children!";
                return GR; 
            }
            else
            {
                Instance ChildToRemove = Children.Instances[Id];
                return RemoveChild(ChildToRemove);
            }
        }


    }
}
