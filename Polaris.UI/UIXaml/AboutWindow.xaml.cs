using Lightning.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Polaris.UI
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : UserControl
    {

        public EventHandler ExitHit { get; set; }

        public AboutWindow()
        {
            InitializeComponent();
        }

        private string GetVersionString()
        {
            StringBuilder SB = new StringBuilder();

            // Split version and engine version? maybe?
            SB.Append(LVersion.GetVersionString());

#if DEBUG
            SB.Append(" (Debug)");
#endif

            return SB.ToString(); 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PolarisAbout_Version.Text = GetVersionString(); 
        }

        private void PolarisAbout_ExitButton_Click(object sender, RoutedEventArgs e) => ExitHit(this, e);
    }
}
