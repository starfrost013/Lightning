using NLua;
using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CXScriptingService
    /// 
    /// January 14, 2022 (modified January 15, 2022: add GameDLL loading functionality)
    /// 
    /// Provides C# scripting services for Lightning.
    /// </summary>
    public partial class CXScriptingService : Service
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "CXScriptingService";

        internal override ServiceImportance Importance => ServiceImportance.High; // may be rebootable?

        public override ServiceStartResult OnStart()
        {
            Logging.Log("ScriptingService Init [CX Enabled]", ClassName);

            // check XML is loaded
            if (DataModel.DATAMODEL_LASTXML_PATH != null) CX_LoadGameDLL();

            ServiceStartResult SSR = new ServiceStartResult { Successful = true };
            SSR.Successful = true;
            return SSR;
        }


        private LoadGameDLLResult CX_LoadGameDLL()
        {
            LoadGameDLLResult LGDR = new LoadGameDLLResult();

            // already checked that it exists as it has already been added by DDMS
            // and also already checked for engine init.

            // checks for gamesettings have already been done at this point
            Workspace Ws = DataModel.GetWorkspace();

        }

        public override void Poll()
        {
            throw new NotImplementedException();
        }

        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("ScriptingService Shutdown", ClassName);
           
            ServiceShutdownResult SSR = new ServiceShutdownResult { Successful = true };
            return SSR; 
        }

        public override void OnDataSent(ServiceMessage Data)
        {
            return;
        }
    }
}
