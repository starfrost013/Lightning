using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Animation
    /// 
    /// August 11, 2021 (modified August 13, 2021)
    /// 
    /// Defines an animation.
    /// </summary>
    public class Animation : PhysicalObject
    {
        internal override string ClassName => "Animation";

        public AnimationFrameCollection Frames { get; set; }

        public AnimationType Type { get; set; }

        /// <summary>
        /// INTERNAL: Gets the total time (int)
        /// </summary>
        /// <returns></returns>
        internal int GetTotalLength()
        {
            int CurTime = 0;

            foreach (AnimationFrame AnimFrame in Frames)
            {
                CurTime += AnimFrame.DefaultTiming;
            }

            return CurTime;
        }

        internal AnimationFrame GetCurrentFrame()
        {
            int CurTime = 0;

            for (int i = 0; i < Frames.Count; i++)
            {
                AnimationFrame Frame = Frames[i];

                if (Frames.Count - i > 1)
                {

                    AnimationFrame FramePlusOne = Frames[i + 1];
                    CurTime += Frame.DefaultTiming;
                    int CurTimePlusOne = CurTime += FramePlusOne.DefaultTiming;

                    if (AnimationTimer.ElapsedMilliseconds > CurTime && AnimationTimer.ElapsedMilliseconds < CurTimePlusOne)
                    {
                        return Frame; 
                    }
                }
                else // last frame
                {
                    int TotalTime = GetTotalLength();

                    if (TotalTime > AnimationTimer.ElapsedMilliseconds)
                    {
                        return Frame;
                    }
                }

            }

            return null; // yes
        }

        internal Stopwatch AnimationTimer { get; set; }
    }
}
