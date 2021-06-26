#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{

    /// <summary>
    /// IShellItemFilter
    /// 
    /// June 26, 2021
    /// 
    /// Defines the IShellItemFilter COM Interface.
    /// </summary>
    [ComImport]
    [Guid(COMIIDs.COM_IID_IShellItemFilter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItemFilter
    {
        public int IncludeItem(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem PSI
            );

        public SHEnumerationItems GetEnumFlagsForItem
            (
            [In]
           [MarshalAs(UnmanagedType.Interface)]
           IShellItem PSI,
            out SHEnumerationItems Flags
            );
            
    }
}
#endif