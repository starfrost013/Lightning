using Lightning.Utilities;
using NLua;
using NLua.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptStateManager
    /// 
    /// Manages Lua scripting, including managing co-operatively tasked Lightning Lua scripts
    /// 
    /// October 2, 2021 (renamed from ScriptInterpreter as LS is long dead)
    /// </summary>
    public class ScriptStateManager : Instance // in the DataModel for now
    {
        internal override string ClassName => "ScriptStateManager";

        internal override InstanceTags Attributes => InstanceTags.Instantiable;

        /// <summary>
        /// All methods that have been exposed.
        /// </summary>
        public List<ScriptMethod> ExposedMethods { get; set; }

        /// <summary>
        /// A list of currently running scripts.
        /// </summary>
        public List<Script> RunningScripts { get; set; } // potentially move to children


        /// <summary>
        /// The Lua sandbox.
        /// </summary>
        public LuaSandbox Sandbox { get; set; }

        public Lua LuaState { get; set; }

        /// <summary>
        /// The current script should be paused by the debugging hook. [TEMP?]
        /// </summary>
        public bool CurScriptPausing { get; set; }

        /// <summary>
        /// The current script should be stopped by the debugging hook. [TEMP?]
        /// </summary>
        public bool CurScriptStopping { get; set; }

        /// <summary>
        /// The Script that is running now.
        /// </summary>
        public Script CurrentScript { get; set; }

        public ScriptStateManager()
        {   
            ExposedMethods = new List<ScriptMethod>();
            RunningScripts = new List<Script>();
            // Pretty temporary code lol

            Logging.Log("[TEMP] Initialising Lua sandbox (this code will be changed)...");
            Sandbox = new LuaSandbox();
            Lua_LoadScript(Sandbox);
        }

        /// <summary>
        /// Prepares a loaded Script object to be run. 
        /// </summary>
        /// <param name="Sc"></param>
        /// <returns></returns>
        public LoadScriptResult Lua_LoadScript(Script Sc)
        {
            LoadScriptResult LSR = new LoadScriptResult();

            if (Sc.Name == null
                || Sc.Name.Length == 0)
            {
                ErrorManager.ThrowError("Script Loader", "CannotRunScriptWithInvalidNameException");
                LSR.FailureReason = "Cannot run a script with an invalid name!";
                return LSR; 
            }
            else
            {  
                RunningScripts.Add(Sc); // run it.

                LSR.Successful = true;
                return LSR; 
            }

        }

        internal void Lua_RunScriptUsingSandbox(Script Sc)
        {
            Logging.Log($"Preparing to run Lua script {Sc.Name}...", ClassName);
            Sc.State = ScriptState.Running;

            Sandbox.CoroutineName = Sc.CoroutineName;
            Logging.Log($"Setting up environment... [TODO: COROUTINES]", ClassName);
            LuaState.DoString(Sandbox.Content); // todo: coroutine
            Lua_EnforceTrust();
            LuaState.DoString(Sandbox.Content_PerformRun);

            Sc.State = ScriptState.Completed;
        }

        private void Lua_EnforceTrust()
        {
            Logging.Log("Enforcing trust...", ClassName);

            // This sort of breaks the Liskov substitution principle.
            // Too bad!
            
            try 
            {
                Type TrustedScript = typeof(TrustedScript);

                List<Type> DerivedTypes = TrustedScript.GetAllDerivedClasses();

                foreach (Type DerivedType in DerivedTypes)
                {
                    TrustedScript NewTrustedScript = (TrustedScript)Activator.CreateInstance(DerivedType);

                    Logging.Log($"Running trusted script with name {NewTrustedScript.Name}...", ClassName);

                    // this is probably dumb
                    // we should load these normally?
                    LuaState.DoString(NewTrustedScript.Content);
                }
            }
            catch (LuaScriptException err)
            {
                // in release too as this, 100% of the time, means engine bug and needs to be debugged
                ErrorManager.ThrowError(ClassName, "FatalErrorExecutingTrustedScriptException", $"ENGINE BUG!!! A TrustedScript caused a fatal error when executing. This means that the engine is borked - please file a bug report! Include the following information:\n\n{err}");
            }
        }

        /// <summary>
        /// Manages the currently running Lua script.
        /// </summary>
        public void Lua_RunLuaScripts(Lua LuaState)
        {
            if (LuaState == null)
            {
                ErrorManager.ThrowError(ClassName, "LuaStateFailureException");
                return; // will never run 
            }
            else
            {
                for (int i = 0; i < RunningScripts.Count; i++)
                {
                    Script Sc = RunningScripts[i];

                    CurrentScript = Sc;
                    
                    if (!Sc.IsSandbox)
                    {
                        try
                        {
                            // Lua state machine
                            switch (Sc.State)
                            {
                                case ScriptState.NotStarted:
                                    bool NameIsNull = (Sc.Name == null);

                                    if (Sc.Content == null
                                    || Sc.Content.Length == 0)
                                    {
                                        ErrorManager.ThrowError(ClassName, "AttemptedToRunLuaScriptWithNoContentException", "The currently running script has null or empty Content! The script has been terminated.");
                                        Sc.State = ScriptState.Terminated;
                                    }
                                    else
                                    {
                                        if (LuaState["__SCRIPTCONTENT"].ToString() != Sc.Content) LuaState["__SCRIPTCONTENT"] = Sc.Content; // temp?
                                    }

                                    Lua_RunScriptUsingSandbox(Sc);
                                    return; 

                                case ScriptState.Running: 

                                    return; 
                                case ScriptState.Paused:
                                    Sc.Timer.Tick();
                                    return; 
                                case ScriptState.Completed:
                                case ScriptState.Terminated:
                                    Sc.Stop();
                                    RunningScripts.Remove(Sc);
                                    return; 
                            }

                        }
                        catch (LuaScriptException err)
                        {
                            if (Sc.Name != null)
                            {
                                ErrorManager.ThrowError(ClassName, "LuaScriptCrashedException", $"The script {Sc.Name} terminated due to a fatal execution error: {err.Message}", err);
                            }
                            else
                            {
                                ErrorManager.ThrowError(ClassName, "LuaScriptCrashedException", $"A script has terminated due to a fatal execution error: {err.Message}", err);

                            }

                            RunningScripts.Remove(Sc);
                        }

                    }
                }

            }


        }

        private void Lua_PauseCurrentScript() //REDESIGN TO HAVE SCRIPTTIMER
        {
            CurrentScript.State = ScriptState.Paused;
            CurScriptPausing = true;
            return;
        }

    }
}
