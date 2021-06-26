using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    [ComImport(),
        Guid(COMIIDs.COM_IID_IFileDialog),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileDialog : IModalWindow
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        int Show([In] IntPtr Parent);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void OnFolderChange([In]
        [MarshalAs(UnmanagedType.Interface)]
            IFileDialog IFD
        );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetFileTypes([In] uint CFileTypes);

        void SetOptions([In] OpenFileDialogFlags Flags);

        OpenFileDialogFlags GetOptions(); 
    }
}
