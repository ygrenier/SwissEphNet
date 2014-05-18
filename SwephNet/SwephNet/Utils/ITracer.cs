using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Trace provider
    /// </summary>
    public interface ITracer
    {

        /// <summary>
        /// Trace a message
        /// </summary>
        void Trace(String message);

    }
}
