using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Determines a method
    /// </summary>
    public class InstanceInfoMethod 
    {
        public string MethodName { get; set; }
        public InstanceInfoProperty Property { get; set; }
        public List<InstanceInfoMethodParameter> Parameters { get; set; }
        public static InstanceInfoMethod FromMethodInfo { get; set; }

        public InstanceInfoMethod()
        {
            Parameters = new List<InstanceInfoMethodParameter>();
        }
    }
}
