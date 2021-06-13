using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Camera
    /// 
    /// April 13, 2021
    /// 
    /// Defines a Camera. A Camera is the viewport of a Lightning level. 
    /// </summary>
    public class Camera : ControllableObject
    {
        internal override string ClassName => "Camera";

        internal override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable | InstanceTags.Instantiable | InstanceTags.Serialisable | InstanceTags.ShownInIDE;

        /// <summary>
        /// Is this Camera active?
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Must be a reference. 
        /// 
        /// The instance we are targeting. 
        /// </summary>
        public PhysicalObject Target { get; set; }

        /// <summary>
        /// The name of the target. 
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// The type of the Camera - see <see cref="CameraType"/>
        /// </summary>
        public CameraType CameraType { get; set; }

        /// <summary>
        /// Camera move left key binding for free cameras.
        /// </summary>
        public ConvertableStringList LeftKeyBinding { get; set; }

        /// <summary>
        /// Camera move right key binding for free cameras.
        /// </summary>
        public ConvertableStringList RightKeyBinding { get; set; }

        /// <summary>
        /// Camera move up key binding for free cameras.
        /// </summary>
        public ConvertableStringList UpKeyBinding { get; set; }

        /// <summary>
        /// Camera move down key binding for free cameras.
        /// </summary>
        public ConvertableStringList DownKeyBinding { get; set; }

        /// <summary>
        /// Camera temporary output to XML key binding for free cameras.
        /// </summary>
        public ConvertableStringList TempSaveKeyBinding { get; set; }

        public override void OnSpawn()
        {
            if (TargetName != null && Target == null)
            {
                Workspace Ws = DataModel.GetWorkspace();

                TryFindTarget(TargetName, Ws); 
            }

            if (LeftKeyBinding == null) LeftKeyBinding = new ConvertableStringList { "LEFT", "A" };
            if (RightKeyBinding == null) RightKeyBinding = new ConvertableStringList { "RIGHT", "D" };
            if (UpKeyBinding == null) UpKeyBinding = new ConvertableStringList { "UP", "W" };
            if (DownKeyBinding == null) DownKeyBinding = new ConvertableStringList { "DOWN", "S" };
            if (TempSaveKeyBinding == null) TempSaveKeyBinding = new ConvertableStringList { "F9" }; ;

        }

        private void TryFindTarget(string TargetName, Instance Parent)
        {
            foreach (Instance InsC in Parent.Children)
            {
                if (InsC.Name == TargetName)
                {
                    Type InstanceType = InsC.GetType();

                    if (InstanceType == typeof(PhysicalObject)
                        || InstanceType.IsSubclassOf(typeof(PhysicalObject)))
                    {
                        Target = (PhysicalObject)InsC;
                    }
                    
                }
            }

            // if we have not found it...
            if (Target == null)
            {
                foreach (Instance InsC in Parent.Children)
                {
                    TryFindTarget(TargetName, InsC);
                }

                // if it has failed...

                // if target is still null 
                if (Target == null)
                {
                    ErrorManager.ThrowError(ClassName, "AttemptedToAcquireInvalidCameraTargetException", $"The Instance with name {TargetName} either does not exist or cannot be used as a CameraTarget!");
                    return; 
                }
                else
                {
                    return; 
                }
            }

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            switch (CameraType)
            {

                case CameraType.FollowObject:
                    RenderFollowObjectCamera(SDL_Renderer);
                    return;
                case CameraType.ChaseObject:
                    RenderChaseObjectCamera(SDL_Renderer);
                    return; 
                case CameraType.Free:
                case CameraType.Fixed:
                default:
                    if (Active)
                    {
                        SDL_Renderer.CCameraPosition = new Vector2(Position.X, Position.Y);
                    }
                    else
                    {
                        return;
                    }
                    return;
            }

        }


        public override void OnKeyDown(Control Control)
        {
            switch (CameraType)
            {
                case CameraType.Free:
                    MoveFreeCamera(Control);
                    return;
                default:
                    return; 
            }


        }

        private void MoveFreeCamera(Control Control)
        {
            bool IsPressingLeft = false;
            bool IsPressingRight = false;
            bool IsPressingUp = false;
            bool IsPressingDown = false;
            bool IsPressingF9 = false;

            string KeyCode = Control.KeyCode.ToString(); 
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            Debug.Assert(GIR.Successful);

            GameSettings GS = (GameSettings)GIR.Instance;

            GetGameSettingResult GGSR1 = GS.GetSetting("FreeCameraMoveIntensityX");
            GetGameSettingResult GGSR2 = GS.GetSetting("FreeCameraMoveIntensityY");

            int FreeCameraMoveIntensityX;
            int FreeCameraMoveIntensityY;

            // if it is not saved.
            if (!GGSR1.Successful || !GGSR2.Successful)
            {
                FreeCameraMoveIntensityX = 10;
                FreeCameraMoveIntensityY = 10;
            }
            else
            {
                GameSetting GSX = GGSR1.Setting;
                GameSetting GSY = GGSR2.Setting;

                FreeCameraMoveIntensityX = (int)GSX.SettingValue;
                FreeCameraMoveIntensityY = (int)GSY.SettingValue;
            }
            
            // cannot use switch statements here :((

            foreach (string CSLItem1 in LeftKeyBinding)
            {
                if (KeyCode == CSLItem1)
                {
                    IsPressingLeft = true; 
                }

            }


            foreach (string CSLItem2 in RightKeyBinding)
            {
                if (KeyCode == CSLItem2)
                {
                    IsPressingRight = true;
                }

            }


            foreach (string CSLItem3 in UpKeyBinding)
            {
                if (KeyCode == CSLItem3)
                {
                    IsPressingUp = true;
                }

            }


            foreach (string CSLItem4 in DownKeyBinding)
            {
                if (KeyCode == CSLItem4)
                {
                    IsPressingDown = true;
                }

            }


            foreach (string CSLItem5 in TempSaveKeyBinding)
            {
                if (KeyCode == CSLItem5)
                {
                    IsPressingF9 = true;
                }

            }

            if (IsPressingLeft) Position.X -= FreeCameraMoveIntensityX;
            if (IsPressingRight) Position.X += FreeCameraMoveIntensityX;
            if (IsPressingUp) Position.Y -= FreeCameraMoveIntensityY;
            if (IsPressingDown) Position.Y += FreeCameraMoveIntensityY;

#if DEBUG
            if (IsPressingF9)
            {
                DataModelDeserialiser DDMS = (DataModelDeserialiser)DataModel.CreateInstance("DataModelDeserialiser");
                DDMS.ATest();
                return; 
            }
#endif

            // END TEMPORARY CODE
        }

        /// <summary>
        /// Renders the follow-object camera type. The follow-object camera always tries to put an object in the middle of the screen.
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        private void RenderFollowObjectCamera(Renderer SDL_Renderer)
        {
            if (Target == null)
            {
                return; 
            }
            else 
            {

                Workspace Ws = DataModel.GetWorkspace();

                GetInstanceResult GGSR = Ws.GetFirstChildOfType("GameSettings");

                // don't do paranoid checks

                Debug.Assert(GGSR.Successful);

                GameSettings GS = (GameSettings)GGSR.Instance;

                GetGameSettingResult GameSettingResult_WindowHeight = GS.GetSetting("WindowHeight");
                GetGameSettingResult GameSettingResult_WindowWidth = GS.GetSetting("WindowWidth");

                if (!GameSettingResult_WindowHeight.Successful
                    || !GameSettingResult_WindowWidth.Successful)
                {
                    ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "WindowWidth or WindowHeight not set!");
                    return;
                }
                else
                {
                    GameSetting WindowHeight_Setting = GameSettingResult_WindowHeight.Setting;
                    GameSetting WindowWidth_Setting = GameSettingResult_WindowWidth.Setting;

                    int WindowHeight = (int)WindowHeight_Setting.SettingValue;
                    int WindowWidth = (int)WindowWidth_Setting.SettingValue;

                    // Set the position of the camera to the position of the target object. 
                    Position.X = (Target.Position.X - (WindowWidth / 2) + (Target.Size.X / 2));
                    Position.Y = (Target.Position.Y - (WindowHeight / 2) + (Target.Size.Y / 2));

                    SDL_Renderer.CCameraPosition = new Vector2(Position.X, Position.Y);
                    // removed redundant physicalobject checks
                }


            }
        }

        private void RenderChaseObjectCamera(Renderer SDL_Renderer)
        {
            if (Target == null)
            {
                return;
            }
            else
            {
                // get a the gamesetting

                Type TargetType = Target.GetType();

                // First, check if the target inherits from physicalobject.

                if (TargetType.IsSubclassOf(typeof(PhysicalObject)) || TargetType == typeof(PhysicalObject))
                {
                    Workspace Ws = DataModel.GetWorkspace();

                    GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

                    //todo: replace with assert
                    if (!GIR.Successful || GIR.Instance == null)
                    {
                        ErrorManager.ThrowError(ClassName, "CannotAcquireUnloadedGlobalSettingsException");
                        return;
                    }
                    else
                    {
                        // Set the window width settings
                        GameSettings GS = (GameSettings)GIR.Instance;

                        GetGameSettingResult WindowWidth_SettingResult = GS.GetSetting("WindowWidth");
                        GetGameSettingResult WindowHeight_SettingResult = GS.GetSetting("WindowHeight");
                        GetGameSettingResult ChaseCameraInFrontOrBehindObjectFactor_SettingResult = GS.GetSetting("ChaseCameraInFrontOrBehindObjectFactor");
                        GetGameSettingResult ChaseCameraAboveOrBelowObjectFactor_SettingResult = GS.GetSetting("ChaseCameraAboveOrBelowObjectFactor");

                        if (!WindowHeight_SettingResult.Successful || WindowHeight_SettingResult.Setting == null
                            || !WindowWidth_SettingResult.Successful || WindowWidth_SettingResult.Setting == null)
                        {
                            ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "WindowWidth or WindowHeight not set!");
                            return;

                        }
                        else
                        {

                            GameSetting WindowWidth_Setting = WindowWidth_SettingResult.Setting;
                            GameSetting WindowHeight_Setting = WindowHeight_SettingResult.Setting;
                            GameSetting ChaseCameraAboveOrBelowObjectFactor_Setting = ChaseCameraInFrontOrBehindObjectFactor_SettingResult.Setting;
                            GameSetting ChaseCameraInFrontOrBehindObjectFactor_Setting = ChaseCameraInFrontOrBehindObjectFactor_SettingResult.Setting;

                            // Get the setting values..
                            int WindowWidth = (int)WindowWidth_Setting.SettingValue;
                            int WindowHeight = (int)WindowHeight_Setting.SettingValue;

                            int ChaseCameraAboveOrBelowObjectFactor = 0;
                            int ChaseCameraInFrontOrBehindObjectFactor = 0;

                            if (!ChaseCameraAboveOrBelowObjectFactor_SettingResult.Successful || ChaseCameraAboveOrBelowObjectFactor_SettingResult.Setting == null
                            || !ChaseCameraInFrontOrBehindObjectFactor_SettingResult.Successful || ChaseCameraInFrontOrBehindObjectFactor_SettingResult.Setting == null)
                            {
                                ChaseCameraAboveOrBelowObjectFactor = 13;
                                ChaseCameraInFrontOrBehindObjectFactor = 3; 
                            }
                            else
                            {
                                ChaseCameraAboveOrBelowObjectFactor = (int)ChaseCameraAboveOrBelowObjectFactor_Setting.SettingValue;
                                ChaseCameraInFrontOrBehindObjectFactor = (int)ChaseCameraInFrontOrBehindObjectFactor_Setting.SettingValue;

                            }

                            // Acquire the settings...

                            // Chase!
                            // slightly above and significantly behind

                            if (ChaseCameraInFrontOrBehindObjectFactor < 0)
                            {
                                Position.X = Target.Position.X + (WindowWidth / Math.Abs(ChaseCameraInFrontOrBehindObjectFactor)); // todo: add game setting
                            }
                            else
                            {
                                Position.X = Target.Position.X - (WindowWidth / ChaseCameraInFrontOrBehindObjectFactor); // todo: add game setting
                            }

                            if (ChaseCameraInFrontOrBehindObjectFactor < 0)
                            {
                                Position.Y = Target.Position.Y - (WindowHeight / Math.Abs(ChaseCameraAboveOrBelowObjectFactor));
                            }
                            else
                            {
                                Position.Y = Target.Position.Y + (WindowHeight / ChaseCameraAboveOrBelowObjectFactor);
                            }

                            SDL_Renderer.CCameraPosition = new Vector2(Position.X, Position.Y);

                        }
                    }
                }
                else
                {
                    return; 
                }

                
            }
        }
    }
}
