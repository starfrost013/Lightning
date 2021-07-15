#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    public enum CommDlgExtendedError
    {

#region General errors
        CDERR_STRUCTSIZE = 0x1,

        CDERR_INITIALIZATION = 0x2,

        CDERR_NOTEMPLATE = 0x3,

        CDERR_NOHINSTANCE = 0x4,

        CDERR_LOADSTRFAILURE = 0x5,

        CDERR_FINDRESFAILURE = 0x6,

        CDERR_LOADRESFAILURE = 0x7,

        CDERR_LOCKRESFAILURE = 0x8,

        CDERR_MEMALLOCFAILURE = 0x9,

        CDERR_MEMLOCKFAILURE = 0xA,

        CDERR_NOHOOK = 0xB,

        CDERR_REGISTERMSGFAIL = 0xC,

        CDERR_DIALOGFAILURE = 0xFFFF,
#endregion

#region Printer Dialog Errors

        PDERR_SETUPFAILURE = 0x1001,

        PDERR_PARSEFAILURE = 0x1002,

        PDERR_RETDEFFAILURE = 0x1003,

        PDERR_LOADDRVFAILURE = 0x1004,

        PDERR_GETDEVMODEFAIL = 0x1005,

        PDERR_INITFAILURE = 0x1006,

        PDERR_NODEVICES = 0x1007,

        PDERR_NODEFAULTPRN = 0x1008,

        PDERR_DNDMMISMATCH = 0x1009,

        PDERR_CREATEICFAILURE = 0x100A,

        PDERR_PRINTERNOTFOUND = 0x100B,

        PDERR_DEFAULTDIFFERENT = 0x100C,

#endregion

#region Choose Font Dialog errors

        CFERR_MAXLESSTHANMIN = 0xFFFE, // not sure what to set this to? MSDN sets it to itself, so i'm just using 0xFFFE as 0 is success

        CFERR_NOFONTS = 0x2001,

#endregion

#region Open / Save File Name Error

        FNERR_SUBCLASSFAILURE = 0x3001,

        FNERR_INVALIDFILENAME = 0x3002,

        FNERR_BUFFERTOOSMALL = 0x3003,

#endregion

#region Find / Replace Text errors

        FRERR_BUFFERLENGTHZERO = 0x4001

#endregion
    }
}
#endif