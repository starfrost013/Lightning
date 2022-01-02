using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// BootWindow
    /// 
    /// January 2, 2022
    /// 
    /// Defines the Lightning boot progress window. Replaces SplashScreen
    /// </summary>
    public class BootWindow
    {
        private double _progress { get; set; }
        public double Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (value < 0.0) value = 0.0;
                if (value > 1.0) value = 1.0;
                _progress = value;
                
            }

        }

        private string CurrentProgressString { get; set; }
        
        public void SetProgress(double NProgress, string NProgressString)
        {
            Progress = NProgress;
            CurrentProgressString = NProgressString;
        }
    }
}
