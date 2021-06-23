#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;
using WParam = System.UInt32;
using LParam = System.UInt32;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// Delegate used for redirecting window messages to the Print Setup dialog box.
    /// </summary>
    /// <param name="Param1"></param>
    /// <param name="Param2"></param>
    /// <param name="Param3"></param>
    /// <param name="Param4"></param>
    /// <returns></returns>
    public delegate UIntPtr PrintSetupDialogHookCallback
    (
        IntPtr Param1,
        uint Param2,
        WParam Param3,
        LParam Param4
    );
}
#endif