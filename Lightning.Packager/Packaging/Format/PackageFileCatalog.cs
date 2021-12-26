using System;
using System.Collections.Generic;
using System.IO; 
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// PackageFileCatalog
    /// 
    /// December 21, 2021
    /// 
    /// Defines package file catalog format.
    /// </summary>
    public class PackageFileCatalog : PackageFileSection
    {
        public override byte[] SectionMarker => new byte[] { 0x01 } ;

        public List<PackageFileCatalogEntry> Entries { get; set; }


        public PackageFileCatalog()
        {
            Entries = new List<PackageFileCatalogEntry>();
        }

        public void WriteSection(BinaryWriter BW)
        {
            BW.Write(SectionMarker);
        }

        public void WriteEntry(PackageFileCatalogEntry Entry)
        {
            Entries.Add(Entry);
        }
    }
}
