#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NuCore.NativeInterop.Win32
{

    /// <summary>
    /// SHEnumerationItems
    /// 
    /// June 26, 2021
    /// 
    /// Implements the SHCONTF enum in .NET (renamed for clarity)
    /// </summary>
    public enum SHEnumerationItems
    {
        SHCONTF_CHECKINGFORCHILDREN = 0x10,

        SHCONTF_FOLDERS = 0x20,

        SHCONTF_NONFOLDERS = 0x40,

        SHCONTF_INCLUDEHIDDEN = 0x80,

        SHCONTF_INIT_ON_FIRST_NEXT = 0x100,

        SHCONTF_NETPRINTERSRCH = 0x200,

        SHCONTF_SHAREABLE = 0x400,

        SHCONTF_STORAGE = 0x800,

        SHCONTF_NAVIGATION_ENUM = 0x1000,

        SHCONTF_FASTITEMS = 0x2000,

        SHCONTF_FLATLIST = 0x4000,

        SHCONTF_ENABLE_ASYNC = 0x8000,

        SHCONTF_INCLUDESUPERHIDDEN = 0x10000
    }
}
#endif