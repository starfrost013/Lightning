using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DebugPage (Lightning IGD Services)
    /// 
    /// August 20, 2021
    /// 
    /// Defines the root class for debug pages
    /// </summary>
    public class DebugPage : PhysicalObject
    {
        internal override string ClassName => "DebugPage";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;
        /// <summary>
        /// The header of this debug page.
        /// </summary>
        public string Header { get; set; }
    }
}
