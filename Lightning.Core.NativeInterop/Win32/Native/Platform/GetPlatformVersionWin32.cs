#if WINDOWS
using Microsoft.Win32; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; 
using System.Text;

namespace NuCore.NativeInterop
{
    /// <summary>
    /// GetPlatformVersion
    /// 
    /// June 24, 2021 (modified June 26, 2021: add error handling)
    /// 
    /// Win32 implementation of the PlatformVersionAcquirer class.
    /// As we are currently running on .NET Core 3.1 (meaning that Environment.OSVersion will ALWAYS return
    /// 6.2.9200 on 8.1+ -- rewrite this when we upgrade to .NET 5) and we can't hit the registry without requiring Microsoft.Win32,
    /// we will hit a random OS component ALWAYS in the PATH AND present in all OSes (%SYSTEMROOT%\System32\rundll32.exe) and query its file versioning 
    /// information in order to obtain the operating system information. 
    /// 
    /// This is completely retarded. Too bad!
    /// </summary>
    public class PlatformVersionAcquirer
    {
        public static PlatformVersion GetPlatformVersion()
        {
            try
            {
                PlatformVersion Version = new PlatformVersion();

                string SysPath = Path.GetPathRoot(Environment.SystemDirectory);
                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo($@"{SysPath}\Windows\system32\rundll32.exe");

                int WindowsBuildNumber = FVI.FileBuildPart;

                Version.OSBuildNumber = WindowsBuildNumber;

                if (WindowsBuildNumber == 7601)
                {
                    Version.OSBrandName = "Windows 7";
                    Version.OSUpdateVersion = "SP1";

                    return Version;
                }
                else
                {
                    if (WindowsBuildNumber == 9200)
                    {
                        Version.OSBrandName = "Windows 8";
                        Version.OSUpdateVersion = "RTM";

                        return Version;
                    }
                    else
                    {
                        if (WindowsBuildNumber == 9600)
                        {
                            Version.OSBrandName = "Windows 8.1";

                            int WindowsVersionDelta = FVI.FilePrivatePart;

                            if (WindowsVersionDelta >= 17031)
                            {
                                Version.OSUpdateVersion = "Update 1/S14";
                            }
                            else
                            {
                                Version.OSUpdateVersion = "RTM";
                            }

                            return Version;

                        }
                        else
                        {
                            if (WindowsBuildNumber >= 9650 && WindowsBuildNumber < 14393)
                            {
                                Version.OSBrandName = "Windows 10";
                                Version.OSUpdateVersion = "Threshold 1/2/Pre-release Redstone 1 (should be unsupported?)";

                                return Version;
                            }
                            else
                            {
                                if (WindowsBuildNumber > 14393 && WindowsBuildNumber <= 21390)
                                {
                                    Version.OSBrandName = "Windows 10";
                                    Version.OSUpdateVersion = "Redstone 1 to Cobalt (pre-Sun Valley)";

                                    return Version;
                                }
                                else
                                {
                                    if (WindowsBuildNumber > 21664 && WindowsBuildNumber <= 21691)
                                    {
                                        Version.OSBrandName = "Windows 10?";
                                        Version.OSUpdateVersion = "co_refresh Wave 1";
                                        return Version;
                                    }
                                    else if (WindowsBuildNumber >= 21990 && WindowsBuildNumber <= 22000)
                                    {
                                        Version.OSBrandName = "Windows 11";
                                        Version.OSUpdateVersion = "Cobalt Wave 1";
                                        return Version;
                                    }
                                    else if (WindowsBuildNumber >= 22114 && WindowsBuildNumber <= 22350)
                                    {
                                        Version.OSBrandName = "Windows 11";
                                        Version.OSUpdateVersion = "Cobalt Wave 2";
                                        return Version;
                                    }
                                    else
                                    {
                                        Version.OSBrandName = "Windows 11";
                                        Version.OSUpdateVersion = "Nickel/22H2?";
                                        return Version;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException err)
            {
                // cannot throw errors at this stage 
                // so just return an error string

                PlatformVersion Version = new PlatformVersion();
#if DEBUG
                Version.OSBrandName = $"Error acquiring platform information - {err}";
                Version.OSBuildNumber = 0x0000DEAD;
                Version.OSUpdateVersion = $"Error acquiring platform information - {err}";
#else
                
                Version.OSBrandName = $"Error acquiring platform information";
                Version.OSBuildNumber = 0x0000DEAD;
                Version.OSUpdateVersion = $"Error acquiring platform information";
#endif

                return Version; 
            }



        }
    }
}
#endif