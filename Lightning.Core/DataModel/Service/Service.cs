using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning [DataModel API]
    /// 
    /// Service Root Class
    /// 
    /// Provides the root class for a service in the Lightning game engine.
    /// 
    /// A service is an instance that is running at all times and can be called on
    /// by any component of the DataModel current state at any time.
    /// 
    /// It can also be called from scripts using ESX2 GetService() method.
    /// </summary>
    public abstract class Service : Instance
    {
        /// <summary>
        /// The name of this service.
        /// </summary>
        public abstract string SName { get; }
        public bool RunningNow { get; set; }

        public abstract ServiceStartResult OnStart();
        public abstract ServiceShutdownResult OnShutdown();

        
    }
}
