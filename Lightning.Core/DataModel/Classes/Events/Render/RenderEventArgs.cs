using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{

    /// <summary>
    /// RenderEventArgs
    /// 
    /// August 16, 2021 (modified December 11, 2021: NuRender) 
    /// 
    /// Defines event arguments for the <see cref="RenderEvent"/> event. 
    /// </summary>
    public class RenderEventArgs
    {
        /// <summary>
        /// The current NuRender scene.
        /// </summary>
        public Scene SDL_Renderer { get; set; }

        /// <summary>
        /// The <see cref="ImageBrush"/> that is used for rendering the texture of this element. <c>null</c> if this PhysicalObject has no texture.
        /// </summary>
        public ImageBrush Tx { get; set; }
    }
}
