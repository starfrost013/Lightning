using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; 
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

                if (TByteList.Count - i > 1)
                {
                    byte NextByte = TByteList[i + 1];

                    // get the two least significant bits (x86, little endian)
                    bool Bit0 = NextByte.GetBit(0); 
                    bool Bit1 = NextByte.GetBit(1);

                    byte D0 = 0;

                    if (!Bit0 && !Bit1) D0 = 0;
                    if (!Bit0 && Bit1) D0 = 1;
                    if (Bit0 && !Bit1) D0 = 2;
                    if (Bit0 && Bit1) D0 = 3;

                    // for fuck sake C#
                    // compiler was designed by fucking UVF nazis
                    byte Final = (byte)((byte)(TByteList[i + 1] << 2) + D0);

                    FByteList.Add(Final);
                }
                else
                {
                    byte Final = (byte)(TByteList[i] << 2);

                    FByteList.Add(Final);

                }

            }

            return FByteList.ToArray();

        }

        public override byte[] CompressFile(string FileNameIn, string FileNameOut = null)
        {
            try
            {
                byte[] OldFileBytes = File.ReadAllBytes(FileNameIn);

                // we already know it exists
                File.Delete(FileNameIn);

                byte[] FileBytes = Compress(OldFileBytes);

#if DEBUG

                int OldFileByteLength = OldFileBytes.Length;
                int NewFileByteLength = FileBytes.Length;

                // will be complled out by default on release anyway
                Debug.Assert(OldFileByteLength < NewFileByteLength, $"Compression format is doing opposite of what is intended! (Old size {OldFileByteLength}, new size {NewFileByteLength}!)");
#endif

                if (FileNameOut == null)
                {
                    File.WriteAllBytes(FileNameIn, FileBytes);
                }
                else
                {
                    File.WriteAllBytes(FileNameOut, FileBytes);
                }
                

                return FileBytes;
            }
            catch (FileNotFoundException)
            {
                ErrorManager.ThrowError(FileNameIn, "6BitCompressionFileNotFoundException", $"Six Bit compression error: File at {FileName} was not found.");
                return null; 
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowError(FileNameIn, "6BitCompressionErrorException", $"Six Bit compression error: Error compressing {FileName}\n\n{ex}");
                return null;
            }
        }

        public override byte[] Decompress(byte[] Bytes)
        {
            for (int i = 0; i < Bytes.Length; i++)
            {
                byte CurByte = Bytes[i];

                if (Bytes.Length - i > 1)
                {
                    byte NextByte = Bytes[i + 1];

                    bool Bit6 = CurByte.GetBit(6);
                    bool Bit7 = CurByte.GetBit(7);
                }
            }
        }
    }
}
