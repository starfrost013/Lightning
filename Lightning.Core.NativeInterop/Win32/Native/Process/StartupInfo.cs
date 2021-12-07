#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop
{
    public class StartupInfo
    {
        public int cb { get; set; }

        /// <summary>
        /// Reserved
        /// </summary>
        public string lpReserved { get; set; }

        /// <summary>
        /// The name of this desktop.
        /// </summary>
        public string lpDesktop { get; set; }

        /// <summary>
        /// The window title of this window.
        /// </summary>
        public string lpTitle { get; set; }

        /// <summary>
        /// The X position of this window.
        /// </summary>
        public int dwX { get; set; }

        /// <summary>
        /// The Y position of this window.
        /// </summary>
        public int dwY { get; set; }

        /// <summary>
        /// The X size of this window.
        /// </summary>
        public int dwXSize { get; set; }

        /// <summary>
        /// The Y size of this window.
        /// </summary>
        public int dwYSize { get; set; }
        public int dwXCountChars { get; set; }
        public int dwYCountChars { get; set; }
        public int dwFillAttribute { get; set; }
        public int dwFlags { get; set; }
        public short wShowWindow { get; set; }
        public short cbReserved2 { get; set; }
        public IntPtr lpReserved2 { get; set; }

        /// <summary>
        /// Pointer to the standard input for this window.
        /// </summary>
        public IntPtr hStdInput { get; set; }

        /// <summary>
        /// Pointer to the standard OUTPUT for this window.
        /// </summary>
        public IntPtr hStdOutput { get; set; }
        public IntPtr hStdError { get; set; }
    }
}
#endif