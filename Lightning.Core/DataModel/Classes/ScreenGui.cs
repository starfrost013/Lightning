using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScreenGui
    /// 
    /// June 29, 2021
    /// 
    /// Defines a GUI that is placed on the screen. Its position is relative to the screen resolution.
    /// </summary>
    public class ScreenGui : GuiRoot
    {
        internal override string ClassName => "ScreenGui";


        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {

            // hack
            // need to find a better way to do this

            GetMultiInstanceResult GMIR = GetAllChildrenOfType("GuiRoot");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {

            }
            else
            {
                foreach (Instance Instance in GMIR.Instances)
                {
                    GuiRoot GIR = (GuiRoot)Instance;
                    //todo: finish this
                }
            }

            // render all children
            base.Render(SDL_Renderer, Tx);
        }
    }
}
