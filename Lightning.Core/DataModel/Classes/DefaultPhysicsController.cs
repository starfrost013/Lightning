using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DefaultPhysicsController
    /// 
    /// July 22, 2021 (updated July 24, 2021) 
    /// 
    /// Implements the default physics engine. (I did GCSE Physics too!)
    /// </summary>
    public class DefaultPhysicsController : PhysicsController
    {
        internal override string ClassName => "DefaultPhysicsController";

        public override void OnInit()
        {
            return; 
        }

        public override void OnTick(ControllableObject Object)
        {
            Workspace Ws = DataModel.GetWorkspace();

            GetInstanceResult GIR = Ws.GetFirstChildOfType("GameSettings");

            if (!GIR.Successful
            || GIR.Instance == null)
            {
                
            }
            else
            {
                GameSettings GS = (GameSettings)GIR.Instance;

                GetGameSettingResult GGSR_GravityLevel = GS.GetSetting("GravityLevel");
                GetGameSettingResult GGSR_GravFlipped = GS.GetSetting("GravityState");

            }

            return;
        }
        public override void OnCollisionStart()
        {
            return;
        }

        public override void OnCollisionEnd()
        {
            return;
        }
    }
}
