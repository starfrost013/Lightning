using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning Core
    /// 
    /// Platforms
    /// 
    /// Defines the valid runtime platforms.
    /// </summary>
    public enum Platforms
    {
        /// <summary>
        /// Windows
        /// .NET Core 3.1.0 
        /// Windows 7, 8.1, 10 (version 1607; build 14393)+, or Windows 11
        /// x86-32
        /// </summary>
        Win32 = 0,

        /// <summary>
        /// Windows
        /// .NET Core 3.1.0
        /// Windows 7, 8.1, or 10.0 (version 1607; build 14393), or Windows 11
        /// x86-64
        /// </summary>
        Win64 = 1,

        /// <summary>
        /// Windows 8.1 or 10v1607+ (Build 15035?) for ARM32 (ARM64 not supported in current .net version)
        /// </summary>
        WinARM32 = 2,

        /// <summary>
        /// 64-bit ARMv8.x Windows (placeholder, futureproofing)
        /// </summary>
        WinARM64 = 3,

        /// <summary>
        /// 64-bit Mac OS 10.13+
        /// </summary>
        MacOS64 = 4,

        /// <summary>
        /// ARM64e (Apple Silicon) macOS 11.0+ (futureproofing)
        /// </summary>
        MacOSARM64 = 5,

        /// <summary>
        /// Linux x86-32 (Kernel version 4.14 or later - Alpine 3.10 / Debian 9 / Ubuntu 16.04 / Fedora 29 / CentOS 7 / RHEL 6 / openSUSE 15.1 / SLES 12 SP2 or later)
        /// </summary>
        Linux32 = 6,

        /// <summary>
        /// Linux x86-64 (Kernel version 4.14 or later - Alpine 3.10 / Debian 9 / Ubuntu 16.04 / Fedora 29 / CentOS 7 / RHEL 6 / openSUSE 15.1 / SLES 12 SP2 or later)
        /// </summary>
        Linux64 = 7,

        /// <summary>
        /// Linux ARMv7 (Kernel version 4.14 or later - Alpine 3.10 / Debian 9 / Ubuntu 16.04 / Fedora 29 / CentOS 7 / RHEL 6 / openSUSE 15.1 / SLES 12 SP2 or later)
        /// </summary>
        LinuxARM32 = 8,

        /// <summary>
        /// Linux ARMv8 (Kernel version 4.14 or later - Alpine 3.10 / Debian 9 / Ubuntu 16.04 / Fedora 29 / CentOS 7 / RHEL 6 / openSUSE 15.1 / SLES 12 SP2 or later)
        /// </summary>
        LinuxARM64 = 9
    }
}
