using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LuaGlobalMethods (ScriptingService)
    /// 
    /// July 16, 2021 (modified October 13, 2021: actually working wait());
    /// 
    /// Defines global methods to be used for Lua.
    /// 
    /// TODO: Move to Sandbox?
    /// </summary>
    public partial class ScriptingService
    {
        public void Wait(uint Milliseconds)
        {
            ScriptGlobals.CurScriptPausing = true;
            DoWait(Milliseconds, ScriptGlobals.RunningScripts[0]); // only one at once?
            //TODO: GET CURRENT SCRIPT
        }

        /// <summary>
        /// Waits for <paramref name="Milliseconds"/> milliseconds.
        /// Performs co-operative yielding - wrapper for coroutine.yield(); 
        /// </summary>
        /// <param name="Milliseconds">The number of milliseconds to wait.</param>
        private void DoWait(uint Milliseconds, Script ScToPause)
        {
            // TEMP
            ScToPause.Pause();
            ScriptGlobals.LuaState.DoString($"coroutine.yield({ScToPause.CoroutineName});");
            return; 
            // END TEMP
        }

        private void DoUnpause(Script ScToPause)
        {
            ScToPause.Start();
            ScriptGlobals.LuaState.DoString($"coroutine.resume({ScToPause.CoroutineName});");
        }


    }
}
