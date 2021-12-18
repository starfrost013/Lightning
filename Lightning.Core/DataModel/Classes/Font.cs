﻿using NuCore.Utilities;
using NuRender;
using NuRender.SDL2; 
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

        private NuRender.Font NRFont { get; set; }

        /// <summary>
        /// Loads this font.
        /// </summary>
        /// <param name="FontFamily">The name of the font family to laod.</param>
        public void Load(Scene SDL_Renderer) // change to result class?
        {
            base.PO_Init();

            Window MainWindow = SDL_Renderer.GetMainWindow();

            // fonts are special, you see
            NRFont = new NuRender.Font();

            NRFont.Name = Name;
            NRFont.FontPath = FontPath;
            NRFont.Size = FontSize;
            NRFont.Load();

            MainWindow.Settings.RenderingInformation.Fonts.Add(NRFont);
        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            return; // prevent crash from calling the wrong method
        }

        /// <summary>
        /// Unloads this font
        /// </summary>
        public void Unload()
        {
            NRFont.Unload
        }
    }
}
