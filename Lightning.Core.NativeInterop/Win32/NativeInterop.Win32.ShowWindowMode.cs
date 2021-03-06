using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    public enum Win32__ShowWindow_Mode
    { 
        SW_HIDE = 0,

        SW_SHOWNORMAL = 1, 

        SW_SHOWMINIMIZED = 2, 

        SW_SHOWMAXIMIZED = 3, 

        SW_SHOWNOACTIVATE = 4, 

        SW_SHOW = 5, 

        SW_MINIMIZE = 6, 

        SW_SHOWMINNOACTIVE = 7, 

        SW_SHOWNA = 8, 

        SW_RESTORE = 9 
    } // many ways of showing windows
}
