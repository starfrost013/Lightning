using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GradientClassConverter
    /// 
    /// August 1, 2021 00:31
    /// 
    /// Defines a type converter for Gradients.
    /// </summary>
    [TypeConverter]
    public class GradientConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Type VType = value.GetType();

            if (VType == typeof(string))
            {
                string VString = (string)value;

                return ConvertFromString(VString);
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
            
        }

        public new LinearGradientBrush ConvertFromString(string String)
        {

            if (String == null
            || String.Length == 0)
            {
                ErrorManager.ThrowError("Gradient Converter", "InvalidGradientException", "Gradient string cannot be null or empty!");
                return null; 
            }

            string[] NewLines = String.Split("\r\n", StringSplitOptions.None);

            LinearGradientBrush Gradient = new LinearGradientBrush();

            foreach (string NewLine in NewLines)
            {
                // Separate by colons
                string[] ColonSeparatedParts = NewLine.Split(':');

                if (ColonSeparatedParts.Length < 2) // must be two components
                {
                    ErrorManager.ThrowError("Gradient Converter", "InvalidGradientException", "Invalid gradient string line detected! Gradient string lines must have two components (point and colour) separated by commas!");
                    return null; 
                }
                else 
                {
                    GradientStop GS = new GradientStop();
                    
                    try
                    {
                        // Convert to int - throw error if error found
                        GS.StopPoint = Convert.ToInt32(ColonSeparatedParts[0]);

                        TypeConverter C4Converter = TypeDescriptor.GetConverter(typeof(Color4));

                        GS.Colour = (Color4)C4Converter.ConvertFromString(ColonSeparatedParts[1]);

                        Gradient.Children.Add(GS);
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        ErrorManager.ThrowError("Gradient Converter", "InvalidGradientException", $"Invalid gradient string line detected - invalid stop point or colour!\n\nException:\n{ex}");
#else
                        ErrorManager.ThrowError("Gradient Converter", "InvalidGradientException", $"Invalid gradient string line detected - invalid stop point or colour");
#endif
                    }
                }
            }

            return Gradient; 
        }
    }
}
