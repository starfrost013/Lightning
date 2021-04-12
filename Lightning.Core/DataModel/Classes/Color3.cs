﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Color3
    /// 
    /// March 7, 2021 (Modified April 12, 2021)
    /// 
    /// Defines an RGB colour.
    /// </summary>
    [TypeConverter(typeof(Color3Converter))]
    public class Color3 : SerialisableObject
    {
        public override string ClassName => "Color3"; 
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        // result class?

        /// <summary>
        /// Convert a relative colour string to a Color3 value.
        /// </summary>
        /// <param name="Colour"></param>
        /// <param name="AddToDataModel">If false, simply creates an object and returns. If true, adds to the DataModel</param>
        /// <returns></returns>
        public static Color3 FromRelative(string Colour, bool AddToDataModel = true)
        {
            string[] Spx = Colour.Split(',');

            if (Spx.Length != 3)
            {
                ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingRelativeColourException");
                return null; // do we use a result class for this?
            }
            else
            {
                try
                {
                    double R00 = Convert.ToDouble(Spx[0]);
                    double R01 = Convert.ToDouble(Spx[1]);
                    double R02 = Convert.ToDouble(Spx[2]);

                    // Error check
                    if (R00 < 0 || R00 > 1
                     || R01 < 0 || R01 > 1
                     || R02 < 0 || R02 > 1)
                    {
                        ErrorManager.ThrowError("Color3 Converter", "RelativeColourOutOfRangeException");
                        return null; //result class?
                    }

                    Color3 C3;

                    if (!AddToDataModel)
                    {
                        C3 = new Color3(); 

                    }
                    else
                    {
                        C3 = (Color3)DataModel.CreateInstance("Color3");

                    }

                    C3.R = Convert.ToByte(R00 * 255);
                    C3.G = Convert.ToByte(R01 * 255);
                    C3.B = Convert.ToByte(R02 * 255);

                    return C3;

                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingRelativeColourFormatException", "Attempted to convert an invalid RelativeColour to a Color3!", err);
#else
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingRelativeColourFormatException", "Attempted to convert an invalid RelativeColour to a Color3!");
#endif
                    return null; 
                }

                
            }
        }

        public static Color3 FromHex(string Colour, bool AddToDataModel = true)
        {
            // Remove the # that is sometimes used. 
            if (Colour.Contains("#"))
            {
                Colour = Colour.Replace("#", "");
            }

            if (Colour.Length != 6)
            {
                ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingHexadecimalColourException");
                return null;
            }
            else
            {
                string R = Colour.Substring(0, 2);
                string G = Colour.Substring(2, 2);
                string B = Colour.Substring(4, 2);

                try
                {
                    byte FR = byte.Parse(R, NumberStyles.HexNumber);
                    byte FG = byte.Parse(G, NumberStyles.HexNumber);
                    byte FB = byte.Parse(B, NumberStyles.HexNumber);

                    Color3 C3;

                    if (AddToDataModel)
                    {
                        C3 = (Color3)DataModel.CreateInstance(typeof(Color3).Name);
                    }
                    else
                    {
                        C3 = new Color3(); 
                    }

                    C3.R = FR;
                    C3.G = FG;
                    C3.B = FB;

                    return C3; 
                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingHexadecimalColourFormatException", "An error occurred when converting a hexadecimal colour string to a Color3", err);
#else
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingHexadecimalColourFormatException", "An error occurred when converting a hexadecimal colour string to a Color3");
#endif
                    // TEMP - ERRORS.XML
                    return null;
                }

            }


        }


        public static Color3 FromString(string Str, bool AddToDataModel = true)
        {
            string[] Str_Components = Str.Split(',');

            if (Str_Components.Length != 3)
            {
                ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingCommaColourFormatException");
                return null;
            }
            else
            {
                try
                {
                    // not sure if we should add to the datamodel here?
                    Color3 C3;

                    if (AddToDataModel)
                    {
                        C3 = (Color3)DataModel.CreateInstance("Color3");
                    }
                    else
                    {
                        C3 = new Color3(); 
                    }


                    string R = Str_Components[0];
                    string G = Str_Components[1];
                    string B = Str_Components[2];

                    byte CR = byte.Parse(R);
                    byte CG = byte.Parse(G);
                    byte CB = byte.Parse(B);

                    C3.R = CR;
                    C3.G = CG;
                    C3.B = CB;

                    return C3; 
                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingCommaColourFormatException", "Attemped to load a Color3 with an invalid component!", err);
#else
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingCommaColourFormatException", "Attemped to load a Color3 with an invalid component!");
#endif
                    return null;
                }
                catch (OverflowException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingCommaColourFormatException", "Attempted to load a Color3 with a component that was not within the range [0-255]!", err);
#else
                    ErrorManager.ThrowError("Color3 Converter", "ErrorConvertingCommaColourFormatException", "Attempted to load a Color3 with a component that was not within the range [0-255]!", err);
#endif
                    return null;
                }
            }
        }

    }
}
