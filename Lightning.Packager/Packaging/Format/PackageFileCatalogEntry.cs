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

        /// <summary>
        /// Size of this file. 
        /// 
        /// DO NOT REARRANGE THIS UNLESS YOU ALSO CHANGE THE CODE IN
        /// <see cref="PackageFile"/>.
        /// </summary>
        public ulong FileSize { get; set; }

        public void WriteEntry(BinaryWriter BW)
        {
            BW.Write(FileName); // should never be null
            BW.Write(FilePointer);
            DateTimeOffset Offset = (DateTimeOffset)Timestamp;
            BW.Write(Offset.ToUnixTimeSeconds());
            BW.Write((uint)FileCompressionMode);
            BW.Write(FileSize); // See warning on property 
        }

    }
}
