using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Date
{
    /// <summary>
    /// DeltaT record
    /// </summary>
    public class RecordDeltaT
    {
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Delta T in 0.01 sec.
        /// </summary>
        public double DeltaT { get; set; }
    }
}
