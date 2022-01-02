using NuCore.Utilities;
using NuRender;
using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Sound
    /// 
    /// May 5, 2021 (modified December 11, 2021)
    /// 
    /// Defines a sound that can be played.
    /// </summary>
    public class Sound : PhysicalObject
    {
        internal override string ClassName => "Sound";


        /// <summary>
        /// Is this sound 3D? does it play from a point?
        /// </summary>
        public bool Is3D { get; set; }

        /// <summary>
        /// The object name to target. Ignored if <see cref="Is3D"/> is false. Defaults to the first camera if not set.
        /// </summary>
        public string TargetObject { get; set; }

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
        internal bool Playing { get; set; }

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

        /// <summary>
        /// The radius of this sound. 
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The SDL channel that this sound is played on.
        /// </summary>
        internal int Channel { get; set; }

        internal SDL_mixer.MusicFinishedDelegate MFDelegate { get; set; }

        /// <summary>
        /// The Loop Count of this sound. 
        /// </summary>
        public int LoopCount { get; set; }

        public override void OnCreate()
        {
            MFDelegate += OnSoundFinished; 
        }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            

            // TEST CODE
            if (SoundPtr != IntPtr.Zero)
            {
                if (!Playing && !Completed)
                {
                    Play();
                }
                else
                {
                    if (Is3D) Set3DVolume(); 
                }

            }

            //END TEST CODE
            
        }

        public void Play()
        {

            int NewVolume = (int)Volume * 128;

            if (!Is3D)
            {
                SDL_mixer.Mix_VolumeMusic(NewVolume);
            }

            if (!Repeat)
            {
                //todo: musicstopped handling
                Playing = true;
                SDL_mixer.Mix_PlayChannel(Channel, SoundPtr, LoopCount);

            }
            else
            {
                Playing = true;
                // repeat endlessly (-1 = infinite)
                SDL_mixer.Mix_PlayChannel(Channel, SoundPtr, -1);
            }
        }

        public void Pause()
        {
            SDL_mixer.Mix_Pause(Channel); 
        }

        /// <summary>
        /// Stops playing this sound.
        /// </summary>
        public void Stop()
        {
            Playing = false;
            Completed = true;
            OnSoundFinished(); 
        }

        private void Set3DVolume()
        {
            Workspace WsQ = DataModel.GetWorkspace();

            PhysicalObject NewPO = null;

            if (TargetObject != null)
            {
                GetInstanceResult GIR = WsQ.GetChild(TargetObject);

                if (GIR.Instance != null
                    && GIR.Successful)
                {
                    Instance TempInstance = (Instance)GIR.Instance;

                    Type InstanceType = TempInstance.GetType();

                    if (InstanceType.IsSubclassOf(typeof(PhysicalObject))
                        || InstanceType == typeof(PhysicalObject))
                    {
                        NewPO = (PhysicalObject)TempInstance;
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "Err3DSoundTargetObjectDoesNotExistException", $"The TargetObject specified for the sound located at {Path}, {TargetObject} must be or inherit from the PhysicalObject class!");
                    }
                }
                else
                {
                    ErrorManager.ThrowError(ClassName, "Err3DSoundTargetObjectDoesNotExistException", $"The TargetObject specified for the sound located at {Path}, {TargetObject} does not exist!");
                    return;
                }
            }
            else
            {
                ErrorManager.ThrowError(ClassName, "Err3DSoundRequiresTargetObjectException");
                return;
            }

            // Actually move it

            int NewVolume = (int)Volume * 128;  

            if (NewPO != null
                && Position != null
                && Radius > 0)
            {
                double MX = NewPO.Position.X - Position.X;
                double MY = NewPO.Position.Y - Position.Y;

                // Use Pythagoras' theorem to determine the radius

                // Pixels
                double DiagDistance = Math.Pow(MX, 2) * Math.Pow(MY, 2);
                DiagDistance = Math.Sqrt(DiagDistance);

                DiagDistance /= Radius;

                if (DiagDistance > 0)
                {
                    NewVolume = (int)(NewVolume / (DiagDistance / 10)); // increase by (15/10)x (Dec 11, 2021)
                }
                else
                {
                    // either MX or MY is 0
                    if (MX != 0)
                    {
                        NewVolume = (int)(NewVolume / (Math.Abs(MX) / 10));// increase by (15/10)x (Dec 11, 2021)
                    }
                    else if (MY != 0)
                    {
                        NewVolume = (int)(NewVolume / (Math.Abs(MY) / 10));// increase by (15/10)x (Dec 11, 2021)
                    }
                }

                // clamp to 0 from 4
                if (NewVolume > 128) NewVolume = 128;
                if (NewVolume < 4) NewVolume = 0; 

                SDL_mixer.Mix_Volume(Channel, NewVolume);
            }
            else
            {
                ErrorManager.ThrowError(ClassName, "Err3DSoundRequiresSoundPositionAndRadiusException");
                return;
            }
        }

        private void OnSoundFinished()
        {
            if (!Repeat) Completed = true; 

            Playing = false; 
        }
    }
}
