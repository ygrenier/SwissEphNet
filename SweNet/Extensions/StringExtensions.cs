using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// String extensions methods
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// String.Contains() for Char
        /// </summary>
        public static bool Contains(this String s, Char c) {
            return s.Contains(c.ToString());
        }

        /// <summary>
        /// String.Contains() for Char
        /// </summary>
        public static bool Contains(this String s, Char[] charSet) {
            if (charSet == null || String.IsNullOrWhiteSpace(s)) return false;
            foreach (var c in charSet) {
                if (s.Contains(c)) return true;
            }
            return false;
        }

    }

}
