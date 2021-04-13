using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Camera
    /// 
    /// April 13, 2021
    /// 
    /// Defines a Camera. A Camera is the viewport of a Lightning level. 
    /// </summary>
    public class Camera : PhysicalObject
    {
        /// <summary>
        /// Is this Camera active?
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (Active)
            {
                SDL_Renderer.CCameraPosition = new Vector2(Position.X, Position.Y);
            }
            else
            {
                return; 
            }
        }

    }
}
