using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Sound
    /// 
    /// May 5, 2021 (modified May 8, 2021)
    /// 
    /// A sound. 
    /// </summary>
    public class Sound : PhysicalObject 
    {
        internal override string ClassName => "Sound";

        /// <summary>
        /// Is this sound 3D? does it play from a point?
        /// </summary>
        public bool Is3D { get; set; }

        /// <summary>
        /// Internal pointer to the loaded sound chunk.
        /// </summary>
        internal IntPtr SoundPtr { get; set; }

        /// <summary>
        /// The path to the sound
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Is this sound playing?
        /// </summary>
        public bool Playing { get; set; }

        /// <summary>
        /// Has this sound completed?
        /// </summary>
        internal bool Completed { get; set; }

        /// <summary>
        /// Does this sound repeat?
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// The volume of the sound.
        /// </summary>
        public double Volume { get; set; }

        public SDL_mixer.MusicFinishedDelegate MFDelegate { get; set; }

        public override void OnCreate()
        {
            MFDelegate += OnSoundFinished; 
        }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            SDL_mixer.Mix_VolumeMusic((int)Volume * 128);

            // TEST CODE
            if (SoundPtr != IntPtr.Zero)
            {
                if (!Playing && !Completed)
                {
                    Play();
                }
                

            }

            //END TEST CODE
            
        }

        public void Play()
        {
            if (!Repeat)
            {
                //todo: musicstopped handling
                Playing = true;
                SDL_mixer.Mix_PlayMusic(SoundPtr, 1);

            }
            else
            {
                Playing = true;
                // repeat endlessly (-1 = infinite)
                SDL_mixer.Mix_PlayMusic(SoundPtr, -1);
            }
        }

        public void OnSoundFinished()
        {
            if (Completed) Completed = true 

            Playing = false; 
        }
    }
}
