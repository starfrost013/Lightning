using Lightning.Core;
using NuCore.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Polaris.Core
{
    /// <summary>
    /// TabCollection
    /// 
    /// May 16, 2021
    /// 
    /// Defines a collection of tabs.
    /// </summary>
    
    [XmlRoot("Tabs")]
    public class TabCollection : IEnumerable
    {
        [XmlElement("Tab")]
        public List<Tab> Tabs { get; set; }

        // may need to be moved
        public static string POLARIS_TAB_COLLECTION_NAMESPACE_PATH = "Polaris.UI";

        public TabCollection()
        {
            Tabs = new List<Tab>();
        }

        public TabCollection(List<Tab> TabList)
        {
            Tabs = new List<Tab>();
            foreach (Tab Tab in TabList)
            {
                Tabs.Add(Tab);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)GetEnumerator();

        public TabCollectionEnumerator GetEnumerator()=> new TabCollectionEnumerator(Tabs);

        public void Add(object Obj)
        {
            if (Obj.GetType() != typeof(Tab))
            {
                ErrorManager.ThrowError("TabCollection", "PolarisCannotAddNonTabToTabCollectionException");
                return;
            }
            else
            {
                Tabs.Add((Tab)Obj);
            }
        }

    }

    /// <summary>
    /// TabCollectionEnumerator
    /// 
    /// May 16, 2021
    /// 
    /// Defines the enumerator for a collection of tabs.
    /// </summary>
    public class TabCollectionEnumerator : IEnumerator
    {

        public TabCollectionEnumerator(List<Tab> TabList)
        {
            Tabs = new List<Tab>();

            foreach (Tab Tab in TabList)
            {
                Tabs.Add(Tab);
            }
        }

        public List<Tab> Tabs { get; set; }

        public int Position = -1;

        public bool MoveNext()
        {
            Position++;
            return (Position < Tabs.Count);
        }

        public void Reset() => Position = -1; 

        object IEnumerator.Current
        {
            get
            {
                return Current; 
            }
        }

        public Tab Current
        {
            get
            {
                try
                {
                    return Tabs[Position];
                }
                catch (IndexOutOfRangeException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Polaris", "PolarisTabCollectionEnumeratorOverflowException", $"Overflow when attempting to access Tab - value is {Current} when it must be between -1 and {Tabs.Count - 1}!", err);
#else
                    ErrorManager.ThrowError("Polaris", "PolarisTabCollectionEnumeratorOverflowException", $"Overflow when attempting to access Tab - value is {Current} when it must be between -1 and {Tabs.Count - 1}!");
#endif

                    return null; // will not run
                }
            }
            set
            {
                throw new InvalidOperationException("WHAT THE FUCK");
            }
        }
    }
}
