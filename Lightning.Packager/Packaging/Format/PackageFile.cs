using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// PackageFile
    /// 
    /// December 21, 2021 (modified December 28, 2021)
    /// 
    /// Defines an LWPAK file.
    /// </summary>
    public class PackageFile
    {
        /// <summary>
        /// The header of this LWPAK file - see <see cref="PackageFileHeader"/>.
        /// </summary>
        public PackageFileHeader Header { get; set; }

        /// <summary>
        /// The file catalog of this LWPAK file - see <see cref="PackageFileCatalog"/>.
        /// </summary>
        public PackageFileCatalog Catalog { get; set; }

        public string FileName { get; set; }

        private static string ClassName => "Packaging Service";
        public PackageFile()
        {
            Header = new PackageFileHeader();
            Catalog = new PackageFileCatalog();
        }

        public void AddEntry(PackageFileCatalogEntry Entry) => Catalog.AddEntry(Entry);

        public void AddEntry(string FileName)
        {
            PackageFileCatalogEntry PFCE = new PackageFileCatalogEntry();
            PFCE.FileName = FileName;
            PFCE.FileCompressionMode = PackageFileCompressionMode.Default;

            Catalog.AddEntry(PFCE);
        }

        public void AddEntry(string FileName, PackageFileCompressionMode CompressionMode)
        {
            PackageFileCatalogEntry PFCE = new PackageFileCatalogEntry();
            PFCE.FileName = FileName;
            PFCE.FileCompressionMode = CompressionMode;

            Catalog.AddEntry(PFCE);

        }
        public void Write()
        {
            Logging.Log("Writing LWPak file", ClassName);

            using (BinaryWriter BW = new BinaryWriter(new FileStream(FileName, FileMode.Create)))
            {
                Logging.Log("Writing header...", ClassName);
                Header.NumberOfEntries = Catalog.Entries.Count;
                Header.Timestamp = DateTime.Now;
                Header.CatalogPointer = PackageFileHeader.HeaderSize + 1;
                Header.DataPointer = (ulong)((long)Header.CatalogPointer + ((Header.NumberOfEntries * Catalog.Entries[0].CatalogEntrySize) + 1));
                Header.WriteHeader(BW);
                
                for (int i = 0; i < Catalog.Entries.Count; i++)
                {

                    PackageFileCatalogEntry PFCE = Catalog.Entries[i];
                    Logging.Log($"Writing catalog entry and file for {PFCE.FileName}...", ClassName);

                    PFCE.Timestamp = DateTime.Now; 
                    // Compressed file size should be zero at this point.
                    PFCE.WriteEntry(BW);

                    BW.BaseStream.Seek(0, SeekOrigin.End);

                    // load for store 
                    byte[] CompressedFile = File.ReadAllBytes(PFCE.FileName);

                    if (PFCE.FileCompressionMode.HasFlag(PackageFileCompressionMode.SixBit))
                    {
                        Logging.Log($"Packaging file {PFCE.FileName} using eight to six modulation...", ClassName);

                        SixBitCompressionFormat SBCF = new SixBitCompressionFormat();
                        CompressedFile = SBCF.Compress(CompressedFile);
                    }

                    if (PFCE.FileCompressionMode.HasFlag(PackageFileCompressionMode.LZMA))
                    {
                        Logging.Log($"Packaging file {PFCE.FileName} using LZMA...", ClassName);
                        LZMACompressionFormat LZMA = new LZMACompressionFormat();
                        CompressedFile = LZMA.Compress(CompressedFile);
                    }

                    BW.Write((ulong)CompressedFile.Length);

                    PFCE.CompressedData = CompressedFile;

                }

                foreach (PackageFileCatalogEntry PFCE in Catalog.Entries) // todo PackageFileCatalogEntryCollection
                {
                    BW.Write(PFCE.CompressedData);
                }
            }
        }

        public void Read()
        {
            using (BinaryReader BR = new BinaryReader(new FileStream(FileName, FileMode.Open)))
            {
                Header.ReadHeader(BR);
                
                for (int i = 0; i < Header.NumberOfEntries; i++)
                {
                    // Add the entries to the file.
                    PackageFileCatalogEntry PFCE = new PackageFileCatalogEntry();
                    PFCE.ReadEntry(BR);
                    Catalog.AddEntry(PFCE);
                }
            }
        }
    }
}
