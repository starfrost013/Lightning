using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class Control : SerialisableObject
    {
        public override string ClassName => "Control";
        public override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable | InstanceTags.Instantiable | InstanceTags.Serialisable;
        public SDL.SDL_Keysym KeyCode { get; set; }
        public bool Repeated { get; set; }
    }
}
