using Lightning.Core;
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

        private void Polaris_FileMenu_New_Click(object sender, RoutedEventArgs e)
        {

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

            OpenFileDialog OFD = new OpenFileDialog();

            OFD.WindowTitle = "Open LGX...";
            OFD.Filter.AddItem("lgx", "Lightning Game XML");

            OFD.ShowDialog(HWND);

            Logging.Log(OFD.FileName, "Polaris UI Event Response Handler");
            
#endif
        }


    }
}
