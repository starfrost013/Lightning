using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// InstanceInfo class.
    /// 
    /// Holds information about an instance - its methods, properties, and their metadata. Used for the IDE. ReflectionMetadata?
    /// 
    /// Converted from System.Reflection types at boot.
    /// 
    /// Translated to and from .NET System.Reflection types as required for the IDE and the datamodel serialiser. 
    /// </summary>
    public class InstanceInfo
    {
        public List<InstanceInfoMethod> Methods { get; set; }
        public List<InstanceInfoProperty> Properties { get; set; }

        public InstanceInfo()
        {
            Methods = new List<InstanceInfoMethod>();
        }
    }
}
