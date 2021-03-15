using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Dynamic Datamodel Serialiser (DDMS) 
    /// 
    /// 2021-03-15
    /// 
    /// Valid components for a DDMS-compliant file.
    /// </summary>
    public enum DDMSComponents
    {
        /// <summary>
        /// Component 0 - Metadata
        /// 
        /// Holds 
        /// </summary>
        Metadata = 0,

        Settings = 1,

        InstanceTree = 2,
    }
}
