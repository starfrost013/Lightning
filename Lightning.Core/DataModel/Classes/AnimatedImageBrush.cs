using Lightning.Core.SDL2;
using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ImageBrush (Texture 2.0)
    /// 
    /// August 7, 2021 (original: April 9, 2021, modified August 10, 2021)
    /// 
    /// Defines an image that can be displayed on the screen. Non-animated 
    /// </summary>
    public class AnimatedImageBrush : ImageBrush
    {
        /// <summary>
        /// <inheritdoc/> -- set to Texture.
        /// </summary>
        internal override string ClassName => "AnimatedImageBrush";

        internal override InstanceTags Attributes { get => (InstanceTags.Instantiable | InstanceTags.Archivable | InstanceTags.Serialisable | InstanceTags.ShownInIDE | InstanceTags.Destroyable | InstanceTags.ParentCanBeNull); }


        private Animation CurrentAnimation { get; set; }

        /// <summary>
        /// Private: Current animation frame
        /// </summary>
        private int CurrentAnimationFrame { get; set; }

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
            ServiceNotification SN = new ServiceNotification();
            SN.ServiceClassName = "RenderService";
            SN.NotificationType = ServiceNotificationType.MessageSend;
            SN.Data.Name = "LoadAnimatedTexture";
            SN.Data.Data.Add((PhysicalObject)Parent); // todo: messagedatacollection
            SN.Data.Data.Add(this);

            ServiceNotifier.NotifySCM(SN);

            TEXTURE_INITIALISED = true;
            return; 
        }

        private void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (CurrentAnimation == null)
            {
                return; 
            }
            else
            {
                GetMultiInstanceResult GMIR = CurrentAnimation.GetAllChildrenOfType("AnimationFrame");

                if (!GMIR.Successful)
                {
                    
                }
                else
                {
                    List<Instance> LI = GMIR.Instances;

                    foreach (Instance Ins in LI)
                    {
                        AnimationFrame AF = (AnimationFrame)Ins;

                        AF.Render(SDL_Renderer, Tx);
                    }
                }

            }

            SnapToParent();
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

        private void SnapToParent()
        {
            // TEMP code
            PhysicalObject PParent = (PhysicalObject)Parent;
            Position = PParent.Position;
            Size = PParent.Size;
            BorderColour = PParent.BorderColour;
            BackgroundColour = PParent.BackgroundColour;
            DisplayViewport = PParent.DisplayViewport;
            Colour = PParent.Colour;

        }
    }
}
