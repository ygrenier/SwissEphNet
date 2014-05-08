using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Models
{
    /// <summary>
    /// Ephemeris calculations
    /// </summary>
    public class EphemerisResult
    {
        /// <summary>
        /// New result
        /// </summary>
        public EphemerisResult() {
            Planets = new List<PlanetValues>();
            Houses = new List<HouseValues>();
            ASMCs = new List<HouseValues>();
        }

        /// <summary>
        /// Reset the result
        /// </summary>
        public void Reset() {
            DateUTC = new DateUT();
            JulianDay = new JulianDay();
            EphemerisTime = new EphemerisTime();
            SideralTime = 0;
            MeanEclipticObliquity = 0;
            TrueEclipticObliquity = 0;
            NutationLongitude = 0;
            NutationObliquity = 0;
            Planets.Clear();
            Houses.Clear();
            ASMCs.Clear();
        }

        /// <summary>
        /// Date UTC
        /// </summary>
        public DateUT DateUTC { get; set; }

        /// <summary>
        /// Julian Day
        /// </summary>
        public JulianDay JulianDay { get; set; }

        /// <summary>
        /// Ephemeris time
        /// </summary>
        public EphemerisTime EphemerisTime { get; set; }

        /// <summary>
        /// Delat T in seconds
        /// </summary>
        public Double DeltaTSec { get { return EphemerisTime.DeltaT * 86400.0; } }

        /// <summary>
        /// Sideral time
        /// </summary>
        public double SideralTime { get; set; }

        /// <summary>
        /// Sideral time in degrees
        /// </summary>
        public double SideralTimeInDegrees { get { return SideralTime * 15; } }

        /// <summary>
        /// ARMC : Sideral time in degrees
        /// </summary>
        public double ARMC { get { return SideralTime * 15; } }

        /// <summary>
        /// Mean ecliptic obliquity
        /// </summary>
        public Double MeanEclipticObliquity { get; set; }

        /// <summary>
        /// True ecliptic obliquity
        /// </summary>
        public Double TrueEclipticObliquity { get; set; }

        /// <summary>
        /// Nutation in longitude
        /// </summary>
        public Double NutationLongitude { get; set; }

        /// <summary>
        /// Nutation in obliquity
        /// </summary>
        public Double NutationObliquity { get; set; }

        /// <summary>
        /// Planets calculation result
        /// </summary>
        public List<PlanetValues> Planets { get; private set; }

        /// <summary>
        /// Houses
        /// </summary>
        public List<HouseValues> Houses { get; private set; }

        /// <summary>
        /// Ascendants, MC etc.
        /// </summary>
        public List<HouseValues> ASMCs { get; private set; }

    }
}
