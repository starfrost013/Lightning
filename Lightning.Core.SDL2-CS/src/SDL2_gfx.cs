using System;
using System.Collections.Generic;
using System.Text;

#region
/* Lightning SDL2 Wrapper
 * 
 * Version 3.0 (NuRender/Lightning) + SDL2_gfx
 * Copyright © 2021 starfrost
 * November 6, 2021
 * 
 * This software is based on the open-source SDL2# - C# Wrapper for SDL2 library.
 *
 * Copyright (c) 2013-2021 Ethan Lee.
 * Copyright © 2021 starfrost.
 * 
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Ethan "flibitijibibo" Lee <flibitijibibo@flibitijibibo.com>
 *
 */
#endregion
namespace Lightning.Core.SDL2
{
    public static partial class SDL_gfx
    {
        #region SDL2# Defines

#if X64

#if DEBUG
        public const string NativeLibName = "SDL2_gfx-v1.0.5-x64-debug.dll";
#else
        public const string NativeLibName = "SDL2_gfx-v1.0.5-x64.dll";
#endif


#elif X86
        public static string NativeLibName = "SDL2_gfx-v1.0.5-x86.dll";
#if DEBUG
        public const string NativeLibName = "SDL2_gfx-v1.0.5-x86-debug.dll";
#else
        public const string NativeLibName = "SDL2_gfx-v1.0.5.dll";
#endif

#elif ARM32
#if DEBUG
        public const string NativeLibName = "SDL2_gfx-v1.0.5-ARM32-debug.dll";
#else
        public const string NativeLibName = "SDL2_gfx-v1.0.5-ARM32.dll";
#endif

#elif ARM64
#if DEBUG
        public const string NativeLibName = "SDL2_gfx-v1.0.5-ARM64-debug.dll";
#else
        public const string NativeLibName = "SDL2_gfx-v1.0.5-ARM64.dll";
#endif
#endif
        #endregion

        #region SDL2_gfxVersion.h 
        // requires 1.0.5 (Lightning/NuRender ONLY)

        public const int SDLGFX_VERSION_MAJOR = 1;
        public const int SDLGFX_VERSION_MINOR = 0;
        public const int SDLGFX_VERSION_REVISION = 5;

        /// <summary>
        /// Returns the current version of SDL2_gfx. It is best to check this in your program.
        /// </summary>
        /// <returns>A <see cref="SDL.SDL_version"/> object containing the current version of SDL2_gfx.</returns>
        public static SDL.SDL_version SDLGFX_Version()
        {
            return new SDL.SDL_version()
            {
                major = SDLGFX_VERSION_MAJOR,
                minor = SDLGFX_VERSION_MINOR,
                patch = SDLGFX_VERSION_REVISION
            };
        }

        #endregion

        #region SDL2_gfxPrimitives.h

        //references to render etc usually DON'T have out keyword.

        /// <summary>
        /// Draw pixel in the currently set renderer color.
        /// 
        /// This is implemented in C# as it is a trivial function. 
        /// </summary>
        /// <param name="Renderer"> The renderer to draw the pixel to.</param>
        /// <param name="X">X (horizontal) coordinate of the pixel.</param>
        /// <param name="Y">Y (vertical) coordinate of the pixel.</param>
        /// <returns></returns>
        public static int pixel(IntPtr Renderer, int X, int Y) => SDL.SDL_RenderDrawPoint(Renderer, X, Y); //short?

        #endregion
    }
}
