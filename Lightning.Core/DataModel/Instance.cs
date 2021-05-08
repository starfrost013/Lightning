using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Lightning DataModel
    /// 
    /// DataModel/Instance Ver0.12.1
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
    /// 2020-04-08  Modified Instance.AddChild(); to make it non-redundant
    /// (continues...)
    /// </summary>
    /// 
    public abstract class Instance
    {
        /// <summary>
        /// Backing field for <see cref="Parent"/>
        /// </summary>
        private Instance _parent { get; set; }

        /// <summary>
        /// The parent of this instance.
        /// </summary>
        internal Instance Parent { get
            {

                // we used to do a lot more in this code block,
                // but code overhauls have occurred that means they're now done somehwere else.
                return _parent; 
                 
            }

            set
            {

                _parent = value; 

            }

        }

        /// <summary>
        /// The children of this instance.
        /// 
        /// Children must be classes that derive from this class.  (2021-03-21)
        /// </summary>
        internal InstanceCollection Children { get; set; }

        /// <summary>
        /// Attributes of this object. OVERRIDE OPTIONAL!
        /// </summary>
        internal virtual InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE | InstanceTags.Destroyable); }

        /// <summary>
        /// The properties and methods of this object.
        /// </summary>
        internal InstanceInfo Info { get; set; }

        /// <summary>
        /// The class name of this object. MUST OVERRIDE! -- READONLY
        /// </summary>
        internal virtual string ClassName { get; }

        /// <summary>
        /// The user-friendly name of this object. OPTIONAL OVERRIDE
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This Instance is deprecated and will be removed in a future release of the engine. Using this in new projects is not recommended.
        /// </summary>
        internal bool Deprecated { get; set; }

        /// <summary>
        /// This Instance is experimental and may be changed or removed at any time in a future release of the engine. Using this in projects you intend to release is not recommended.
        /// </summary>
        internal bool Experimental { get; set; }

        public Instance()
        {
            Name = "Instance";
            Children = new InstanceCollection();
        }

        public void GenerateInstanceInfo()
        {
            // April 11, 2021
            InstanceInfoResult IIR = InstanceInfo.FromType(this.GetType());

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
        
        /// <summary>
        /// Gets the Parent of this Instance.
        /// </summary>
        /// <returns></returns>
        public Instance GetParent() => Parent;
        
        /// <summary>
        /// Removes all children of this Instance.
        /// </summary>
        public void RemoveAllChildren() => Children.Clear();

        /// <summary>
        /// Adds a child of type <paramref name="ClassName">to this Instance.</paramref>
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public object AddChild(string ClassName) => DataModel.CreateInstance(ClassName, this);

        /// <summary>
        /// Adds the Instance <paramref name="Ins"/> to the children of this Instance. Used for grouping and ungrouping
        /// </summary>
        /// <param name="Ins">The Instance you wish to add to the children of this Instance.</param>
        public void AddChildI(Instance Ins) => Children.Add(Ins, this);

        public GenericResult RemoveChild(Instance Chl)
        {
            GenericResult GR = new GenericResult();
            
            if (Children.Contains(Chl))
            {
                Children.Remove(Chl); // 2021-04-15: use the actual remove method
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

        public GetMultiInstanceResult GetAllChildrenOfType(string ClassName)
        {
            List<Instance> NewLI = new List<Instance>();

            return GetAllChildrenOfType_DoGetChildren(this, NewLI, ClassName);

        }

        private GetMultiInstanceResult GetAllChildrenOfType_DoGetChildren(Instance Parent, List<Instance> InstanceList, string ClassName)
        {
            GetMultiInstanceResult GIR = new GetMultiInstanceResult();

            foreach (Instance ThisChild in Children)
            {
                if (ThisChild.ClassName == ClassName
                    && ThisChild.GetType().Name == ClassName)
                {
                    InstanceList.Add(ThisChild);

                    if (ThisChild.Children.Count > 0)
                    {
                        GetAllChildrenOfType_DoGetChildren(Parent, InstanceList, ClassName);
                    }
                }
            }

            GIR.Successful = true;
            GIR.InstanceList = InstanceList;
            return GIR; 
        }

        public virtual void OnCreate()
        {
            return; 
        }
    }
}
