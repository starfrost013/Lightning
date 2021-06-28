#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{

    /// <summary>
    /// PageSetupDialog
    /// 
    /// June 21, 2021 (modified June 23, 2021) 
    /// 
    /// Defines a class used to control the properties and behaviour of the Win32 Page Setup Dialog.
    /// </summary>
    public struct PageSetupDialog // struct for marshalling
    {
        /// <summary>
        /// Size of this structure.
        /// </summary>
        public int LStructSize;

        /// <summary>
        /// HWND for an optional owner window for the Page Setup Dialog.
        /// </summary>
        public IntPtr HwndOwner;

        public IntPtr HDevMode;

        public IntPtr HDevNames;

        public PageSetupDialogFlags Flags;

        public Win32Point PTPaperSize;

        public Win32Rect RTMinMargin;

        public Win32Rect RTMargin;

        public IntPtr HInstance;

        public int LCustData;

        public PageSetupDialogSetupHookCallback PaintHookCallbackDelegate;

        public PageSetupDialogPaintHookCallback SetupHookCallbackDelegate;

        public IntPtr LPPageSetupTemplateName;

        public IntPtr HPageSetupTemplate;
    }
}
#endif