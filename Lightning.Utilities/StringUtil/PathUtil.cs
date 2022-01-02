using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace NuCore.Utilities
{
    /// <summary>
    /// PathUtil
    /// 
    /// April 10, 2021 (modified December 29, 2021: add file extension checks for LWPak)
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

        public static string GetXmlPath(string XmlPath, string Path)
        {
            if (!XmlPath.Contains(':'))
            {
                return Path;
            }
            else
            {
                string[] XmlPathComponents = XmlPath.Split('\\');

                StringBuilder SB = new StringBuilder();

                if (XmlPathComponents.Length < 1)
                {
                    return Path; // obvious relative path
                }
                else
                {
                    // Exclude the last component (the filename)
                    for (int i = 0; i < XmlPathComponents.Length - 1; i++)
                    {
                        string XmlPathComponent = XmlPathComponents[i];
                        SB.Append(XmlPathComponent);
                    }
                }

                SB.Append($@"\{Path}");

                return SB.ToString();
            }

        }

        /// <summary>
        /// Determines if a file is a text file.
        /// 
        /// Data-based check is not viable as any character may be in a text file.
        /// Doesn't cover cases such as .html.zp
        /// </summary>
        /// <param name="FileName">The file name to check</param>
        /// <returns>A boolean determining if this file has an extension indicating that it is a text file.</returns>
        public static bool IsTextFile(this string FileName) => (FileName.Contains(".lua") // Lua script
        || FileName.Contains(".txt") // Text file
        || FileName.Contains(".asc") // Text file
        || FileName.Contains(".xml") // XML file
        || FileName.Contains(".lgx") // Lightning Game Project
        || FileName.Contains(".rbxlx") // Roblox XML format
        || FileName.Contains(".ldraw") // Lego Digital Designer LDraw file
        || FileName.Contains(".xaml") // XAML (WPF/UWP/Avalonia/MAUI/Silverlight/Win10+ shell...)
        || FileName.Contains(".json") // JSON file
        || FileName.Contains(".yaml") // YAML file
        || FileName.Contains(".yml") // YAML file
        || FileName.Contains(".cs") // C# source file
        || FileName.Contains(".c") // C source file
        || FileName.Contains(".cgi") // Common Gateway Interface source file
        || FileName.Contains(".h") // C/C++ header file
        || FileName.Contains(".hpp") // C/C++ header file
        || FileName.Contains(".cpp") // C++ source file
        || FileName.Contains(".java") // Java source file
        || FileName.Contains(".jsp") // JavaServer Pages source file
        || FileName.Contains(".html") // HTML file
        || FileName.Contains(".mhtml") // MHTML (single-file) file
        || FileName.Contains(".rs") // Rust source file
        || FileName.Contains(".css") // CSS file
        || FileName.Contains(".php") // PHP source file
        || FileName.Contains(".ini") // Windows configuration file
        || FileName.Contains(".inf") // Windows setup information file
        || FileName.Contains(".bat") // Windows batch file
        || FileName.Contains(".cmd") // Windows batch file 
        || FileName.Contains(".sh") // Unix/Linux shell script
        || FileName.Contains(".service") // systemd Service
        || FileName.Contains(".rc") // Windows resource file
        || FileName.Contains(".resources") // .NET Framework / Core resource file
        || FileName.Contains(".asp") // ASP file
        || FileName.Contains(".aspx") // ASP.NET WebForms form 
        || FileName.Contains(".asmx") // ASP.NET WebService file
        || FileName.Contains(".ashx") // ASP.NET WebHandler file
        || FileName.Contains(".cshtml") // ASP.NET Razor Pages file
        || FileName.Contains(".py") // Python source file
        || FileName.Contains(".js")); // Javascript source file

        public static bool IsValidFileName(this string FileName) =>
        FileName != null &&
        (!FileName.Contains(@"\")
        || !FileName.Contains(@"/")
        || !FileName.Contains(":")
        || !FileName.Contains("*")
        || !FileName.Contains("?")
        || !FileName.Contains("\"")
        || !FileName.Contains("<")
        || !FileName.Contains(">")
        || !FileName.Contains("@"));
    }
}
