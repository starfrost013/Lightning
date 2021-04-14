using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lightning.Core
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
        public override string ClassName => "Camera";

        public override InstanceTags Attributes => InstanceTags.Archivable | InstanceTags.Destroyable | InstanceTags.Instantiable | InstanceTags.Serialisable | InstanceTags.ShownInIDE;

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

        public override void OnSpawn()
        {
            if (TargetName != null && Target == null)
            {
                Workspace Ws = DataModel.GetWorkspace();

                TryFindTarget(TargetName, Ws); 
            }
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
            // TODO: make non-hardcoded
            // TEMPORARY CODE
            switch (Control.KeyCode.ToString())
            {
                case "LEFT":
                case "A":
                    Position.X += 10;
                    return;
                case "RIGHT":
                case "D":
                    Position.X -= 10;
                    return;
                case "UP":
                case "W":
                    Position.Y += 10;
                    return;
                case "DOWN":
                case "S":
                    Position.Y -= 10;
                    return;
            }
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
                    Position.X = Target.Position.X + (WindowWidth / 2);
                    Position.Y = Target.Position.Y + (WindowHeight / 2);


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

                        if (!WindowHeight_SettingResult.Successful || WindowHeight_SettingResult.Setting == null
                            || !WindowWidth_SettingResult.Successful || WindowWidth_SettingResult.Setting == null)
                        {
                            ErrorManager.ThrowError(ClassName, "FailedToObtainCriticalGameSettingException", "WindowWidth or WindowHeight not set!");
                            return;

                        }
                        else
                        {
                            // Acquire the settings...
                            GameSetting WindowWidth_Setting = WindowWidth_SettingResult.Setting;
                            GameSetting WindowHeight_Setting = WindowHeight_SettingResult.Setting;

                            // Get the setting values..
                            int WindowWidth = (int)WindowWidth_Setting.SettingValue;
                            int WindowHeight = (int)WindowHeight_Setting.SettingValue;

                            // Chase!
                            // slightly above and significantly behind
                            Position.X = Target.Position.X - (WindowWidth / 3); // todo: add game setting
                            Position.Y = Target.Position.Y + (WindowHeight / 13);

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
