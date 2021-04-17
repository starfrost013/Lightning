using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// An enum value
    /// </summary>
    public class EnumValue : SerialisableObject
    {
        internal override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Instantiable | InstanceTags.Destroyable;
        internal override string ClassName => "";
        
        // Name uses the actual Instance name. 
        public int Id { get; set; }
        public object Value { get; set; }
    }
}
