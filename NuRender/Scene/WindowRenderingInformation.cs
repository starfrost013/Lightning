using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// WindowRenderingInformation
    /// 
    /// September 19, 2021 (mmodified December 11, 2021: add BlendingMode)
    /// 
    /// Defines window rendering information.
    /// </summary>
    public class WindowRenderingInformation 
    {
        /// <summary>
        /// The window blending mode of this window.
        /// </summary>
        public SDL.SDL_BlendMode BlendingMode { get; set; }

        /// <summary>
        /// Lightning compatibility
        /// </summary>
        public Vector2Internal CCameraPosition { get; set; }

        /// <summary>
        /// Fonts that have been loaded.
        /// </summary>
        public List<Font> Fonts { get; set; } //TEMP; TODO: FONTCOLLECTION

        /// <summary>
        /// Cache used for loading images and rendering them faster.
        /// </summary>
        public List<Image> ImageCache { get; set; }

        /// <summary>
        /// The unmanaged memory pointer to the SDL renderer.
        /// </summary>
        public IntPtr RendererPtr { get; internal set; }

        /// <summary>
        /// The unmanaged memory pointer to the SDL window.
        /// </summary>
        public IntPtr WindowPtr { get; internal set; }



        #region temp - until fontcollection

        /// <summary>
        /// Gets the font with the name Name. 
        /// </summary>
        /// <param name="Name">The name of the font to acquire.</param>
        /// <returns>todo</returns>
        public Font GetFontWithName(string Name) 
        {
            foreach (Font Font in Fonts)
            {
                if (Font.Name == Name)
                {
                    return Font; 
                }
            }

            return null; 
        }
        

        #endregion 
        public WindowRenderingInformation()
        {
            Fonts = new List<Font>();
            CCameraPosition = new Vector2Internal();
            ImageCache = new List<Image>();
        }
    }
}
