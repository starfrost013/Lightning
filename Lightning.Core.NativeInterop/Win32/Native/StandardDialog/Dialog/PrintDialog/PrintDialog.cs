#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// PrintDialog 
    /// 
    /// June 21, 2021
    ///
    /// Defines parameters for the Win32 common print dialog
    /// </summary>
    public struct PrintDialog
    {
        public int LStructSize;

        /// <summary>
        /// Optional HWND of a parent window for the print dialog
        /// </summary>
        public IntPtr HwndOwner;
        public IntPtr HDevMode;
        public IntPtr HDevNames;
        public IntPtr HDC;

        [MarshalAs(UnmanagedType.U4)]
        public PrintDialogFlags Flags;

        public short NFromPage;
        public short NToPage;
        public short NMinPage;
        public short NMaxPage;
        public short NCopies;
        public IntPtr HInstance;
        public IntPtr HCustomData;
        public PrintDialogHookCallback LPFNPrintDelegate;
        public PrintSetupDialogHookCallback LPFNSetupDelegate;
        public string LPPrintTemplateName;
        public string LPSetupTemplateName;
        public IntPtr HPrintTemplate;
        public IntPtr HSetupTemplate;

    }
}
#endif