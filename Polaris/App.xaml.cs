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
    /// Polaris (LightningSDK Development Environment)
    /// 
    /// © 2021 Lightning Development Team.
    /// 
    /// Development package for Lightning-based games. 
    /// 
    /// Development begun       May 13, 2021
    /// </summary>
    public partial class App : Application
    {
        public static LaunchArgs ProcessedLaunchArguments { get; set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the version information. 
            LVersion.LoadVersion(); 

            LaunchArgsResult LA = LaunchArgs.HandleArgs(e.Args);

            ProcessedLaunchArguments = LA.Arguments;

        }
    }
}
