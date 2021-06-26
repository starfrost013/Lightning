#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SIGDN = System.UInt32; 

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// COM IShellItem interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(COMIIDs.COM_IID_IShellItem)]
    public interface IShellItem
    {
        void BindToHandler(IntPtr pbc,
            [MarshalAs(UnmanagedType.LPStruct)]
            Guid BHID,
            [MarshalAs(UnmanagedType.LPStruct)]
            Guid RIID);

        void GetParent(out IShellItem PPSI);

        void GetDisplayName(SIGDN SIGDNName, out IntPtr PPSZNamePointer);

        void GetAttributes(uint SFGAOMask, out uint PSFGOAttributes);

        void Compare(IShellItem PSIItem, uint Hint, out int PIOrder); 
    }
}
#endif
