#if DEBUG
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// MainDebugPage
    /// 
    /// August 20, 2021 (modified August 24, 2021)
    /// 
    /// Defines the main debugging page.
    /// </summary>
    public class MainDebugPage : DebugPage
    {
        internal override string ClassName => "MainDebugPage";

        private bool MAINDEBUGPAGE_INITIALISED { get; set; }
        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
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

        private void MDP_Build()
        {
            Text Txt = (Text)DataModel.CreateInstance("Text", this);

            Txt.Content = $"Instances: {DataModel.CountInstances()}";
            Txt.Position = Position + new Vector2(5, 20);
            Txt.Colour = new Color4(255, 255, 255, 255);

            GlobalSettings GS = DataModel.GetGlobalSettings();

            if (GS.DebugDefaultFontName != null)
            {
                Txt.FontFamily = GS.DebugDefaultFontName;
            }
            else
            {
                Txt.FontFamily = "Arial.14pt for DEBUG";
            }
            
            
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
        }
        
    }
}
#endif