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
namespace Lightning.Core.NativeInterop.Win32
{
    public static class NativeMethodsWin32
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AllocConsole(); // allocconsole probably works better

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumDisplayDevicesW(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumDisplaySettingsW(string lpszDeviceName, uint iModeNum, ref DEVMODE lpDevMode);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int MessageBoxA(IntPtr Hwnd,
            string lpText,
            string lpCaption,
            [MarshalAs(UnmanagedType.U4)]
            MessageBoxType uType);
        public static uint Win32__AttachConsole_Default_PID = 0x0ffffffff; // .NET 

        
    }
}
#endif