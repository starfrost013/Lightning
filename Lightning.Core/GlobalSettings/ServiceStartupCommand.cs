using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lightning.Core
{
    /// <summary>
    /// Non-DataModel (Instance Core)
    /// 
    /// ServiceStartupCommand
    /// 
    /// 2021-04-03
    /// 
    /// Holds information for instructing the SCM to start a service at system boot time. 
    /// </summary>
    public class ServiceStartupCommand
    {
        [XmlElement("StartOrder")]
        /// <summary>
        /// The start order of this service.
        /// </summary>
        public int StartOrder { get; set; }

        [XmlElement("ServiceName")]
        public string ServiceName { get; set; }
    }
}
