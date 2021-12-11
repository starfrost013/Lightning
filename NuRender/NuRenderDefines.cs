using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// NuRender [NuRenderDefines]
    /// 
    /// August 17, 2021 (modified December 11, 2021: v0.4.0 -> v0.4.1 - Add file not found check for texture loading)
    /// 
    /// Defines global defines for the NuRender API.
    /// </summary>
    public static partial class NuRender
    {
        public static int NURENDER_API_VERSION_MAJOR = 0;
        public static int NURENDER_API_VERSION_MINOR = 4;
        public static int NURENDER_API_VERSION_REVISION = 1;

        public static int NURENDER_SDL_API_VERSION_MAJOR = SDL.SDL_MAJOR_VERSION;
        public static int NURENDER_SDL_API_VERSION_MINOR = SDL.SDL_MINOR_VERSION;
        public static int NURENDER_SDL_API_VERSION_REVISION = SDL.SDL_PATCHLEVEL;

        public static int NURENDER_SDL_IMAGE_API_VERSION_MAJOR = SDL_image.SDL_IMAGE_MAJOR_VERSION;
        public static int NURENDER_SDL_IMAGE_API_VERSION_MINOR = SDL_image.SDL_IMAGE_MINOR_VERSION;
        public static int NURENDER_SDL_IMAGE_API_VERSION_REVISION = SDL_image.SDL_IMAGE_PATCHLEVEL;

        public static int NURENDER_SDL_MIXER_API_VERSION_MAJOR = SDL_mixer.SDL_MIXER_MAJOR_VERSION;
        public static int NURENDER_SDL_MIXER_API_VERSION_MINOR = SDL_mixer.SDL_MIXER_MINOR_VERSION;
        public static int NURENDER_SDL_MIXER_API_VERSION_REVISION = SDL_mixer.SDL_MIXER_PATCHLEVEL;

        public static int NURENDER_SDL_TTF_API_VERSION_MAJOR = SDL_ttf.SDL_TTF_MAJOR_VERSION;
        public static int NURENDER_SDL_TTF_API_VERSION_MINOR = SDL_ttf.SDL_TTF_MINOR_VERSION;
        public static int NURENDER_SDL_TTF_API_VERSION_REVISION = SDL_ttf.SDL_TTF_PATCHLEVEL;

        public static int NURENDER_SDL_GFX_API_VERSION_MAJOR = SDL_gfx.SDLGFX_VERSION_MAJOR;
        public static int NURENDER_SDL_GFX_API_VERSION_MINOR = SDL_gfx.SDLGFX_VERSION_MINOR;
        public static int NURENDER_SDL_GFX_API_VERSION_REVISION = SDL_gfx.SDLGFX_VERSION_REVISION;

        /// <summary>
        /// Fake class name to use for logging etc
        /// </summary>
        public static string ClassName = "NuRender";

        /// <summary>
        /// The namespace NuRender objects are located within. DO NOT CHANGE UNLESS YOU KNOW WHAT YOU ARE DOING!
        /// </summary>
        public static string NURENDER_NAMESPACE_PATH = "NuRender";

        /// <summary>
        /// The default ASIC draw colour for SDL.
        /// </summary>
        public static Color4Internal NURENDER_DEFAULT_SDL_DRAW_COLOUR = new Color4Internal(255, 0, 0, 0);
    }
}
