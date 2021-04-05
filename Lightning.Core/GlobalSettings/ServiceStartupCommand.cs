using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Non-DataModel (Instance Core)
    /// 
    /// ServiceStartupCommand
    /// 
    /// 2021-04-03
    /// 
    /// Holds information for instructing the SCM to strt a service at system boot time. 
    /// </summary>
    public class ServiceStartupCommand
    {
        /// <summary>
        /// The start order of this service.
        /// </summary>
        public int StartOrder { get; set; }
        public string SvcName { get; set; }
    }
}
