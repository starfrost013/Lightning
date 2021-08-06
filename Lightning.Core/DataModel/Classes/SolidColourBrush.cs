using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class SolidColourBrush : Brush 
    {
        internal override string ClassName => "SolidColourBrush";

        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            base.Render(SDL_Renderer, Tx);
        }
    }
}
