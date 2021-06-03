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

        public ScriptInterpreter()
        {   
            ExposedMethods = new List<ScriptMethod>();
            RunningScripts = new List<Script>();
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
        /// Advances all scripts by one line. Handles Runtime Errors.
        /// </summary>
        public void Interpret()
        {
            foreach (Script Sc in RunningScripts)
            {

            }
        }

        private void InterpretLine()
        {

        }
    }
}
