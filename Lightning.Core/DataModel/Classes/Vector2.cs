using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// A GCSE Maths conforming implementation of 2D vectors.
    /// 
    /// 2021-03-09
    /// 
    /// April 11, 2021: Added TypeConverter for DDMS
    /// </summary>
    [TypeConverter(typeof(Vector2Converter))]
    public class Vector2 : SerialisableObject
    {
        public override string ClassName => "Vector2";

        /// <summary>
        /// The X position.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The Y position. 
        /// </summary>
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
        public static bool operator ==(Vector2 A, Vector2 B)
        {
            if (A.X == B.X && A.Y == B.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Vector2 A, Vector2 B)
        {
            if (A.X == B.X && A.Y == B.Y)
            {
                return false;
            }
            else
            {
                return true; 
            }

        }

        public override bool Equals(object obj)
        {
            Type ObjType = obj.GetType();

            if (typeof(Vector2) != ObjType)
            {
                return false;
            }
            else
            {
                return (this == (Vector2)obj);
            }
            
        }

        public override int GetHashCode() => base.GetHashCode();

        public static Vector2 FromString(string Str, bool AddToDataModel = true)
        {
            // We do not add this to the DataModel, as it is an attribute

            // Do not change, as useless objects will pollute the workspace if we add it

            Vector2 V2;

            // Add it to the DataModel if it is not in it.
            if (AddToDataModel)
            {
                V2 = (Vector2)DataModel.CreateInstance("Vector2");
            }
            else
            {
                V2 = new Vector2();
            }

            string[] Str_Split = Str.Split(',');

            if (Str_Split.Length != 2)
            {
                ErrorManager.ThrowError("Vector2Converter", "Vector2ConversionInvalidNumberOfComponentsException");
                return null;
            }
            else
            {
                try
                {
                    // Convert to each component. 
                    double X = Convert.ToDouble(Str_Split[0]);
                    double Y = Convert.ToDouble(Str_Split[1]);

                    V2.X = X;
                    V2.Y = Y;
                    return V2;
                }
                catch (OverflowException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Vector2Converter", "Vector2InvalidConversionException", "An integer overflow occurred when converting to a Vector2!", err);
#else
                    ErrorManager.ThrowError("Vector2Converter", "Vector2InvalidConversionException", "An integer overflow occurred when converting to a Vector2!", err);             
#endif
                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Vector2Converter", "Vector2InvalidConversionException", err);
#else
                    ErrorManager.ThrowError("Vector2Converter", "Vector2InvalidConversionException");
#endif
                }
            }

            return V2;
        }
    }
}
