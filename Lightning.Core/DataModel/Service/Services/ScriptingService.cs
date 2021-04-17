using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// ScriptingService.
    /// 
    /// April 13, 2021
    /// 
    /// Provides scripting services. Manages LightningScript scripts.
    /// </summary>
    public class ScriptingService : Service
    {
        internal override string ClassName => "ScriptingService";
        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.ParentLocked; // non-serialisable or archivable as it is automatically created
        internal override ServiceImportance Importance => ServiceImportance.High;

        internal List<Script> RunningScripts { get; set; }
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

        public override void OnKeyDown(Control KeyCode) => throw new NotImplementedException();

    }
}
