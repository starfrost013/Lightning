using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// PathUtil
    /// 
    /// April 10, 2021
    /// 
    /// Provides utilities surrounding paths.
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// Prepends the Lightning installation directory to <paramref name="Path"/>.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string GetLightningPath(string Path)
        {
            string LIPath = AppDomain.CurrentDomain.BaseDirectory;

            return @$"{LIPath}\{Path}";
        }

        private static string GetXmlPath(string XmlPath, string Path)
        {
            if (!XmlPath.Contains(':'))
            {
                return Path;
            }
            else
            {
                return $@"{XmlPath}\{Path}";
            }
        }
    }
}
