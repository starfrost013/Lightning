#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// StandardDialogType
    /// 
    /// June 20, 2021
    /// 
    /// Enumeration for standard dialogs for Win32 (probably should have used COM, but oh well...)
    /// </summary>
    public enum StandardDialogType
    {
        /// <summary>
        /// Win32 open file dialog
        /// </summary>
        OpenFileDialog = 0,

        /// <summary>
        /// Win32 save file dialog
        /// </summary>
        SaveFileDialog = 1,

        /// <summary>
        /// Win32 open folder dialog
        /// </summary>
        OpenFolderDialog = 2,

        /// <summary>
        /// Win32 find and replace dialog
        /// </summary>
        FindAndReplace = 3,

        /// <summary>
        /// Win32 print dialog
        /// </summary>
        Print = 4,

        /// <summary>
        /// Win32 page setup dialog
        /// </summary>
        PageSetup = 5,

        /// <summary>
        /// Win32 font dialog
        /// </summary>
        Font = 6,

        /// <summary>
        /// Win32 colour dialog
        /// </summary>
        Color = 7


    }
}
#endif