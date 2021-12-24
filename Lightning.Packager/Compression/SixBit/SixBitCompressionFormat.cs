using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.Packaging
{
    public class SixBitCompressionFormat : CompressionFormat
    {
        public override byte[] Compress(byte[] Bytes)
        {
            //Compress to 6-bit
            List<byte> TByteList = new List<byte>();
            List<byte> FByteList = new List<byte>();

            foreach (byte Byte in Bytes)
            {
                // relational pattern isn't supported in C# 8.0

                if (Byte == 0x20)
                {
                    TByteList.Add(0x00);
                }
                else
                {
                    if (Byte >= 0x30 && Byte <= 0x39)
                    {
                        TByteList.Add((byte)(Byte - 0x30));
                    }
                    else if (Byte >= 0x41 && Byte <= 0x5A)
                    {
                        TByteList.Add((byte)(Byte - 0x37));
                    }
                    else if (Byte >= 0x61 && Byte <= 0x7A)
                    {
                        TByteList.Add((byte)(Byte - 0x3D));
                    }
                    else if (Byte == 0x3C
                    || Byte == 0x3E)
                    {
                        TByteList.Add((byte)(Byte + 0x01));
                    }
                }
                  
            }

            for (int i = 0; i < TByteList.Count; i++)
            {
                byte Byte = TByteList[i];
                // get the two least significant bits (x86, little endian)
                bool Bit0 = Byte.GetBit(6); 
                bool Bit1 = Byte.GetBit(7);

                byte D0 = 0;

                if (!Bit0 && !Bit1) D0 = 0;
                if (!Bit0 && Bit1) D0 = 1;
                if (Bit0 && !Bit1) D0 = 2;
                if (Bit0 && Bit1) D0 = 3;

                // for fuck sake C#
                // compiler was designed by fucking UVF nazis
                byte Final = (byte)((byte)(TByteList[i + 1] << 2) + D0);

                if (TByteList.Count - i > 1)
                {
                    FByteList.Add(Final);
                }

            }
            


        }
    }
}
