using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwissEphNet
{

    /// <summary>
    /// C tools
    /// </summary>
    public static partial class C
    {
        static readonly char[] nchars = "0123456789.+-Ee".ToCharArray();

        /// <summary>
        /// 
        /// </summary>
        public static double atof(String s) {
            s = (s ?? String.Empty).Trim();
            int i = s.IndexOfFirstNot(nchars);
            if (i >= 0)
                s = s.Substring(0, i);
            double result = 0;
            if (double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result))
                return result;
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static double fmod(double numer, double denom)
        {
            return numer % denom;
        }

    }

}
