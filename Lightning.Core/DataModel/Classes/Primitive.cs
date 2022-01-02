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
    /// April 12, 2021 (modified December 11, 2021: Add NotAntialiased)
    /// 
    /// A shared class for all primitive shapes. 
    /// </summary>
    public class Primitive : PhysicalObject
    {
        internal override string ClassName => "Primitive";

        /// <summary>
        /// If true, this object will be bordered.
        /// </summary>
        public bool Bordered { get; set; }

        /// <summary>
        /// Determines the border size of this object.
        /// </summary>
        public Vector2 BorderSize { get; set; }

        /// <summary>
        /// Is this Primitive filled?
        /// </summary>
        public bool Fill { get; set; }


        /// <summary>
        /// If true, this object will not be antialiased.
        /// </summary>
        public bool NotAntialiased { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Scene SDL_Renderer, ImageBrush Tx) => base.Render(SDL_Renderer, Tx);

    }
}
