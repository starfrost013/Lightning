#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// OpenFileDialogFlagsEx
    /// 
    /// June 20, 2021
    /// 
    /// Defines extended flags for the Win32 open file dialog.
    /// </summary>
    public enum OpenFileDialogFlagsEx
    {
        /// <summary>
        /// Unofficial: used for marking none
        /// 
        /// Doing it this way is clearer than simply checking for 0.
        /// </summary>
        OFN_NONE = 0x0, 

        /// <summary>
        /// If set, the Places Bar will not be displayed in the enum. 
        /// </summary>
        OFN_EX_NOPLACESBAR = 0x1
    }
}
#endif