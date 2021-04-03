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
