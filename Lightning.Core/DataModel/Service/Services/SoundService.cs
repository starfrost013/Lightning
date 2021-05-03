using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class SoundService : Service
    {
        internal override string ClassName => "SoundService";
        internal override ServiceImportance Importance => ServiceImportance.High;

        public override ServiceStartResult OnStart()
        {
            ServiceStartResult SSR = new ServiceStartResult();

            int SDLMixerResult = SDL_mixer.Mix_Init(SDL_mixer.MIX_InitFlags.MIX_INIT_OGG | SDL_mixer.MIX_InitFlags.MIX_INIT_MP3 | SDL_mixer.MIX_InitFlags.MIX_INIT_FLAC);

            if (SDLMixerResult < 0)
            {
                SSR.FailureReason = $"SDL_mixer failed to initialise: {SDL.SDL_GetError()}";
                return SSR;
            }
            else
            {
                SSR.Successful = true;
                return SSR; 
            }
        }

        public override ServiceShutdownResult OnShutdown()
        {
            ServiceShutdownResult SSR = new ServiceShutdownResult();

            SDL_mixer.Mix_Quit();

            SSR.Successful = true;
            return SSR; 
        }

        public override void Poll()
        {
            return; 
        }
    }
}
