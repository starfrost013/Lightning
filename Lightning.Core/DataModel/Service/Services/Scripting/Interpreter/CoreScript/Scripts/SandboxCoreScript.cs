using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ImportOverrideCoreScript
    /// 
    /// June 6, 2021
    /// 
    /// Defines a corescript that overrides the Lua import function to prevent importation of non-trusted assemblies.
    /// </summary>
    public class SandboxCoreScript : CoreScript 
    {
        internal override string ClassName => "SandboxCoreScript";

        /// <summary>
        /// Protected corescript content. 
        /// 
        /// Restricts global environment to safe objects.
        /// 
        /// Uses load(); 
        /// </summary>
        internal override string ProtectedContent =>
            "Print = print;\n" +
            "NEW_ENV = {print = print};\n" +
            "print(__SCRIPTCONTENT);\n" +
            "local called_chunk = load(__SCRIPTCONTENT, \"CHUNK\", \"t\", NEW_ENV)" +
            "pcall(called_chunk)";

        /// <summary>
        /// Constructor for the SandboxCoreScript class. Instantiated as this class is not actually added to the DataModel and therefore its oncreate() is never run
        /// todo: fix this
        /// </summary>
        public SandboxCoreScript()
        {
            CurrentScriptRunningStopwatch = new Stopwatch();
            WaitCountdownStopwatch = new Stopwatch();
        }
    }
}
