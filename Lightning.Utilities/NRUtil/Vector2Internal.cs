﻿#if WINDOWS
using NuCore.NativeInterop.Win32;
#endif
using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Vector2
    /// 
    /// March 9, 2021 (modified April 17, 2021: The great re-namespaceing).
    /// 
    /// Temporary version of Vector2 for bringup and early dev of NuRender. All DM stuff removed.
    /// </summary>
    public class Vector2Internal
    {

        /// <summary>
        /// The X component of this Vector2.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The Y component of this Vector2.. 
        /// </summary>
        public double Y { get; set; }

        public Vector2Internal()
        {

        }

        public Vector2Internal(double NX, double NY)
        {
            X = NX;
            Y = NY;
        }

        // July 25, 2021: These were totally busted, so I had to fix them. 
        public static Vector2Internal operator +(Vector2Internal A, Vector2Internal B) => new Vector2Internal(A.X + B.X, A.Y + B.Y);
        public static Vector2Internal operator +(Vector2Internal A, double B) => new Vector2Internal(A.X + B, A.Y + B);
        public static Vector2Internal operator -(Vector2Internal A, Vector2Internal B) => new Vector2Internal(A.X - B.X, A.Y - B.Y);
        public static Vector2Internal operator -(Vector2Internal A, double B) => new Vector2Internal(A.X - B, A.Y - B);
        public static Vector2Internal operator -(double A, Vector2Internal B) => new Vector2Internal(A - B.X, A - B.Y);
        public static Vector2Internal operator *(Vector2Internal A, Vector2Internal B) => new Vector2Internal(A.X * B.X, A.Y * B.Y);
        public static Vector2Internal operator *(Vector2Internal A, double B) => new Vector2Internal(A.X * B, A.Y * B);
        
        public static Vector2Internal operator /(Vector2Internal A, Vector2Internal B) => new Vector2Internal(A.X / B.X, A.Y / B.Y);
        public static Vector2Internal operator /(Vector2Internal A, double B) => new Vector2Internal(A.X / B, A.Y / B);
        public static Vector2Internal operator /(double A, Vector2Internal B) => new Vector2Internal(A / B.X, A / B.Y);

        public static bool operator ==(Vector2Internal A, Vector2Internal B)
        {
            // Prevent a stack overflow by upcasting
            object OA = (object)A;
            object OB = (object)B;

            if (OA != null && OB != null)
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
            else
            {
                // One or the other is null...
                if (OA == null && OB == null)
                {
                    return true;
                }
                else
                {
                    return false; 
                }
            }
        }

        public static bool operator !=(Vector2Internal A, Vector2Internal B)
        {
            // Prevent a stack overflow by upcasting
            object OA = (object)A;
            object OB = (object)B;

            if (OA != null && OB != null)
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
            else
            {
                if (OA == null && OB == null)
                {
                    return false; 
                }
                else
                {
                    return true; 
                }
            }


        }

        public static bool operator <(Vector2Internal A, Vector2Internal B) 
        {
            if (A == null || B == null)
            {
                return false; // not sure what to do here
            }
            else 
            {
                if ((A.X < B.X) && (A.Y < B.Y))
                {
                    return true;
                }
                else
                {
                    return false; 
                }
            }
        }

        public static bool operator >(Vector2Internal A, Vector2Internal B)
        {
            if (A == null || B == null)
            {
                return false;
            }
            else
            {
                if ((A.X > B.X) && (A.Y > B.Y))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool Equals(object obj)
        {
            Type ObjType = obj.GetType();

            if (typeof(Vector2Internal) != ObjType)
            {
                return false;
            }
            else
            {
                return (this == (Vector2Internal)obj);
            }
            
        }

        public override int GetHashCode() => base.GetHashCode();

        public static Vector2Internal FromString(string Str)
        {
            // We do not add this to the DataModel, as it is an attribute

            // Do not change, as useless objects will pollute the workspace if we add it

            Vector2Internal V2;

            V2 = new Vector2Internal();

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

#if WINDOWS
        /// <summary>
        /// [WIN32-ONLY] Gets a Native Point.
        /// </summary>
        /// <returns></returns>
        public static Vector2Internal FromNativePoint(Win32Point PointW32)
        {
            Vector2Internal V2 = null; // SHOULD NEVER STAY NULL

            V2 = new Vector2Internal();
            
            if (PointW32 == null)
            {
                ErrorManager.ThrowError("Vector2 Converter", "AttemptedToPassInvalidW32PointToVector2FromNativePointException");
                return V2; 
            }
            else
            {
                V2.X = PointW32.X;
                V2.Y = PointW32.Y;

                return V2;
            }

        }
#endif

        public static double GetDotProduct(Vector2Internal A, Vector2Internal B) => A.X * B.X + A.Y * B.Y; // Per Box2D-Lite (GDC 2006)

        public Vector2Internal GetAbs() => new Vector2Internal(Math.Abs(X), Math.Abs(Y));

    }
}
