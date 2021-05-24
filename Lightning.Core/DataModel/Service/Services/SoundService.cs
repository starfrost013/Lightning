using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.IO; 
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

            Logging.Log("Initialising SDL2_mixer...", ClassName);

            // Init SDL_mixer.
            int SDLMixerResult = SDL_mixer.Mix_Init(SDL_mixer.MIX_InitFlags.MIX_INIT_OGG | SDL_mixer.MIX_InitFlags.MIX_INIT_MP3 | SDL_mixer.MIX_InitFlags.MIX_INIT_FLAC | SDL_mixer.MIX_InitFlags.MIX_INIT_MOD | SDL_mixer.MIX_InitFlags.MIX_INIT_OPUS | SDL_mixer.MIX_InitFlags.MIX_INIT_MID);

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
                    && GMIR.Instances != null)
                {
                    List<Instance> IX = GMIR.Instances;

                    int CurChannel = 0;

                    foreach (Instance Ins in IX)
                    {
                        // this must be here
                        CurChannel++; 

                        Sound Snd = (Sound)Ins;

                        if (Snd.Path == null
                            || !File.Exists(Snd.Path))
                        {
                            ErrorManager.ThrowError(ClassName, "ErrorLoadingSoundChunkException", $"Cannot find the sound located at {Snd.Path}!");
                        }

                        Logging.Log($"Loading sound at {Snd.Path}...");

                        Snd.SoundPtr = SDL_mixer.Mix_LoadWAV(Snd.Path);

                        Snd.Channel = CurChannel;

                        if (Snd.SoundPtr != IntPtr.Zero)
                        {
                            continue; 
                        }
                        else
                        {
                            ErrorManager.ThrowError(ClassName, "ErrorLoadingSoundChunkException", $"An error occurred loading the sound at {Snd.Path} - {SDL.SDL_GetError()}");
                            return; 
                        }
                    }

                    SOUNDS_LOADED = true; 
                }
                else
                {
                    ErrorManager.ThrowError(ClassName, "ErrorAcquiringSoundListException");
                }
            }

            return; 
        }
    }
}
