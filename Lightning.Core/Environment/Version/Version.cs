using System;
using System.Collections.Generic;
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
        public static string BuildOwner { get; set; }
        public static DateTime BuildDate { get; set; }
        public static string GCHash { get; set; }
        public static int Major { get; set; }
        public static int Minor { get; set; }
        public static int Revision { get; set; }

        public static void LoadVersion()
        {
            //BuildOwner = Resources
        }

    }
}
