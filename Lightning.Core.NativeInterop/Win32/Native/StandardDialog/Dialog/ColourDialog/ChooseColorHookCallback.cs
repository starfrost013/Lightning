#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;
using WParam = System.UInt32;
using LParam = System.UInt32;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// ChooseColorHookCallback
    /// 
    /// June 20, 2021
    /// 
    /// Callback delegate for Colour dialog. Used to receive Win32 window messages instead of the Colour dialog box receiving them.
    /// 
    /// (Param names are Microsoft's fault not mine)
    /// </summary>
    /// <param name="Param1">A Handle to the colour dialog box for which the Win32 window message is intended.</param>
    /// <param name="Param2">The identifier of the Win32 window message being received.</param>
    /// <param name="Param3">Additional information about the message to be received. Depends on Param2.</param>
    /// <param name="Param4">Further additional information about the message. The exact meaning depends on the value of the unnamedParam2 parameter (context-specific). If the unnamedParam2 parameter indicates the WM_INITDIALOG message, then unnamedParam4 is a pointer to a CHOOSECOLOR structure containing the values specified when the dialog was created.</param>
    /// <returns>A UIntPtr containing the result of the delegate. If it is 0, your custom delegate is processing window messages. If it is not, an error occurred.</returns>
    public delegate UIntPtr ChooseColorHookCallback(
            IntPtr Param1,
            uint Param2,
            WParam Param3,
            LParam Param4
        );
}
#endif