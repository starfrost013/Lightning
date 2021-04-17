using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace Lightning.Core.API
{
    /// <summary>
    /// Global data required across all Services.
    /// </summary>
    public class ServiceGlobalData
    {
        public Stopwatch ServiceUpdateTimer { get; set; }

    }
}
