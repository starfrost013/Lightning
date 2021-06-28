using System;
using System.Collections.Generic;
using System.Text;
using LParam = System.IntPtr; 

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// OpenFileName
    /// 
    /// June 20, 2021
    /// 
    /// Defines settings and flags for the Open File Dialog
    /// </summary>
    public struct OpenFileName // struct for marshaling purposes
    {
        /// <summary>
        /// Size of this structure [INTERNAL] 
        /// </summary>
        public int LStructSize;
        /// <summary>
        /// HWND of the window that owns this Open File Dialog.
        /// </summary>
        public IntPtr HwndOwner;
        public IntPtr HInstance;

        /// <summary>
        /// Filter for the file types to be used - equivalent to Microsoft.Win32.OpenFileDialog.Filter
        /// </summary>
        public string LPFilter;
        public string LPCustomFilter;
        public int LPCustomFilterLength;
        public int StartFilterIndex;
        public string LPFileName;
        public int LPFileNameLength;
        public string LPFileTitle;
        public int LPFileTitleLength;
        public string LPInitialDirectory;
        public string LPDialogTitle;
        public OpenFileDialogFlags Flags;
        public short NFileOffset;
        public short NFileExtension;
        public string LPStrDefinedExtension;
        public LParam LCustomData;
        public OpenFileDialogHookCallback LPFileNameHook;
        public string LPTemplateName;
        
        /// <summary>
        /// Reserved - do not use!
        /// </summary>
        public IntPtr PVReserved;

        /// <summary>
        /// Reserved - do not use!
        /// </summary>
        public int DWReserved;

        public OpenFileDialogFlagsEx FlagsEx;

    }
}
