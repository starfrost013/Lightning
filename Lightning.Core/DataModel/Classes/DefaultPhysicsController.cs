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

        public override void OnTick(PhysicalObject Object, PhysicsState PS) // TODO: TEMP VERY VERY BAD DO NOT USE FOR LONGER THAN LIKE A DAY
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

                GetMultiInstanceResult GMIR = Ws.GetAllChildrenOfType("PhysicalObject");

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
                        PhysicalObject ObjectToTest = (PhysicalObject)Instance;

                        if (ObjectToTest.AABB == null
                        || !ObjectToTest.PhysicsEnabled)
                        {
                            continue;
                        }
                        else
                        {
                            AABB AABBToTestForCollision = ObjectToTest.AABB;
                            
                            Object.Position += Object.Velocity;

                            CollisionResult CollisionResult = AABBtoAABB(Object, ObjectToTest);

                            if (CollisionResult.Successful)
                            {
                                // Probably NOT the way it's supposed to be done and a bit hacky, but meh

                                // Handles when an object is spawned within another object (i.e. no velocity to resolve an impulse).
                                if (Object.Velocity == new Vector2(0, 0)
                                && ObjectToTest.Velocity == new Vector2(0, 0))
                                {
                                    Manifold CollisionManifold = CollisionResult.Manifold;
                                    
                                    // eww elseif
                                    if (CollisionManifold.NormalVector == new Vector2(-1, 0))
                                    {
                                        Object.Position.X += (CollisionManifold.PenetrationAmount / 2);
                                        ObjectToTest.Position.X -= (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    else if (CollisionManifold.NormalVector == new Vector2(0, 0))
                                    {
                                        Object.Position.X -= (CollisionManifold.PenetrationAmount / 2);
                                        ObjectToTest.Position.X += (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    else if (CollisionManifold.NormalVector == new Vector2(0, -1)) 
                                    {
                                        Object.Position.Y += (CollisionManifold.PenetrationAmount / 2);
                                        ObjectToTest.Position.Y -= (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    else
                                    {
                                        Object.Position.Y -= (CollisionManifold.PenetrationAmount / 2);
                                        ObjectToTest.Position.Y += (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    

                                    continue; 
                                }
                                else
                                {
                                    // We have a collision. 
                                    Vector2 CollisionNormal = Object.Velocity - ObjectToTest.Velocity;

                                    // Identify the velocity at normal.
                                    double NormalVelocity = Vector2.GetDotProduct(CollisionNormal, ObjectToTest.Velocity);

                                    if (NormalVelocity > 0)
                                    {
                                        continue;
                                    }

                                    // Calculate elasticity
                                    double MinimumElasticity = Math.Min(Object.Elasticity, ObjectToTest.Elasticity);

                                    // Calculate impulse scalar (multiplier for the impulse)

                                    double ImpulseScalar = -(1 * MinimumElasticity) * NormalVelocity;
                                    ImpulseScalar /= (Object.InverseMass + ObjectToTest.InverseMass);

                                    // Apply force in OPPOSITE directions to force apart
                                    Vector2 CollisionImpulse = CollisionNormal * ImpulseScalar;

                                    Object.Velocity -= CollisionImpulse * Object.InverseMass;
                                    ObjectToTest.Velocity += CollisionImpulse * Object.InverseMass;

                                }
                            }
                            else
                            {
                                if (Object.Velocity < PS.TerminalVelocity)
                                {
                                    Object.Velocity.X += PS.Gravity.X;
                                    Object.Velocity.Y -= PS.Gravity.Y;
                                }
                                
                                continue; 
                            }

                        }
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Collision test - AABB vs AABB. Checks if the AABB represented by <paramref name="AABB1"/> is within tge AABB represented by <paramref name="AABB2"/>
        /// </summary>
        /// <param name="AABB1">The first AABB you wish to check.</param>
        /// <param name="AABB2">The second </param>
        /// <returns>A boolean determining if the AABB represented by <paramref name="AABB1"/> is within the AABB represented by <paramref name="AABB2"/></returns>
        private CollisionResult AABBtoAABB(PhysicalObject ObjA, PhysicalObject ObjB)
        {

            CollisionResult CR = new CollisionResult();

            // is this required?
            CR.Manifold.PhysicalObjectA = ObjA;
            CR.Manifold.PhysicalObjectB = ObjB;

            if (ObjA.AABB == null
            || ObjB.AABB == null)
            {
                CR.FailureReason = "One or both objects does not have a valid AABB!";
                return CR;
            }    

            Vector2 AB = ObjB.Position - ObjA.Position;

            AABB AABB_A = ObjA.AABB;
            AABB AABB_B = ObjB.AABB;

            // Calculate half
            double HalfX_A = (AABB_A.Maximum.X - AABB_A.Position.X) / 2;
            double HalfX_B = (AABB_B.Maximum.X - AABB_B.Position.X) / 2;

            double XOverlap = HalfX_B + HalfX_A - Math.Abs(AB.X);
            
            if (XOverlap > 0)
            {
                double HalfY_A = (AABB_A.Maximum.Y - AABB_A.Position.Y) / 2;
                double HalfY_B = (AABB_B.Maximum.Y - AABB_B.Position.Y) / 2;

                double YOverlap = HalfY_B + HalfY_A - Math.Abs(AB.Y);

                if (YOverlap > 0) // check for overlap
                {
                    if (XOverlap > YOverlap)
                    {
                        if (AB.X < 0)
                        {
                            CR.Manifold.NormalVector = new Vector2(-1, 0); // negative x-axis (leftmost side is closest)
                        }
                        else
                        { 
                            CR.Manifold.NormalVector = new Vector2(0, 0); // positive x-axis (rightmost side is closest)
                        }
                        CR.Manifold.PenetrationAmount = XOverlap;
                        CR.Successful = true;
                        return CR;
                    }
                    else
                    {
                        if (AB.Y < 0)
                        {
                            CR.Manifold.NormalVector = new Vector2(0, -1); // negative y-axis (top side is closest)
                        }
                        else
                        {
                            CR.Manifold.NormalVector = new Vector2(0, 1); // positive x-axis (bottom side is closest)
                        }

                        CR.Manifold.PenetrationAmount = YOverlap;
                        CR.Successful = true;
                        return CR; 
                    }
                }
                else
                {
                    CR.FailureReason = "Objects not colliding";
                    return CR;
                }
            }
            else
            {
                CR.FailureReason = "Objects not colliding";
                return CR;
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
