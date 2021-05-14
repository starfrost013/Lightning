using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop
{
    /// <summary>
    /// StartupInfoEx
    /// 
    /// May 15, 2021 00:18
    /// 
    /// Extended startup info for creaton of processes in Win32.
    /// </summary>
    public class StartupInfoEx : StartupInfo
    {
        public IntPtr lpAttributeList { get; set; }
    }
}
