using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    public class PageSetupDialog
    {
        public int LStructSize;

        /// <summary>
        /// HWND for an optional owner window for the Page Setup Dialog.
        /// </summary>
        public IntPtr HwndOwner;

        public IntPtr HDevMode;

        public IntPtr HDevNames;

        public PageSetupDialogFlags Flags;
    }
}
