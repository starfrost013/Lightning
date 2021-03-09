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

            string ProductVersion = FVI.FileVersion;

            string[] Version = ProductVersion.Split('.');

            if (Version.Length != 4)
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

            string BuildDatePath = Properties.Resources.BuildDate;
            string OwnerPath = Properties.Resources.BuildInformation;


            BuildDate = DateTime.Parse(BuildDatePath);
            Owner = OwnerPath;

            return; 

        }

        public static string GetVersionString()
        {
            if (BuildDate == null
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
