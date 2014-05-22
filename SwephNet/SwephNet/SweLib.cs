using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Library
    /// </summary>
    public class SweLib
    {
        /// <summary>
        /// Value to convert Degrees to Radian
        /// </summary>
        public const double DEGTORAD = 0.0174532925199433;

        /// <summary>
        /// Value to convert Radian to Degrees
        /// </summary>
        public const double RADTODEG = 57.2957795130823;

        /// <summary>
        /// Reduce x modulo 360 degrees
        /// </summary>
        public static double DegNorm(double x)
        {
            double y;
            y = (x % 360.0);
            if (Math.Abs(y) < 1e-13) y = 0;	// Alois fix 11-dec-1999
            if (y < 0.0) y += 360.0;
            return (y);
        }

        /// <summary>
        /// Convert a degrees value to radians
        /// </summary>
        public static double DegToRad(double x)
        {
            return x * DEGTORAD;
        }

    }
}
