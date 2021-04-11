using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// PhysicalObject
    /// 
    /// April 9, 2021 (modified April 11, 2021)
    /// 
    /// Defines a physically rendered object in Lightning, with a Position, Size, and a Texture (stored as a logical child). Rendered every frame by RenderService.
    /// </summary>
    public class PhysicalObject : SerialisableObject
    {
        /// <summary>
        /// <inheritdoc/> -- set to PhysicalObject.
        /// </summary>
        public override string ClassName => "PhysicalObject";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override InstanceTags Attributes => base.Attributes;

        /// <summary>
        /// The position of this object in the world. 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The size of this object.
        /// </summary>
        public Vector2 Size { get; set; }


        // Texture is a child. 

    }
}
