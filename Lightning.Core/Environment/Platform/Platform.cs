using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core
{
    public static class Platform
    {
        public static Platforms GetPlatforms()
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
