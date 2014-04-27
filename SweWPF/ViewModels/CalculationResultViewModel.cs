using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// Calculation result
    /// </summary>
    public class CalculationResultViewModel : ChildViewModel
    {

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
    }

}
