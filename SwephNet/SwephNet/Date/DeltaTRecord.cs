using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet.Date
{
    /// <summary>
    /// Record of DeltaT in a file
    /// </summary>
    public class DeltaTRecord
    {
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// DeltaT value
        /// </summary>
        public Double Value { get; set; }
    }
}
