using NuCore.Utilities; 
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

        /// <summary>
        /// The major version of this build.
        /// </summary>
        public static int Major { get; set; }

        /// <summary>
        /// The minor version of this build.
        /// </summary>
        public static int Minor { get; set; }

        /// <summary>
        /// The build number of this build.
        /// </summary>
        public static int Build { get; set; }

        /// <summary>
        /// The revision number of this build - use only if a release or build was bungled, and a quick fix required
        /// </summary>
        public static int Revision { get; set; }

        /// <summary>
        /// Internally used to determine if the version information is loaded.
        /// </summary>
        private static bool IsLoaded { get; set; }

        /// <summary>
        /// 2020-03-08
        /// </summary>
        public static void LoadVersion()
        {
            Assembly CurAssembly = Assembly.GetEntryAssembly();
            string CurAsmLocation = CurAssembly.Location;

            FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(CurAsmLocation);

            string ProductVersion = FVI.FileVersion;

            ProductVersion = ProductVersion.Trim(); 

            string[] Version = ProductVersion.Split('.');

            if (Version.Length != 4)
            {
                Console.WriteLine($"Invalid version information (not 4 components!)");
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
                    Console.WriteLine($"Invalid version information (all components must be numbers!)");
                    //ErrorManager.ThrowError("Engine Version Identifier", "InvalidVersionInformationException");
                    return; 
                }
            }

            string BuildDatePath = Properties.Resources.BuildDate;
            string OwnerPath = Properties.Resources.BuildInformation;

            if (BuildDatePath == null
            || BuildDatePath.Length == 0) BuildDatePath = "Failed to load build date - it is either not present or empty in Lightning.Core.resources!";

            if (OwnerPath == null
            || OwnerPath.Length == 0) OwnerPath = "Failed to load build owner information - it is either not present or empty in Lightning.Core.resources!";

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
                || !IsLoaded )
            {
                return null;
            }
            else
            {
#if DEBUG
                return $"v{Major}.{Minor}.{Build}.{Revision} - built at {BuildDate} by {Owner}";
#else
                return $"v{Major}.{Minor}.{Build}.{Revision} - built at {BuildDate} by {Owner}";
#endif
            }

        }

    }
}
