using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core.API
{
    [TypeConverter]
    public class StatementTokenConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
        
            if (sourceType == typeof(string))
            {
                return true; 
            }
            else
            {
                return base.CanConvertTo(context, sourceType);
            }
            
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Type VType = value.GetType(); 

            if (VType == typeof(string))
            {
                return StatementToken.FromString((string)value);

            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
            
        }


    }
}
