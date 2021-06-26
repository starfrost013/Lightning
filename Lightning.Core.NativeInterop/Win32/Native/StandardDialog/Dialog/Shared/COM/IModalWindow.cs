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
     Guid(COMIIDs.COM_IID_IModalWindow),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IModalWindow
    {
        /// <summary>
        /// Implements IModalWindow.Show();
        /// </summary>
        /// <param name="Parent"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        int Show([In] IntPtr Parent);




    }
}
#endif