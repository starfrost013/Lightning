#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HResult = System.UInt32;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// COM IFileDialogEvents interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(COMIIDs.COM_IID_IFileDialogEvents)]
    public interface IFileDialogEvents
    {
        HResult OnFolderChanging(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IFileDialog IFD,
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem Provider);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        int Show([In] IntPtr Parent);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void OnFolderChange([In]
        [MarshalAs(UnmanagedType.Interface)]
            IFileDialog IFD
        );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetFileTypes([In] uint CFileTypes);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void OnSelectionChange(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IFileDialog IFD);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]

        void SetFileTypeIndex([In] uint IFileType); // TODO: implement IFileType 

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void OnShareViolation(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem ISI
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void OnTypeChange(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IFileDialog IFD);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void Advise(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IFileDialogEvents IFDE,
            out uint Cookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void OnOverwrite(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IFileDialog PFD,
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem PSI,

            );
    }
}
#endif