using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ImportOverrideCoreScript
    /// 
    /// June 6, 2021 (modified October 21, 2021)
    /// 
    /// Defines a corescript that overrides the Lua import function to prevent importation of non-trusted assemblies.
    /// </summary>
    public class LuaSandbox : Script 
    {
        internal override string ClassName => "SandboxCoreScript";

        /// <summary>
        /// Defines the available Lua scripting APIs.
        /// </summary>
        internal string Environment = "print = print, " +
        "CLRPackage = CLRPackage, " +
        "luanet = luanet, " +
        "pairs = pairs, " +
        "ipairs = ipairs, " +
        "load = load, " + // automatically killed before script is run
        "pcall = pcall, " +
        "type = type, " +
        "math = math, " +
        "table = table, " +
        "_VERSION = _VERSION, " +
        "utf8 = utf8, " +
        "select = select, " +
        "tostring = tostring, " +
        "tonumber = tonumber, " +
        "string = string, " +
        "rawlen = rawlen, " +
        "rawset = rawset, " + 
        "rawequal = rawequal, " +
        "coroutine = coroutine, " +
        #if DEBUG
            "debug = debug, " +
        #endif
        "__SCRIPTCONTENT = __SCRIPTCONTENT";
        
        internal bool LUASANDBOX_INITIALISED { get; set; }

        internal override bool IsSandbox => true;

        /// <summary>
        /// Protected corescript content. 
        /// 
        /// Restricts global environment to safe objects.
        /// 
        /// Uses load(); 
        /// </summary>
        public override string Content =>
            $"NEW_ENV = {{{Environment}}};\n" +
            "_ENV = NEW_ENV;\n" +
#if DEBUG 
            "for i, v in pairs(_ENV) do\n" +
            "   print(i);\n" +
            "end\n" +
#endif
            "local called_chunk = load(__SCRIPTCONTENT, \"CHUNK\", \"t\", _ENV)" +
            $"{CoroutineName} = coroutine.create(function ()" + // todo: this is dumb 
            "pcall(called_chunk)" +
            $"coroutine.yield({CoroutineName}) end);"; // todo: call after all trustedscripts have been run
            // also todo loop through all classes that inherit from trustedscript and execute their ProtectedContents

        public string Content_PerformRun => $"coroutine.resume({CoroutineName})";

        /// <summary>
        /// Constructor for the SandboxCoreScript class. Instantiated as this class is not actually added to the DataModel and therefore its oncreate() is never run
        /// todo: fix this
        /// </summary>
        public LuaSandbox()
        {
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
