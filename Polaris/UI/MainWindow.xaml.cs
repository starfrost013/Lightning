using Lightning.Core;
using Lightning.Core.API;  
using Polaris.Core;
using Polaris.UI; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool UI_INITIALISED { get; set; }
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            App.StandardOutput.StringWritten += Output_StringWritten;
            PolarisState = new PolarisState();
            PolarisState.Init(App.ProcessedLaunchArguments);
        }

        private void Output_StringWritten(object sender, ConsoleRedirectorEventArgs e)
        {
            if (UI_INITIALISED)
            {
                Output OutputUI = (Output)Polaris_OutputFrame.Content;

                OutputUI.AddMessage(e.TheString, MessageSeverity.Message);
            }

            Debug.WriteLine(e.TheString);
        }

        private void Window_SetTitleWithFilename(string FileName)
        {
            if (DataModel.DATAMODEL_LASTXML_PATH != null)
            {
                Title = $"Polaris (pre-alpha) ({DataModel.DATAMODEL_LASTXML_PATH})";
            }
        }

       
    }
}
