using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// PhysicsService
    /// 
    /// June 20, 2021
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


                if (!GGSR_GravityLevel.Successful
                || GGSR_GravityLevel.Setting == null) 
                {
                    PhysState.Gravity = PhysicsState.GravityDefaultValue; // todo: move to globalsettings?
                }
                else
                {
                    GameSetting GravityLevel_Setting = GGSR_GravityLevel.Setting;

                    Vector2 Gravity = null; 

                    try
                    {
                        Gravity = (Vector2)GravityLevel_Setting.SettingValue;
                        
                    }
                    catch (InvalidCastException)
                    {
                        Gravity = PhysicsState.GravityDefaultValue;
                    }
                    finally
                    {
                        PhysState.Gravity = Gravity;
                    }


                }

                if (!GGSR_GravityState.Successful
                || GGSR_GravityState.Setting == null)
                {
                    PhysState.GravityState = GravityState.Normal;
                }
                else 
                {
                    GameSetting PhysicsState_Setting = GGSR_GravityState.Setting;

                    try
                    {
                        GravityState GSState = (GravityState)PhysicsState_Setting.SettingValue;
                        PhysState.GravityState = GSState;
                    }
                    catch (InvalidCastException)
                    {
                        PhysState.GravityState = GravityState.Normal;
                    }
                }

                if (!GGSR_ObjectKillBoundary.Successful
                || GGSR_ObjectKillBoundary.Setting == null)
                {
                    PhysState.ObjectKillBoundary = PhysicsState.ObjectKillBoundaryDefaultValue;
                }
                else
                {
                    GameSetting ObjectKillBoundary_Setting = GGSR_ObjectKillBoundary.Setting;
                }

            }

        }

        private void UpdatePhysics()
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("ControllableObject");

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
                    ControllableObject CO = (ControllableObject)Instance;

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

                            PC.OnTick(CO);
                        }

                    }

                }
            }

        }

        private GetPhysicsControllerResult GetPhysicsController(ControllableObject CO) // COMPLETE THIS 2021-07-22 MORNING (ADD TYPE CHECKING ETC)
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
                        ErrorManager.ThrowError(ClassName, "EnablePhysicsSetButNoPhysicsControllerSet", $"A {CO.ClassName} class name does not have a PhysicsController set, but PhysicsEnabled is set - disabling physics for this object...");
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
