using NLua;
using NLua.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// LightningScript
    /// 
    /// A dynamically typed, interpreted, DataModel-aware scripting language for Lightning
    /// 
    /// April 27, 2021 (modified June 3, 2021)
    /// </summary>
    public class ScriptInterpreter : Instance // in the DataModel for now
    {
        internal override string ClassName => "ScriptInterpreter";

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
        public ScriptInterpreter()
        {   
            ExposedMethods = new List<ScriptMethod>();
            RunningScripts = new List<Script>();
            // Pretty temporary code lol
            Sandbox = new LuaSandbox();
            LoadScript(Sandbox);
        }

        /// <summary>
        /// Prepares a loaded Script object to be run. 
        /// </summary>
        /// <param name="Sc"></param>
        /// <returns></returns>
        public LoadScriptResult LoadScript(Script Sc)
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

        internal void RunScriptUsingSandbox() => LuaState.DoString(Sandbox.ProtectedContent); 

        /// <summary>
        /// Advances all scripts by one token and its children. Handles Runtime Errors.
        /// </summary>
        public void Interpret(Lua LuaState)
        {
            for (int i = 0; i < RunningScripts.Count; i++)
            {
                Script Sc = RunningScripts[i];

                if (!Sc.IsSandbox)
                {
                    InterpretToken(Sc, true, LuaState);
                }
                
                
            }
        }

        /// <summary>
        /// Runs a Lua script.
        /// </summary>
        /// <param name="T0"></param>
        private void InterpretToken(Script Sc, bool IsLua = false, Lua LuaState = null)
        {

            try
            {
                if (LuaState != null
                    && !Sc.IsPaused)
                {
                    bool NameIsNull = false;

                    if (Sc.Name == null) NameIsNull = true; 

                    // this is a crap implementation
                    // but it might work
                    Sc.CurrentScriptRunningStopwatch.Start();

                    Type ScType = Sc.GetType();

                    if (ScType.IsSubclassOf(typeof(CoreScript)))
                    {
                        CoreScript CoreSc = (CoreScript)Sc;

                        if (CoreSc.ProtectedContent == null
                                || CoreSc.ProtectedContent.Length == 0)
                        {
                            if (NameIsNull)
                            {
                                ErrorManager.ThrowError(ClassName, "AttemptedToRunLuaCoreScriptWithNoProtectedContentException", "The currently running CoreScript has null or empty ProtectedContent! The script has been terminated.");
                            }
                            else
                            {
                                ErrorManager.ThrowError(ClassName, "AttemptedToRunLuaCoreScriptWithNoProtectedContentException", $"The CoreScript {CoreSc.Name} has null or empty ProtectedContent! The script has been terminated.");
                            }


                            RunningScripts.Remove(CoreSc);

                            return;
                        }
                        else
                        {
                            // Set the __SCRIPTCONTENT global variable to allow load() to be used so that we can sandbox the environment.
                            LuaState["__SCRIPTCONTENT"] = CoreSc.ProtectedContent;
                            RunScriptUsingSandbox();
                        }
                    }
                    else
                    {

                        Sc.CurrentScriptRunningStopwatch.Start();

                        if (Sc.Content == null
                        || Sc.Content.Length == 0)
                        {
                            if (NameIsNull)
                            {
                                ErrorManager.ThrowError(ClassName, "AttemptedToRunLuaScriptWithNoContentException", "The currently running script has null or empty Content! The script has been terminated.");
                            }
                            else
                            {
                                ErrorManager.ThrowError(ClassName, "AttemptedToRunLuaScriptWithNoContentException", $"The Script {Sc.Name} has null or empty Content! The script has been terminated.");
                            }
                        }
                        else
                        {
                            // Set the __SCRIPTCONTENT global variable to allow load() to be used so that we can sandbox the environment.

                            // This will run the sandbox, which will sandbox the user's script, and run it using Lua's pcall() function. Woo!
                            LuaState["__SCRIPTCONTENT"] = Sc.Content;
                            RunScriptUsingSandbox();
                        }
                    }
                    // Check if the script content is null or empty and then call dostring to run the script
                    // script will be captured by Lua's debug hook 

                    Sc.CurrentlyExecutingLine = 0;

                    Sc.CurrentScriptRunningStopwatch.Stop();

                    RunningScripts.Remove(Sc); 

                }
                else
                {

                    ErrorManager.ThrowError(ClassName, "LuaStateFailureException");

                    return; // will never run 


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
