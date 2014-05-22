using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Sweph error
    /// </summary>
    public class SweError : Exception
    {
        /// <summary>
        /// New error
        /// </summary>
        public SweError(String message)
            : base(message)
        {
        }

        /// <summary>
        /// New error
        /// </summary>
        public SweError(Exception innerException, String message)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// New error
        /// </summary>
        public SweError(String message, params object[] args)
            : base(String.Format(message, args))
        {
        }

        /// <summary>
        /// New error
        /// </summary>
        public SweError(Exception innerException, String message, params object[] args)
            : base(String.Format(message, args), innerException)
        {
        }
    }
}
