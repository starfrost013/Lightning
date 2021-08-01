using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Button
    /// 
    /// July 12, 2021
    /// 
    /// Defines a button
    /// </summary>
    public class Button : TextBox
    {

        
        private bool TEXTBOX_INITIALISED { get; set; }

        /// <summary>
        /// TEMP: Defines init failure
        /// 
        /// TODO: Result class
        /// </summary>
        private bool TEXTBOX_INITIALISATION_FAILED { get; set; }

        private Text ItemText { get; set; }

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            if (TEXTBOX_INITIALISATION_FAILED) return; 
            
            if (!TEXTBOX_INITIALISED)
            {
                Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
            
        }


        private void DoRender(Renderer SDL_Renderer, Texture Tx)
        {
            ItemRectangle.Render(SDL_Renderer, Tx);

            ItemText.Render(SDL_Renderer, Tx);
        }

        
    }
}
