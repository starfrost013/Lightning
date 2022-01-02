#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;
using WParam = System.UInt32;
using LParam = System.UInt32;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// Delegate used for changing the sources of window messages in the Win32 open file dialog 
    /// </summary>
    /// <param name="Param1"></param>
    /// <param name="Param2"></param>
    /// <param name="Param3"></param>
    /// <param name="Param4"></param>
    /// <returns></returns>
    public delegate IntPtr OpenFileDialogHookCallback
        (
            IntPtr Param1,
            uint Param2,
            WParam Param3,
            LParam Param4
        );

}
#endif