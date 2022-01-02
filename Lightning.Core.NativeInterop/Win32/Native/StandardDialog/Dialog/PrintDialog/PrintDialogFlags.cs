#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// PrintDialogFlags
    /// 
    /// June 21, 2021
    /// 
    /// Flags for the Win32 print dialog.
    /// </summary>
    public enum PrintDialogFlags
    {
        PD_ALLPAGES = 0,

        PD_SELECTION = 0x1,

        PD_NOSELECTION = 0x4,

        PD_NOPAGENUMS = 0x8,

        PD_COLLATE = 0x10,

        PD_PRINTTOFILE = 0x20,

        PD_PRINTSETUP = 0x40,

        PD_NOWARNING = 0x80,

        PD_RETURNDC = 0x100,

        PD_RETURNIC = 0x200,

        PD_RETURNDEFAULT = 0x400,

        PD_SHOWHELP = 0x800,

        PD_ENABLEPRINTHOOK = 0x1000,

        PD_ENABLESETUPHOOK = 0x2000,
        
        PD_ENABLEPRINTTEMPLATE = 0x4000,

        PD_ENABLESETUPTEMPLATE = 0x8000,

        PD_ENABLEPRINTTEMPLATEHANDLE = 0x10000,

        PD_ENABLESETUPTEMPLATEHANDLE = 0x20000,

        PD_USEDEVMODECOPIES = 0x40000,

        PD_USEDEVMODECOPIESANDCOLLATE = 0x40000,

        PD_DISABLEPRINTTOFILE = 0x80000,
        
        PD_HIDEPRINTTOFILE = 0x100000,

        PD_NONETWORKBUTTON = 0x200000,
    }
}
#endif