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
    /// The default physics controller implements a box collider. 
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
                ErrorManager.ThrowError(ClassName, "GameSettingsFailedToLoadException");
                return; // never runs
            }
            else
            {
                GameSettings GS = (GameSettings)GIR.Instance;


                AABB CAABB = Object.AABB;

                GetMultiInstanceResult GMIR = DataModel.GetAllChildrenOfType("ControllableObject");

                if (CAABB == null) return; 

                if (!GMIR.Successful
                || GMIR.Instances == null)
                {
                    return; // Position and Size must be set somheow for Physics to be enabled, but this error is thrown somewhere else
                }
                else
                {
                    List<Instance> ControllableObjectList = GMIR.Instances;

                    foreach (Instance Instance in ControllableObjectList)
                    {
                        ControllableObject ObjectToTest = (ControllableObject)Instance;

                        if (ObjectToTest.AABB == null)
                        {
                            continue;
                        }
                        else
                        {
                            AABB AABBToTestForCollision = ObjectToTest.AABB;
                            
                            // Move it according to its current acceleration. 
                            Object.Speed += Object.Acceleration;
                            Object.Position += Object.Speed;

                            if (AABBtoAABB(CAABB, AABBToTestForCollision))
                            {
                            
                            }
                            else
                            {
                                continue; 
                            }

                        }
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Collision test - AABB vs AABB. Checks if AABB1 is in AABB2,
        /// </summary>
        /// <param name="AABB1">The forist</param>
        /// <param name="AABB2"></param>
        /// <returns></returns>
        private bool AABBtoAABB(AABB AABB1, AABB AABB2)
        {
            if (AABB1.Position.X > AABB2.Position.X
            && AABB1.Position.X < AABB2.Position.X + AABB2.Size.X
            && AABB1.Position.Y > AABB2.Position.Y
            && AABB1.Position.Y < AABB2.Position.Y + AABB2.Size.Y)
            {
                return true;
            }
            else
            {
                return false; 
            }
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
