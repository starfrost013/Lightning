using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq; 
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
                    TByteList.Add(0x3E);
                    TByteList.Add(0x3E);
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
                if (FileNameOut == null) File.Delete(FileNameIn);

                List<byte> FileBytesT = new List<byte>();

                // weird little workaround for this
                if (PathUtil.IsTextFile(FileNameIn))
                {
                    FileBytesT = Compress(OldFileBytes).ToList();
                }
                else
                {
                    FileBytesT = OldFileBytes.ToList();
                }

                byte[] FileBytes = FileBytesT.ToArray();
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
                ErrorManager.ThrowError(FileNameIn, "6BitCompressionFileNotFoundException", $"Six Bit compression error: File at {FileNameIn} was not found.");
                return null; 
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowError(FileNameIn, "6BitCompressionErrorException", $"Six Bit compression error: Error compressing {FileNameIn}\n\n{ex}");
                return null;
            }
        }

        public override byte[] Decompress(byte[] Bytes)
        {
            List<byte> DecompressedBytes = new List<byte>();

            for (int i = 0; i < Bytes.Length; i++)
            {
                // no idea if this works
                byte CurByte = Bytes[i];

                byte NextByte = Bytes[i + 1];

                bool Bit6 = CurByte.GetBit(6);
                bool Bit7 = CurByte.GetBit(7);

                byte D0 = 0;

                if (!Bit6 && Bit7) D0 = 1;
                if (Bit6 && !Bit7) D0 = 2;
                if (Bit6 && Bit7) D0 = 3;

                byte FinalByte = (byte)(CurByte - D0);

                FinalByte = (byte)(FinalByte >> 2);

                DecompressedBytes.Add(FinalByte);
            }

            return DecompressedBytes.ToArray();
        }

        public override byte[] DecompressFile(string FileNameIn, string FileNameOut = null)
        {
            try
            {
                byte[] CompressedInfo = File.ReadAllBytes(FileNameIn);

                if (FileNameOut == null) File.Delete(FileNameIn);

                byte[] DecompressedInfo = Decompress(CompressedInfo);

                if (FileNameOut == null)
                {
                    File.WriteAllBytes(FileNameIn, DecompressedInfo);
                }
                else
                {
                    File.WriteAllBytes(FileNameOut, DecompressedInfo);
                }

                return DecompressedInfo; 
            }
            catch (FileNotFoundException)
            {
                ErrorManager.ThrowError(FileNameIn, "6BitCompressionFileNotFoundException", $"Six Bit decompression error: File at {FileNameIn} was not found.");
                return null;
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowError(FileNameIn, "6BitCompressionErrorException", $"Six Bit decompression error: Error compressing {FileNameIn}\n\n{ex}");
                return null;
            }
        }
    }
}
