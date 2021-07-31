using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class GradientStop : GuiElement
    {
        internal override string ClassName => "GradientStop";
       
        public double StopPoint { get; set; }
    }
}
