using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// OperatorTokenConverter
    /// 
    /// April 18, 2021
    /// 
    /// Converts operator tokens to and from strings. For non-strings, uses the base TypeConverter method.
    /// </summary>
    [TypeConverter]
    public class OperatorTokenConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true; 
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
            
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                OperatorToken OT = (OperatorToken)value;

                return OT.ToString();
            }
            else
            {

                return base.ConvertTo(context, culture, value, destinationType);
            }

        }

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
            Type ValueType = value.GetType(); 

            if (ValueType == typeof(string))
            {
                return OperatorToken.FromString((string)value); 
            }
            else
            {
                return base.ConvertTo(context, culture, value, ValueType);
            }
            
        }
    }
}
