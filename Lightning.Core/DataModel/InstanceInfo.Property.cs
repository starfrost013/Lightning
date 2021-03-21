using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class InstanceInfoProperty
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public InstanceSecurity Security { get; set; }

        /// <summary>
        /// The value of this object.
        /// </summary>
        public object Value { get; set; }

        // hack.
        public T GetValue<T>()
        {
            return (T)Value;
        }

    }
}
