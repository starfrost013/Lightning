using System;
using System.Collections.Generic;
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
                    return 12;
                }
                else
                {
                    return 12 + FileName.Length;
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

        public PackageFileCompressionMode FileCompressionMode { get; set; }

        /// <summary>
        /// Size of this file.
        /// </summary>
        public ulong FileSize { get; set; }
    }
}
