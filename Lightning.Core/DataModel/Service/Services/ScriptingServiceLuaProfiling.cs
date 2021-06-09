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
            ScriptGlobals.LuaState.DebugHook += LuaDebugHook;

            // fix my retardation
            ScriptGlobals.LuaState.SetDebugHook(LuaHookMask.Line | LuaHookMask.Return, 0);
        }

        public void LuaDebugHook(object sender, DebugHookEventArgs e)
        {
            Script RunningScript = null;

            foreach (Script Sc in ScriptGlobals.RunningScripts)
            {
                Type ScType = Sc.GetType();

                bool IsCoreScript = (ScType == typeof(CoreScript)
            || ScType.IsSubclassOf(typeof(CoreScript)));

                if (!Sc.IsPaused)
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
                        Logging.Log($"Current line: {RunningScript.CurrentlyExecutingLine}", ClassName);
                        return;
                    case LuaHookEvent.Return:
                        RunningScript.CurrentlyExecutingLine = 0;
                        return; 
                        
                }
            }
        }
    }
}
