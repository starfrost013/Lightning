using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core.API
{
    [TypeConverter]
    public class StringListConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
            
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Type VType = value.GetType(); 

            if (VType != typeof(string))
            {
                ErrorManager.ThrowError("String-to-String List Converter", "AttemptedToConvertNonStringToStringListException");
                return null; 
            }
            else
            {
                string SValue = (string)value;

                string[] SValue_Commas = SValue.Split(',');

                if (SValue_Commas.Length == 0)
                {
                    ErrorManager.ThrowError("String-to-String List Conveter", "AttemptedToConvertInvalidStringToStringListException");
                    return null;
                }
                else
                {
                    List<string> LS0 = new List<string>();  

                    foreach (string SValue_Comma in SValue_Commas)
                    {
                        LS0.Add(SValue_Comma);
                    }

                    return LS0; 
                }
                
            }
        }
    }
}
