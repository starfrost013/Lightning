using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class InstanceCollection : IEnumerable
    {
        public List<Instance> Instances { get; set; }

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

        public void Add(object Obj)
        {
            // Get the types of this and the object.
            Type ObjType = Obj.GetType();
            Type ThisType = this.GetType();

            // Instance children must be the same or child classes
            if (ObjType == ThisType)
            {
                Add_PerformAdd(Obj);
            }
            else
            {
                if (ObjType.IsSubclassOf(ThisType))
                {
                    Add_PerformAdd(Obj);
                }
                else
                {
                    ErrorManager.ThrowError("DataModel", "CannotAddThatInstanceAsChildException", $"{ObjType} cannot be a child of {ThisType}!");
                }
            }
        }

        public void Add_PerformAdd(object Obj)
        {
            // polymorphism mandates this being the instance we want.
            Instances.Add((Instance)Obj); 
        }

        /// <summary>
        /// Clear the InstanceCollection.
        /// </summary>
        public void Clear() => Instances.Clear();
        
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
