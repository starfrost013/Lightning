using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{ 
    /// <summary>
    /// Non-DataModel (Engine Core)
    /// 
    /// GlobalSettings
    /// 
    /// 2021-04-03
    /// 
    /// Holds global settings for the engine.
    /// 
    /// uses Lightning.Core.StaticSerialiser.dll
    /// </summary>
    public static class GlobalSettings
    {
        public static List<ServiceStartupCommand> ServiceStartupCommands { get; set; }
    }
}
