using System;
using System.Collections.Generic;
using System.IO;
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
        public Stopwatch KillTimer { get; set; }

        public List<LightningProcess> LightningProcesses { get; set; }
        public Tester()
        {
            LightningProcesses = new List<LightningProcess>();   
        }

        public void StandardRun(LaunchArgs LA)
        {

            if (LA.Directory == null) LA.Directory = "Content\\Tests\\";

            string Dir = LA.Directory;

            if (!LA.Recurse)
            {
                Run(Dir, LA);
            }
            else 
            {
                RunRecursive(Dir, LA);
            }
            
        }

        private void Run(string DirPath, LaunchArgs LA)
        {
            string[] FileNames = Directory.GetFiles(DirPath);

            foreach (string FileName in FileNames)
            {
                if (FileName.Contains(".xml")
                || FileName.Contains(".lgx"))
                {
                    InitLightning(FileName, LA);
                }
                
            }

            UpdateAllProcesses(); 
            
        }

        private void RunRecursive(string DirPath, LaunchArgs LA)
        {
            string[] FileNames = Directory.GetFiles(DirPath);

            foreach (string FileName in FileNames)
            {
                InitLightning(FileName, LA);
            }

            string[] DirectoryNames = Directory.GetDirectories(DirPath);

            if (DirectoryNames.Length > 0)
            {
                foreach (string DirectoryName in DirectoryNames)
                {
                    RunRecursive(DirectoryName, LA);
                }
            }
            else
            {
                UpdateAllProcesses(); // enter the main loop
            }
        }

        /// <summary>
        /// Private: Initialises Lightning for each file
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="LA"></param>
        private void InitLightning(string FileName, LaunchArgs LA)
        {
            LightningProcess LPX = new LightningProcess();

            LightningProcesses.Add(LPX);
            LPX.Run(FileName, LA);

        }
        
        /// <summary>
        /// Private: Updates all processes
        /// </summary>
        private void UpdateAllProcesses()
        {
            while (LightningProcesses.Count > 0)
            {
                for (int i = 0; i < LightningProcesses.Count; i++)
                {
                    LightningProcess Proc = LightningProcesses[i];

                    Proc.Update();

                    if (!Proc.IsRunning) LightningProcesses.Remove(Proc);
                }
            }
        }
    }
}
