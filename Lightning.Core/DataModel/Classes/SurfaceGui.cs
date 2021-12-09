using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// SurfaceGui
    /// 
    /// July 5, 2021 00:06 (modified August 15, 2021)
    /// 
    /// Defines a GUI that is attached and displays on a specific object.
    /// </summary>
    public class SurfaceGui : Gui
    {
        /// <summary>
        /// The target object name
        /// </summary>
        public string TargetObjectName { get; set; }
        
        private bool SURFACEGUI_INITIALISED { get; set; }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!SURFACEGUI_INITIALISED)
            {
                ForceToSurface();
            }
            else
            {
                base.Render(SDL_Renderer, Tx);
            }
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        private void ForceToSurface()
        {
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GuiElement");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiElementsException");
                return;
            }
            else
            {
                List<Instance> GuiElementList = GMIR.Instances;

                foreach (Instance Instance in GuiElementList)
                {
                    GuiElement GuiElement = (GuiElement)Instance;

                    GetTargetObjectResult GTOR = GetTargetObject();

                    if (!GTOR.Successful
                        || GTOR.FailureReason != null)
                    {
                        return;
                    }
                    else
                    {
                        
                        if (GuiElement.Position == null
                        || GuiElement.Size == null) continue;

                        if (GuiElement.Position.X > GTOR.TargetObject.Position.X || GuiElement.Position.X < (GTOR.TargetObject.Position.X + GTOR.TargetObject.Size.X)) GuiElement.Position.X = GTOR.TargetObject.Position.X;
                        if (GuiElement.Position.Y > GTOR.TargetObject.Position.Y || GuiElement.Position.Y < (GTOR.TargetObject.Position.Y + GTOR.TargetObject.Size.Y)) GuiElement.Position.Y = GTOR.TargetObject.Position.Y;

                        
                        GTOR.Successful = true;
                        SURFACEGUI_INITIALISED = true;
                        return;
                    }
                }
            }
        }

        private GetTargetObjectResult GetTargetObject()
        {
            GetTargetObjectResult GTOR = new GetTargetObjectResult();

            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("PhysicalObject");

            if (!GMIR.Successful 
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "TheWorkspaceHasBeenDestroyedException");
                GTOR.FailureReason = "Failed to obtain workspace children!";
                return GTOR; // will not run
            }
            else
            {
                foreach (Instance GM in GMIR.Instances)
                {
                    PhysicalObject PO = (PhysicalObject)GM;

                    if (GM.Name == TargetObjectName)
                    {
                        GTOR.Successful = true;
                        GTOR.TargetObject = PO;
                        return GTOR; 
                    }

                }
            }

            GTOR.FailureReason = $"Failed to obtain target object -- the Instance with the name {TargetObjectName} does not exist!";
            return GTOR;
        }

        public override void OnClick(object Sender, MouseEventArgs EventArgs) => base.OnClick(Sender, EventArgs);
    }
}
