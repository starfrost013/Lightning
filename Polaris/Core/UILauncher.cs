using System;
using System.Collections.Generic;
using System.Text;
using System.Windows; 

namespace Polaris.Core
{
    /// <summary>
    /// UILauncher
    /// 
    /// May 18, 2021
    /// 
    /// Launches a UI Host. <see cref="T"/> must inherit from <see cref="Window"/>.
    /// </summary>
    public static class UILauncher<T> where T : Window
    {
        /// <summary>
        /// Launch a UI host. 
        /// </summary>
        /// <param name="VWindow">The window object you wish to launch.</param>
        /// <param name="ShowDialog">If the window is to be shown as a dialog and block all other windows in the application. Optional.</param>
        /// <param name="OwnerWindow">The owner window of this window. If not specified, will use the main window of the application. Optional.</param>
        public static void LaunchUI(T VWindow, bool ShowDialog = false, Window OwnerWindow = null)
        {
            T T = VWindow;

            if (OwnerWindow != null)
            {
                T.Owner = OwnerWindow;
            }
            else
            {
                T.Owner = Application.Current.MainWindow;
            }

            if (ShowDialog)
            {
                T.ShowDialog();
            }
            else
            {
                T.Show();
            }
            
        }
    }
}
