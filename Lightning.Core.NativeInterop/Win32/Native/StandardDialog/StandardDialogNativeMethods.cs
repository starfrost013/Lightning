#if WINDOWS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lightning.Core.NativeInterop.Win32
{
    /// <summary>
    /// StandardDialogAPI
    /// 
    /// June 20, 2021 (modified June 21, 2021)
    /// 
    /// Defines P/Invoke definitions for the Win32 COMDLG32 library.
    /// </summary>
    public class StandardDialogNativeMethods
    {
        /// <summary>
        /// Opens the Win32 Open File colour dialog box.
        /// </summary>
        /// <param name="CC">See <see cref="ChooseColor"/>.</param>
        /// <returns>true if successful - Call commdlggetextendederror() if false</returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool CHOOSECOLOR([In, Out] ChooseColor CC);

        /// <summary>
        /// Opens the Win32 Open File common dialog box.
        /// </summary>
        /// <param name="OFN">See <see cref="OpenFileName"/>.</param>
        /// <returns></returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName OFN);

        /// <summary>
        /// Opens the Win32 Save File common dialog box.
        /// </summary>
        /// <param name="OFN">See <see cref="OpenFileName"/>.</param>
        /// <returns>true if successful - Call commdlggetextendederror() if false</returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool GetSaveFileName([In, Out] OpenFileName OFN);

        /// <summary>
        /// Opens the Win32 Print common dialog box.
        /// </summary>
        /// <param name="OFN">See <see cref="PrintDialog"/>.</param>
        /// <returns>true if successful - Call commdlggetextendederror() if false</returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool PrintDlg([In, Out] PrintDialog PD);

        /// <summary>
        /// Opens the Win32 Font selection common dialog box.
        /// </summary>
        /// <param name="OFN">See <see cref=ChooseFont"/>.</param>
        /// <returns>true if successful - Call commdlggetextendederror() if false</returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool ChooseFont([In, Out] ChooseFont CF);

        /// <summary>
        /// Opens the Win32 page setup common dialog box.
        /// </summary>
        /// <param name="OFN">See <see cref="PageSetupDialog"/>.</param>
        /// <returns>true if successful - Call commdlggetextendederror() if false</returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool PageSetupDlg(ref PageSetupDialog PSD);

        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern int CommDlgExtendedError();
    }
}
#endif