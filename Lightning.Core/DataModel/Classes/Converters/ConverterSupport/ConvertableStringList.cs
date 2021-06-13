using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ConvertableList
    /// 
    /// June 13, 2021
    /// 
    /// Defines a convertable string list. This is mostly a "shell" class for TypeConverters. (I wish this didn't exist but it has to).
    /// </summary>
    /// 

    [TypeConverter(typeof(StringListConverter))]
    public class ConvertableStringList : List<string>
    {
        public ConvertableStringList()
        {

        }

        public ConvertableStringList(List<string> StringList)
        {
            foreach (string List in StringList)
            {
                Add(List); 
            }
        }
        
    }
}
