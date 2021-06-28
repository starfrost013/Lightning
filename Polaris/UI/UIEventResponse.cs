﻿using Lightning.Core;
using Lightning.Core.API;
#if WINDOWS // probably not the best lol
using Lightning.Core.NativeInterop.Win32; 
#endif
using Polaris.Core;
using Polaris.UI; 
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace Polaris
{
    /// <summary>
    /// MainWindow [UIEventResponse]
    /// 
    /// June 26, 2021
    /// 
    /// Defines UI event responses for Polaris.
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Test event handler for uilauncher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Polaris_HelpMenu_About_Click(object sender, RoutedEventArgs e) => UILauncher<AboutWindowHost>.LaunchUI(new AboutWindowHost());

        private void Polaris_TabUIFrame_Navigated(object sender, NavigationEventArgs e)
        {
            TabUI TUI = (TabUI)e.Content;

            UIPopulator UIP = new UIPopulator();
            UIP.PopulateTabs(PolarisState, TUI.Polaris_TabManager);
        }

        private void Polaris_ExplorerFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Explorer Explorer = (Explorer)e.Content;

            UIPopulator UIP = new UIPopulator();
            UIP.PopulateTreeView(PolarisState, Explorer.Polaris_Explorer);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


#if DEBUG
            Logging.Log("TEST!", "Polaris MainWindow Testing Utilities", MessageSeverity.Message);
#endif
            Window_SetTitleWithFilename(DataModel.DATAMODEL_LASTXML_PATH);

            UI_INITIALISED = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PolarisState.Shutdown();
        }

        /// <summary>
        /// Launches the Win32 open dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Polaris_FileMenu_Open_Click(object sender, RoutedEventArgs e)
        {
#if WINDOWS // temp

            WindowInteropHelper WLH = new WindowInteropHelper(this);
            IntPtr HWND = WLH.Handle; 

            OpenFileName OFN = new OpenFileName();
            OFN.Flags = OpenFileDialogFlags.OFN_OVERWRITEPROMPT;
            OFN.LStructSize = Marshal.SizeOf(OFN);
            OFN.LPFilter = @"All Files\*.*\[0]\[0]";
            OFN.StartFilterIndex = 1;
            OFN.LPDialogTitle = "Penis";
            OFN.LPFileName = null;
            OFN.LPFileNameLength = 0;
            OFN.LPStrDefinedExtension = null;
            OFN.LPInitialDirectory = null;
            OFN.LPFileTitleLength = 0;
            
            OFN.HwndOwner = HWND; 

            bool Result = StandardDialogNativeMethods.GetOpenFileName(OFN);

            if (!Result)
            {
                // TEMP: API TEST
                Logging.Log($"Comdlg32 error - TEMP - {StandardDialogNativeMethods.CommDlgExtendedError()}");
                return; 
            }
#endif
        }
    }
}
