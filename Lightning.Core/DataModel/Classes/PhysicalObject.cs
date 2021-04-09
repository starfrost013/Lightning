using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class PhysicalObject : SerialisableObject
    {
        public override string ClassName => "PhysicalObject";
        public override InstanceTags Attributes => base.Attributes;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public string SpritePath { get; set; }
        public Texture Tex { get; set; }
    }
}
