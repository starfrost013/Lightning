using Lightning.Core.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Font
    /// 
    /// June 30, 2021
    ///  
    /// Defines a font
    /// </summary>
    public class Font : PhysicalObject
    {

        internal override string ClassName => "Font";

        /// <summary>
        /// Path to the font file of this font
        /// </summary>
        public string FontPath { get; set; }

        /// <summary>
        /// Size of the font to be loaded
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Unmanaged ptr to SDL2_ttf font structure
        /// </summary>
        internal IntPtr FontPointer { get; set; }


        internal bool FONT_LOADED { get; set; }

        /// <summary>
        /// Loads this font.
        /// </summary>
        /// <param name="FontFamily">The name of the font family to laod.</param>
        public void Load() // change to result class?
        {
            base.Init(); 

            if (FontPath == null
                || FontPath.Length == 0)
            {
                ErrorManager.ThrowError(ClassName, "NullOrZeroLengthFileFontNameException");
                return; 
            }
            else
            {
                if (FontSize == 0)
                {
                    ErrorManager.ThrowError(ClassName, "InvalidFontSizeException");
                    return;
                }
                else
                {
                    FontPointer = SDL_ttf.TTF_OpenFont($"{FontPath}", FontSize);

                    if (FontPointer == IntPtr.Zero)
                    {
                        ErrorManager.ThrowError(ClassName, "FailedToLoadFontException", $"Failed to load font: {SDL.SDL_GetError()}");
                        return;
                    }
                    else
                    {
                        FONT_LOADED = true;
                        return; 
                    }
                }
            }


        }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            return; // prevent crash from calling the wrong method
        }

        /// <summary>
        /// Unloads this font
        /// </summary>
        public void Unload()
        {
            if (FontPointer != IntPtr.Zero)
            {
                SDL_ttf.TTF_CloseFont(FontPointer);
            }
           
        }
    }
}
