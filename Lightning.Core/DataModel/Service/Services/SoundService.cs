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

        public bool SOUNDS_LOADED { get; set; }
        public override ServiceStartResult OnStart()
        {
            ServiceStartResult SSR = new ServiceStartResult();

            Logging.Log("Initialising SDL2_mixer", ClassName);

            // Init SDL_mixer.
            int SDLMixerResult = SDL_mixer.Mix_Init(SDL_mixer.MIX_InitFlags.MIX_INIT_OGG | SDL_mixer.MIX_InitFlags.MIX_INIT_MP3 | SDL_mixer.MIX_InitFlags.MIX_INIT_FLAC);

            if (SDLMixerResult < 0)
            {
                SSR.FailureReason = $"SDL_mixer failed to initialise: {SDL.SDL_GetError()}";
                return SSR;
            }
            else
            {
                Logging.Log("Initialising Audio", ClassName);

                // Init audio. CD quality and stereo. We can go down to 22khz if necessary
                int SDLDriverMixResult = SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.Mix_AudioFormat.MIX_DEFAULT_FORMAT, 2, 2048); // 2048 bytes should be okay, possibly reduce to 1024

                if (SDLDriverMixResult < 0)
                {
                    SSR.FailureReason = $"SDL_mixer failed to initialise audio services: {SDL.SDL_GetError()}";
                    return SSR;
                }
                else
                {
                    SSR.Successful = true;
                    return SSR;
                }

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
            // Load audio. 

            if (!SOUNDS_LOADED)
            {
                Workspace Ws = DataModel.GetWorkspace();
                GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("Sound");

                if (GMIR.Successful
                    && GMIR.InstanceList != null)
                {
                    List<Instance> IX = GMIR.InstanceList;

                    foreach (Instance Ins in IX)
                    {
                        Sound Snd = (Sound)Ins;
                        Snd.SoundPtr = SDL_mixer.Mix_LoadWAV(Snd.Path);

                        if (Snd.SoundPtr)
                        {

                        }
                    }
                }
                else
                {

                }
            }

            return; 
        }
    }
}
