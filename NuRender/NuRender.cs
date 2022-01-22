using NuCore.Utilities;
using NuRender.SDL2;
using System;

namespace NuRender
{  
    /// <summary>
    /// Lightning NuRender 
    /// 
    /// Development beginning: July 30, 2021
    /// 
    /// A new renderer for Lightning, powered by SDL2.
    /// </summary>
    public static partial class NuRender
    {

        public static bool NuRender_Init()
        {
            if (!ErrorManager.ERRORMANAGER_LOADED) ErrorManager.Init();
            Logging.Log("NuRender", ClassName);
            Logging.Log("© 2022 starfrost\n", ClassName); // two lines
            Logging.Log("Version information:", ClassName);
            Logging.Log($"NuRender API {NURENDER_API_VERSION_MAJOR}.{NURENDER_API_VERSION_MINOR}.{NURENDER_API_VERSION_REVISION}\n", ClassName);
            Logging.Log($"SDL2 {NURENDER_SDL_API_VERSION_MAJOR}.{NURENDER_SDL_API_VERSION_MINOR}.{NURENDER_SDL_API_VERSION_REVISION}", ClassName);
            Logging.Log($"SDL2_image {NURENDER_SDL_IMAGE_API_VERSION_MAJOR}.{NURENDER_SDL_IMAGE_API_VERSION_MINOR}.{NURENDER_SDL_IMAGE_API_VERSION_REVISION}", ClassName);
            Logging.Log($"SDL2_mixer {NURENDER_SDL_MIXER_API_VERSION_MAJOR}.{NURENDER_SDL_MIXER_API_VERSION_MINOR}.{NURENDER_SDL_MIXER_API_VERSION_REVISION}", ClassName);
            Logging.Log($"SDL2_ttf {NURENDER_SDL_TTF_API_VERSION_MAJOR}.{NURENDER_SDL_TTF_API_VERSION_MINOR}.{NURENDER_SDL_TTF_API_VERSION_REVISION}", ClassName);
            Logging.Log($"SDL2_gfx {NURENDER_SDL_GFX_API_VERSION_MAJOR}.{NURENDER_SDL_GFX_API_VERSION_MINOR}.{NURENDER_SDL_GFX_API_VERSION_REVISION}\n", ClassName);

            Logging.Log("Initialising SDL2...", ClassName);

            if (SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_EVERYTHING) < 0)
            {
                ErrorManager.ThrowError(ClassName, "NRCannotInitialiseSDL2Exception", $"A fatal error occurred during initialisation of SDL2: {SDL.SDL_GetError()}");

                return false;  
            }
            else
            {
                // That was successful. Initialise SDL2_image.
                Logging.Log("Initialising SDL2_image...", ClassName);
                if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_EVERYTHING) < 0)
                {
                    ErrorManager.ThrowError(ClassName, "NRCannotInitialiseSDL2ImageException", $"A fatal error occurred during initialisation of SDL2_image: {SDL.SDL_GetError()}");

                    //todo: error
                    return false; 
                }
                else
                {
                    // That was successful. Initialise SDL2_mixer.
                    Logging.Log("Initialising SDL2_mixer...", ClassName);

                    if (SDL_mixer.Mix_Init(SDL_mixer.MIX_InitFlags.MIX_INIT_EVERYTHING) < 0)
                    {
                        ErrorManager.ThrowError(ClassName, "NRCannotInitialiseSDL2MixerException", $"A fatal error occurred during initialisation of SDL2_mixer: {SDL.SDL_GetError()}");
                        return false; 
                    }
                    else
                    {
                        // That was successful. Initialise SDL2_ttf.
                        Logging.Log("Initialising SDL2_ttf...", ClassName);

                        if (SDL_ttf.TTF_Init() < 0)
                        {
                            ErrorManager.ThrowError(ClassName, "NRCannotInitialiseSDL2TtfException", $"A fatal error occurred during initialisation of SDL2_ttf: {SDL.SDL_GetError()}");
                            return false;
                        }
                        else
                        {
                            return true; 
                        }
                    }
                }
            }
        }
    }
}
