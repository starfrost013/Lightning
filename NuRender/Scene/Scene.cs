﻿using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Scene
    /// 
    /// August 6, 2021 (modified December 10, 2021)
    /// 
    /// Defines a NuRender scene.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// The list of windows in this Scene. 
        /// </summary>
        public WindowCollection Windows { get; set; } 
        
        /// <summary>
        /// Scene constructor.
        /// </summary>
        public Scene()
        {
            Windows = new WindowCollection();
        }

        public void AddWindow(WindowSettings WS) => Windows.Add(WS);

        public Window GetMainWindow() => Windows.GetMainWindow();

        /// <summary>
        /// NuRender (single-threaded) MAIN method. 
        /// 
        /// CURRENTLY HAS TO BE PUBLICLY CALLED
        /// </summary>
        public void Render()
        {
            foreach (Window Win in Windows) Win.Main();
        }
       
        
    }
}
