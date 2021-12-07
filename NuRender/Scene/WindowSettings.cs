using NuCore.Utilities;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// SceneSettings
    /// 
    /// August 17, 2021
    /// 
    /// Define scene settings.
    /// </summary>
    public class WindowSettings
    {
        /// <summary>
        /// Internal: The Window ID of this scene.
        /// </summary>
        internal long WindowID { get; set; }

        /// <summary>
        /// The name of this NuRender application. Will be used as the window title! 
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// The default position that this window is
        /// </summary>
        public Vector2 WindowPosition { get; set; }

        /// <summary>
        /// The window size of the current scene
        /// </summary>
        public Vector2 WindowSize { get; set; }

        /// <summary>
        /// The size of the current screen's viewport
        /// </summary>
        public Vector2 Viewport { get; set; }

        /// <summary>
        /// The window flags of this window - see <see cref="SDL.SDL_WindowFlags"/.>. Optional.
        /// </summary>
        public SDL.SDL_WindowFlags WindowFlags { get; set; }

        /// <summary>
        /// The window mode of this window - see <see cref="WindowMode"/>.
        /// </summary>
        public WindowMode WindowMode { get; set; }

        /// <summary>
        /// The rendering information for this window.
        /// </summary>
        public WindowRenderingInformation RenderingInformation { get; set; }

        public WindowSettings()
        {
            WindowPosition = new Vector2(200, 200);
            WindowSize = new Vector2(960, 640); // set to default
            Viewport = WindowSize; // set default
            ApplicationName = "NuRender Window";
            RenderingInformation = new WindowRenderingInformation();
            WindowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;

            
            WindowID = 0; 

        }

    }
}
