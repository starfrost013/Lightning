#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop
{
    /// <summary>
    /// ProcessInformation
    /// 
    /// May 15, 2021 00:24
    /// 
    /// Holds information about a process [Win32]
    /// </summary>
    public class ProcessInformation
    {
        /// <summary>
        /// A pointer to the unmanaged structure of this process.
        /// </summary>
        public IntPtr hProcess { get; set; }

        /// <summary>
        /// A pointer to the unmanaged main thread of this process.
        /// </summary>
        public IntPtr hThread { get; set; }

        /// <summary>
        /// The PID of this process.
        /// </summary>
        public int dwProcessId { get; set; }

        /// <summary>
        /// The TID of the main thread of this process.
        /// </summary>
        public int dwThreadId { get; set; }
    }
}
#endif