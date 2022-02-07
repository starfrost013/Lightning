#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// Lightning
/// 
/// 2021-03-05
/// 
/// Provides native interop services for Windows-based Lightning applications
/// 
/// March 5, 2021   Move from Emerald to Lightning
/// </summary>
namespace NuCore.NativeInterop.Win32
{
    public static class NativeMethodsWin32
    {

        /// <summary>
        /// Polaris: Create a process using win32
        /// </summary>
        /// <param name="lpApplicationName"></param>
        /// <param name="lpCommandLine"></param>
        /// <param name="lpProcessAttributes"></param>
        /// <param name="lpThreadAttributes"></param>
        /// <param name="bInheritHandles"></param>
        /// <param name="dwCreationFlags"></param>
        /// <param name="lpEnvironment"></param>
        /// <param name="lpCurrentDirectory"></param>
        /// <param name="lpStartupInfo"></param>
        /// <param name="lpProcessInformation"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern string CreateProcess(string lpApplicationName,
        string lpCommandLine,
        ref SecurityAttributes lpProcessAttributes,
        ref SecurityAttributes lpThreadAttributes,
        bool bInheritHandles,
        uint dwCreationFlags,
        IntPtr lpEnvironment,
        string lpCurrentDirectory,
        ref StartupInfoEx lpStartupInfo,
        out ProcessInformation lpProcessInformation);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern int MessageBoxA(IntPtr Hwnd,
            string lpText,
            string lpCaption,
            [MarshalAs(UnmanagedType.U4)]
            MessageBoxType uType);

        /// <summary>
        /// Imported from Tiralen 2022/01/03
        /// 
        /// Acquires a system metric
        /// </summary>
        /// <param name="SysMetric">The <see cref="SystemMetric"/> to acquire.</param>
        /// <returns>The value of the system metric specified by the parameter <paramref name="SysMetric"/>.</returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric SysMetric); // temp

        public static uint Win32__AttachConsole_Default_PID = 0x0ffffffff; // .NET REQUIREMENT 


        
    }
}
#endif