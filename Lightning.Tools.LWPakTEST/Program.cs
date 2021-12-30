using Lightning.Core.Packaging;
using System;

namespace Lightning.Tools.LWPakTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing writing lwpak file...");
            WriteLWPakFile();

            Console.WriteLine("Testing reading lwpak file...");
            ReadLWPakFile();
        }

        private static void WriteLWPakFile()
        {
            PackageFile PF = new PackageFile();
            PF.FileName = "Test.lwpak";

            PF.AddEntry(@"Content\LWPakImage.jpg", PackageFileCompressionMode.LZMA);
            PF.AddEntry(@"Content\LWPAKSmallFile.bin", PackageFileCompressionMode.LZMA);
            PF.AddEntry(@"Content\LWPAKLargeFile.bin", PackageFileCompressionMode.LZMA);
            PF.AddEntry(@"Content\LWPakXML.xml", PackageFileCompressionMode.LZMA);

            PF.Write();
        }

        private static void ReadLWPakFile()
        {
            PackageFile PF = new PackageFile();
            PF.FileName = "Test.lwpak";
            PF.Read();
        }
    }
}
