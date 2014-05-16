using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Represents a Julian Day as Ephemeris Time
    /// </summary>
    public struct EphemerisTime
    {

        /// <summary>
        /// Create a new Ephemeris Time
        /// </summary>
        /// <param name="day">The Julian Day</param>
        /// <param name="deltaT">The DeltaT value</param>
        public EphemerisTime(JulianDay day, Double deltaT)
            : this() {
            this.JulianDay = day;
            this.DeltaT = deltaT;
            this.Value = this.JulianDay.Value + this.DeltaT;
        }

        /// <summary>
        /// Implicit cast a Ephemeris Time to double
        /// </summary>
        public static implicit operator double(EphemerisTime et) { return et.Value; }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString() {
            return Value.ToString();
        }

        /// <summary>
        /// The Julian Day
        /// </summary>
        public JulianDay JulianDay { get; private set; }

        /// <summary>
        /// The DelatT
        /// </summary>
        public Double DeltaT { get; private set; }

        /// <summary>
        /// The Ephemeris Time value
        /// </summary>
        public double Value { get; private set; }


    }
}
