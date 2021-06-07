using KeraLua; 
using NLua.Event; 
using System;
using System.Collections.Generic;
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
            foreach (Script Sc in ScriptGlobals.RunningScripts)
            {
                if (Sc.IsRunning)
                {
                    if (Sc.Content != e.LuaDebug.Source)
                    {
                        ErrorManager.ThrowError(ClassName, "CannotHaveMultipleUnpausedScriptsRunningException");
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
