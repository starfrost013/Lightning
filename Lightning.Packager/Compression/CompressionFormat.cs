using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lightning.Core.Packaging
{
    public class CompressionFormat
    {
        public virtual byte[] Compress(List<byte> Bytes)
        {
            throw new NotImplementedException();
        }

        public virtual byte[] CompressFile(string FileNameIn, string FileNameOut = null)
        {
            throw new NotImplementedException(); 
        }

        public virtual byte[] Compress(byte[] Bytes) => Compress(Bytes.ToList());

        public virtual byte[] Decompress(List<byte> Bytes)
        {
            throw new NotImplementedException();
        }

        public virtual byte[] DecompressFile(string FileNameIn, string FileNameOut = null)
        {
            throw new NotImplementedException();
        }

        public virtual byte[] Decompress(byte[] Bytes) => Compress(Bytes.ToList());

    }
}
