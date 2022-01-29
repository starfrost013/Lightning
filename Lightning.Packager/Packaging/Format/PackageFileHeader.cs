using NuCore.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using System.IO;
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// PackageHeader
    /// 
    /// December 21, 2021 (modified December 31, 2021)
    /// 
    /// Defines a packaging format for lwpak (Lightning Package) files.
    /// 
    /// Version history:
    /// V1.0 2021/12/21 initial version
    /// V1.1 2021/12/31 Change uint to byte for compression mode
    /// </summary>
    public class PackageFileHeader
    {
        /// <summary>
        /// Magic byte indicator.
        /// </summary>
        public static byte[] Magic { get { return new byte[] { 0xDE, 0xAD, 0xBA, 0xBE, 0x77, 0x77 }; } }

        /// <summary>
        /// Major component of LWPak file format version.
        /// </summary>
        public const byte VersionMajor = 1;

        /// <summary>
        /// Minor component of LWPak file format version.
        /// </summary>
        public const byte VersionMinor = 2; 
        
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

        private string ClassName => "Packaging Service";

        internal const int HeaderSize = 36; 

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
            Stream.Write(DataPointer);
        }

        public void ReadHeader(BinaryReader Stream) // todo: headerreadresult so that it can be aborted
        {
            try
            {
                Stream.BaseStream.Seek(0, SeekOrigin.Begin);

                byte[] Bytes = Stream.ReadBytes(6);

                if (Bytes == Magic)
                {
                    Logging.Log("Valid header found!", ClassName);

                    int FVersionMajor = Stream.ReadByte();
                    int FVersionMinor = Stream.ReadByte();

                    // Check file version.
                    if (FVersionMajor != VersionMajor
                    || FVersionMinor != VersionMinor)
                    {
                        ErrorManager.ThrowError(ClassName, "LWPakInvalidHeaderException", $"Attempted to read an incompatible version of the LWPAK file format.\nThis version of Lightning implements version {VersionMajor}.{VersionMinor} of the LWPAK format.\nThis file is version {FVersionMajor}.{FVersionMinor}.");
                        return;        
                    }

                    long UnixSeconds = Stream.ReadInt64();
                    NumberOfEntries = Stream.ReadInt32();

                    if (NumberOfEntries < 0)
                    {
                        ErrorManager.ThrowError(ClassName, "LWPakInvalidHeaderException", $"Invalid number of entries! (must be above zero, got {NumberOfEntries}");
                        return; 
                    }

                    CatalogPointer = Stream.ReadUInt64();
                    DataPointer = Stream.ReadUInt64();
#if DEBUG
                    DateTime FileDateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(UnixSeconds);
                    Logging.Log($"File format version {FVersionMajor}.{FVersionMinor}\nTimestamp {FileDateTime.ToString("yyyy-MM-dd HH:mm:ss")}\nNumber of entries {NumberOfEntries}\nCatalog pointer {CatalogPointer}\nData pointer {DataPointer}", ClassName);
#endif

                }
                else
                {
                    ErrorManager.ThrowError(ClassName, "LWPakInvalidHeaderException", "Invalid header magic");
                }
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowError(ClassName, "LWPakInvalidHeaderException", $"Unknown error: \n\n{ex}");
            }
           
        }
    }
}
