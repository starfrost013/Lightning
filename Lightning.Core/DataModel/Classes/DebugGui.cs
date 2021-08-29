﻿using Lightning.Utilities;
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
    /// Defines a special type of GUI used for debugging.
    /// </summary>
    public class DebugGui : Gui
    {
        internal override string ClassName => "DebugGui";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;
        /// <summary>
        /// The header of this debug page.
        /// </summary>
        public string Header { get; set; }

        internal bool DEBUGPAGE_INITIALISED { get; set; }

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

                        //DP_Init_CreateDebugPage(DbgPageBegin, DbgPageEnd);
                        
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


        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!DEBUGPAGE_INITIALISED)
            {
                DP_Init();
            }
            else
            {
                DoRender(SDL_Renderer, Tx);
            }
        }


        private void DoRender(Renderer SDL_Renderer, ImageBrush Tx)
        {
            List<DebugPage> DebugPages = GetDebugPages();

            foreach (DebugPage DP in DebugPages)
            {
                if (DP.IsOpen)
                {
                    DP.Render(SDL_Renderer, Tx);
                    break; // only render one page.
                }
           }
        }

        internal List<DebugPage> GetDebugPages()
        {
            // TEMP CODE UNTIL WE CAN FIX THIS SHIT 
            GetMultiInstanceResult GMIR = GetAllChildrenOfType("DebugPage");

            if (!GMIR.Successful
            || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "UnableToAcquireDebugGuiException");
                return null; 
            }
            else
            {
                List<Instance> Instances = GMIR.Instances;
                List<DebugPage> DebugPages = ListTransfer<Instance, DebugPage>.TransferBetweenTypes(Instances);

                return DebugPages; 
                
            }
        }
    }
}
