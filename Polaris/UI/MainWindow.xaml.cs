using Polaris.Core;
using Polaris.UI; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Polaris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// TEMP: Polaris state
        /// </summary>
        public PolarisState PolarisState { get; set; }
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            PolarisState = new PolarisState();
            PolarisState.Init(App.ProcessedLaunchArguments);
        }

        /// <summary>
        /// Test event handler for uilauncher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Polaris_HelpMenu_About_Click(object sender, RoutedEventArgs e) => UILauncher<AboutWindowHost>.LaunchUI(new AboutWindowHost());

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UIPopulator UIP = new UIPopulator();
            UIP.PopulateTreeView(PolarisState, Polaris_Explorer); // pass by value and update the object.
            UIP.PopulateTabs(PolarisState, Polaris_TabManager); 
        }
    }
}
