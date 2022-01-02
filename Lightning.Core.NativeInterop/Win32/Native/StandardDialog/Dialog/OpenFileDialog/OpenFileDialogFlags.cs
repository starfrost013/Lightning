#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{

    /// <summary>
    /// OpenFileDialogFlags
    /// 
    /// June 20, 2021
    /// 
    /// Defines flags for the Win32 Open File Dialog.
    /// </summary>
    public enum OpenFileDialogFlags
    {
        /// <summary>
        /// Unofficial: used for marking none
        /// 
        /// Doing it this way is clearer than simply checking for 0.
        /// </summary>
        OFN_NONE = 0x0,

        OFN_READONLY = 0x1, 

        /// <summary>
        /// If this flag is set, the dialog will trigger on overwrite.
        /// </summary>
        OFN_OVERWRITEPROMPT = 0x2,

        OFN_NOCHANGEDIR = 0x8,

        OFN_SHOWHELP = 0x10,

        OFN_ENABLEHOOK = 0x20,
        
        OFN_ENABLETEMPLATE = 0x40,

        OFN_ENABLETEMPLATEHANDLE = 0x80,

        OFN_NOVALIDATE = 0x100,

        /// <summary>
        /// If set, multi-selection will be enabled.
        /// </summary>

        OFN_ALLOWMULTISELECT = 0x200,

        OFN_EXTENSIONHIDDEN = 0x400,

        OFN_PATHMUSTEXIST = 0x800,

        OFN_FILEMUSTEXIST = 0x1000,

        OFN_CREATEPROMPT = 0x2000,

        OFN_SHAREAWARE = 0x4000,

        OFN_NOREADONLYRETURN = 0x8000,

        OFN_NOTESTFILECREATE = 0x10000,

        OFN_NONETWORKBUTTON = 0x20000,

        OFN_NOLONGNAMES = 0x40000,
        
        OFN_EXPLORER = 0x80000,

        OFN_NODEREFERENCELINKS =  0x100000,

        OFN_LONGNAMES = 0x200000,

        OFN_ENABLEINCLUDENOTIFY = 0x400000,

        OFN_DONTADDTORECENT = 0x2000000,

        OFN_HIDEREADONLY = 0x10000000,
    }
}
#endif