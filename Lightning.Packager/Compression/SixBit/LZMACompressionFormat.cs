using NuCore.Utilities;
using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lightning.Core.Packaging
{
    public class LZMACompressionFormat : CompressionFormat
    {
        public override byte[] Compress(byte[] Bytes)
        {
            throw new NotImplementedException("Not supported by current implementation of 7zip API/LZMA SDK");
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

            return File.ReadAllBytes(FileNameOut);
        }

        public override byte[] Decompress(byte[] Bytes)
        {
            throw new NotImplementedException("Not supported by current implementation of 7zip API/LZMA SDK");
        }

        public override byte[] DecompressFile(string FileNameIn, string FileNameOut = null)
        {
            SevenZip.Compression.LZMA.Decoder LZMADecoder = new SevenZip.Compression.LZMA.Decoder();

            using (FileStream In = new FileStream(FileNameIn, FileMode.Open))
            {
                using (FileStream Out = new FileStream(FileNameOut, FileMode.Open))
                {

                }
            }
        }

    }
}
