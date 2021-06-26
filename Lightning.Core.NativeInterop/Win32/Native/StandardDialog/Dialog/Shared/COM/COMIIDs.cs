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
        /// Interface ID for the IModalWindow COM interface
        /// </summary>
        public const string COM_IID_IModalWindow = "b4db1657-70d7-485e-8e3e-6fcb5a5c1802";

        /// <summary>
        /// Interface ID for the IFileDialog COM interface
        /// </summary>
        public const string COM_IID_IFileDialog = "42f85136-db7e-439c-85f1-e4075d135fc8";

        /// <summary>
        /// Interface ID for the IShellItemEvents COM interface
        /// </summary>
        public const string COM_IID_IFileDialogEvents = "973510DB-7D7F-452B-8975-74A85828D354";

        /// <summary>
        /// Interface ID for the IShellItem COM interface
        /// </summary>
        public const string COM_IID_IShellItem = "43826d1e-e718-42ee-bc55-a1e261c37bfe";

        /// <summary>
        /// Interface ID for the IShellItemFilter COM interface
        /// </summary>
        public const string COM_IID_IShellItemFilter = "2659B475-EEB8-48b7-8F07-B378810F48CF";

    }
}
#endif
