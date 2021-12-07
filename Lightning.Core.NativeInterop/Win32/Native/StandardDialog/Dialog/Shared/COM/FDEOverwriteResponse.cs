#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// FDEOverwriteResponse
    /// 
    /// June 26, 2021
    /// 
    /// Defines responses to a file overwrite request.
    /// </summary>
    public enum FDEOverwriteResponse
    {
        /// <summary>
        /// The application does not handle the event - display a dialog and ask the user
        /// </summary>
        FDEOR_DEFAULT = 0,

        /// <summary>
        /// The file should be overwritten.
        /// </summary>
        FDEOR_ACCEPT = 1,
        
        /// <summary>
        /// The file should not be overwritten.
        /// </summary>
        FDEOR_REFUSE = 2
    }
}
#endif