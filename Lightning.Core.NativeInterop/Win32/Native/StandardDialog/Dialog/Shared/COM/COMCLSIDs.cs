#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// COMCLSIDS
    /// 
    /// June 24, 2021
    ///
    /// Defines COM CLSIDs for Win32 COM APIs.
    /// </summary>
    public static class COMCLSIDs
    {
        /// <summary>
        /// CLSID for the OpenFileDialog COM interface
        /// </summary>
        public const string COM_CLSID_OpenFileDialog = "DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7";
    }
}
#endif
