using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// ScriptingService
    /// 
    /// April 13, 2021
    /// 
    /// Provides scripting services.
    /// </summary>
    public class ScriptingService : Service
    {
        public override ServiceImportance Importance => ServiceImportance.High;

        public override ServiceStartResult OnStart()
        {
            Logging.Log("ScriptingService Init", ClassName);
            ServiceStartResult SSR = new ServiceStartResult { Successful = true };
            SSR.Successful = true;
            return SSR;
        }

        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("ScriptingService Shutdown", ClassName);
            ServiceShutdownResult SSR = new ServiceShutdownResult { Successful = true };
            return SSR; 
        }

        public override void Poll()
        {
            return; 
        }
    }
}
