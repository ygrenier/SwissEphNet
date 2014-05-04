using SweNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// New result
        /// </summary>
        public CalculationResultViewModel() {
            Planets = new ObservableCollection<PlanetInfos>();
            Houses = new ObservableCollection<HouseInfos>();
            ASMCs = new ObservableCollection<HouseInfos>();
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
        public ObservableCollection<PlanetInfos> Planets { get; private set; }

        /// <summary>
        /// Houses
        /// </summary>
        public ObservableCollection<HouseInfos> Houses { get; private set; }

        /// <summary>
        /// Ascendants, MC etc.
        /// </summary>
        public ObservableCollection<HouseInfos> ASMCs { get; private set; }

    }

}
