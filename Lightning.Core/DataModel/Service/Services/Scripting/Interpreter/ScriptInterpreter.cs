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
        public void Interpret()
        {
            foreach (Script Sc in RunningScripts)
            {
                string CurLine = Sc.ScriptContent[Sc.CurrentlyExecutingLine];

                Sc.CurrentlyExecutingLine++; 
            }
        }

        /// <summary>
        /// Interprets an individual Token in each script.
        /// 
        /// </summary>
        /// <param name="T0"></param>
        private void InterpretToken(Token T0)
        {
            Type T0Type = T0.GetType();


            if (T0Type == typeof(OperatorToken))
            {
                OperatorToken T0_Op = (OperatorToken)T0;

                switch (T0_Op.Type)
                {
                    case OperatorTokenType.Assignment:
                        return;
                    case OperatorTokenType.Plus: // todo: implement last token set feature so that we can do stuff like 1 + 2 + 3 + 4 + 5

                        for (int i = 0; i < T0_Op.Children.Count; i++)
                        {
                            Token NT = T0_Op.Children[i];

                            try
                            {

                                if (i == 0)
                                {
                                    Variable Vf = new Variable();
                                    // TEMP
                                    Vf.Name = Variable.GenerateAutomaticVariableName();
                                    continue; 
                                }
                                else
                                {
                                    Type NTType = NT.GetType();

                                    if (NTType == typeof(NumberToken))
                                    {
                                        NumberToken OT = (NumberToken)NT;
                                       
                                    }
                                    else if (NTType == typeof(ValueToken))
                                    {
                                        ValueToken VT = (ValueToken)NT;
                                    }
                                }
                            }
                            catch (InvalidOperationException err)
                            {
                                ErrorManager.ThrowError(ClassName, "SyntaxErrorException", "Must have a variable or number within an operation!", err);
                            }


                        }

                        return;
                }
            }
            
        }
    }
}
