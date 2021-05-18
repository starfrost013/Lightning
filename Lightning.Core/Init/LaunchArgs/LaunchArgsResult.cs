using Lightning.Core; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class LaunchArgsResult
    {
        public LaunchArgsAction Action { get; set; }
        public LaunchArgs Arguments { get; set; }

        public LaunchArgsResult()
        {
            Arguments = new LaunchArgs();
        }
        
    }
}
