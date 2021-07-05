using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// SurfaceGui
    /// 
    /// July 5, 2021 00:06
    /// 
    /// Defines a GUI that is attached and displays on a specific object.
    /// </summary>
    public class SurfaceGui : GuiRoot
    {
        /// <summary>
        /// The target object name
        /// </summary>
        public string TargetObjectName { get; set; }
        
        private bool SURFACEGUI_INITIALISED { get; set; }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
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
                        if (GuiElement.Position.X < GTOR.TargetObject.Position.X || GuiElement.Position.Y > GTOR.TargetObject.Position.X) GuiElement.Position.X = GTOR.TargetObject.Position.X;
                        if (GuiElement.Position.Y < GTOR.TargetObject.Position.Y || GuiElement.Position.Y > GTOR.TargetObject.Position.Y) GuiElement.Position.Y = GTOR.TargetObject.Position.Y;

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
    }
}
