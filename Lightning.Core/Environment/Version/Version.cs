using Lightning.Utilities; 
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
    public static class LVersion
    {
        /// <summary>
        /// The branh of this build.
        /// </summary>
        public static string Branch { get; set; }

        /// <summary>
        /// The account and machine name of the individual who built the current Lightning build.
        /// </summary>
        public static string Owner { get; set; }

        /// <summary>
        /// The build date of this build.
        /// </summary>
        public static string BuildDate { get; set; }

        /// <summary>
        /// The Git commit hash of this build.
        /// </summary>
        public static string GCHash { get; set; }
        public static int Major { get; set; }
        public static int Minor { get; set; }
        public static int Build { get; set; }
        public static int Revision { get; set; }

        public static bool IsLoaded { get; set; }

        /// <summary>
        /// 2020-03-08
        /// </summary>
        public static void LoadVersion()
        {
            Assembly CurAssembly = Assembly.GetExecutingAssembly();
            string CurAsmLocation = CurAssembly.Location;

            FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

            string ProductVersion = FVI.FileVersion;

            ProductVersion = ProductVersion.Trim(); 

            string[] Version = ProductVersion.Split('.');

            if (Version.Length != 4)
            {
                //ERRORMANAGER needs to be no longer dependent on datamodel before this error can exist
                //ErrorManager.ThrowError("Engine Version Identifier", "InvalidVersionException");
                return; 
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
                    //ErrorManager.ThrowError("Engine Version Identifier", "InvalidVersionInformationException");
                    return; 
                }
            }

            string BuildDatePath = Properties.Resources.BuildDate;
            string OwnerPath = Properties.Resources.BuildInformation;

            BuildDatePath = BuildDatePath.RemoveDaysOfWeek();
            BuildDatePath = BuildDatePath.Trim();

            BuildDate = BuildDatePath;
            Owner = OwnerPath;

            IsLoaded = true; 
            
            return; 

        }

        public static string GetVersionString()
        {
            if (BuildDate == null
                || Owner == null
                || !IsLoaded 
                )
            {
                return null;
            }
            else
            {
                return $"v{Major}.{Minor}.{Build}.{Revision} - built at {BuildDate} by {Owner}";
            }

        }

    }
}
