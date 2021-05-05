using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Sound
    /// 
    /// May 5, 2021
    /// 
    /// A sound. 
    /// </summary>
    public class Sound : PhysicalObject 
    {
        /// <summary>
        /// Is this sound 3D? does it play from a point?
        /// </summary>
        public bool Is3D { get; set; }

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
        /// Does this sound repeat?
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// The volume of the sound.
        /// </summary>
        public double Volume { get; set; }
    }
}
