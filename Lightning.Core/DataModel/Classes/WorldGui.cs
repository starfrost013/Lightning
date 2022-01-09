using NuCore.Utilities;
using NuRender;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// WorldGui
    /// 
    /// July 4, 2021
    /// 
    /// Defines a GUI that exists in the wordl.
    /// </summary>
    public class WorldGui : Gui
    {
        private bool WORLDGUI_INITIALISED { get; set; }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx, IntPtr RenderTarget)
        {
            if (WORLDGUI_INITIALISED)
            {
                base.Render(SDL_Renderer, Tx, IntPtr.Zero);
            }
            else
            {
                ForceToWorld(); 
            }
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        private void ForceToWorld()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GuiElement");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiElementsException");
                return; 
            }
            else
            {
                List<Instance> Instances = GMIR.Instances;

                foreach (Instance Ins in Instances)
                {
                    GuiElement GE = (GuiElement)Ins;

                    GE.ForceToScreen = false; 
                }

                WORLDGUI_INITIALISED = true; 
            }
        }

        public override void OnClick(object Sender, MouseEventArgs EventArgs) => base.OnClick(Sender, EventArgs);
    }
}
