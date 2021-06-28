#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    public struct ChooseFont // struct for marshalling
    {
        public int LStructSize;

        /// <summary>
        /// HWND of a parent window for the Choose Font dialog.
        /// </summary>
        public IntPtr HWNDOwner;

        /// <summary>
        /// Pointer to a HDC
        /// </summary>
        public IntPtr HDC;
        public IntPtr LPLogFont;
        public int IFontPointSize;
        public int Flags;
        public int RGBColours;
        public IntPtr LCustomData;
        public ChooseColorHookCallback LPHookCallback;
        public IntPtr HInstance;
        string LPSZStyle;
        string NFontType;
        private string __MISSING_ALIGNMENT__;
        public int NSizeMin;
        public int NSizeMax;
    }
}
#endif