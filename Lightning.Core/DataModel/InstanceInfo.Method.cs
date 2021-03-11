using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Determines a method
    /// </summary>
    public class InstanceInfoMethod : Instance
    {
        public override string ClassName => "InstanceInfoMethod";
        public override InstanceTags Attributes => InstanceTags.Instantiable;
        public string MethodName { get; set; }
        public InstanceInfoProperty Property { get; set; }
        public List<InstanceInfoMethodParameter> Parameters { get; set; }

        public static InstanceInfoMethod FromMethodInfo { get; set; }
    }
}
