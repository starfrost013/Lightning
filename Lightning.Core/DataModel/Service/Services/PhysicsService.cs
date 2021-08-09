using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// PhysicsService
    /// 
    /// June 20, 2021 (modified July 28, 2021)
    /// 
    /// Implements a 2D rigid body physics engine for Lightning.
    /// </summary>
    public class PhysicsService : Service
    {
        internal override string ClassName => "PhysicsService";
        internal override ServiceImportance Importance => ServiceImportance.Low;

        private bool PHYSICSSERVICE_INITIALISED { get; set; }

        private PhysicsState PhysState { get; set; }
        public override ServiceStartResult OnStart()
        {
            Logging.Log("PhysicsService starting...", ClassName);
            PhysState = new PhysicsState();
            return new ServiceStartResult { Successful = true };

        }
        public override ServiceShutdownResult OnShutdown() => new ServiceShutdownResult { Successful = true };

        public override void Poll()
        {
            if (!PHYSICSSERVICE_INITIALISED)
            {
                Init();
            }
            else
            {
                UpdatePhysics();
                return;
            }
                 
        }

        private void Init()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful 
            || GIR.Instance == null)
            {
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
                return; // will never run
            }
            else
            {
                GameSettings GS = (GameSettings)GIR.Instance;

                GetGameSettingResult GGSR_GravityLevel = GS.GetSetting("GravityLevel");
                GetGameSettingResult GGSR_GravityState = GS.GetSetting("GravityState");
                GetGameSettingResult GGSR_ObjectKillBoundary = GS.GetSetting("ObjectKillBoundary");
                GetGameSettingResult GGSR_TerminalVelocity = GS.GetSetting("TerminalVelocity");
                GetGameSettingResult GGSR_PositionalCorrectionPercentage = GS.GetSetting("PositionalCorrectionPercentage");
                GetGameSettingResult GGSR_PositionalCorrectionSlop = GS.GetSetting("PositionalCorrectionSlop");
                GetGameSettingResult GGSR_EpsilonVelocity = GS.GetSetting("EpsilonVelocity");

                if (!CheckSettingSuccessfullyLoaded(GGSR_GravityLevel)) 
                {
                    PhysState.Gravity = PhysicsState.GravityDefaultValue; // todo: move to globalsettings?
                }
                else
                {
                    GameSetting GravityLevel_Setting = GGSR_GravityLevel.Setting;

                    try
                    {
                        PhysState.Gravity = (Vector2)GravityLevel_Setting.SettingValue;
                        
                    }
                    catch (Exception)
                    {
                        PhysState.Gravity = PhysicsState.GravityDefaultValue;
                    }


                }

                if (!CheckSettingSuccessfullyLoaded(GGSR_GravityState))
                {
                    PhysState.GravityState = GravityState.Normal;
                }
                else 
                {
                    GameSetting PhysicsState_Setting = GGSR_GravityState.Setting;

                    try
                    {
                        PhysState.GravityState = (GravityState)PhysicsState_Setting.SettingValue;
                    }
                    catch (Exception)
                    {
                        PhysState.GravityState = GravityState.Normal;
                    }
                }

                if (!CheckSettingSuccessfullyLoaded(GGSR_ObjectKillBoundary))
                {
                    PhysState.ObjectKillBoundary = PhysicsState.ObjectKillBoundaryDefaultValue;
                }
                else
                {
                    GameSetting ObjectKillBoundary_Setting = GGSR_ObjectKillBoundary.Setting;

                    try
                    {
                        PhysState.ObjectKillBoundary = (Vector2)ObjectKillBoundary_Setting.SettingValue;
                    }
                    catch (Exception)
                    {
                        PhysState.ObjectKillBoundary = PhysicsState.ObjectKillBoundaryDefaultValue;
                        
                    }
                }

                if (!CheckSettingSuccessfullyLoaded(GGSR_TerminalVelocity))
                {
                    PhysState.TerminalVelocity = PhysicsState.TerminalVelocityDefaultValue;
                }
                else
                {
                    GameSetting TerminalVelocity_Setting = GGSR_TerminalVelocity.Setting;

                    try
                    {
                        PhysState.TerminalVelocity = (Vector2)TerminalVelocity_Setting.SettingValue; 
                    }
                    catch (Exception)
                    {
                        PhysState.TerminalVelocity = PhysicsState.TerminalVelocityDefaultValue;
                    }
                }

                if (!CheckSettingSuccessfullyLoaded(GGSR_PositionalCorrectionPercentage))
                {
                    PhysState.PositionalCorrectionPercentage = PhysicsState.PositionalCorrectionPercentageDefaultValue;
                }
                else
                {
                    GameSetting PositionalCorrectionPercentage_Setting = GGSR_TerminalVelocity.Setting;

                    try
                    {
                        PhysState.PositionalCorrectionPercentage = (double)PositionalCorrectionPercentage_Setting.SettingValue; 
                    }
                    catch (Exception)
                    {
                        PhysState.PositionalCorrectionPercentage = (double)PhysicsState.PositionalCorrectionPercentageDefaultValue;
                    }
                }

                if (!CheckSettingSuccessfullyLoaded(GGSR_PositionalCorrectionSlop))
                {
                    PhysState.PositionalCorrectionSlop = PhysicsState.PositionalCorrectionSlopDefaultValue;
                }
                else 
                {
                    GameSetting PositionalCorrectionSlop_Setting = GGSR_TerminalVelocity.Setting;

                    try
                    {
                        PhysState.PositionalCorrectionSlop = (double)PositionalCorrectionSlop_Setting.SettingValue;
                    }
                    catch (Exception)
                    {
                        PhysState.PositionalCorrectionSlop = PhysicsState.PositionalCorrectionSlopDefaultValue;
                    }
                }
                
                if (!CheckSettingSuccessfullyLoaded(GGSR_EpsilonVelocity))
                {
                    PhysState.EpsilonVelocity = PhysicsState.EpsilonVelocityDefaultValue;
                }
                else
                {
                    GameSetting EpsilonVelocity_Setting = GGSR_EpsilonVelocity.Setting;

                    try
                    {
                        PhysState.EpsilonVelocity = (double)EpsilonVelocity_Setting.SettingValue;
                    }
                    catch (Exception)
                    {
                        PhysState.EpsilonVelocity = PhysicsState.EpsilonVelocityDefaultValue;
                    }
                }

                if (PhysState.PositionalCorrectionPercentage <= 00)
                {
                    ErrorManager.ThrowError(ClassName, "PhysicsStatePositionalCorrectionPercentageMustBeAboveZeroException", $"The PositionalCorrectionPercentage GameSetting must be set to a value above zero (it is currently set to {PhysState.PositionalCorrectionPercentage}). It has been reset to a higher value");
                    PhysState.PositionalCorrectionPercentage = PhysicsState.PositionalCorrectionPercentageDefaultValue;
                }
                else
                {
                    if (PhysState.PositionalCorrectionSlop <= 0)
                    {
                        ErrorManager.ThrowError(ClassName, "PhysicsStatePositionalCorrectionSlopMustBeAboveZeroException", $"The PositionalCorrectionSlop GameSetting must be set to a value above zero (it is currently set to {PhysState.PositionalCorrectionPercentage}). It has been reset to a higher value");
                        PhysState.PositionalCorrectionSlop = PhysicsState.PositionalCorrectionSlopDefaultValue;
                    }
                    else
                    {
                        if (PhysState.EpsilonVelocity <= 0)
                        {
                            ErrorManager.ThrowError(ClassName, "PhysicsStateEpsilonVelocityPercentageMustBeAboveZeroException", $"The EpsilonVelocity GameSetting must be set to a value above zero! It will be reset to a default value. (it is currently set to {PhysState.EpsilonVelocity}!)");
                            PhysState.EpsilonVelocity = PhysicsState.EpsilonVelocityDefaultValue;
                        }
                    }
                }
                
                

                PHYSICSSERVICE_INITIALISED = true;
                return; 

            }

        }

        private bool CheckSettingSuccessfullyLoaded(GetGameSettingResult GGSR) => (GGSR.Successful && GGSR.Setting != null);

        private void UpdatePhysics()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("PhysicalObject");

            if (!GMIR.Successful
                || GMIR.Instances == null)
            {
                ErrorManager.ThrowError(ClassName, "FailedToObtainListOfControllableObjectsException");
            }
            else
            {
                List<Instance> InstanceList = GMIR.Instances;

                foreach (Instance Instance in InstanceList)
                {
                    PhysicalObject CO = (PhysicalObject)Instance;

                    GetPhysicsControllerResult GPCR = GetPhysicsController(CO);

                    if (CO.PhysicsEnabled)
                    {
                        if (!GPCR.Successful || CO.PhysicsController == null)
                        {
                            ErrorManager.ThrowError(ClassName, "FailedToObtainPhysicsControllerException");
                            CO.PhysicsEnabled = false;
                            continue;
                        }
                        else
                        {
                            PhysicsController PC = (PhysicsController)GPCR.PhysController;

                            PC.OnTick(CO, PhysState); // Very BAD test AND temp code
                        }

                    }

                }
            }

        }

        private GetPhysicsControllerResult GetPhysicsController(PhysicalObject CO) // COMPLETE THIS 2021-07-22 MORNING (ADD TYPE CHECKING ETC)
        {
            GetPhysicsControllerResult GPCR = new GetPhysicsControllerResult();

            // Set up the physics controller - throw an error and disable physics if there is none. 
            if (CO.PhysicsEnabled)
            {
                if (CO.PhysicsController == null)
                {

                    if (CO.Name != null)
                    {
                        ErrorManager.ThrowError(ClassName, "EnablePhysicsSetButNoPhysicsControllerSet", $"The object with name {CO.Name} does not have a PhysicsController set, but PhysicsEnabled is set - disabling physics for this object...");
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "EnablePhysicsSetButNoPhysicsControllerSet", $"An {CO.ClassName} object does not have a PhysicsController set, but PhysicsEnabled is set - disabling physics for this object...");
                        CO.PhysicsEnabled = false;

                    }

                    GPCR.FailureReason = "PhysicsEnabled is set but no PhysicsController is set!";
                    return GPCR;
                }
                else
                {
                    Type PhysCtrlType = CO.PhysicsController.GetType();

                    if (!PhysCtrlType.IsSubclassOf(typeof(PhysicsController)))
                    {
                        GPCR.FailureReason = $"The PhysicsController set is not a PhysicsController - it is of type {PhysCtrlType}!";
                        return GPCR;
                    }
                    else
                    {

                        GPCR.PhysController = (PhysicsController)CO.PhysicsController;
                        GPCR.Successful = true;
                        return GPCR;
                    }


                }
            }
            else
            {
                GPCR.FailureReason = "Physics not enabled for this object";
                // physicsenabled set ot false by default
                return GPCR; 
            }
            
        }

        public override void OnDataSent(ServiceMessage Data)
        {
            return; 
        }
    }
}
