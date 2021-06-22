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
        /// Colour Dialog
        /// </summary>
        /// <param name="CC">See <see cref="ChooseColor"/>.</param>
        /// <returns>true if successful - Call commdlggetextendederror() if false</returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool CHOOSECOLOR([In, Out] ChooseColor CC);

        /// <summary>
        /// Opens an Open File N
        /// </summary>
        /// <param name="OFN"></param>
        /// <returns></returns>
        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName OFN);

        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool GetSaveFileName([In, Out] OpenFileName OFN);

        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool PrintDlg([In, Out] PrintDialog PD);

        [DllImport("comdlg32.dll", SetLastError = true)]
        public static extern bool ChooseFont([In, Out] ChooseFont CF); 
    }
}
#endif