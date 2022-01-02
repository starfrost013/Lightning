#if DEBUG
using NuCore.Utilities;
using NuRender; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// MainDebugPage
    /// 
    /// August 20, 2021 (modified December 11, 2021: Initial NR port)
    /// 
    /// Defines the main debugging page.
    /// </summary>
    public class MainDebugPage : DebugPage
    {
        internal override string ClassName => "MainDebugPage";

        public bool Active { get; set; }
        private bool MAINDEBUGPAGE_INITIALISED { get; set; }

        public override void Render(Scene SDL_Renderer, ImageBrush Tx)
        {
            if (!MAINDEBUGPAGE_INITIALISED)
            {
                MDP_Build();
            }
            else
            {

                return; 
            }

        }

        internal void MDP_Build()
        {
            DebugGui DGUI = GetDebugGui();

            Text Txt = (Text)DataModel.CreateInstance("Text", DGUI);

            Txt.Content = $"Instances: {DataModel.CountInstances()}";
            Txt.Position = DGUI.Position + new Vector2(0, 20);
            Txt.Colour = new Color4(255, 255, 255, 255);
            Txt.ForceToScreen = true; //TEMPHACK
            Txt.DisableTTF = true;

            MAINDEBUGPAGE_INITIALISED = true; 
        }

        private DebugGui GetDebugGui()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("DebugGui");

            if (GIR.Instance == null
            || !GIR.Successful)
            {
                ErrorManager.ThrowError(ClassName, "UnableToAcquireDebugGuiException");
            }
            else
            {
                DebugGui DGUI = (DebugGui)GIR.Instance;

                return DGUI; 
            }

            return null; 
        }



        
    }
}
#endif