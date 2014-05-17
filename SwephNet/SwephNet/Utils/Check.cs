using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Some check methods
    /// </summary>
    public static class Check
    {

        /// <summary>
        /// Check if an argument is not null
        /// </summary>
        public static void ArgumentNotNull(object value, String name, String message = null) {
            if (value == null) {
                if (!String.IsNullOrEmpty(message))
                    throw new ArgumentNullException(name, message);
                else
                    throw new ArgumentNullException(name);
            }
        }

    }
}
