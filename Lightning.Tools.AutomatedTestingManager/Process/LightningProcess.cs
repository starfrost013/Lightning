using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Tools.AutomatedTestingManager
{
    public class LightningProcess 
    {
        public Process Process { get; set; }
        public int Lifetime { get; set; }
        public Stopwatch LifeTimer { get; set; }
        internal bool IsRunning { get; set; }

        public LightningProcess()
        {
            LifeTimer = new Stopwatch();
            Process = new Process();
            
        }

        public void Run(string FileName, LaunchArgs LA)
        {
            
            if (LA.LightningDirectory == null)
            {
                Process.StartInfo.FileName = $".\\Lightning.exe"; 
            }
            else 
            {
                Process.StartInfo.FileName = $@"{LA.LightningDirectory}\Lightning.exe";
            }

            Process.StartInfo.ArgumentList.Add(FileName);

            if (LA.ProcessLifetime <= 0) 
            {
                Lifetime = 30000;
            }
            else
            {
                Lifetime = LA.ProcessLifetime;
            }

            //Process.StartInfo.UseShellExecute = true;
            
            LifeTimer.Start();
            Process.Start();
            IsRunning = true;
        }

        public void Update()
        {
            if (!IsRunning)
            {
                return; 
            }
            else 
            {
                if (LifeTimer.ElapsedMilliseconds > Lifetime)
                {
                    Process.Kill();
                    LifeTimer.Stop();
                    IsRunning = false; 
                }
            }
        }
    }
}
