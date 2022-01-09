using NuCore.Utilities;
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

        public override void OnTick(PhysicalInstance Object, PhysicsState PS) // TODO: TEMP VERY VERY BAD DO NOT USE FOR LONGER THAN LIKE A DAY
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

                int ObjCollisionCount = 0;
                int ObjToTestCollisionCount = 0;

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
                        PhysicalInstance ObjectToTest = (PhysicalInstance)Instance;

                        if (Object == ObjectToTest)
                        {
                            continue; // don't check for collisions against ourselves
                        }

                        if (ObjectToTest.AABB == null
                        || !ObjectToTest.PhysicsEnabled)
                        {
                            continue;
                        }
                        else
                        {
                            AABB AABBToTestForCollision = ObjectToTest.AABB;

                            if (Object.Velocity > PS.TerminalVelocity.GetAbs())
                            {
                                if (Object.Velocity.X < 0 && PS.TerminalVelocity.X < 0
                                || (Object.Velocity.X > 0 && PS.TerminalVelocity.X > 0)) Object.Velocity.X = PS.TerminalVelocity.X;
                                if (Object.Velocity.X < 0 && PS.TerminalVelocity.X > 0
                                || (Object.Velocity.X > 0 && PS.TerminalVelocity.X < 0)) Object.Velocity.X = -PS.TerminalVelocity.X;

                                if (Object.Velocity.Y < 0 && PS.TerminalVelocity.Y < 0
                                || (Object.Velocity.Y > 0 && PS.TerminalVelocity.Y > 0)) Object.Velocity.Y = PS.TerminalVelocity.Y;
                                if (Object.Velocity.Y < 0 && PS.TerminalVelocity.Y > 0
                                || (Object.Velocity.Y > 0 && PS.TerminalVelocity.Y < 0)) Object.Velocity.Y = -PS.TerminalVelocity.Y;
                            }

                            // hackish
                            Brush CurBrush = Object.GetBrush(); 
                            
                            Object.Position.X += Object.Velocity.X;

                            if (Math.Abs(Object.Velocity.Y) > Math.Abs(PS.EpsilonVelocity))
                            {
                                Object.Position.Y += Object.Velocity.Y;
                            }
                            
                             
                            CollisionResult CollisionResult = AABBtoAABB(Object, ObjectToTest);

                            if (CollisionResult.Successful)
                            {
                                // Probably NOT the way it's supposed to be done and a bit hacky, but meh

                                ObjCollisionCount++;
                                ObjToTestCollisionCount++;

                                // Handles when an object is spawned within another object (i.e. no velocity to resolve an impulse).
                                if (Object.Velocity == new Vector2(0, 0)
                                && ObjectToTest.Velocity == new Vector2(0, 0))
                                {
                                    Manifold CollisionManifold = CollisionResult.Manifold;

                                    // eww elseif
                                    if (CollisionManifold.NormalVector == new Vector2(-1, 0) && ObjectToTest.Solidity.HasFlag(Solidity.Sides))
                                    {
                                        if (!Object.Anchored) Object.Position.X += (CollisionManifold.PenetrationAmount / 2);
                                        if (!ObjectToTest.Anchored) ObjectToTest.Position.X -= (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    else if (CollisionManifold.NormalVector == new Vector2(0, 0) && ObjectToTest.Solidity.HasFlag(Solidity.Sides))
                                    {
                                        if (!Object.Anchored) Object.Position.X -= (CollisionManifold.PenetrationAmount / 2);
                                        if (!ObjectToTest.Anchored) ObjectToTest.Position.X += (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    else if (CollisionManifold.NormalVector == new Vector2(0, -1) && ObjectToTest.Solidity.HasFlag(Solidity.Top))
                                    {
                                        if (!Object.Anchored) Object.Position.Y += (CollisionManifold.PenetrationAmount / 2);
                                        if (!ObjectToTest.Anchored) ObjectToTest.Position.Y -= (CollisionManifold.PenetrationAmount / 2);
                                    }
                                    else
                                    {
                                        if (ObjectToTest.Solidity.HasFlag(Solidity.Bottom))
                                        {
                                            if (!Object.Anchored) Object.Position.Y -= (CollisionManifold.PenetrationAmount / 2);
                                            if (!ObjectToTest.Anchored) ObjectToTest.Position.Y += (CollisionManifold.PenetrationAmount / 2);
                                        }

                                    }

                                }
                                else
                                {
                                    // We have a collision. 
                                    Vector2 CollisionNormal = Object.Velocity - ObjectToTest.Velocity;

                                    // Determine which side we are colliding from.
                                    // Don't change velocity for axes we are not colliding from.

                                    bool BlockLeft;
                                    bool BlockRight;
                                    bool BlockTop;
                                    bool BlockBottom;

                                    BlockLeft = (Object.AABB.Position.X > ObjectToTest.AABB.Position.X && Object.AABB.Position.X < ObjectToTest.AABB.Centre.X); // Determine if we are intersecting left.
                                    BlockRight = (!BlockLeft && (Object.AABB.Position.X > ObjectToTest.AABB.Centre.X) && (Object.AABB.Position.X < ObjectToTest.AABB.Maximum.X)); // Determine if we are intersecting right.
                                    BlockTop = (Object.AABB.Maximum.Y > ObjectToTest.AABB.Position.Y && Object.AABB.Maximum.Y < ObjectToTest.AABB.Centre.Y); // Determine if we are intersecting top.
                                    BlockBottom = (!BlockLeft && (Object.AABB.Maximum.Y > ObjectToTest.AABB.Centre.Y) && (Object.AABB.Maximum.Y < ObjectToTest.AABB.Maximum.Y)); // Determine if we are intersecting bottom.

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

                                    if (Double.IsNaN(ImpulseScalar) || ImpulseScalar == 0) // prevent going to infinity
                                    {
                                        ImpulseScalar = 1;
                                    }

                                    // Apply force in OPPOSITE directions to force apart
                                    Vector2 CollisionImpulse = CollisionNormal * ImpulseScalar;


                                    if (!Object.Anchored)
                                    {
                                        if (Object.Velocity.X < 0)
                                        {
                                            if (BlockLeft) Object.Velocity.X -= CollisionImpulse.X * Object.InverseMass;
                                        }
                                        else
                                        {
                                            if (BlockRight) Object.Velocity.X += CollisionImpulse.X * Object.InverseMass;
                                        }

                                        if (Object.Velocity.Y < 0)
                                        {
                                            if (BlockBottom) Object.Velocity.Y += CollisionImpulse.Y * Object.InverseMass;
                                        }
                                        else
                                        {
                                            if (BlockTop) Object.Velocity.Y -= CollisionImpulse.Y * Object.InverseMass;
                                        }

                                    }

                                    //TODO: method

                                    if (!ObjectToTest.Anchored)
                                    {
                                        if (ObjectToTest.Velocity.X < 0)
                                        {
                                            if (BlockLeft) ObjectToTest.Velocity.X -= CollisionImpulse.X * ObjectToTest.InverseMass;
                                        }
                                        else
                                        {
                                            if (BlockRight) ObjectToTest.Velocity.X += CollisionImpulse.X * ObjectToTest.InverseMass;
                                        }

                                        if (ObjectToTest.Velocity.Y < 0)
                                        {
                                            if (BlockBottom) ObjectToTest.Velocity.Y += CollisionImpulse.Y * ObjectToTest.InverseMass;
                                        }
                                        else
                                        {
                                            if (BlockTop) ObjectToTest.Velocity.Y -= CollisionImpulse.Y * ObjectToTest.InverseMass;
                                        }

                                    }

                                    PerformPositionalCorrection(CollisionResult.Manifold, PS);
                                }
                            }
                            else
                            {
                                Vector2 AbsoluteTerminalVelocity = PS.TerminalVelocity.GetAbs();

                                if (!Object.Anchored) // falling to the ground
                                {
                                    Object.Velocity.X += PS.Gravity.X;
                                    Object.Velocity.Y -= PS.Gravity.Y;
                                    // TEMP testing - /10 MAY be moved to gamesettings.
                                } 
                                
                            }

                            Object.IsColliding = (ObjCollisionCount > 0);
                            ObjectToTest.IsColliding = (ObjToTestCollisionCount > 0);

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
        private CollisionResult AABBtoAABB(PhysicalInstance ObjA, PhysicalInstance ObjB)
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

            AABB AABB_A = ObjA.AABB;
            AABB AABB_B = ObjB.AABB;

            Vector2 AB = AABB_B.Centre - AABB_A.Centre;

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

        private void PerformPositionalCorrection(Manifold M, PhysicsState PS)
        {
            double PenetrationMultiplier = M.PenetrationAmount * PS.PositionalCorrectionPercentage;

            double PosToCheckA = M.PhysicalObjectA.Position.Y - M.PhysicalObjectA.Size.Y;
            double PosToCheckB = M.PhysicalObjectB.Position.Y - M.PhysicalObjectB.Size.Y;

            if (PosToCheckA > PosToCheckB)
            {
                if (!M.PhysicalObjectA.Anchored) M.PhysicalObjectA.Velocity.Y -= PenetrationMultiplier;
                if (!M.PhysicalObjectB.Anchored) M.PhysicalObjectB.Velocity.Y += PenetrationMultiplier;
            }
            else
            {
                if (!M.PhysicalObjectA.Anchored) M.PhysicalObjectA.Velocity.Y += PenetrationMultiplier;
                if (!M.PhysicalObjectB.Anchored) M.PhysicalObjectB.Velocity.Y -= PenetrationMultiplier;
            }
                

        }

    }
}
