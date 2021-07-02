using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GuiElement
    /// 
    /// June 30, 2021
    /// 
    /// Defines an individual GUI element. 
    /// </summary>
    public class GuiElement : GuiRoot
    {
    
        /// <summary>
        /// This is completely fucking bullshit but is required for screengui and it makes me sad
        /// </summary>
        internal bool ForceToScreen { get; set; }
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            base.Render(SDL_Renderer, Tx);
        }
       
    }
}
