using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GuiRoot
    /// 
    /// June 28, 2021
    /// 
    /// Defines the root class for all Lightning UI elements and objects.
    /// </summary>
    public class GuiRoot : PhysicalObject
    {
        internal override string ClassName => "GuiRoot";

        /// <summary>
        /// Run when this UI element is clicked.
        /// </summary>
        public virtual void OnClick(Vector2 RelativePosition)
        {
            return; 
        }

        /// <summary>
        /// Basic rendering for GuiRoot.
        /// 
        /// Renders all the children of this GUI element.
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {

            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GuiRoot");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfGuiRootsException", "Failed to obtain list of GuiRoots!");
                return; 
            }
            else
            {
                List<Instance> GuiRoots = GMIR.Instances;

                foreach (Instance Instance in GuiRoots)
                {
                    GuiRoot GuiRoot = (GuiRoot)Instance;

                    // for now
                    GuiRoot.Render(SDL_Renderer, null); 
                }
            }
        }

    }
}
