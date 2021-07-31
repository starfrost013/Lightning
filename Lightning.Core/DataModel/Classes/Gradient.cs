using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Gradient
    /// 
    /// July 30, 2021
    /// 
    /// Defines a UI gradient.
    /// </summary>
    public class Gradient : GuiElement
    {
        internal override string ClassName => "Gradient";

       
        private bool GRADIENT_INITIALISED { get; set; }

        internal override InstanceTags Attributes => base.Attributes;

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (!GRADIENT_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
        }

        private void Init()
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GradientStop");
            
            if (!GMIR.Successful
            || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGradientStopsException");

            }
            else 
            {
                List<Instance> Instances = GMIR.Instances;

                if (Instances.Count == 0)
                {
                    ErrorManager.ThrowError(ClassName, "GradientMustHaveAboveZeroStopsException");
                    DataModel.RemoveInstance(this, Parent);
                }

                foreach (Instance Ins in Instances)
                {
                    GradientStop GS = (GradientStop)Ins;

                    double MaxGradientWidth = 0.0;


                }
            }
        }

        private void DoRender(Renderer SDL_Renderer, Texture Tx)
        {

        }
    }
}
