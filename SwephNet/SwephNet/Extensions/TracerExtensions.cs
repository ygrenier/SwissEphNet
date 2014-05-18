using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Extensions for ITracer
    /// </summary>
    public static class TracerExtensions
    {
        /// <summary>
        /// Trace a formatted message
        /// </summary>
        public static void Trace(this ITracer tracer, String message, params object[] args)
        {
            if (tracer != null)
            {
                if (args != null && args.Length > 0)
                    message = String.Format(message, args);
                tracer.Trace(message);
            }
        }

    }
}
