using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptingService.
    /// 
    /// April 13, 2021 (modified May 3, 2021)
    /// 
    /// Provides scripting services. Manages LightningScript scripts.
    /// </summary>
    public partial class ScriptingService : Service
    {
        internal override string ClassName => "ScriptingService";
        internal override ServiceImportance Importance => ServiceImportance.High; // may be rebootable?
        internal ScriptInterpreter ScriptGlobals { get; set; }
        private bool SCRIPTS_LOADED { get; set; }

        internal ScriptTokeniser Tokeniser { get; set; }

        public ScriptingService()
        {
            ScriptGlobals = new ScriptInterpreter();
            Tokeniser = new ScriptTokeniser();
        }

        public override ServiceStartResult OnStart()
        {
            Logging.Log("ScriptingService Init", ClassName);

            // Register the Scripting API.
            RegisterAPI();

            ServiceStartResult SSR = new ServiceStartResult { Successful = true };

            return SSR;
        }

        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("ScriptingService Shutdown", ClassName);
            ServiceShutdownResult SSR = new ServiceShutdownResult { Successful = true };
            return SSR; 
        }

        /// <summary>
        /// Registers a method for the usage of scripts.
        /// 
        /// ONLY THROW FATAL ERRORS!!!!!
        /// </summary>
        /// <param name="MethodFullName"></param>
        internal void RegisterMethod(string MethodFullName)
        {
            string ProcessedMethodName = MethodFullName.Replace(";", ".");

            // Verify that this method can be instantiated. If it fails...
            if (!XmlUtil.CheckIfValidTypeForInstantiation(ProcessedMethodName))
            {
                string ErrorString = $"Attempted to register {MethodFullName}, which is not in the System or Lightning.* namespaces and therefore cannot be registered for use by scripts!";

                ErrorManager.ThrowError(ClassName, "AttemptedToExposeInvalidMethodToScriptingException", ErrorString);

                // Crash.
                ServiceNotification WeAreCrashing = new ServiceNotification(ServiceNotificationType.Crash, ClassName, ErrorString);
                ServiceNotifier.NotifySCM(WeAreCrashing); // Notify the SCM that we're crashing. 
            }
            else
            {
                Logging.Log($"Registering method {MethodFullName}", ClassName);

                GetScriptMethodResult GSMR = RegisterMethod_DoRegisterMethod(MethodFullName);

                if (!GSMR.Successful)
                {
                    Debug.Assert(GSMR.FailureReason != null);
                    ErrorManager.ThrowError(ClassName, "ErrorExposingMethodToScriptingException", GSMR.FailureReason);
                }
                else
                {
                    Debug.Assert(GSMR.Method != null);
                    ScriptMethod SM = GSMR.Method;
                    Logging.Log($"Exposing {GSMR.Method.Name} to scripting...", ClassName);
                    ScriptGlobals.ExposedMethods.Add(SM);
                }

            }
        }

        private GetScriptMethodResult RegisterMethod_DoRegisterMethod(string MethodFullName)
        {
            GetScriptMethodResult GSMR = new GetScriptMethodResult();

            ScriptMethod SM = new ScriptMethod();

            string[] Mth_Spb = MethodFullName.Split(";");

            if (Mth_Spb.Length != 3)
            {
                GSMR.FailureReason = "Attempted to parse an invalid method name: Only a namespace was supplied or a class was supplied without a method!";
                return GSMR;
            }
            else
            {
                string MethodNamespace = Mth_Spb[0];
                string MethodClass = Mth_Spb[1];
                string Method = Mth_Spb[2];

                Type MType = Type.GetType($"{MethodNamespace}.{MethodClass}");

                if (!MType.IsSubclassOf(typeof(Instance)))
                {
                    GSMR.FailureReason = $"{MType} is not in the DataModel and therefore cannot be exposed to scripts!";
                    return GSMR; 
                }
                else
                {
                    // Create a test instance
                    if (!MType.IsAbstract)
                    {
                        Instance TestIns = (Instance)Activator.CreateInstance(MType); // no point running tons of extra code to add and then remove from the DataModel.

                        // we have to manually generate instanceinfo here as we don't want to add it to the datamodel
                        TestIns.GenerateInstanceInfo();

                        InstanceInfoMethod CIIM = TestIns.Info.GetMethod(Method);

                        if (CIIM == null)
                        {
                            GSMR.FailureReason = $"The method {Method} does not exist in the class {MethodClass}!";
                        }
                        else
                        {
                            SM.Name = MethodFullName; // set it!

                            foreach (InstanceInfoMethodParameter IIMP in CIIM.Parameters)
                            {
                                ScriptMethodParameter SMP = new ScriptMethodParameter();

                                SMP.Name = IIMP.ParamName;
                                SMP.Type = IIMP.ParamType;

                                SM.Parameters.Add(SMP);
                            }
                        }
                    }
                    
                }

                GSMR.Successful = true;
                GSMR.Method = SM;
                return GSMR; 
            }

        }

        public override void Poll()
        {
            if (!SCRIPTS_LOADED)
            {
                Logging.Log("Serialising all scripts...", ClassName);
                SerialiseAll();
            }
            else
            {
                return;
            }
            
        }

        public void SerialiseAll()
        {
            // TODO: "GETALLCHILDRENOFTYPE" that is also recursive
            Workspace Ws = DataModel.GetWorkspace();

            foreach (Instance Ins in Ws.Children)
            {
                if (Ins.GetType() == typeof(Script))
                {
                    Script LeScript = (Script)Ins;

                    TokenListResult TLR = Tokeniser.Tokenise((Script)LeScript);

                    if (!TLR.Successful)
                    {
                        ErrorManager.ThrowError(ClassName, "ErrorLoadingOrTokenisingScriptException");
                    }
                    else
                    {
                        LeScript.Tokens = TLR.TokenList;
                    }
                }
            }

            SCRIPTS_LOADED = true;
        }

    }
}
