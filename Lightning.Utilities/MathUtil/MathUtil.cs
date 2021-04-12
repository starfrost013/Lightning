using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// Utilities: MathUtl
    /// 
    /// April 12, 2021
    /// 
    /// Provides mathematical utilities.
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// Converts degrees to radians. 
        /// </summary>
        /// <param name="Deg">The number of degrees that need to be converted to Radians.</param>
        /// <returns></returns>
        public static double DegreesToRadians(double Deg)
        {
            if (Deg < 0 || Deg > 360)
            {
                throw new OverflowException("Degrees must be between 0 and 360!");
            }
            else
            {
                return Deg * (Math.PI / 180);
            }
        }
        
    }
}
