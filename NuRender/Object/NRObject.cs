using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// NRObject
    /// 
    /// August 27, 2021 (modified December 5, 2021)
    /// 
    /// Defines a NuRender object.
    /// </summary>
    public abstract class NRObject
    {
        /// <summary>
        /// Fake ClassName for code style compatibility and logging
        /// </summary>
        public virtual string ClassName { get; }

        /// <summary>
        /// The name of this NRObject.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Event handler for the <see cref="NRSDLExitEvent"/> event.
        /// </summary>
        public NRSDLExitEvent OnExit { get; set; }

        /// <summary>
        /// The colour of this NuRender object. May not be used.
        /// </summary>
        public Color4Internal Colour { get; set; }

        /// <summary>
        /// The position of this NuRender object. 
        /// </summary>
        public Vector2Internal Position { get; set; }

        /// <summary>
        /// Ran on the creation of this NuRender object.
        /// </summary>
        public abstract void Start(WindowRenderingInformation RenderingInformation);

        /// <summary>
        /// Ran each time this NuRender object is rendered.
        /// </summary>
        /// <param name="RenderInfo"></param>
        public abstract void Render(WindowRenderingInformation RenderInfo);

        public NRObject() 
        {
            Colour = new Color4Internal();
            Position = new Vector2Internal();

        }
    }
}
