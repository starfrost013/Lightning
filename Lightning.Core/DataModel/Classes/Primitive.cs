using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
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
        /// <summary>
        /// The colour of this Primitive.
        /// </summary>
        public Color4 Colour { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public new void Render(IntPtr SDL_Renderer, Texture Tx) => base.Render(SDL_Renderer, Tx);

    }
}
