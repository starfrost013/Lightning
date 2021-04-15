using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Core
{
    public class InstanceInfoProperty
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public InstanceAccessibility Accessibility { get; set; }

    }
}
