using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class Animation : PhysicalObject
    {
        internal override string ClassName => "Animation";

        public AnimationFrameCollection Frames { get; set; }
    }
}
