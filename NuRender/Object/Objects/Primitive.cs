using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public class Primitive : NRObject 
    {
        public override string ClassName => "Primitive";
        
        /// <summary>
        /// Determines if this primitive is anti-aliased.
        /// </summary>
        public bool Antialiased { get; set; }

        /// <summary>
        /// Determines if this primitive is bordered.
        /// </summary>
        public bool Bordered { get; set;  }

        /// <summary>
        /// Determines if this primitive is filled.
        /// </summary>
        public bool Filled { get; set; }

        public override void Render(WindowRenderingInformation RenderInfo)
        {
            return; 
        }

        public override void Start(WindowRenderingInformation RenderingInformation)
        {
            return;
        }
    }
}
