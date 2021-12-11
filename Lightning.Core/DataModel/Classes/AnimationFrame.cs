using NuCore.Utilities;
using NuRender;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AnimationFrame
    /// 
    /// August 10, 2021 (modified August 15, 2021: sets position to parent)
    /// 
    /// Defines an animation frame
    /// </summary>
    public class AnimationFrame : ImageBrush
    {
        internal override string ClassName => "AnimationFrame";

        /// <summary>
        /// Default time this animation will be played in frames. CAN BE CHANGED BY SCRIPTING AND THE MAXFPS <see cref="GameSetting"/>!
        /// </summary>
        public int DefaultTiming { get; set; }

        private bool ANIMATIONFRAME_INITIALISED { get; set; }
        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            if (!ANIMATIONFRAME_INITIALISED)
            {
                AnimFrame_Init();
            }
            else
            {
                base.Render(SDL_Renderer, Tx); // call base imagebrush renderer
            }
        }

        private void AnimFrame_Init()
        {
            // kind of hacky i guess?
            Instance ParentOfParent = Parent.Parent;

            if (ParentOfParent == null)
            {
                ErrorManager.ThrowError(ClassName, "AnimationFrameMustBeChildOfChildOfAnimatedImageBrushException");
                Parent.RemoveChild(this); // destroy this object
                return;
            }
            else
            {
                Type POPType = ParentOfParent.GetType();

                if (POPType != typeof(AnimatedImageBrush))
                {
                    ErrorManager.ThrowError(ClassName, "AnimationFrameMustBeChildOfChildOfAnimatedImageBrushException");
                    Parent.RemoveChild(this);
                    return; 
                }
                else
                {
                    AnimatedImageBrush AIB = (AnimatedImageBrush)ParentOfParent;

                    if (Position == null && AIB.Position != null) Position = AIB.Position;
                    if (Size == null && AIB.Size != null) Size = AIB.Size;

                    TEXTURE_INITIALISED = true;
                    
                    ANIMATIONFRAME_INITIALISED = true;
                    return;
                    
                }
            }
        }
    }
}
