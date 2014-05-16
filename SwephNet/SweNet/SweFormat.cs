using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Format methods
    /// </summary>
    public static class SweFormat
    {
        /// <summary>
        /// Format a value to format : D ° MM' SS.0000
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatAsDegrees(Double value) {
            bool minus = value < 0;
            value = Math.Abs(value);
            var deg = (int)value;
            var min = (int)((value * 60.0) % 60.0);
            var sec = ((value * 3600.0) % 60.0);
            return String.Format("{0}{1,3:##0}° {2,2:#0}' {3,7:#0.0000}", minus ? '-' : ' ', deg, min, sec);
        }

        /// <summary>
        /// Format a value to format : HH:mm:ss
        /// </summary>
        public static string FormatAsTime(Double value) {
            var deg = (int)value;
            value = Math.Abs(value);
            var min = (int)((value * 60.0) % 60.0);
            var sec = (int)((value * 3600.0) % 60.0);
            return String.Format("{0,2:00}:{1:00}:{2:00}", deg, min, sec);
        }

        /// <summary>
        /// Format a value to format : 'HH' h 'mm' m 'ss' s
        /// </summary>
        public static string FormatAsHour(Double value) {
            var deg = (int)value;
            value = Math.Abs(value);
            var min = (int)((value * 60.0) % 60.0);
            var sec = (int)((value * 3600.0) % 60.0);
            return String.Format("{0,2:#0} h {1:00} m {2:00} s", deg, min, sec);
        }

    }

}
