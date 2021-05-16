using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Polaris.Core
{
    /// <summary>
    /// TabUI
    /// 
    /// May 16, 2021
    /// 
    /// Defines a UI tab for Polaris. 
    /// </summary>
    public class Tab
    {
        [XmlElement("FriendlyName")]
        public string FriendlyName { get; set; }

        [XmlElement("Name")]
        /// <summary>
        /// The name of this UI tab.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The class name of the usercontrol used 
        /// </summary>
        [XmlElement("UserControlClassName")]
        public string UserControlClassName { get; set; }
    }
}
