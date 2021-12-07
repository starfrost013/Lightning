using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Animation
    /// 
    /// August 11, 2021 (modified August 21, 2021: NumberOfRepeats)
    /// 
    /// Defines an animation.
    /// </summary>
    public class Animation : PhysicalObject
    {
        internal override string ClassName => "Animation";

        internal override InstanceTags Attributes => base.Attributes | InstanceTags.ParentCanBeNull;

        public AnimationType Type { get; set; }

        /// <summary>
        /// Determines if this animation is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// INTERNAL: Timer used for animations.
        /// </summary>
        internal FrameTimer AnimationTimer { get; set; }

        /// <summary>
        /// Number of repeats of this animation
        /// </summary>
        internal int NumberOfRepeats { get; set; }

        /// <summary>
        /// The maximum repeats of this animation. 
        /// </summary>
        public int MaxRepeats { get; set; }

        /// <summary>
        /// Event handler for the <see cref="AnimationUpdated"/> event.
        /// 
        /// Default event handler may be implemented by any Lightning class.
        /// 
        /// Scripts may modify the event handler function. 
        /// </summary>
        public AnimationUpdated OnAnimationUpdated { get; set; }
        public override void OnCreate()
        {
            AnimationTimer = new FrameTimer(); 
        }

        /// <summary>
        /// INTERNAL: Gets the total time (int)
        /// </summary>
        /// <returns></returns>
        internal int GetTotalLength()
        {
            int CurTime = 0;

            List<AnimationFrame> Frames = GetFrames(); 
  
            foreach (AnimationFrame AnimFrame in Frames)
            {
                CurTime += AnimFrame.DefaultTiming;
            }

            return CurTime;
        }

        internal List<AnimationFrame> GetFrames()
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("AnimationFrame"); 

            if (GMIR.Instances == null
            || !GMIR.Successful)
            {
                ErrorManager.ThrowError(ClassName, "FailedToAcquireListOfAnimationFramesException");
                return null; // will never run 
            }
            else
            {
                List<Instance> InstanceList = GMIR.Instances;
                List<AnimationFrame> AnimationFrames = ListTransfer<Instance, AnimationFrame>.TransferBetweenTypes(InstanceList);

                return AnimationFrames;
            }
        }

        internal AnimationFrame GetCurrentFrame()
        {
            int CurTime = 0;

            List<AnimationFrame> Frames = GetFrames();

            for (int i = 0; i < Frames.Count; i++)
            {
                AnimationFrame Frame = Frames[i];

                if (Frames.Count - i > 1)
                {

                    AnimationFrame FramePlusOne = Frames[i + 1];
                    CurTime += Frame.DefaultTiming;
                    int CurTimePlusOne = CurTime + FramePlusOne.DefaultTiming;

                    if (AnimationTimer.ElapsedFrames > CurTime && AnimationTimer.ElapsedFrames < CurTimePlusOne)
                    {
                        return Frame; 
                    }
                }
                else // last frame
                {
                    int TotalTime = GetTotalLength();

                    if (TotalTime > AnimationTimer.ElapsedFrames)
                    {
                        return Frame;
                    }
                }

            }

            return null; // anim completed
        }


    }
}
