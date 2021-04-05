using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lightning.Core
{
    
    public class ServiceStartupCommandCollection : IEnumerable
    {
        [XmlElement("StartupService")]
        public List<ServiceStartupCommand> Commands { get; set; }

        public ServiceStartupCommandCollection(List<ServiceStartupCommand> NewCommands) // if we end up passing references to this a lot we are going to have to make this code worse :(
        {
            Commands = NewCommands; 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ServiceStartupCommandCollectionEnumerator GetEnumerator()
        {
            return new ServiceStartupCommandCollectionEnumerator(Commands);
        }
    }

    public class ServiceStartupCommandCollectionEnumerator : IEnumerator
    {
        public int Position = -1;
        public void Reset() => Position = -1; 

        public bool MoveNext()
        {
            Position++;
            return (Position < Commands.Count);
        }

        public List<ServiceStartupCommand> Commands { get; set; }
        
        object IEnumerator.Current
        {
            get
            {
                return (object)Current;
            }
        }

        public ServiceStartupCommand Current
        {
            get
            {
                return Commands[Position];
            }
            set
            {
                throw new InvalidOperationException(); // you can't do this
            }
        }

        public ServiceStartupCommandCollectionEnumerator(List<ServiceStartupCommand> NCommands)
        {
            Commands = NCommands;
        }

    }
}
