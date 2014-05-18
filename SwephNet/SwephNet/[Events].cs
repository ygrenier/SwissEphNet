using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Arguments for trace event
    /// </summary>
    public class TraceEventArgs : EventArgs
    {
        /// <summary>
        /// Create new arguments
        /// </summary>
        public TraceEventArgs(String message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Message
        /// </summary>
        public String Message { get; private set; }
    }

}
