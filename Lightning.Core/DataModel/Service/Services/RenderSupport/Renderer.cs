using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Renderer (Non-DataModel)
    /// 
    /// April 9, 2021
    /// 
    /// Holds information about the Renderer and the latest inforamtion that we are r
    /// </summary>
    public class Renderer
    {
        /// <summary>
        /// The current Camera position. 
        /// </summary>
        public Vector2 CCameraPosition { get; set; }

        /// <summary>
        /// The SDL window.
        /// </summary>
        public IntPtr Window { get; set; }

        /// <summary>
        /// The SDL renderer.
        /// </summary>
        public IntPtr SDLRenderer { get; set; }

        /// <summary>
        /// A cache of textures.
        /// </summary>
        public List<Texture> TextureCache { get; set; }

        /// <summary>
        /// The window size.
        /// </summary>
        public Vector2 WindowSize { get; set; }

        public Renderer()
        {
            TextureCache = new List<Texture>();
            // not added to the datamodel
            CCameraPosition = new Vector2();
        }
    }
}
