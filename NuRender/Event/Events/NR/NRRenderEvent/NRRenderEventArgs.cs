using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public class NRRenderEventArgs : NREventArgs
    {
        public WindowRenderingInformation RenderingInformation { get; set; }
    }
}
