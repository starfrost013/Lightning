using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// PackageFileSections
    /// 
    /// December 22, 2021
    /// 
    /// Defines package file sections
    /// </summary>
    public enum PackageFileSections
    {
        Catalog = 0x01,

        CompressedData = 0xCC,
    }
}
