using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// PackageFile
    /// 
    /// December 21, 2021
    /// 
    /// Defines an LWPAK file.
    /// </summary>
    public class PackageFile
    {
        public PackageFileHeader Header { get; set; }

        public List<PackageFileSection> Section { get; set; }
    }
}
