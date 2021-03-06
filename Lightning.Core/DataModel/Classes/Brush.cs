using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{

    /// <summary>
    /// Brush
    /// 
    /// August 1, 2021 (modified August 5, 2021)
    /// 
    /// Defines a brush used for painting a PhysicalInstance.
    /// </summary>
    public abstract class Brush : PhysicalInstance
    {
        internal override string ClassName => "Brush";

        internal override InstanceTags Attributes => base.Attributes | InstanceTags.ParentCanBeNull;

        /// <summary>
        /// Determines if the brush has been initialised.
        /// </summary>
        internal bool BRUSH_INITIALISED { get; set; }

    }
}
