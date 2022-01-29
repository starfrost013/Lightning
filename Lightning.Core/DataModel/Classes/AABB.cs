using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{

    /// <summary>
    /// AABB
    /// 
    /// July 23, 2021
    /// 
    /// Defines an axis aligned bounding box.
    /// </summary>
    public class AABB
    {
        /// <summary>
        /// The position of this object.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The size of this object.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// The maximum extent of this AABB
        /// </summary>
        public Vector2 Maximum { get; set; }
        
        /// <summary>
        /// The centre of this AABB
        /// </summary>
        public Vector2 Centre { get; set; }
        public AABB()
        {

        }

        public AABB(Vector2 Pos, Vector2 InsSize)
        {
            Position = Pos;
            Size = InsSize;
            Maximum = Position + Size;
            Centre = Position + (Size / 2);
        }

        public static AABB operator +(AABB A, AABB B) => new AABB(new Vector2(A.Position.X + B.Position.X, A.Position.X + B.Position.X), new Vector2(A.Size.X + B.Size.X, A.Size.Y + B.Size.Y));
        public static AABB operator -(AABB A, AABB B) => new AABB(new Vector2(A.Position.X - B.Position.X, A.Position.X - B.Position.X), new Vector2(A.Size.X - B.Size.X, A.Size.Y - B.Size.Y));
        public static AABB operator *(AABB A, AABB B) => new AABB(new Vector2(A.Position.X * B.Position.X, A.Position.X * B.Position.X), new Vector2(A.Size.X * B.Size.X, A.Size.Y * B.Size.Y));
        public static AABB operator /(AABB A, AABB B) => new AABB(new Vector2(A.Position.X / B.Position.X, A.Position.X / B.Position.X), new Vector2(A.Size.X / B.Size.X, A.Size.Y / B.Size.Y));

        public static CollisionResult IsColliding(PhysicalInstance ObjA, PhysicalInstance ObjB)
        {
            CollisionResult CR = new CollisionResult();

            // is this required?
            CR.Manifold.PhysicalInstanceA = ObjA;
            CR.Manifold.PhysicalInstanceB = ObjB;

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
    }
}
