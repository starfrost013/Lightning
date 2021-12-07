using NuRender.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Control
    /// 
    /// April 14, 2021 (modified May 24, 2021)
    /// 
    /// A control binding.
    /// </summary>
    public class Control : SerialisableObject
    {
        internal override string ClassName => "Control";
        internal override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable | InstanceTags.Instantiable | InstanceTags.Serialisable;
        public SDL.SDL_Keysym KeyCode { get; set; }
        public bool Repeated { get; set; }
    }
}
