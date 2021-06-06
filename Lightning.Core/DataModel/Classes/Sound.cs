using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Sound
    /// 
    /// May 5, 2021 (modified May 9, 2021)
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

        public override void Render(Renderer SDL_Renderer, Texture Tx)
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
            SDL_mixer.Mix_Pause(1); 
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

                // We can use trigonometry for this,
                // but as we only have one unknown it's easier to use Pythagoras' theorem (a^2 + b^2 = c^2)

                // Pixels
                double DiagDistance = Math.Pow(MX, 2) * Math.Pow(MY, 2);
                DiagDistance = Math.Sqrt(DiagDistance);

                DiagDistance /= Radius;

                if (DiagDistance > 0)
                {
                    NewVolume = (int)(NewVolume / (DiagDistance / 15));
                }
                else
                {
                    // either MX or MY is 0
                    if (MX != 0)
                    {
                        NewVolume = (int)(NewVolume / (Math.Abs(MX) / 15));
                    }
                    else if (MY != 0)
                    {
                        NewVolume = (int)(NewVolume / (Math.Abs(MY) / 15));
                    }
                }

                // Shouldn't happen but just in case... 
                if (NewVolume > 128) NewVolume = 128;
                if (NewVolume < 4) NewVolume /= 5; 
                if (NewVolume < 0) NewVolume = 0;

                //commented out for fps test build 
                //Logging.Log($"Setting 3D sound volume to {NewVolume}...", ClassName);
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
