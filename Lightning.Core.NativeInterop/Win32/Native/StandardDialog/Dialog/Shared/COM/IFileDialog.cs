#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HRESULT = System.UInt32;

namespace Lightning.Core.NativeInterop.Win32
{
    [ComImport(),
        Guid(COMIIDs.COM_IID_IFileDialog),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileDialog : IModalWindow
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetOptions([In] OpenFolderDialogFlags OFDF);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void GetOptions(out OpenFileDialogFlags OFDFPtr);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetDefaultFolder(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem PSI
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetFolder(
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem PSI
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void GetFolder(
            [MarshalAs(UnmanagedType.Interface)]
            out IShellItem PSI // passed as ptr
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void GetCurrentSelection(
            [MarshalAs(UnmanagedType.Interface)]
            out IShellItem PSI // passed as ptr
             );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetFileName([In] string FileName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void GetFileName(out string FileName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetTitle([In] string Title);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetOkButtonLabel([In] string OKButtonText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetFileNameLabel([In] string FileNameLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void GetResult
            (
            [MarshalAs(UnmanagedType.Interface)]
            out IShellItem ISI
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void AddPlace
            (
            [In]
            [MarshalAs(UnmanagedType.Interface)]
            IShellItem PSI,
            FDAP ListPlacement
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        /// <summary>
        /// Sets the default extension
        /// </summary>
        /// <param name="DefaultExtension"></param>
        void SetDefaultExtension(string DefaultExtension);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        /// <summary>
        /// Closes with optional error code.
        /// </summary>
        /// <param name="HResult">Exit code to exit with</param>
        void Close
            (
            [MarshalAs(UnmanagedType.Error)]
            int HResult
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void SetClientGuid
            (
            [In]
            ref Guid GUID
            );

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        void ClearClientData();

        void SetFilter
            (
            [MarshalAs(UnmanagedType.Interface)]
            IShellItemFilter Filter
            ); 
    }
}
#endif