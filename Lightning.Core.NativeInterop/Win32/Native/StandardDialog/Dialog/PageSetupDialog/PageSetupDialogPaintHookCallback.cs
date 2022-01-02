#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;
using WParam = System.UInt32;
using LParam = System.UInt32;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// PageSetupDialogPaintHookCallback
    /// 
    /// June 23, 2021
    /// 
    /// Defines a delegate that can be used to redirect window messages sent to the Page Setup dialog box for the purposes of drawing the dialog box.
    /// </summary>
    /// <returns></returns>
    public delegate UIntPtr PageSetupDialogPaintHookCallback
    (
        IntPtr Param1,
        uint Param2,
        WParam  Param3,
        LParam Param4

    );
}
#endif