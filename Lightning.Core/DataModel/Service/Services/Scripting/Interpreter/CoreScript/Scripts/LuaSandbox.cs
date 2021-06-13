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
    public class LuaSandbox : CoreScript 
    {
        internal override string ClassName => "SandboxCoreScript";

        internal string Environment = "print = print, pairs = pairs, ipairs = ipairs, load = load, pcall = pcall, __SCRIPTCONTENT = __SCRIPTCONTENT";

        internal bool LUASANDBOX_INITIALISED { get; set; }

        /// <summary>
        /// Protected corescript content. 
        /// 
        /// Restricts global environment to safe objects.
        /// 
        /// Uses load(); 
        /// </summary>
        internal override string ProtectedContent =>
            "Print = print;\n" +
            $"NEW_ENV = {{{Environment}}};\n" +
            "_ENV = NEW_ENV;\n" +
            "for i, v in pairs(_ENV) do\n" +
            "   print(i);\n" +
            "end\n" +
            "print(ScTest);" +
            "local called_chunk = load(__SCRIPTCONTENT, \"CHUNK\", \"t\", _ENV)" +
            "pcall(called_chunk)";

        /// <summary>
        /// Constructor for the SandboxCoreScript class. Instantiated as this class is not actually added to the DataModel and therefore its oncreate() is never run
        /// todo: fix this
        /// </summary>
        public LuaSandbox()
        {
            CurrentScriptRunningStopwatch = new Stopwatch();
            WaitCountdownStopwatch = new Stopwatch();
            LUASANDBOX_INITIALISED = true; 
        }

        public void AddToSandbox(string FunctionName)
        {
            if (FunctionName == null
                || FunctionName.Length == 0)
            {
                ErrorManager.ThrowError(ClassName, "AttemptedToAddInvalidFunctionToSandboxException");
                // we crash here
            }
            else
            {
                StringBuilder SB = new StringBuilder();
                SB.Append(Environment);
                SB.Append($", {FunctionName} = {FunctionName} ");
                Environment = SB.ToString();
            }

        }
    }
}
