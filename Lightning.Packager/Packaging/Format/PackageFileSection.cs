using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.Packaging
{
    public class PackageFileSection
    {
        public string Name { get; set; }
        public virtual byte[] SectionMarker { get; } 
    }
}
