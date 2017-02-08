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
        static readonly char[] fchars = "0123456789.+-Ee".ToCharArray();
        static readonly char[] ichars = "0123456789".ToCharArray();

        /// <summary>
        /// 
        /// </summary>
        public static double atof(string s) {
            s = (s ?? string.Empty).Trim();
            int i = s.IndexOfFirstNot(fchars);
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
        public static int atoi(string s)
        {
            s = (s ?? string.Empty).Trim();
            int i = s.IndexOfFirstNot(ichars);
            if (i >= 0)
                s = s.Substring(0, i);
            int result = 0;
            if (int.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result))
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
