using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lightning.Core.Packaging
{
    public class PackageFileCatalogEntry
    {
        public int CatalogEntrySize
        {
            get
            {
                if (FileName == null)
                {
                    return 25;
                }
                else
                {
                    return 25 + FileName.Length;
                }
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// The name of this file.
        /// </summary>
        public string FileName { get; set; } // May implement an inode-like system

        /// <summary>
        /// Pointer to the start of this file.
        /// </summary>
        public ulong FilePointer { get; set; }

        /// <summary>
        /// Timestamp of this LWPak file. (time_t 64bit)
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The compression mode of this file - see <see cref="PackageFileCompressionMode"/>.
        /// </summary>
        public PackageFileCompressionMode FileCompressionMode { get; set; }

        private string ClassName => "Packaging Services";

        /// <summary>
        /// Size of this file. 
        /// 
        /// DO NOT REARRANGE THIS UNLESS YOU ALSO CHANGE THE CODE IN
        /// <see cref="PackageFile"/>.
        /// </summary>
        public ulong FileSize { get; set; }
        
        internal byte[] CompressedData { get; set; }
        public void ReadEntry(BinaryReader BR)
        {
            // Assume we are already at the right position.

            try
            {
                FileName = BR.ReadString();
                FilePointer = BR.ReadUInt64();
                Timestamp = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BR.ReadInt64());
                FileCompressionMode = (PackageFileCompressionMode)BR.ReadByte();
                FileSize = BR.ReadUInt64();
                Logging.Log($"Filename: {FileName}\n" +
                $"File position: {FilePointer}\n" +
                $"Timestamp: {Timestamp.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
                $"File compression mode: {FileCompressionMode}\n" +
                $"File size: {FileSize}", ClassName);
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowError(ClassName, "LWPakInvalidCatalogException", $"An error occurred reading the LWPak file catalog.\n{ex}");
            }
            
        }

        public void WriteEntry(BinaryWriter BW)
        {
            BW.Write(FileName); // should never be null
            BW.Write(FilePointer);
            DateTimeOffset Offset = (DateTimeOffset)Timestamp;
            BW.Write(Offset.ToUnixTimeSeconds());
            BW.Write((byte)FileCompressionMode);
            //BW.Write(FileSize); // See warning on property 
        }

    }
}
