using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Lightning.Tools.AutomatedTestingManager
{
    /// <summary>
    /// Tester
    /// 
    /// July 29, 2021
    /// 
    /// The main implementation class for the Lightning.Tools.AutomatedTestingManager tool.
    /// </summary>
    public class Tester
    {
        public Stopwatch Tx { get; set; }

        public void StandardRun(LaunchArgs LA)
        {
            string Dir = LA.Directory;

            
        }

        private void Run()
        {

        }

        private void RunRecursive()
        {

        }

        private void InitLightning(string FileName)
        {
            using (Process LightningProc = new Process())
            {
                LightningProc.StartInfo.FileName = FileName;

            }
        }
    }
}
