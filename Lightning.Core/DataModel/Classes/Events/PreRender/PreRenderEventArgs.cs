using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{

    /// <summary>
    /// PreRenderEventArgs
    /// 
    /// January 9, 2022
    /// 
    /// Defines event arguments for the <see cref="RenderEvent"/> event. 
    /// </summary>
    public class PreRenderEventArgs
    {
        /// <summary>
        /// The current NuRender scene.
        /// </summary>
        public Scene SDL_Renderer { get; set; }
    }
}
