using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double fmod(double numer, double denom)
        {
            return numer % denom;
        }

        public static void qsort<T>(CPointer<T> array, int n, Comparison<T> compare)
        {
            var list = new List<T>(array.ToArray().Take(n));
            list.Sort(compare);
            for (int i = 0; i < list.Count; i++)
                array[i] = list[i];
        }

        class bcomparer<TKey, TVal> : IComparer<TVal>
        {
            public bcomparer(TKey key, Func<TKey, TVal, int> compare)
            {
                Key = key;
                Comparer = compare;
            }
            public int Compare(TVal x, TVal y)
            {
                bool xIsDefault = x == null || x.Equals(default(TVal));
                bool yIsDefault = y == null || y.Equals(default(TVal));
                if (yIsDefault && !xIsDefault)
                {
                    int c = Comparer(Key, x);
                    if (c != 0) return -c;
                    return c;
                }
                else if (xIsDefault && !yIsDefault)
                {
                    return Comparer(Key, y);
                }
                else
                    return -1;
            }
            public TKey Key { get; }
            public Func<TKey, TVal, int> Comparer { get; }
        }

        public static CPointer<TVal> bsearch<TKey, TVal>(TKey key, CPointer<TVal> array, int n, Func<TKey, TVal, int> compare)
        {
            var list = new List<TVal>(array.ToArray().Take(n));
            var idx = list.BinarySearch(default(TVal), new bcomparer<TKey, TVal>(key, compare));
            return idx >= 0 ? array + idx : new CPointer<TVal>();
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int strlen(string s) => s?.Length ?? 0;

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string strcpy(out string a, string b) => a = b;

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void strncpy(out string a, string b, int n)
            => a = b != null ? b.Substring(0, Math.Min(n, b.Length)) : null;

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void strcat(ref string a, string b) => a = string.Concat(a, b);

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void strncat(ref string a, string b, int n) {
            n = Math.Min(n, b?.Length ?? 0);
            if (n > 0)
                a = string.Concat(a, b.Substr(0, n));
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int strcmp(string a, string b)
        {
            return string.Compare(a, b);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int strncmp(string a, string b, int n)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                return string.Compare(a, b);
            return string.Compare(a.Substring(0, Math.Min(a.Length, n)), b.Substring(0, Math.Min(b.Length, n)));
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int strstr(string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                return -1;
            return a.IndexOf(b);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int strchr(string s, char c)
        {
            if (string.IsNullOrEmpty(s))
                return -1;
            return s.IndexOf(c);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void rewind(CFile file)
        {
            if (file != null)
                file.Seek(0, System.IO.SeekOrigin.Begin);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void fclose(CFile file)
            => file?.Dispose();

    }

}
