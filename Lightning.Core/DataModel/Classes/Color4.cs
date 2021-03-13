using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// An ARGB colour.
    /// 
    /// 2021-03-09
    /// </summary>
    public class Color4 : SerialisableObject
    {
        public override string ClassName => "Color4";
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

    }
}
