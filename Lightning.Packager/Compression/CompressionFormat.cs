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

        public virtual byte[] CompressFile(string FileName)
        {
            throw new NotImplementedException(); 
        }

        public virtual byte[] Compress(byte[] Bytes) => Compress(Bytes.ToList());
    }
}
