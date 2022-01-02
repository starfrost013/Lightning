using KeraLua; 
using NLua.Event;
using NuCore.Utilities; 
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
    // TODO: MOVE TO SCRIPTSTATEMANAGER
    public partial class ScriptingService 
    {
    // END TODO: MOVE TO SCRIPTSTATEMANAGER
        public void OnStart_SetLuaDebugHook()
        {
            ScriptGlobals.LuaState.DebugHook += LuaDebugHook;

            // fix my retardation
            ScriptGlobals.LuaState.SetDebugHook(LuaHookMask.Call | LuaHookMask.Line | LuaHookMask.Return, 0);
        }

        public void LuaDebugHook(object sender, DebugHookEventArgs e)
        {
            Script RunningScript = null;

            foreach (Script Sc in ScriptGlobals.RunningScripts)
            {
                Type ScType = Sc.GetType();

                if (Sc.State != ScriptState.Paused)
                {
                    RunningScript = Sc;
                }
                else
                {
                    continue; 
                }
            }

            if (RunningScript == null)
            {
                return; 
            }
            else
            {
                // todo: refactor to only get it once
                GlobalSettings GS = DataModel.GetGlobalSettings();

                Logging.Log($"Lua script {RunningScript.Name} has run for: {RunningScript.Timer.ElapsedMilliseconds}ms", ClassName); 

                if (RunningScript.Timer.ElapsedMilliseconds > GS.MaxLuaScriptExecutionTime)
                {
                    ScriptGlobals.RunningScripts.Remove(RunningScript);

                    if (RunningScript.Name != null)
                    {
                        Logging.Log($"The Lua script {RunningScript.Name} was stopped due to reaching the global execution time limit ({GS.MaxLuaScriptExecutionTime}ms)", ClassName, MessageSeverity.Error);
                    }
                    else
                    {
                        Logging.Log($"The currently executing Lua script was stopped due to reaching the global execution time limit ({GS.MaxLuaScriptExecutionTime}ms)", ClassName, MessageSeverity.Error);
                    }

                    RunningScript.CurrentlyExecutingLine = 0; // old way of stopping script
                    RunningScript.Stop(); // new way of stopping script
                    ScriptGlobals.PauseScript(RunningScript);
                    return;
                }

                switch (e.LuaDebug.Event)
                {
                    case LuaHookEvent.Call:
                        Logging.Log($"Lua function call", LuaClassName);
                        return; 
                    case LuaHookEvent.Line:
                        RunningScript.CurrentlyExecutingLine++;
                        Logging.Log($"Executing {RunningScript.Name}, line {RunningScript.CurrentlyExecutingLine}", LuaClassName);

                        return;
                    case LuaHookEvent.Return:
                        RunningScript.CurrentlyExecutingLine = 0;
                        RunningScript.Stop(); // script complete
                        return; 
                        
                }
            }
        }

        
    }
}
