using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// A GCSE Maths conforming implementation of 2D vectors.
    /// 
    /// 2021-03-09
    /// </summary>
    public class Vector2 : SerialisableObject
    {
        public override string ClassName => "Vector2";
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2()
        {

        }

        public Vector2(double NX, double NY)
        {
            X = NX;
            Y = NY;
        }

        public static Vector2 operator +(Vector2 A, Vector2 B) => new Vector2(A.X + B.X, B.Y + B.Y);
        public static Vector2 operator -(Vector2 A, Vector2 B) => new Vector2(A.X - B.X, B.Y - B.Y);
        public static Vector2 operator *(Vector2 A, Vector2 B) => new Vector2(A.X * B.X, B.Y * B.Y);
        public static Vector2 operator /(Vector2 A, Vector2 B) => new Vector2(A.X / B.X, B.Y / B.Y);

        public Vector2 Scale(Vector2 A, Vector2 Amount) => A * Amount; 

    }
}
