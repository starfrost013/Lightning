#if WINDOWS
using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// OpenFolderDialogFlags
    /// 
    /// June 24, 2021
    /// 
    /// Defines flags for the COM OpenFolderDialogFlags API.
    /// </summary>
    public enum OpenFolderDialogFlags
    {
        FOS_OVERWRITEPROMPT = 0,

        FOS_STRICTFILETYPES = 0x1,
        
        FOS_NOCHANGEDIR = 0x2,

        FOS_PICKFOLDERS = 0x4,

        FOS_FORCEFILESYSTEM = 0x8,

        FOS_ALLNONSTORAGEITEMS = 0x10,

        FOS_NOVALIDATE = 0x20,

        FOS_ALLOWMULTISELECT = 0x40,

        FOS_PATHMUSTEXIST = 0x80,

        FOS_FILEMUSTEXIST = 0x100,

        FOS_CREATEPROMPT = 0x200,

        FOS_SHAREAWARE = 0x400,

        FOS_NOREADONLYRETURN = 0x800,

        FOS_NOTESTFILECREATE = 0x1000,

        FOS_HIDEMRUPLACES = 0x2000,

        FOS_HIDEMPINNEDPLACES = 0x4000,

        FOS_NODEREFERENCELINKS = 0x8000,

        FOS_OKBUTTONNEEDSINTERACTION = 0x10000,

        FOS_DONTADDTORECENT = 0x20000,

        FOS_FORCESHOWHIDDEN = 0x40000,

        FOS_DEFAULTNOMINIMODE = 0x80000,

        FOS_FORCEPREVIEWPANEON = 0x100000,

        FOS_SUPPORTSTREAMABLEITEMS = 0x200000
    }
}
#endif