using KeraLua; 
using NLua.Event; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// ScriptingServiceLuaProfiling (partial class: ScriptingService)
/// 
/// June 7, 2021
/// 
/// Implements Lua profiling for Lightning
/// </summary>
namespace Lightning.Core.API 
{
    public partial class ScriptingService
    {
        public void OnStart_SetLuaDebugHook()
        {
            LuaState.DebugHook += LuaDebugHook; 
        }

        private void LuaDebugHook(object sender, DebugHookEventArgs e)
        {
            Script RunningScript = null;

            foreach (Script Sc in ScriptGlobals.RunningScripts)
            {
                if (Sc.IsRunning)
                {
                    if (Sc.Content != e.LuaDebug.Source)
                    {
                        // block multiple scripts
                        ErrorManager.ThrowError(ClassName, "CannotHaveMultipleUnpausedScriptsRunningException");
                    }
                    else
                    {
                        RunningScript = Sc;
                    }
                }
            }

            if (RunningScript == null)
            {
                ErrorManager.ThrowError(ClassName, "InternalLuaStateErrorException", "For some reason the Lua debug hook got called despite no scripts running. This is bad and probably means something very bad happened.");
            }
            else
            {
                // todo: refactor to only get it once
                GlobalSettings GS = DataModel.GetGlobalSettings();

                if (RunningScript.CurrentScriptRunningStopwatch.ElapsedMilliseconds > GS.MaxLuaScriptExecutionTime)
                {

                    ScriptGlobals.RunningScripts.Remove(RunningScript);

                    if (RunningScript.Name != null)
                    {
                        Logging.Log($"The Lua script {RunningScript.Name} was stopped due to reaching the global execution time limit ({GS.MaxLuaScriptExecutionTime}ms)", ClassName, MessageSeverity.Error);
                        RunningScript.CurrentlyExecutingLine = 0;
                        return; 
                    }
                    else
                    {
                        Logging.Log($"The currently executing Lua script was stopped due to reaching the global execution time limit ({GS.MaxLuaScriptExecutionTime}ms)", ClassName, MessageSeverity.Error);
                        RunningScript.CurrentlyExecutingLine = 0;
                        return;
                    }
                }

                switch (e.LuaDebug.Event)
                {
                    case LuaHookEvent.Line:
                        RunningScript.CurrentlyExecutingLine++;
                        return;
                        
                }
            }
        }
    }
}
