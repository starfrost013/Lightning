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
using System.Windows.Shapes;

namespace Polaris.UI
{
    /// <summary>
    /// Interaction logic for Output .xaml
    /// </summary>
    public partial class Output : Page
    {
        public List<string> Messages { get; set; }

        public Output()
        {
            Messages = new List<string>(); 
            InitializeComponent();
        }

        
    }
}
