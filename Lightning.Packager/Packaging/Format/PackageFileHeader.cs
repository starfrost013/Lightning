using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// PackageHeader
    /// 
    /// December 21, 2021
    /// 
    /// Defines a packaging format for lwpak (Lightning Package) files.
    /// </summary>
    public class PackageFileHeader
    {
        /// <summary>
        /// Magic byte indicator.
        /// </summary>
        public static byte[] Magic { get { return new byte[] { 0x57, 0x5F, 0x5F, 0x41, 0x51, 0x13 }; } }

        /// <summary>
        /// Major component of LWPak file format version.
        /// </summary>
        public const int VersionMajor = 1;

        /// <summary>
        /// Minor component of LWPak file format version.
        /// </summary>
        public const int VersionMinor = 0; 
        
        /// <summary>
        /// Timestamp of this LWPak file. (time_t 64bit)
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The number of files in this LWPAK.
        /// </summary>
        public int NumberOfEntries { get; set; }

        /// <summary>
        /// Pointer to the beginning of the file catalog. 
        /// </summary>
        public ulong CatalogPointer { get; set; }

        /// <summary>
        /// Pointer to the beginning of the file data pointer 
        /// </summary>
        public ulong DataPointer { get; set; }

        public void WriteHeader(BinaryWriter Stream)
        {
            Stream.Seek(0, SeekOrigin.Begin);

            Stream.Write(Magic);
            Stream.Write(VersionMajor);
            Stream.Write(VersionMinor);

            DateTimeOffset Offset = (DateTimeOffset)Timestamp;
            Stream.Write(Offset.ToUnixTimeSeconds());
            Stream.Write(NumberOfEntries);
            Stream.Write(CatalogPointer);
        }
    }
}
