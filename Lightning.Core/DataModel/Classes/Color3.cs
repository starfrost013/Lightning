using NuCore.Utilities;
using NuRender; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Color3
    /// 
    /// March 7, 2021 (modified December 9, 2021: Now forwarder to Color3Internal in NuCore.Utilities)
    /// 
    /// Defines an RGB colour.
    /// </summary>
    [TypeConverter(typeof(Color3Converter))]
    public class Color3 : Instance
    {
        internal override string ClassName => "Color3";

        /// <summary>
        /// The red component of this <see cref="Color3"/>.
        /// </summary>
        public byte R { get { return C3Internal.R; } set { C3Internal.R = value; } }

        /// <summary>
        /// The green component of this <see cref="Color3"/>.
        /// </summary>
        public byte G { get { return C3Internal.G; } set { C3Internal.G = value; } }

        /// <summary>
        /// The blue component of this <see cref="Color3"/>.
        /// </summary>
        public byte B { get { return C3Internal.B; } set { C3Internal.B = value; } }

        private Color3Internal C3Internal { get; set; }

        public Color3() // old code compat (July 12, 2021)
        {
            C3Internal = new Color3Internal(); 
        }

        public Color3(byte CR, byte CG, byte CB)
        {
            C3Internal = new Color3Internal(); 
            // use for non-DataModel ONLY
            R = CR;
            G = CG;
            B = CB;
        }

        // result class?

        /// <summary>
        /// Convert a relative colour string to a Color3 value.
        /// </summary>
        /// <param name="Colour"></param>
        /// <param name="AddToDataModel">If false, simply creates an object and returns. If true, adds to the DataModel</param>
        /// <returns></returns>
        public static Color3 FromRelative(string Colour, bool AddToDataModel = true, Instance Parent = null) 
        {
            // December 9, 2021 (NuRender integration)
            Color3Internal C3I = Color3Internal.FromRelative(Colour);

            if (!AddToDataModel)
            {
                return new Color3(C3I.R, C3I.G, C3I.B);
            }
            else
            {
                Color3 NC3 = (Color3)DataModel.CreateInstance("Color3", Parent); // create a new Color3 and add it to the datamodel
                NC3.R = C3I.R;
                NC3.G = C3I.G;
                NC3.B = C3I.B;

                return NC3; 
            }
        }
       

        public static Color3 FromHex(string Colour, bool AddToDataModel = true, Instance Parent = null)
        {
            // December 9, 2021 (NuRender integration)
            Color3Internal C3I = Color3Internal.FromHex(Colour);

            if (!AddToDataModel)
            {
                return new Color3(C3I.R, C3I.G, C3I.B);
            }
            else
            {
                Color3 NC3 = (Color3)DataModel.CreateInstance("Color3", Parent); // create a new Color3 and add it to the datamodel
                NC3.R = C3I.R;
                NC3.G = C3I.G;
                NC3.B = C3I.B;

                return NC3;
            }

        }


        public static Color3 FromString(string Str, bool AddToDataModel = true, Instance Parent = null)
        {
            // December 9, 2021 (NuRender integration)
            Color3Internal C3I = Color3Internal.FromString(Str);

            if (!AddToDataModel)
            {
                return new Color3(C3I.R, C3I.G, C3I.B);
            }
            else
            {
                Color3 NC3 = (Color3)DataModel.CreateInstance("Color3", Parent); // create a new Color3 and add it to the datamodel
                NC3.R = C3I.R;
                NC3.G = C3I.G;
                NC3.B = C3I.B;

                return NC3;
            }
        }



        #region Not really blending (these operations will use the Color3Internal values)

        public static Color3 operator +(Color3 A, Color3 B) => new Color3((byte)(A.R + B.R), (byte)(A.G + B.G), (byte)(A.B + B.B));
        public static Color3 operator +(double A, Color3 B) => new Color3((byte)(A + B.R), (byte)(A + B.G), (byte)(A + B.B));
        public static Color3 operator +(Color3 A, double B) => new Color3((byte)(A.R + B), (byte)(A.G + B), (byte)(A.B + B));
        public static Color3 operator -(Color3 A, Color3 B) => new Color3((byte)(A.R - B.R), (byte)(A.G - B.G), (byte)(A.B - B.B));
        public static Color3 operator -(double A, Color3 B) => new Color3((byte)(A - B.R), (byte)(A - B.G), (byte)(A - B.B));
        public static Color3 operator -(Color3 A, double B) => new Color3((byte)(A.R - B), (byte)(A.G - B), (byte)(A.B - B));
        public static Color3 operator *(Color3 A, Color3 B) => new Color3((byte)(A.R * B.R), (byte)(A.G * B.G), (byte)(A.B * B.B));
        public static Color3 operator *(double A, Color3 B) => new Color3((byte)(A * B.R), (byte)(A * B.G), (byte)(A * B.B));
        public static Color3 operator *(Color3 A, double B) => new Color3((byte)(A.R * B), (byte)(A.G * B), (byte)(A.B * B));
        public static Color3 operator /(Color3 A, Color3 B) => new Color3((byte)(A.R / B.R), (byte)(A.G / B.G), (byte)(A.B / B.B));
        public static Color3 operator /(double A, Color3 B) => new Color3((byte)(A / B.R), (byte)(A / B.G), (byte)(A / B.B));
        public static Color3 operator /(Color3 A, double B) => new Color3((byte)(A.R / B), (byte)(A.G / B), (byte)(A.B / B));

        #endregion

        #region NuRender conversions 

        // New: Dec 15, 2021

        public static explicit operator Color3(Color3Internal C3I) => new Color3(C3I.R, C3I.G, C3I.B);

        public static explicit operator Color3Internal(Color3 C3) => new Color3Internal(C3.R, C3.G, C3.B);

        #endregion

    }
}
