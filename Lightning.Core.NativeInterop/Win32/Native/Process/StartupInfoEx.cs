#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop
{
    /// <summary>
    /// StartupInfoEx
    /// 
    /// May 15, 2021 00:18 (modified June 20, 2021: ifdef)
    /// 
    /// Extended startup info for creaton of processes in Win32.
    /// </summary>
    public class StartupInfoEx : StartupInfo
    {
        public IntPtr lpAttributeList { get; set; }
    }
}
#endif