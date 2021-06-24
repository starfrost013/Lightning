using Lightning.Core.NativeInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Platform [Detect Platform Version]
    /// 
    /// June 24, 2021
    /// 
    /// Detects the Platform version. 
    /// This will allow us to enable tweaks based on the exact OS version we are running in future.
    /// </summary>
    public static partial class Platform
    {
        public static PlatformVersion GetPlatformVersion() => PlatformVersionAcquirer.GetPlatformVersion();

    }
}
