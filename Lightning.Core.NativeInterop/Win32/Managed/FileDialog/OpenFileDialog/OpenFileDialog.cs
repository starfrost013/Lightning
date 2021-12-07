#if WINDOWS
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace NuCore.NativeInterop.Win32
{
    /// <summary>
    /// OpenFileDialog
    /// 
    /// July 14, 2021 (modified July 15, 2021: FunctionalA)
    /// 
    /// Defines an actually nice API for using the W32 common Open File Dialog.
    /// </summary>
    public class OpenFileDialog : FileDialog
    {

        public OpenFileDialog()
        {
            Filter = new OpenFileDialogFilter(); 
        }

        public void Show() => DoShow(IntPtr.Zero);
        public void ShowDialog(IntPtr HWND) => DoShow(HWND);

        private void DoShow(IntPtr HWND)
        {
            OpenFileName OFD = new OpenFileName();

            OFD.HwndOwner = HWND;
            OFD.HInstance = IntPtr.Zero;

            if (Flags != OpenFileDialogFlags.OFN_NONE)
            {
                OFD.Flags = Flags; 
            }
            else
            {
                if (UseOverwritePrompt) OFD.Flags += 2;
                if (Multiselect) OFD.Flags += 0x200;
                if (FileMustExist) OFD.Flags += 0x800;
                if (PathMustExist) OFD.Flags += 0x1000;

                
            }

            OFD.LPFileNameLength = 32767; // Max for ANSI

            OFD.LPFilter = Filter.ToString();

            // Setup required W32 info
            OFD.LStructSize = Marshal.SizeOf(OFD); // get the unmanaged structure size (for memory allocation purposes
            OFD.HInstance = IntPtr.Zero;
            OFD.LPFileName = "";

            if (WindowTitle == null
                || WindowTitle == "")
            {
                OFD.LPDialogTitle = "Open file...";
            }
            else
            {
                OFD.LPDialogTitle = WindowTitle;
            }


            OFD.LPFileTitleLength = 32767;
            OFD.LPFileName = "";
            OFD.LPTemplateName = null;
            OFD.NFileOffset = 0;
            OFD.LPCustomFilter = null;
            OFD.LPCustomFilterLength = 0;
            OFD.LPFileTitle = null;
            OFD.LPStrDefinedExtension = null;
            OFD.FlagsEx = FlagsEx;
            OFD.HwndOwner = HWND;
            OFD.StartFilterIndex = StartFilterIndex;
            OFD.NFileExtension = 32767;
            OFD.LCustomData = IntPtr.Zero;
            OFD.LPFileNameHook = null;
            
            if (Directory.Exists(InitialDirectory))
            {
                OFD.LPInitialDirectory = InitialDirectory;
            }
            else
            {
                OFD.LPInitialDirectory = null;
            }

            bool Result = StandardDialogNativeMethods.GetOpenFileName(OFD);

            bool CallWasSuccessful = false;

            int ErrorCode = 0; // INIT 

            if (!Result)
            {
                ErrorCode = StandardDialogNativeMethods.CommDlgExtendedError();

                if (ErrorCode == 0)
                {
                    CallWasSuccessful = true;
                }
            }
            else
            {
                CallWasSuccessful = true;
            }

            if (CallWasSuccessful)
            {
                FileName = OFD.LPFileName;
            }
            else
            {
                throw new Win32Exception($"COMDLG32 error ==> {(CommDlgExtendedError)ErrorCode}");
            }
            

            
        }


    }
}
#endif