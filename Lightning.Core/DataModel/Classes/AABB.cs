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

        public AABB()
        {

        }

        public AABB(Vector2 Pos, Vector2 InsSize)
        {
            Position = Pos;
            Size = InsSize;
        }

        public static AABB operator +(AABB A, AABB B) => new AABB(new Vector2(A.Position.X + B.Position.X, A.Position.X + B.Position.X), new Vector2(A.Size.X + B.Size.X, A.Size.Y + B.Size.Y));
        public static AABB operator -(AABB A, AABB B) => new AABB(new Vector2(A.Position.X - B.Position.X, A.Position.X - B.Position.X), new Vector2(A.Size.X - B.Size.X, A.Size.Y - B.Size.Y));
        public static AABB operator *(AABB A, AABB B) => new AABB(new Vector2(A.Position.X * B.Position.X, A.Position.X * B.Position.X), new Vector2(A.Size.X * B.Size.X, A.Size.Y * B.Size.Y));
        public static AABB operator /(AABB A, AABB B) => new AABB(new Vector2(A.Position.X / B.Position.X, A.Position.X / B.Position.X), new Vector2(A.Size.X / B.Size.X, A.Size.Y / B.Size.Y));
    }
}
