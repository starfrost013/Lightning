using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// MainDebugPage
    /// 
    /// August 20, 2021
    /// 
    /// Defines the main debugging page.
    /// </summary>
    public class MainDebugPage : DebugPage
    {
        internal override string ClassName => "MainDebugPage";


        private bool MAINDEBUGPAGE_INITIALISED { get; set; }
        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!DEBUGPAGE_INITIALISED)
            {
                DP_Init(); 
            }
            else
            {

                return; 
            }

        }


        
    }
}
