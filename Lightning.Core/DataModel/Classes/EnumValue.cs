using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// An enum value
    /// </summary>
    public class EnumValue : SerialisableObject
    {
        public override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Instantiable | InstanceTags.Destroyable;
        public override string ClassName => "";
        
        // Name uses the actual Instance name. 
        public int Id { get; set; }
        public object Value { get; set; }
    }
}
