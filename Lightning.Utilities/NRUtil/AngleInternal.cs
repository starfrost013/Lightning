using System;
using System.Collections.Generic;
using System.Text;

namespace NuCore.Utilities
{
    /// <summary>
    /// AngleInternal
    /// 
    /// January 8, 2022
    /// 
    /// Defines an angle
    /// </summary>
    public static class AngleInternal
    {
        /// <summary>
        /// Clamps double <paramref name="D"></paramref>to between zero and 360.
        /// </summary>
        /// <param name="D">The angle to be clamped.</param>
        /// <returns>If <paramref name="D"></paramref> is below zero. If it is above 360, 360. Otherwise, the value of <paramref name="D"></paramref> is returned. </returns>
        public static double ClampToAngle(this double D)
        {
            if (D < 0) return 0;
            if (D > 360) return 360.00;

            return D;
        }

        /// <summary>
        /// Converts the angle in degrees <paramref name="D"> to radians</paramref>
        /// </summary>
        /// <param name="D">The angle <paramref name="D"/> in degrees.</param>
        /// <returns><paramref name="D"/> in radians.</returns>
        public static double DegreesToRadians(this double D) => D * (Math.PI / 180);


        /// <summary>
        /// Converts the angle in radians <paramref name="D"> to degrees</paramref>
        /// </summary>
        /// <param name="D">The angle <paramref name="D"/> in radians.</param>
        /// <returns><paramref name="D"/> in degrees.</returns>
        public static double RadiansToDegrees(this double D) => D * (180 / Math.PI);
    }
}
