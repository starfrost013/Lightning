#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// COMIIDS
    /// 
    /// June 23, 2021
    ///
    /// Defines COM IIDs for Win32 COM APIs.
    /// </summary>
    public static class COMIIDs
    {
        /// <summary>
        /// IID for the IFileDialog COM interface
        /// </summary>
        public static string COM_IID_IFileDialog = "42f85136-db7e-439c-85f1-e4075d135fc8";
    }
}
#endif
