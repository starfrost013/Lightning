using NLua;
using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// CXScriptingService
    /// 
    /// January 14, 2022 (modified January 22, 2022)
    /// 
    /// Provides C# scripting services for Lightning.
    /// </summary>
    public partial class CXScriptingService : Service
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "CXScriptingService";

        internal override ServiceImportance Importance => ServiceImportance.High; // may be rebootable?

        /// <summary>
        /// The client Lightning application.
        /// </summary>
        private App ClientApp { get; set; }

        /// <summary>
        /// The currently loaded GameDLL.
        /// </summary>
        private static Assembly CurrentGameDLL { get; set; }

        public static bool IsGameDLLLoaded => (CurrentGameDLL == null);

        public override ServiceStartResult OnStart()
        {
            Logging.Log("ScriptingService Init [CX Enabled]", ClassName);

            OnLoad += OnLoaded; // Register event handler for DLL loading.
            return new ServiceStartResult { Successful = true };
        }

        public void OnLoaded()
        {
            // check XML is loaded
            LoadGameDLLResult LGDR = new LoadGameDLLResult();

            if (DataModel.DATAMODEL_LASTXML_PATH != null)
            {
                try
                {
                    LGDR = CX_LoadGameDLL();
                }
                catch (Exception ex)
                {
                    ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", $"Error occurred loading GameDLL:\n\n{ex}");
                }
            }
            else
            {
                return;
            }

            if (LGDR.Successful)
            {
                CurrentGameDLL = LGDR.GameDLL;

            }
            else
            {
                ServiceNotification SN1 = new ServiceNotification(ServiceNotificationType.UnrecoverableCrash, ClassName, $"Failed to load GameDLL: {LGDR.FailureReason}");
                ServiceNotifier.NotifySCM(SN1);
                return;
            }

            return;
        }

        private LoadGameDLLResult CX_LoadGameDLL()
        {
            LoadGameDLLResult LGDR = new LoadGameDLLResult();

            // already checked that it exists as it has already been added by DDMS
            // and also already checked for engine init.

            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GMIR = Ws.GetFirstChildOfType("GameSettings");

            // checks for gamesettings have already been done at this point
            GameSettings GS = (GameSettings)GMIR.Instance;

            GetGameSettingResult GGSR_DLL = GS.GetSetting("GameDLL");

            if (!GGSR_DLL.Successful)
            {
                string ErrorString = $"Error loading GameDLL: The GameDLL setting must be set!\nScripts will not be run.";
                ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", ErrorString);
                LGDR.FailureReason = ErrorString;
                return LGDR;
            }
            else
            {
                GameSetting GameDLLSetting = GGSR_DLL.Setting;

                
                if (GameDLLSetting.SettingValue == null)
                {
                    string ErrorString = $"Error loading GameDLL: Invalid GameDLL or GameNamespace setting!\nScripts will not be run.";
                    ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", ErrorString);
                    LGDR.FailureReason = ErrorString;
                    return LGDR;
                }
                else
                {
                    string GameDLLPath = (string)GameDLLSetting.SettingValue;

                    try // attempt to load Game DLL
                    {
                        if (!File.Exists(GameDLLPath))
                        {
                            string ErrorString = $"Error loading GameDLL: Cannot find {GameDLLPath}!\nScripts will not be run.";
                            ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", ErrorString);
                            LGDR.FailureReason = ErrorString;
                            return LGDR; 
                        }
                        else
                        {
                            Logging.Log($"Loading GameDLL at {GameDLLPath}", ClassName);

                            LGDR.GameDLL = Assembly.LoadFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{GameDLLPath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        string ErrorString = $"Error loading GameDLL:\n\n{ex}Scripts will not be run.";
                        ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", ErrorString);
                        LGDR.FailureReason = ErrorString;
                        return LGDR;
                    }

                    
                }

                LGDR.Successful = CX_Init_LoadGMain(LGDR.GameDLL);
                return LGDR;
            }
        }

        private void CX_Init_RunGStart()
        {
            Logging.Log("Loading GMain.Start()");
            ClientApp.Start();
        }

        private bool CX_Init_LoadGMain(Assembly GameDLL)
        {
            Logging.Log("Finding GMain class...", ClassName);

            Type[] TheTypes = GameDLL.GetTypes();

            foreach (Type Type in TheTypes)
            {
                if (Type.IsSubclassOf(typeof(App)))
                {
                    Logging.Log("Found GMain!", ClassName);

                    // hack to get around datamodel.createinstance problems
                    ClientApp = (App)Activator.CreateInstance(Type);
                    Workspace Ws = DataModel.GetWorkspace();
                    Ws.AddChildI(ClientApp); // works as we have the assembly loaded


                    return true;
                }
            }

            return false; 

        }

        public override void Poll()
        {
            if (ClientApp != null) ClientApp.Render(); 
        }

        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("CXScriptingService Shutdown", ClassName);
           
            ServiceShutdownResult SSR = new ServiceShutdownResult { Successful = true };
            return SSR; 
        }

        public override void OnDataSent(ServiceMessage Data)
        {
            return;
        }
    }
}
