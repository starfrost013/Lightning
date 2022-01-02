#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// PageSetupDialogFlags
    /// 
    /// June 22, 2021
    /// 
    /// Defines flags that control the appearance and behaviour of the Page Setup common Win32 dialogs.
    /// </summary>
    public enum PageSetupDialogFlags
    {
        PSD_DEFAULTMINMARGINS = 0,
        
        PSD_INWININIINTLMEASURE = 0, // wut? win.ini? it's not 1993

        PSD_MINMARGINS = 0x1,

        PSD_MARGINS = 0x2,

        PSD_INTHOUSANDTHSOFINCHES = 0x4,

        PSD_INHUNDREDTHSOFMILLIMETRES = 0x8,
        
        PSD_DISABLEMARGINS = 0x10,
        
        PSD_DISABLEPRINTER = 0x20,

        PSD_NOWARNING = 0x80,

        PSD_DISABLEORIENTATION = 0x100,

        PSD_DISABLEPAPER = 0x200,

        PSD_ENABLEPAGESETUPHOOK = 0x2000,
        
        PSD_ENABLEPAGESETUPTEMPLATE = 0x8000,

        PSD_ENABLEPAGESETUPTEMPLATEHANDLE = 0x20000,

        PSD_ENABLEPAGEPAINTHOOK = 0x40000,
        
        PSD_DISABLEPAGEPRINTING = 0x80000,

        PSD_NONETWORKBUTTON = 0x200000,

    }
}
#endif