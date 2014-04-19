/// <summary>
/// trace management
/// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SweNet
{
    partial class SwissEph
    {

#if TRACE
        /// <summary>
        /// Trace a message
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void trace(String format, params object[] args) {
            var h = OnTrace;
            if (h != null)
                h(this, new TraceEventArgs(C.sprintf(format, args)));
        }

        /// <summary>
        /// Event raised when a trace is emit
        /// </summary>
        public event EventHandler<TraceEventArgs> OnTrace;
#endif

    }
}
