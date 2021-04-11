using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Vector2Converter [Non-DataModel]
    /// 
    /// April 11, 2021
    /// 
    /// Converts strings to Vector2 for DDMS attributes 
    /// </summary>
    [TypeConverter]
    public class Vector2Converter : TypeConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(Vector2))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(sourceType); 
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() != typeof(string))
            {
                ErrorManager.ThrowError("Vector2Converter", "Vector2ConversionInvalidNumberOfComponentsException");
                return null;
            }
            else
            {
                return ConvertFromString((string)value);
            }
            
        }

        public new Vector2 ConvertFromString(string Str)
        {
            // We do not add this to the DataModel, as it is an attribute

            Vector2 V2 = new Vector2(); // Do not change, as useless objects will pollute the workspace if we add it

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