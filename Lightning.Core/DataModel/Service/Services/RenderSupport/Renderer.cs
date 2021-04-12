﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
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

        public Renderer()
        {
            TextureCache = new List<Texture>(); 
        }
    }
}