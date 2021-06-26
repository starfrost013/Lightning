using Lightning.Core.NativeInterop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core
{

    /// <summary>
    /// Platform
    /// 
    /// March 2, 2021 (possibly from Emerald) (modified June 26, 2021: major update to add platform versions)
    /// 
    /// Defines the current platform Lightning is running on. A platform may be anything supported by .NET Core 3.1 / .NET 5.0,
    /// so various Linuxes, Windows 7, 8.1, 10 1607+, or 11, and MacOS 10.13+ for x86-64.
    /// </summary>
    public static partial class Platform
    {

        public static Platforms PlatformName { get; set; }
        public static PlatformVersion Version { get; set; }

        /// <summary>
        /// Populates the Platform information.
        /// </summary>
        public static void PopulatePlatformInformation()
        {
            PlatformName = GetPlatformIdentifier();
            Version = PlatformVersionAcquirer.GetPlatformVersion(); // move to method? perhaps
        }

        private static Platforms GetPlatformIdentifier()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                switch (RuntimeInformation.OSArchitecture)
                {
                    case Architecture.X86:
                        return Platforms.Win32;
                    case Architecture.X64:
                        return Platforms.Win64;
                    case Architecture.Arm:
                        return Platforms.WinARM32;
                    case Architecture.Arm64:
                        return Platforms.WinARM64;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                switch (RuntimeInformation.OSArchitecture)
                {
                    case Architecture.X86:
                        return Platforms.MacOS64;
                    case Architecture.Arm64:
                        return Platforms.MacOSARM64;
                }
            }
            else
            {
                switch (RuntimeInformation.OSArchitecture)
                {
                    case Architecture.X86:
                        return Platforms.Linux32;
                    case Architecture.X64:
                        return Platforms.Linux64;
                    case Architecture.Arm:
                        return Platforms.LinuxARM32;
                    case Architecture.Arm64:
                        return Platforms.LinuxARM64;
                }
            }

            return Platforms.Win32; 
        }
    }
}
