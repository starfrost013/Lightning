using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// FrameTimer (non-DataModel)
    /// 
    /// August 15, 2021
    /// 
    /// Implements a frame timer used for Lightning animations. The length of one frame is one second divided by the MaxFPS <see cref="GameSetting"/>.
    /// 
    /// This increments the 
    /// </summary>
    public class FrameTimer
    {
        public long ElapsedFrames { get; set; }

        /// <summary>
        /// Backing field for <see cref="Running"/>.
        /// </summary>
        private bool _running { get; set; }

        public bool Running
        {
            get
            {
                return _running;
            }
            set
            {
                _running = value;

                if (_running) Reset();
            }
        }

        public void Update()
        {
            if (Running) ElapsedFrames++;
        }

        public void Reset()
        {
            if (Running) ElapsedFrames = 0;
        }

       
    }
}
