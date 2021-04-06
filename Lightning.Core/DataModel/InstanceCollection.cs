using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Core
{
    public class InstanceCollection : IEnumerable
    {
        public List<Instance> Instances { get; set; }

        public int Count => Instances.Count; 

        public InstanceCollection()
        {
            Instances = new List<Instance>(); 
        }

        public InstanceCollection(List<Instance> Instances)
        {
            Instances = new List<Instance>(); 

            foreach (Instance Inx in Instances)
            {
                Instances.Add(Inx); 
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public InstanceCollectionEnumerator GetEnumerator()
        {
            return new InstanceCollectionEnumerator(Instances);
        }

        /// <summary>
        /// Adds the object <paramref name="Obj"/> to the children of this Object. 
        ///
        /// This transparently works with any level in the hierarchy, hopefully.
        /// 
        /// April 6, 2021
        /// </summary>
        /// <param name="Obj"></param>
        public void Add(object Obj)
        {
            // Get the types of the object and its parent.
            Type ObjType = Obj.GetType();

            if (ObjType.IsSubclassOf(typeof(Instance)))
            {
                Instance TestInstance = (Instance)Obj;

                Instance TestInstanceParent = TestInstance.GetParent();

                // Check if tbis Instance has a parent. 
                if (TestInstanceParent == null)
                {
                    // this Instance is part of the DataModel Root
                    if (TestInstance.Attributes.HasFlag(InstanceTags.ParentCanBeNull))
                    {
                        Add_PerformAdd(Obj);
                    }
                    else
                    {

                        GetInstanceResult WorkSvc = DataModel.GetFirstChildOfType("Workspace");
                        
                        if (!WorkSvc.Successful)
                        {
                            Debug.Assert(WorkSvc.FailureReason != null);

                        }
                        else
                        {
                            // Get the current workspace.

                            Workspace TheWorkspace = (Workspace)WorkSvc.Instance;

                            Add_PerformAdd(Obj, TheWorkspace);

                        }
                        // throw error
                        ErrorManager.ThrowError("DataModel", "AttemptedToAddInstanceToDataModelRootWithoutParentCanBeNullAttributeSetException", $"The Instance of type {ObjType.Name}");
                    }
                    
                }
                else // If it has a parent, add it to the parent. 
                {
                    Type ParentType = this.GetType();

                    // Instance children must be the same or child classes
                    if (ObjType == ParentType)
                    {
                        Add_PerformAdd(Obj, TestInstanceParent);
                    }
                    else
                    {
                        if (ObjType.IsSubclassOf(ParentType))
                        {
                            Add_PerformAdd(Obj, TestInstanceParent);
                        }
                        else
                        {

                            ErrorManager.ThrowError("DataModel", "CannotAddThatInstanceAsChildException", $"{ObjType.Name} cannot be a child of {ParentType.Name}!");

                        }


                    }
                }
            }
            else
            {
                // throw an error
                ErrorManager.ThrowError("DataModel", "ThatIsNotAnInstancePleaseDoNotTryToAddItToTheDataModelException", $"Attempted to add an object of class {ObjType.Name} to the DataModel when it does not inherit from Instance!");
            }

        }

        public void Add_PerformAdd(object Obj, Instance Parent = null)
        {
            // polymorphism mandates this being the instance we want.

            if (Parent == null)
            {
                Instances.Add((Instance)Obj);
            }
            else
            {
                Parent.Children.Instances.Add((Instance)Obj);
            }
        }

        /// <summary>
        /// Clear the InstanceCollection.
        /// </summary>
        public void Clear() => Instances.Clear();

        /// <summary>
        /// Gets the first child of this Instance with ClassName <see cref="ClassName"/>
        /// </summary>
        /// <returns>A <see cref="GetInstanceResult"/> object. The Instance is <see cref="GetInstanceResult.Instance"/>.</returns>
        public GetInstanceResult GetFirstChildOfType(string ClassName)
        {
            GetInstanceResult GIR = new GetInstanceResult();

            foreach (Instance Child in Instances)
            {
                if (Child.ClassName == ClassName)
                {
                    GIR.Instance = Child;
                    GIR.Successful = true;
                    return GIR;
                }
            }

            GIR.FailureReason = $"This instance does not have a child of ClassName {ClassName}!";

            return GIR;
        }

        /// <summary>
        /// Gets the last chil of this Instance with Class Name <see cref="ClassName"/>
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public GetInstanceResult GetLastChildOfType(string ClassName)
        {
            GetInstanceResult GIR = new GetInstanceResult();

            // create a list that we will use for the discovery of these instances. 
            List<Instance> MatchingInstances = new List<Instance>();

            foreach (Instance Child in Instances)
            {
                if (Child.ClassName == ClassName)
                {
                    MatchingInstances.Add(Child);
                }
            }

            // if we do not find any instances...
            if (MatchingInstances.Count == 0)
            {
                GIR.FailureReason = $"This instance does not have a child of ClassName {ClassName}!";
                return GIR;
            }
            else
            {
                GIR.Successful = true;
                GIR.Instance = MatchingInstances[MatchingInstances.Count - 1];
                return GIR;
            }
        }

    }

    public class InstanceCollectionEnumerator : IEnumerator
    {
        public List<Instance> Instances { get; set; }
        public Instance Current { get
            {
                try
                {
                    return Instances[Position];
                }
                catch (IndexOutOfRangeException)
                {
                    ErrorManager.ThrowError("DataModel", "AttemptedToAcquireInvalidInstanceException");
                    return null;
                }
            } 

        }

        object IEnumerator.Current
        {
            get
            {
                return (object)Current;
            }
        }

          
        private int Position = -1;

        public void Reset() => Position = -1;

        public bool MoveNext()
        {
            Position++;
            return (Position < Instances.Count);
        }

        public InstanceCollectionEnumerator(List<Instance> NewInstances) => Instances = NewInstances;

    }
}
