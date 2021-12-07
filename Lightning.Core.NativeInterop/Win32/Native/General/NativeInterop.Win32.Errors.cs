#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// Lightning Native Interop (Win32)
    /// 
    /// March 5, 2021 (v1.0.10) - modified June 20, 2021
    /// 
    /// An enum that contains Win32 errors for Win32 interop.
    /// 
    /// I didn't want to include an 800KB binary in an engine that is supposed to be small
    /// and elegant, so we only have those errors that are necessary.
    /// </summary>
    public enum Win32Errors
    {
        /// <summary>
        /// The operation was successful.
        /// </summary>
        Successful = 0,
        
        /// <summary>
        /// Access denied.
        /// </summary>
        AccessDenied = 5,

        /// <summary>
        /// The process cannot access the file because it is being used by another process.
        /// </summary>
        SharingViolation = 20,

        /// <summary>
        /// Failed to open files
        /// </summary>
        OpenFailed = 110,
    }
}
#endif