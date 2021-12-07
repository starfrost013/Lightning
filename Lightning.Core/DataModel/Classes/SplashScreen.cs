using System;
using System.Collections.Generic;
using System.IO; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// SplashScreen
    /// 
    /// July 18, 2021
    /// 
    /// Defines a splash screen
    /// </summary>
    public class SplashScreen : PhysicalObject
    {
        internal override string ClassName => "SplashScreen";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;
        private bool SPLASHSCREEN_INITIALISED { get; set; }

        private bool SPLASHSCREEN_INITIALISATION_FAILED { get; set; }

        private string TexturePath { get; set; }

        internal override bool Deprecated => true;

        private void Init()
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
                GameSettings TheGameSettings = (GameSettings)GIR.Instance;
                GetGameSettingResult GGSR_Path = TheGameSettings.GetSetting("SplashScreenPath");

                if (!GGSR_Path.Successful
                    || GGSR_Path.Setting == null)
                {
                    Init_TryGlobalSettings();
                }
                else
                {
                    GameSetting SplashScreenPathSetting = GGSR_Path.Setting;
                    string SettingValue = (string)SplashScreenPathSetting.SettingValue;

                    if (!File.Exists(SettingValue))
                    {
                        Init_TryGlobalSettings();
                    }
                    else
                    {
                        TexturePath = SettingValue;
                    }
                }
            }

            
        }
       
        /// <summary>
        /// Tries to check globalsettings to load the default. 
        /// </summary>
        private void Init_TryGlobalSettings()
        {
            GlobalSettings GS = DataModel.GetGlobalSettings();

            if (GS.DefaultSplashScreen == null)
            {
                SPLASHSCREEN_INITIALISATION_FAILED = true;
                DataModel.RemoveInstance(this, this.Parent);
            }
            else
            {
                // set the texture path
                TexturePath = GS.DefaultSplashScreen;
            }
        }

        public override void Render(Renderer SDL_Renderer, ImageBrush Tx)
        {
            if (!SPLASHSCREEN_INITIALISED)
            {
                base.PO_Init(); 
                Init(); 
            }
            else
            {
                if (Tx.Path != TexturePath) Tx.Path = TexturePath; 
                if (!SPLASHSCREEN_INITIALISATION_FAILED) base.Render(SDL_Renderer, Tx); 
            }
        }
    }
}
