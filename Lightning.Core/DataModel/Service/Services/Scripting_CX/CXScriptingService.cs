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
    /// January 14, 2022 (modified January 15, 2022: add GameDLL loading functionality)
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

        public override ServiceStartResult OnStart()
        {
            Logging.Log("ScriptingService Init [CX Enabled]", ClassName);

            // check XML is loaded
            LoadGameDLLResult LGDR = new LoadGameDLLResult();
            ServiceStartResult SSR = new ServiceStartResult();

            if (DataModel.DATAMODEL_LASTXML_PATH != null) LGDR = CX_LoadGameDLL();

            if (LGDR.Successful)
            {
                DataModel.CurrentGameDLL = LGDR.GameDLL;
            }
            else
            {
                SSR.FailureReason = $"Failed to load GameDLL for C# Scripting: {LGDR.FailureReason}";
                return SSR; 
            }

            SSR.Successful = true;
            return SSR;
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
            GetGameSettingResult GGSR_NameSpace = GS.GetSetting("GameNamespace");

            if (!GGSR_DLL.Successful
            || !GGSR_NameSpace.Successful)
            {
                string ErrorString = $"Error loading GameDLL: Both GameDLL and GameNamespace setting must be found!\nScripts will not be run.";
                ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", ErrorString);
                LGDR.FailureReason = ErrorString;
                return LGDR;
            }
            else
            {
                GameSetting GameDLLSetting = GGSR_DLL.Setting;
                GameSetting GameNameSpaceSetting = GGSR_NameSpace.Setting;

                
                if (GameDLLSetting.SettingValue == null
                || GameNameSpaceSetting.SettingValue == null)
                {
                    string ErrorString = $"Error loading GameDLL: Invalid GameDLL or GameNamespace setting!\nScripts will not be run.";
                    ErrorManager.ThrowError(ClassName, "ErrorLoadingGameDLLException", ErrorString);
                    LGDR.FailureReason = ErrorString;
                    return LGDR;
                }
                else
                {
                    string GameDLLPath = (string)GameDLLSetting.SettingValue;
                    string GameNamespace = (string)GameNameSpaceSetting.SettingValue;

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
                            LGDR.GameDLL = Assembly.LoadFile(GameDLLPath);
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

                LGDR.Successful = CX_Init_FindGMain();
                return LGDR;
            }
        }


        private bool CX_Init_FindGMain()
        {
            Logging.Log("Finding GMain class...", ClassName);

            Type[] TheTypes = DataModel.CurrentGameDLL.GetTypes();

            foreach (Type Type in TheTypes)
            {
                if (Type.Name == "GMain")
                {
                    if (Type.IsAbstract // static types are both abstract and sealed
                    && Type.IsSealed)
                    {
                        return true; 
                    }
                }
            }

            return false; 

        }

        public override void Poll()
        {
            throw new NotImplementedException();
        }

        public override ServiceShutdownResult OnShutdown()
        {
            Logging.Log("ScriptingService Shutdown", ClassName);
           
            ServiceShutdownResult SSR = new ServiceShutdownResult { Successful = true };
            return SSR; 
        }

        public override void OnDataSent(ServiceMessage Data)
        {
            return;
        }
    }
}
