#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;
using HMenu = System.IntPtr;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// HEditMenu
    /// 
    /// June 20, 2021
    /// 
    /// Internal COMDLG32 structure required for some standard dialogs
    /// </summary>
    public class HEditMenu
    {
        /// <summary>
        /// Pointer to a Win32 menu.
        /// </summary>
        public HMenu Menu { get; set; }

        /// <summary>
        /// Menu/Dialog ID for root menu item
        /// </summary>
        public short IDEdit { get; set; }

        /// <summary>
        /// Menu/Dialog ID for cut submenu item
        /// </summary>
        public short IDCut { get; set; }

        /// <summary>
        /// Menu/Dialog ID for copy submenu item
        /// </summary>
        public short IDCopy { get; set; }

        /// <summary>
        /// Menu/Dialog ID for paste submenu item
        /// </summary>
        public short IDPaste { get; set; }

        /// <summary>
        /// Menu/Dialog ID for clear submenu item
        /// </summary>
        public short IDClear { get; set; }

        /// <summary>
        /// Menu/Dialog ID for undosubmenu item
        /// </summary>
        public short IDUndo { get; set; }
    }
}
#endif