using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Primitive
    /// 
    /// April 12, 2021
    /// 
    /// A shared class for all primitive shapes. 
    /// </summary>
    public class Primitive : PhysicalObject
    {
        internal override string ClassName => "Primitive";

        /// <summary>
        /// Is this Primitive filled?
        /// </summary>
        public bool Fill { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Scene SDL_Renderer, ImageBrush Tx) => base.Render(SDL_Renderer, Tx);

    }
}
