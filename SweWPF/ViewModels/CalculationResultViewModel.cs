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
    public class CalculationResultViewModel : ViewModel
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
        public DateUT DateUTC {
            get { return _DateUTC; }
            set {
                _DateUTC = value;
                RaisePropertyChanged("DateUTC");
            }
        }
        private DateUT _DateUTC;

        /// <summary>
        /// Julian Day
        /// </summary>
        public JulianDay JulianDay {
            get { return _JulianDay; }
            set {
                _JulianDay = value;
                RaisePropertyChanged("JulianDay");
            }
        }
        private JulianDay _JulianDay;

        /// <summary>
        /// Ephemeris time
        /// </summary>
        public EphemerisTime EphemerisTime {
            get { return _EphemerisTime; }
            set {
                _EphemerisTime = value;
                RaisePropertyChanged("EphemerisTime");
                RaisePropertyChanged("DeltaTSec");
            }
        }
        private EphemerisTime _EphemerisTime;

        /// <summary>
        /// Delat T in seconds
        /// </summary>
        public Double DeltaTSec { get { return EphemerisTime.DeltaT * 86400.0; } }

        /// <summary>
        /// Sideral time
        /// </summary>
        public double SideralTime {
            get { return _SideralTime; }
            set {
                _SideralTime = value;
                RaisePropertyChanged("SideralTime");
                RaisePropertyChanged("SideralTimeInDegrees");
                RaisePropertyChanged("ARMC");
            }
        }
        private double _SideralTime;

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
        public Double MeanEclipticObliquity {
            get { return _MeanEclipticObliquity; }
            set {
                _MeanEclipticObliquity = value;
                RaisePropertyChanged("MeanEclipticObliquity");
            }
        }
        private Double _MeanEclipticObliquity;

        /// <summary>
        /// True ecliptic obliquity
        /// </summary>
        public Double TrueEclipticObliquity {
            get { return _TrueEclipticObliquity; }
            set {
                _TrueEclipticObliquity = value;
                RaisePropertyChanged("TrueEclipticObliquity");
            }
        }
        private Double _TrueEclipticObliquity;

        /// <summary>
        /// Nutation in longitude
        /// </summary>
        public Double NutationLongitude {
            get { return _NutationLongitude; }
            set {
                _NutationLongitude = value;
                RaisePropertyChanged("NutationLongitude");
            }
        }
        private Double _NutationLongitude;

        /// <summary>
        /// Nutation in obliquity
        /// </summary>
        public Double NutationObliquity {
            get { return _NutationObliquity; }
            set {
                _NutationObliquity = value;
                RaisePropertyChanged("NutationObliquity");
            }
        }
        private Double _NutationObliquity;

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
