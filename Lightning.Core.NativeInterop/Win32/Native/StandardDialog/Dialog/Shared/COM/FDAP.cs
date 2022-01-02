#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// FDAP
    /// 
    /// June 26, 2021
    /// 
    /// List placement for file dialogs in COM/Win32
    /// </summary>
    public enum FDAP
    {
        FDAP_BOTTOM = 0,

        FDAP_TOP = 1

    }
}
#endif