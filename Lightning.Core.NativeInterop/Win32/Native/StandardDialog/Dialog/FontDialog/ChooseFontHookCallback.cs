using System;
using System.Collections.Generic;
using System.Text;
using WParam = System.UInt32;
using LParam = System.UInt32;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// ChooseFontHookCallback
    /// 
    /// June 22, 2021
    /// 
    /// Defines a callback function for redirecting and receiving window messages to the Win32 font dialog.
    /// </summary>
    public delegate UIntPtr ChooseFontHookCallback
    (
        IntPtr Param1,
        uint Param2,
        WParam Param3,
        WParam Param4
    );
}
