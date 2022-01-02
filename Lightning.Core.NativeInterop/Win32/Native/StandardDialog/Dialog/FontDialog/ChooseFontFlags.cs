using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{

    /// <summary>
    /// ChooseFontFlags
    /// 
    /// June 22, 2021
    /// 
    /// Flags to control the behaviour and view of the Win32 Choose Font Dialog
    /// </summary>
    public enum ChooseFontFlags
    {
        CC_SCREENFONTS = 0x1,

        CC_PRINTERFONTS = 0x2,

        CF_BOTH = (CC_SCREENFONTS | CC_PRINTERFONTS),

        CF_SHOWHELP = 0x4,

        CF_ENABLEHOOK = 0x8,

        CF_ENABLETEMPLATE = 0x10,

        CF_ENABLETEMPLATEHANDLE = 0x20,

        CF_INITTOLOGFONTSTRUCT = 0x40,

        CF_USESTYLE = 0x80,

        CF_EFFECT = 0x100,
        
        CF_APPLY = 0x200,

        CF_ANSIONLY = 0x400,

        CF_SCRIPTSONLY = 0x400,

        CF_NOVECTORFONTS = 0x800,

        CF_NOOEMFONTS = 0x800,

        CF_NOSIMULATIONS = 0x1000,

        CF_LIMITSIZE = 0x2000,

        CF_FIXEDPITCHONLY = 0x4000,

        CF_WYSIWYG = 0x8000,

        CF_FORCEFONTEXIST = 0x10000,

        CF_SCALABLEONLY = 0x20000,

        CF_TTONLY = 0x40000,

        CF_NOFACESEL = 0x80000,

        CF_NOSTYLESEL = 0x100000,

        CF_NOSIZESEL = 0x200000,

        CF_SELECTSCRIPT = 0x400000,

        CF_NOSCRIPTSEL = 0x800000,

        CF_NOVERTFONTS = 0x1000000,

        CF_INACTIVEFONTS = 0x2000000
    }
}
