using NuCore.Utilities;
using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lightning.Core.Packaging
{
    /// <summary>
    /// LZMACompressionFormat
    /// 
    /// December 25, 2021 (modified December 28, 2021)
    /// 
    /// Defines the code for the LZMA compression format.
    /// Uses LZMA SDK v2104 beta.
    /// </summary>
    public class LZMACompressionFormat : CompressionFormat
    {
        public override byte[] Compress(byte[] Bytes)
        {
            // implementation doesn't support not compressing to file
            // and overall just very weird
            // so we are doing this workaround
            string TempDir = Path.GetTempPath();
            TempDir = @$"{TempDir}\NCLZMA";
            if (!Directory.Exists(TempDir)) Directory.CreateDirectory(TempDir);
            string CompressedFileName = (@$"{TempDir}\{DateTime.Now.ToString("yyy MM dd hh MM ss").Replace(" ", "")}");

            byte[] CompressedData = CompressFile(CompressedFileName);
            File.Delete(CompressedFileName);
            return CompressedData;
        }

        public override byte[] CompressFile(string FileNameIn, string FileNameOut = null)
        {
            SevenZip.Compression.LZMA.Encoder LZMAEncoder = new SevenZip.Compression.LZMA.Encoder();

            using (FileStream In = new FileStream(FileNameIn, FileMode.Open))
            {
                FileStream Out = null;

                if (FileNameOut == null)
                {
                    Out = new FileStream(FileNameIn, FileMode.Open);
                }
                else
                {
                    Out = new FileStream(FileNameOut, FileMode.Open);


                }

                LZMAEncoder.WriteCoderProperties(Out);
                LZMAEncoder.Code(In, Out, -1, -1, null);
            }
            
            if (FileNameOut == null)
            {
                return File.ReadAllBytes(FileNameIn);
            }
            else
            {
                return File.ReadAllBytes(FileNameOut);
            }
        }

        public override byte[] Decompress(byte[] Bytes)
        {
            // implementation doesn't support not compressing to file
            // and overall just very weird
            // so we are doing this workaround
            string TempDir = Path.GetTempPath();
            TempDir = @$"{TempDir}\NCLZMA";
            if (!Directory.Exists(TempDir)) Directory.CreateDirectory(TempDir);
            string CompressedFileName = (@$"{TempDir}\{DateTime.Now.ToString("yyy MM dd hh MM ss").Replace(" ", "")}");

            byte[] CompressedData = DecompressFile(CompressedFileName);
            File.Delete(CompressedFileName);
            return CompressedData;
        }

        public override byte[] DecompressFile(string FileNameIn, string FileNameOut = null)
        {
            SevenZip.Compression.LZMA.Decoder LZMADecoder = new SevenZip.Compression.LZMA.Decoder();

            using (BinaryReader In = new BinaryReader(new FileStream(FileNameIn, FileMode.Open)))
            {
                using (BinaryReader Out = new BinaryReader(new FileStream(FileNameOut, FileMode.Open)))
                {
                    byte[] Properties = new byte[5];

                    In.BaseStream.Read(Properties, (int)In.BaseStream.Position, 5);

                    long Length = In.ReadInt64();

                    LZMADecoder.SetDecoderProperties(Properties);
                    LZMADecoder.Code(In.BaseStream, Out.BaseStream, In.BaseStream.Length, Length, null);
                }
            }

            if (FileNameOut == null)
            {
                return File.ReadAllBytes(FileNameIn);
            }
            else
            {
                return File.ReadAllBytes(FileNameOut);
            }
        }

    }
}
