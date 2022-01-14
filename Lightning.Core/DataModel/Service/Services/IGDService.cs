using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// IGDService (In-Game Debugging Service)
    /// 
    /// August 20, 2021 (modified August 29, 2021)
    /// 
    /// Provides in-game debugging services for Lightning.
    /// </summary>
    public class IGDService : Service 
    {

        internal override string ClassName => "IGDService";

        private bool IGDSERVICE_INITIALISED { get; set; }

        public DebugSettings Settings { get; set; }

        private string DebugGuiName => "DebugGui";

        public override ServiceStartResult OnStart() 
        {
            Logging.Log("IGDService Init", ClassName);

            Settings = new DebugSettings();

            ServiceStartResult SSR = new ServiceStartResult();

            SSR.Successful = true; 
            
            return SSR;
        }

        private void OnStart_ShutdownDebugNotEnabled()
        {
            string ReasonText = "DebugMode GameSetting turned off - shutting down IGDService as in-game debugging services are disabled...";

            Logging.Log(ReasonText, ClassName); 

            ServiceNotification SN = new ServiceNotification();
            SN.Reason = ReasonText;

            SN.ServiceClassName = ClassName;
            SN.NotificationType = ServiceNotificationType.Shutdown;
            ServiceNotifier.NotifySCM(SN);
        }

        public override ServiceShutdownResult OnShutdown() => new ServiceShutdownResult { Successful = true };

        internal override ServiceImportance Importance => ServiceImportance.Low;

        public override void Poll()
        {
            if (!IGDSERVICE_INITIALISED)
            {
                IGD_Init();
            }

            return;
        }

        
        private void IGD_Init()
        {

            Workspace Ws = DataModel.GetWorkspace();
            Logging.Log("Determining if in-game debugging services are enabled...", ClassName);

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful
            || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
            }
            else
            {
                GameSettings GS = (GameSettings)GIR.Instance;

                GetGameSettingResult GGSR_DebugMode = GS.GetSetting("DebugMode");

                if (!GGSR_DebugMode.Successful)
                {
                    OnStart_ShutdownDebugNotEnabled();
                    return; 
                }
                else
                {
                    GameSetting DebugMode_Setting = GGSR_DebugMode.Setting;

                    bool IsThisService = (bool)DebugMode_Setting.SettingValue;

                    if (!IsThisService)
                    {
                        OnStart_ShutdownDebugNotEnabled();
                        return; 
                    }
                }
            }

#if DEBUG

            Settings.CurrentDebugString = DebugStrings.GetDebugString();
            Init_CreateDebugText(); // call before font loading so we don't have to load the font again
            Init_CreateDebugGui();
#endif
            IGDSERVICE_INITIALISED = true;

        }


        private void Init_GetWindowWidthAndHeight()
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
                GameSettings GS = (GameSettings)GIR.Instance;

                GetGameSettingResult GGSR_INeedToFixThisSystem = GS.GetSetting("WindowWidth");
                GetGameSettingResult GGSR_WindowHeight = GS.GetSetting("WindowHeight");

                if (!GGSR_INeedToFixThisSystem.Successful
                || !GGSR_WindowHeight.Successful
                || GGSR_INeedToFixThisSystem.Setting == null
                || GGSR_WindowHeight.Setting == null)
                {
                    ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "Failed to obtain WindowWidth or WindowHeight game setting!");
                    return; 
                }
                else
                {
                    GameSetting WindowWidth_Setting = GGSR_INeedToFixThisSystem.Setting;
                    GameSetting WindowHeight_Setting = GGSR_WindowHeight.Setting;

                    try
                    {
                        Settings.WindowHeight = (int)WindowWidth_Setting.SettingValue;
                        Settings.WindowWidth = (int)WindowHeight_Setting.SettingValue;
                        
                    }
                    catch (Exception err)
                    {
#if DEBUG 
                        ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", $"Failed to obtain WindowWidth or WindowHeight game setting!\n\n{err}");
#else
                        ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "Failed to obtain WindowWidth or WindowHeight game setting!");
#endif
                    }
                }

            }
        }

        /// <summary>
        /// DEBUG ONLY: Renders engine debugging information.
        /// </summary>
        private void Init_CreateDebugText()
        {
#if DEBUG
            // Because Text by definition must be within a GUI,
            // we can't just add text.
            //
            // As this is a debug feature, we are just going to create a new screengui object
            // and put it in the workspace.
            // 
            // This is better than adding tons of (redundant...) code in order to write a debug feature.

            GuiRoot GR = (GuiRoot)DataModel.CreateInstance("GuiRoot"); // will enter workspace

            ScreenGui SG = (ScreenGui)DataModel.CreateInstance("ScreenGui", GR);

            // create 3 lines of text (todo: multiline text)
            Text DebugTextLine1 = (Text)DataModel.CreateInstance("Text", SG);
            Text DebugTextLine2 = (Text)DataModel.CreateInstance("Text", SG);
            Text DebugTextLine3 = (Text)DataModel.CreateInstance("Text", SG);
            
            Color4 DebugTextColour = new Color4(255, 255, 0, 0);

            string DoNotUse = "Debug GUI Component - Do not use!";

            GR.Name = DoNotUse;
            SG.Name = DoNotUse;
            DebugTextLine1.Name = DoNotUse;
            DebugTextLine2.Name = DoNotUse;
            DebugTextLine3.Name = DoNotUse;

            DebugTextLine1.Content = $"Lightning for {Platform.PlatformName} {LVersion.GetShortenedVersionString()} (Debug)";
            DebugTextLine2.Content = $"DataModel version {DataModel.DATAMODEL_API_VERSION_MAJOR}.{DataModel.DATAMODEL_API_VERSION_MINOR}.{DataModel.DATAMODEL_API_VERSION_REVISION}";
            DebugTextLine3.Content = Settings.CurrentDebugString;

            // TODO: default positioning stuff so we don't have to do this

            Vector2 Position = new Vector2(0, 0);

            // TEMP for DEBUG only
            GR.Position = Position;
            SG.Position = Position;
            DebugTextLine1.Position = Position;
            DebugTextLine2.Position = new Vector2(Position.X, Position.Y + 12);
            DebugTextLine3.Position = new Vector2(Position.X, Position.Y + 24);

            DebugTextLine1.DisableTTF = true;
            DebugTextLine2.DisableTTF = true;
            DebugTextLine3.DisableTTF = true;

            DebugTextLine1.Colour = DebugTextColour;
            DebugTextLine2.Colour = DebugTextColour;
            DebugTextLine3.Colour = DebugTextColour;
#else
            throw new Exception("Do not call on Release builds ever!");
#endif
        }

        private void Init_CreateDebugGui()
        {
            Init_GetWindowWidthAndHeight();

            Vector2 DbgPageBegin = new Vector2(0.2, 0.2);
            Vector2 DbgPageEnd = new Vector2(0.8, 0.8);

            Init_CreateDebugPage(DbgPageBegin, DbgPageEnd);
        }

        private void Init_CreateDebugPage(Vector2 DbgPageBegin, Vector2 DbgPageEnd)
        {
            Workspace Ws = DataModel.GetWorkspace();

            // TEMP code until nested rendering
            DebugGui DGUI = (DebugGui)DataModel.CreateInstance("DebugGui", Ws);

            DGUI.Name = DebugGuiName;
            DGUI.Position = (Settings.WindowSize * DbgPageBegin);
            DGUI.Settings = Settings; 

            TextBox Main = (TextBox)DGUI.AddChild("TextBox");

            Main.Position = (Settings.WindowSize * DbgPageBegin);
            Main.Size = (Settings.WindowSize * DbgPageEnd);
            Main.BackgroundColour = new Color4(127, 0, 0, 0);
            Main.Content = $"Lightning Debug Menu - Lightning {LVersion.GetVersionString()} - {LVersion.BuildDate}";
            Main.DisableTTF = true;
            Main.DoNotAutoResize = true;
            Main.Fill = true;

            MainDebugPage MDP = (MainDebugPage)DataModel.CreateInstance("MainDebugPage", DGUI);
            MDP.IsOpen = true;
            MDP.MDP_Build(); 
        }

        
        public override void OnDataSent(ServiceMessage Data)
        {
            return; 
        }
    }
}
