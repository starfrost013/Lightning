using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class Control : SerialisableObject
    {
        internal override string ClassName => "Control";
        internal override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable | InstanceTags.Instantiable | InstanceTags.Serialisable;
        public SDL.SDL_Keysym KeyCode { get; set; }
        public bool Repeated { get; set; }
    }
}
