using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// Lightning Native Interop (Win32)
    /// 
    /// March 5, 2021 (v1.0.10)
    /// 
    /// An enum that contains Win32 errors for Win32 interop.
    /// 
    /// I didn't want to include an 800KB binary in an engine that is supposed to be small
    /// and elegant, so we only have those errors that are necessary.
    /// </summary>
    public enum Win32Errors
    {
        Successful = 0,

        AccessDenied = 5
    }
}
