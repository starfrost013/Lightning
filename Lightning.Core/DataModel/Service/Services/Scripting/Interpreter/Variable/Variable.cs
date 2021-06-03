using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Variable
    /// 
    /// May 3, 2021
    /// 
    /// Defines a script variable.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of this variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of this variable.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Is this viewable? 
        /// </summary>
        public bool IsDebuggable { get; set; }

        public VariableTypes VariableType { get; set; }

        public static string GenerateAutomaticVariableName()
        {
            Random Rand = new Random();
            int RandNext = Rand.Next(100000000, 999999999);

            // make sure there is no possible way that a collision can occur

            int Year = Rand.Next(1901, 2099);
            int Month = Rand.Next(1, 12);
            int Day = Rand.Next(1, 28); // can't be assed to deal with invalid dates such as 4/29/2012
            int Hour = Rand.Next(0, 23);
            int Minute = Rand.Next(0, 59);
            int Second = Rand.Next(0, 59);
            int Millisecond = Rand.Next(0, 999);
            
            // apparently DateTimeOffset : DateTime ig
            DateTimeOffset DT = new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond);

            long FinalDTString = DT.ToUnixTimeSeconds();

            return $"Polaris_Variable_20210603_{RandNext}_{FinalDTString}";
        }

        public void DetermineVariableType()
        {
            if (Value == null)
            {

            }
            else
            {

            }
        }
    }
}
