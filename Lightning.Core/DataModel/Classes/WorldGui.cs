﻿using System;
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
    public class WorldGui : GuiRoot
    {
        private bool WORLDGUI_INITIALISED { get; set; }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (WORLDGUI_INITIALISED)
            {
                base.Render(SDL_Renderer, Tx);
            }
            else
            {
                ForceToWorld(); 
            }
            
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
    }
}
