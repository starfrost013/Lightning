#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// OpenFileDialog
    /// 
    /// July 14, 2021
    /// 
    /// Defines an actually nice API for using the W32 common Open File Dialog.
    /// </summary>
    public class OpenFileDialog
    {
        /// <summary>
        /// The filename. Automatically filled in by the API.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Filter to use
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determine if a
        /// </summary>
        public bool ReadOnly { get; set; }
        /// <summary>
        /// Gets or sets a boolean that determines if the user will be prompted on overwrite. Alternative to <see cref="Flags"/>.  Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool UseOverwritePrompt { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determinesif multi-selection functionality will be available. Alternative to <see cref="Flags"/>. Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool Multiselect { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determines if the file to be saved must exist. Alternative to <see cref="Flags"/>. Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool FileMustExist { get; set; }

        /// <summary>
        /// Gets or sets a boolean that determines if the path to be saved must exist. Alternative to <see cref="Flags"/>. Will be disregarded if <see cref="Flags"/> is set.
        /// </summary>
        public bool PathMustExist { get; set; }

        /// <summary>
        /// The Win32 API flags for this dialog. See <see cref="OpenFileDialogFlags"/>. Used for advanced configuration.
        /// </summary>
        public OpenFileDialogFlags Flags { get; set; }

        /// <summary>
        /// The Win32 Extended API flags for this dialog. See <see cref="OpenFileDialogFlags"/>. Used for advanced configuration.
        /// </summary>
        public OpenFileDialogFlagsEx FlagsEx { get; set; }

        public void Show() => DoShow(IntPtr.Zero);
        public void ShowDialog(IntPtr HWND) => DoShow(HWND);

        private void DoShow(IntPtr HWND)
        {
            OpenFileName OFD = new OpenFileName();

            OFD.HwndOwner = HWND;
            OFD.HInstance = IntPtr.Zero;

            if (Flags != OpenFileDialogFlags.OFN_NONE)
            {
                OFD.Flags = Flags; 
            }
            else
            {
                if (UseOverwritePrompt) OFD.Flags += 2;
                if (Multiselect) OFD.Flags += 0x200;
                if (FileMustExist) OFD.Flags += 0x800;
                if (PathMustExist) OFD.Flags += 0x1000;

                OFD.LPFileNameLength = 32767; // Max for ANSI
                OFD.
                

            }
            

        }


    }
}
#endif