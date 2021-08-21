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

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            Rectangle Rect = (Rectangle)DataModel.CreateInstance("Rectangle", this);
        }
    }
}
