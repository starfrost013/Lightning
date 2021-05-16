using Lightning.Core;
using Lightning.Core.API;
using Polaris.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Polaris
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Activated(object sender, EventArgs e)
        {
            LaunchArgs LA = new LaunchArgs();

            // Do not initialise services or game xml

            DataModel DM = new DataModel();

            DataModel.Init();

        }
    }
}
