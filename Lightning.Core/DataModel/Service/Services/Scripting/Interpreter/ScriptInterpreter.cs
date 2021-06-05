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

        public ScriptInterpreterState State { get; set; }

        public ScriptInterpreter()
        {   
            ExposedMethods = new List<ScriptMethod>();
            RunningScripts = new List<Script>();
            State = new ScriptInterpreterState(); 
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

        /// <summary>
        /// Advances all scripts by one token and its children. Handles Runtime Errors.
        /// </summary>
        public void Interpret(Lua LuaState)
        {
            foreach (Script Sc in RunningScripts)
            {
                string CurLine = Sc.ScriptContent[Sc.CurrentlyExecutingLine];

                Sc.CurrentlyExecutingLine++;

                InterpretToken(Sc, true, LuaState); 
            }
        }

        /// <summary>
        /// Interprets an individual Token in each script.
        /// 
        /// </summary>
        /// <param name="T0"></param>
        private void InterpretToken(Script Sc, bool IsLua = false, Lua LuaState = null)
        {

            try
            {
                if (LuaState != null
                    && !Sc.IsPaused)
                {
                    // this is a crap implementation
                    // but it might work
                    Sc.CurrentScriptRunningStopwatch.Start();
                    LuaState.DoString(Sc.Content);

                    Sc.CurrentScriptRunningStopwatch.Stop();

                    GlobalSettings GS = DataModel.GetGlobalSettings();

                    if (Sc.CurrentScriptRunningStopwatch.ElapsedMilliseconds > GS.MaxLuaScriptExecutionTime)
                    {
                        // prevent the script from running

                        // don't throw an error
                        if (Sc.Name != null)
                        {
                            Logging.Log($"The Lua script {Sc.Name} was stopped due to reaching the global execution time limit ({GS.MaxLuaScriptExecutionTime}ms)", ClassName, MessageSeverity.Error);
                            RunningScripts.Remove(Sc);
                        }

                    }
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

            }
            
        }
    }
}
