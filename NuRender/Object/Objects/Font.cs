﻿using NuCore.Utilities;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public class Font : NRObject
    {


        /// <summary>
        /// Font path to load the font from. Default = C:\Windows\Fonts
        /// </summary>
        public string FontPath { get; set; }

        /// <summary>
        /// Pointer to unmanaged SDL_Font structure
        /// </summary>
        internal IntPtr Pointer { get; private set; }

        /// <summary>
        /// Determines if this font is loaded.
        /// </summary>
        public bool Loaded { get; set; }

        /// <summary>
        /// Bcking field for <see cref="Size"/>.
        /// </summary>
        private int _size { get; set; }

        /// <summary>
        /// The size of this Font.
        /// </summary>
        public int Size 
        { 
            get
            {
                if (_size <= 0 || _size >= 1539)
                {
                    return 18; // ignore invalid defualt font sizes
                }
                else
                {
                    return _size;
                }
                
            }
            set
            {
                if (value <= 0 || value >= 1539) // invalid font size
                {
                    _size = 18;
                }
                else
                {
                    _size = value; 
                }
            }   

        
        }
        private string FullFontPath
        {
            get
            {
                if (FontPath != null)
                {
                    return $@"{FontPath}\{Name}.ttf";
                }
                else
                {
#if WINDOWS
                    string SysRoot = Environment.GetEnvironmentVariable("SystemRoot");

                    return $"{SysRoot}\\Fonts\\{Name}.ttf";
#else
                    ErrorManager.Throw(ClassName, "NROnlyImplementedInWindowsException", "System font loading is presently only implemented in Windows builds of Lightning/NuRender!");

#endif

                }
            }
        }

        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            if (!Loaded) Load(); 
        }

        public void Load()
        {
            Logging.Log("Loading font...", ClassName);

            if (Size == 0) // Dec 18, 2021 (default size 18)
            {
                Pointer = SDL_ttf.TTF_OpenFont(FullFontPath, 18);
            }
            else
            {
                Pointer = SDL_ttf.TTF_OpenFont(FullFontPath, Size);
            }

            if (Pointer == null)
            {
                ErrorManager.ThrowError(ClassName, "NRCannotLoadFontException", $"Fatal error occurred loading font: {SDL.SDL_GetError()}");
                return; 
            }

            Loaded = true; 
        }

        public void Unload()
        {
            if (Pointer != null) SDL_ttf.TTF_CloseFont(Pointer);
        }

        public override void Render(WindowRenderingInformation RenderingInformation) 
        {
            return; 
        }

    }
}
