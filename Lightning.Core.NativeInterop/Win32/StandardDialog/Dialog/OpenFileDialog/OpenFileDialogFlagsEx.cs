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
        /// If set, the Places Bar will not be displayed in the enum. 
        /// </summary>
        OFN_EX_NOPLACESBAR = 0x1
    }
}
