using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection; 
using System.Resources;
using System.Runtime; 
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// 2021-03-03  Lightning (Continuous Deployment)
    /// 
    /// Version Information
    /// </summary>
    public static class Version
    {
        public static string Branch { get; set; }
        public static string Owner { get; set; }
        public static DateTime BuildDate { get; set; }
        public static string GCHash { get; set; }
        public static int Major { get; set; }
        public static int Minor { get; set; }
        public static int Build { get; set; }
        public static int Revision { get; set; }

        /// <summary>
        /// 2020-03-08
        /// </summary>
        public static void LoadVersion()
        {
            Assembly CurAssembly = Assembly.GetExecutingAssembly();
            string CurAsmLocation = CurAssembly.Location;

            FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            string ProductVersion = FVI.ProductVersion;

            string[] Version = ProductVersion.Split(',');

            if (Version.Length != 3)
            {
                // TODO - THROW ERROR - WHEN ERRORS.XML EXISTS

                return; 
                // TODO - THROW ERROR
            }
            else
            {
                try
                {
                    Major = Convert.ToInt32(Version[0]);
                    Minor = Convert.ToInt32(Version[1]);
                    Build = Convert.ToInt32(Version[2]);
                    Revision = Convert.ToInt32(Version[3]);
                }
                catch (FormatException)
                {
                    // TODO - THROW ERROR
                    return; 
                }
            }

            string BuildDatePath = "Lightning.Core.BuildDate.txt";
            string OwnerPath = "Lightning.Core.BuildInformation.txt";

            using (StreamReader SR = new StreamReader(CurAssembly.GetManifestResourceStream(BuildDatePath)))
            {
                if (SR.BaseStream.Position - SR.BaseStream.Length < 1)
                {
                    // TODO - ERRORS.XML - THROW ERROR
                    return;
                }

                BuildDatePath = SR.ReadLine();
            }

            using (StreamReader SR = new StreamReader(CurAssembly.GetManifestResourceStream(OwnerPath)))
            {
                if (SR.BaseStream.Position - SR.BaseStream.Length < 1)
                {
                    // TODO - ERRORS.XML - THROW ERROR
                    return;
                }

                Owner = SR.ReadLine();
            }

        }

        public static string GetVersionString()
        {
            if (Branch == null
                || Owner == null
                )
            {
                return null; // TEMP: ERRORS.XML!!!
            }
            else
            {
                return $"v{Major}.{Minor}.{Build}.{Revision} - built at {BuildDate} by {Owner}.";
            }

        }

    }
}
