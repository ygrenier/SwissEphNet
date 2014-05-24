using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet.Houses
{

    /// <summary>
    /// House calculation result
    /// </summary>
    public class HouseResult
    {
        /// <summary>
        /// Create a new calculation result
        /// </summary>
        public HouseResult(double[] houses, double[] ascmc)
        {
            this.Houses = houses;
            this.AscMc = ascmc;
        }

        /// <summary>
        /// List of houses cuspids
        /// </summary>
        public Double[] Houses { get; private set; }

        /// <summary>
        /// Liste of asc and mc positions
        /// </summary>
        public Double[] AscMc { get; private set; }
    }

}
