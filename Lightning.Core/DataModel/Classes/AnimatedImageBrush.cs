using Lightning.Core.SDL2;
using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AnimatedImageBrush
    /// 
    /// August 10, 2021 (modified August 15, 2021)
    /// 
    /// Defines a brush used for animated images. Contains a list of <see cref="Animation"/>s.
    /// </summary>
    public class AnimatedImageBrush : ImageBrush
    {
        /// <summary>
        /// <inheritdoc/> -- set to AnimatedImageBrush.
        /// </summary>
        internal override string ClassName => "AnimatedImageBrush";

        internal override InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE | InstanceTags.Destroyable | InstanceTags.ParentCanBeNull); }

        /// <summary>
        /// The name of the current animation.
        /// </summary>
        public string CurrentAnimationName { get; set; }

        public override void OnCreate()
        {
            Type ParentType = Parent.GetType();

            if (ParentType != typeof(PhysicalObject)
            && !ParentType.IsSubclassOf(typeof(PhysicalObject))) 
            {
                ErrorManager.ThrowError(ClassName, "BrushMustHavePhysicalObjectParentException");
                Parent.RemoveChild(this);
            }
        }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            base.PO_Init();

            if (!TEXTURE_INITIALISED)
            {
                Anim_Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx); 
            }

        }

        internal void Anim_Init()
        {
            LoadAllAnimations();
            TEXTURE_INITIALISED = true;
            return; 
        }

        /// <summary>
        /// PRIVATE: Gets the active <see cref="Animation"/>.
        /// </summary>
        /// <returns>The currently active <see cref="Animation"/>.. If there are no <see cref="Animation"/>s active, it sets the first animation stored within this AnimatedImageBrush to active and returns that. Failing that, returns <c>null</c>.</returns>
        private Animation GetActiveAnimation()
        {
            List<Animation> Animations = GetAnimations(); 

            foreach (Animation Animation in Animations)
            {
                if (Animation.Active) // if this animation is set to active, return it
                {
                    return Animation;
                }
            }

            // default to the first animation
            if (Animations.Count > 0)
            {
                Animation FirstAnim = Animations[0];

                FirstAnim.Active = true; 
                return FirstAnim;
            }
            else // no animations
            {
                return null; 
            }
 
        }

        private void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            Animation CurrentAnimation = GetActiveAnimation(); 

            if (CurrentAnimation == null)
            {
                return; 
            }
            else
            {
                switch (CurrentAnimation.Type)
                {
                    case AnimationType.Continuous:
                        GetMultiInstanceResult GMIR = CurrentAnimation.GetAllChildrenOfType("AnimationFrame");

                        if (!GMIR.Successful)
                        {
                            ErrorManager.ThrowError(ClassName, "FailedToAcquireListOfAnimationFramesException");
                            return; //never runs
                        }
                        else
                        {
                            List<Instance> LI = GMIR.Instances;

                            if (!CurrentAnimation.AnimationTimer.IsRunning) CurrentAnimation.AnimationTimer.Start();

                            List<AnimationFrame> Frames = CurrentAnimation.GetFrames();

                            if (Frames.Count == 0) return; 

                            AnimationFrame AF = CurrentAnimation.GetCurrentFrame();

                            if (AF == null) // animation ended
                            {
                                CurrentAnimation.AnimationTimer.Restart();
                                AF = Frames[0];
                            }

                            // temporary hack code until render refactoring done
                            AF.Render(SDL_Renderer, AF);



                        }
                        return; 

                }


            }

            SnapToParent();
        }

        /// <summary>
        /// Loads all animations for this AnimatedImageBrush.
        /// </summary>
        /// <param name="ImgBrush"></param>
        private void LoadAllAnimations()
        {
            List<Animation> Animations = GetAnimations();

            foreach (Animation Animation in Animations)
            {
                List<AnimationFrame> Frames = Animation.GetFrames();

                foreach (AnimationFrame Frame in Frames)
                {
                    // Load each texture
                    ServiceNotification SN = new ServiceNotification();
                    SN.ServiceClassName = "RenderService";
                    SN.NotificationType = ServiceNotificationType.MessageSend;
                    SN.Data.Name = "LoadTexture";
                    SN.Data.Data.Add((PhysicalObject)Parent); // todo: messagedatacollection
                    SN.Data.Data.Add(Frame);

                    ServiceNotifier.NotifySCM(SN);
                }
            }
        }

        internal List<Animation> GetAnimations()
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("Animation");
            
            if (GMIR.Instances == null
            || !GMIR.Successful)
            {
                ErrorManager.ThrowError(ClassName, "FailedToAcquireListOfAnimationsException");
                return null; // will never run (probably even with voltage glitching considering il instructions don't get executed directly :D)
            }
            else
            {
                List<Instance> InstanceList = (List<Instance>)GMIR.Instances;

                List<Animation> AnimationList = ListTransfer<Instance, Animation>.TransferBetweenTypes(InstanceList); 
                return AnimationList; 
            }

        }

    }
}
