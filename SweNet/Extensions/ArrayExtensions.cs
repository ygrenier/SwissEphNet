using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Extensions de tableau
    /// </summary>
    public static class ArrayExtensions
    {
        /*
        /// <summary>
        /// 
        /// </summary>
        public static T[] Slice<T>(this T[] arr, int start) {
            if (arr == null) return null;
            return arr.Skip(start).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public static T[] Slice<T>(this T[] arr, int start, int count) {
            if (arr == null) return null;
            return arr.Skip(start).Take(count).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public static T[] Slice<T>(this T[,] arr, int dimension, int start) {
            if (arr == null) return null;
            return Enumerable.Range(0, arr.GetUpperBound(1)).Skip(start).Select(i => arr[dimension, i]).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public static T[] Slice<T>(this T[,] arr, int dimension, int start, int count) {
            if (arr == null) return null;
            return Enumerable.Range(0, arr.GetUpperBound(1)).Skip(start).Take(count).Select(i => arr[dimension, i]).ToArray();
        }
        */

        /// <summary>
        /// Make an CPointer from an array
        /// </summary>
        public static CPointer<T> GetPointer<T>(this T[] array, int index = 0) {
            return new CPointer<T>(array, index);
        }

    }
}
