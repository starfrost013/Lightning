using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{

    /// <summary>
    /// Lightning
    /// 
    /// Copyright © 2021 starfrost
    /// 
    /// Signifies an object that is serialisable using DDMS.
    /// </summary>
    public class SerialisableObject : Instance
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ClassName => "SerialisableObject";
        public static string SchemaName { get; set; }
    }
}
