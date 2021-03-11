using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class InstanceInfoMethodParameter : Instance
    {
        public Type Type { get; set; }
        public string ParamName { get; set; }

        public void FromMethodInfo()
        {

        }
    }
}
