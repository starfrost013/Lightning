using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// WindowRenderingInformation
    /// 
    /// September 19, 2021
    /// 
    /// Defines window rendering information.
    /// </summary>
    public class WindowRenderingInformation 
    {
        /// <summary>
        /// The unmanaged memory pointer to the SDL window.
        /// </summary>
        public IntPtr WindowPtr { get; internal set; }

        /// <summary>
        /// The unmanaged memory pointer to the SDL renderer.
        /// </summary>
        public IntPtr RendererPtr { get; internal set; }

        /// <summary>
        /// Fonts that have been loaded.
        /// </summary>
        public List<Font> Fonts { get; set; } //TEMP; TODO: FONTCOLLECTION

        /// <summary>
        /// Cache used for loading images and rendering them faster.
        /// </summary>
        public List<Image> ImageCache { get; set; } 

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
        }
    }
}
