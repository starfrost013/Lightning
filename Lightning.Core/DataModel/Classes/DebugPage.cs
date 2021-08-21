using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DebugPage (Lightning IGD Services)
    /// 
    /// August 20, 2021 (modified August 21, 2021)
    /// 
    /// Defines the root class for debug pages
    /// </summary>
    public class DebugPage : PhysicalObject
    {
        internal override string ClassName => "DebugPage";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;
        /// <summary>
        /// The header of this debug page.
        /// </summary>
        public string Header { get; set; }

        internal bool DEBUGPAGE_INITIALISED { get; set; }

        private ScreenGui DebugGui { get; set; } // this is shit

        internal void DP_Init()
        {

            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful
            || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
            }
            else
            {
                GameSettings Settings = (GameSettings)GIR.Instance;
                GetGameSettingResult GGSR_WindowWidth = Settings.GetSetting("WindowWidth");
                GetGameSettingResult GGSR_WindowHeight = Settings.GetSetting("WindowHeight");

                if (!GGSR_WindowWidth.Successful
                || !GGSR_WindowHeight.Successful
                || GGSR_WindowWidth.Setting == null
                || GGSR_WindowHeight.Setting == null)
                {
                    ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "Failed to obtain WindowWidth and WindowHeight GameSettings for in-game debugging services!");
                    return;
                }
                else
                {

                    GameSetting WindowWidth_Setting = GGSR_WindowWidth.Setting;
                    GameSetting WindowHeight_Setting = GGSR_WindowHeight.Setting;

                    try
                    {
                        int WindowWidth = (int)WindowWidth_Setting.SettingValue;
                        int WindowHeight = (int)WindowHeight_Setting.SettingValue;

                        Vector2 DbgPageBegin = new Vector2(WindowWidth * 0.2, WindowHeight * 0.2); // todo: gamesetting for this
                        Vector2 DbgPageEnd = new Vector2(WindowWidth * 0.8, WindowHeight * 0.8);

                        DP_Init_CreateDebugPage(DbgPageBegin, DbgPageEnd);
                        
                    }
                    catch (Exception err)
                    {
#if DEBUG
                        ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", $"WindowWidth and WindowHeight have an invalid value!\n\n{err}");
#else
                        ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "WindowWidth and WindowHeight have an invalid value!");
#endif
                        return; 
                    }

                    DEBUGPAGE_INITIALISED = true;
                    return;

                }
            }
        }

        private void DP_Init_CreateDebugPage(Vector2 DbgPageBegin, Vector2 DbgPageEnd)
        {

        }
    }
}
