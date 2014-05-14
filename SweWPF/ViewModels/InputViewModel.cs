using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{
    /// <summary>
    /// Input informations ViewModel
    /// </summary>
    public class InputViewModel : ViewModel
    {

        public InputViewModel() {
            ListEphemerisModes = new List<Tuple<SweNet.EphemerisMode, string>> {
                new Tuple<EphemerisMode,String>(SweNet.EphemerisMode.SwissEphemeris, "Swiss Ephemeris"),
                new Tuple<EphemerisMode,String>(SweNet.EphemerisMode.JPL, "JPL Ephemeris"),
                new Tuple<EphemerisMode,String>(SweNet.EphemerisMode.Moshier, "Moshier Ephemeris"),
            };
            EphemerisMode = SweNet.EphemerisMode.SwissEphemeris;
            ListJplFiles = new List<Tuple<string, string>> {
                new Tuple<String,String>(SwissEphNet.SwissEph.SE_FNAME_DE406, "JPL D406"),
                new Tuple<String,String>(SwissEphNet.SwissEph.SE_FNAME_DE431, "JPL D431")
            };
            JplFile = SwissEphNet.SwissEph.SE_FNAME_DFT;
            InputDate = new InputDateViewModel();
            Planets = new List<Planet>();
            InputDate.Date = new DateUT(DateTime.Now);
            PositionCenter = SweNet.PositionCenter.Geocentric;
            ListPositionCenters = new List<Tuple<SweNet.PositionCenter, string>>(new Tuple<SweNet.PositionCenter, string>[]{
                new Tuple<SweNet.PositionCenter, string>(SweNet.PositionCenter.Geocentric,"Geocentric"),
                new Tuple<SweNet.PositionCenter, string>(SweNet.PositionCenter.Topocentric,"Topocentric"),
                new Tuple<SweNet.PositionCenter, string>(SweNet.PositionCenter.Heliocentric,"Heliocentric"),
                new Tuple<SweNet.PositionCenter, string>(SweNet.PositionCenter.Barycentric,"Barycentric"),
                new Tuple<SweNet.PositionCenter, string>(SweNet.PositionCenter.SiderealFagan,"Sidereal Fagan"),
                new Tuple<SweNet.PositionCenter, string>(SweNet.PositionCenter.SiderealLahiri,"Sidereal Lahiri"),
            });
            Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            HouseSystems = new List<Tuple<HouseSystem, String>>();
            foreach (var hs in Enum.GetValues(typeof(SweNet.HouseSystem)).Cast<HouseSystem>()) {
                HouseSystems.Add(new Tuple<HouseSystem, string>(hs, SweHouse.GetHouseSystemName(hs)));
            }
            HouseSystem = SweNet.HouseSystem.Placidus;
            Planets.AddRange(new Planet[] { 
                Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
                Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
                Planet.MeanNode, Planet.TrueNode,
                Planet.MeanApog, Planet.OscuApog, Planet.Earth
            });
            Planets.AddRange(new Planet[] { Planet.AsAsteroid(433), Planet.AsAsteroid(3045), Planet.AsAsteroid(7066) });
        }

        /// <summary>
        /// 
        /// </summary>
        public Models.InputCalculation CreateInputData() {
            var result = new Models.InputCalculation() {
                PositionCenter = this.PositionCenter,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Altitude = this.Altitude,
                HouseSystem = this.HouseSystem,
                EphemerisMode = this.EphemerisMode,
                JplFile = this.JplFile
            };
            result.DateUT = null; result.JulianDay = null; result.DateET = null;
            switch (InputDate.DateType) {
                case EphemerisDateType.EphemerisTime:
                    result.DateET = this.InputDate.Date;
                    break;
                case EphemerisDateType.JulianDay:
                    result.JulianDay = this.InputDate.JulianDay;
                    break;
                case EphemerisDateType.UniversalTime:
                    result.DateUT = this.InputDate.DateUTC;
                    break;
            }
            result.Planets.Clear();
            result.Planets.AddRange(Planets);
            return result;
        }

        /// <summary>
        /// List of ephemeris modde
        /// </summary>
        public List<Tuple<EphemerisMode, String>> ListEphemerisModes { get; private set; }

        /// <summary>
        /// Current ephemeris mode
        /// </summary>
        public EphemerisMode EphemerisMode {
            get { return _EphemerisMode; }
            set {
                _EphemerisMode = value;
                RaisePropertyChanged("EphemerisMode");
                RaisePropertyChanged("IsJplMode");
            }
        }
        private EphemerisMode _EphemerisMode;

        /// <summary>
        /// Indicate if the ephemeris mode is JPL
        /// </summary>
        public bool IsJplMode { get { return EphemerisMode == SweNet.EphemerisMode.JPL; } }

        /// <summary>
        /// List of JPL files versions
        /// </summary>
        public List<Tuple<String, String>> ListJplFiles { get; private set; }

        /// <summary>
        /// Current JPL file
        /// </summary>
        public String JplFile {
            get { return _JplFile; }
            set {
                _JplFile = value;
                RaisePropertyChanged("JplFile");
            }
        }
        private String _JplFile;

        /// <summary>
        /// Input date
        /// </summary>
        public InputDateViewModel InputDate { get; private set; }

        /// <summary>
        /// List of position centers
        /// </summary>
        public List<Tuple<PositionCenter,String>> ListPositionCenters { get; private set; }

        /// <summary>
        /// Current position center
        /// </summary>
        public PositionCenter PositionCenter {
            get { return _PositionCenter; }
            set { 
                _PositionCenter = value;
                RaisePropertyChanged("PositionCenter");
            }
        }
        private PositionCenter _PositionCenter;

        /// <summary>
        /// Latitude
        /// </summary>
        public Latitude Latitude {
            get { return _Latitude; }
            set {
                _Latitude = value;
                RaisePropertyChanged("Latitude");
            }
        }
        private Latitude _Latitude;

        /// <summary>
        /// Longitude
        /// </summary>
        public Longitude Longitude {
            get { return _Longitude; }
            set { 
                _Longitude = value;
                RaisePropertyChanged("Longitude");
            }
        }
        private Longitude _Longitude;

        /// <summary>
        /// Altitude
        /// </summary>
        public int Altitude {
            get { return _Altitude; }
            set {
                _Altitude = value;
                RaisePropertyChanged("Altitude");
            }
        }
        private int _Altitude;

        /// <summary>
        /// House system
        /// </summary>
        public HouseSystem HouseSystem {
            get { return _HouseSystem; }
            set {
                _HouseSystem = value;
                RaisePropertyChanged("HouseSystem");
            }
        }
        private HouseSystem _HouseSystem;

        /// <summary>
        /// House systems
        /// </summary>
        public List<Tuple<HouseSystem, String>> HouseSystems { get; private set; }

        /// <summary>
        /// Planets to calculate
        /// </summary>
        public List<Planet> Planets { get; private set; }

    }
}
