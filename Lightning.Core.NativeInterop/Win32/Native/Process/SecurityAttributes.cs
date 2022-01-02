#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop
{
    /// <summary>
    /// SECURITY_ATTRIBUTES
    /// 
    /// May 14, 2021
    /// 
    /// Win32 security attributes class for Polaris.
    /// </summary>
    public class SecurityAttributes
    {
        /// <summary>
        /// Length of this structure.
        /// </summary>
        int nLength { get; set; }
        IntPtr lpSecurityDescriptor { get; set; }
        int bInheritHandle { get; set; }
    }
}
#endif