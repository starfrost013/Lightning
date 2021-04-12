﻿using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core
{
    [TypeConverter]
    public class Color4Converter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(Color3))
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
            if (value.GetType() == typeof(string))
            {
                string Value_Str = (string)value;
                return ConvertFromString(Value_Str);
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }

        public new Color4 ConvertFromString(string Text)
        {
            // Determine the method we want to call.
            // This could be made easier by the length or something
            if (Text.Contains('#'))
            {
                return Color4.FromHex(Text, false); 
            }
            else
            {
                if (Text.Contains('.') || Text.Contains("1,1,1")) // dumb hack
                {
                    return Color4.FromRelative(Text, false);
                }
                else
                {
                    return Color4.FromString(Text, false); 
                }
            }
        }

    }
}