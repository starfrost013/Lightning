﻿using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lightning.Core
{
    [TypeConverter]
    public class Color3Converter : TypeConverter
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

        public new Color3 ConvertFromString(string Text)
        {
            // Determine the method we want to call.
            // This could be made easier by the length or something
            if (Text.Contains('#'))
            {
                return Color3.FromHex(Text); 
            }
            else
            {
                if (Text.Contains('.'))
                {
                    return Color3.FromRelative(Text);
                }
                else
                {
                    return Color3.FromString(Text); 
                }
            }
        }

    }
}
