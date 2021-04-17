using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{

    /// <summary>
    /// SerialisableObject
    /// 
    /// March 5, 2021 (modified April 16, 2021)
    /// 
    /// Signifies an object that is serialisable using DDMS.
    /// </summary>
    public class SerialisableObject : Instance
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "SerialisableObject";
        public static string SchemaName { get; set; }
    }
}
