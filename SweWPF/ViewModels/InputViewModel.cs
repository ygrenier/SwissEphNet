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
    public class InputViewModel : ChildViewModel
    {

        public InputViewModel() {
            Planets = new List<Planet>();
            TimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
            TimeZone = TimeZoneInfo.Local;
            //Date = new DateUT(DateTime.Now);
            Date = new DateUT(1974, 8, 16, 0, 30, 0);
            Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            HouseSystems = new string[] { "Placidus", "Campanus", "Regiomontanus", "Koch", "Equal", "Vehlow equal", "Horizon", "B=Alcabitus" };
            HouseSystem = HouseSystems[0];
            DoCalculationCommand = new RelayCommand(() => {
                MainModel.DoCalculation(this);
            });
            Planets.AddRange(new Planet[] { 
                Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
                Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
                Planet.MeanNode, Planet.TrueNode,
                Planet.MeanApog, Planet.OscuApog, Planet.Earth
            });
            Planets.AddRange(new Planet[] { 433, 3045, 7066 });
        }

        private TimeZoneInfo _TimeZone;

        public TimeZoneInfo TimeZone {
            get { return _TimeZone; }
            set {
                if (_TimeZone != value) {
                    _TimeZone = value;
                    RaisePropertyChanged("TimeZone");
                    RaisePropertyChanged("DateUTC");
                }
            }
        }

        private DateUT _Date;

        public DateUT Date {
            get { return _Date; }
            set {
                if (_Date != value) {
                    _Date = value;
                    RaisePropertyChanged("Date");
                    RaisePropertyChanged("DateUTC");
                }
            }
        }

        public DateUT DateUTC {
            get {
                TimeSpan daylight = TimeSpan.Zero;
                if (Date.Year > 0 && TimeZone.SupportsDaylightSavingTime && TimeZone.IsDaylightSavingTime(Date.ToDateTime()))
                    daylight = TimeSpan.FromHours(1);
                return Date - (TimeZone.BaseUtcOffset + daylight);
            }
        }

        /// <summary>
        /// Liste of time zones
        /// </summary>
        public List<TimeZoneInfo> TimeZones { get; private set; }

        private Latitude _Latitude;

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

        private Longitude _Longitude;

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

        private int _Altitude;

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

        private String _HouseSystem;

        /// <summary>
        /// House system
        /// </summary>
        public String HouseSystem {
            get { return _HouseSystem; }
            set {
                _HouseSystem = value;
                RaisePropertyChanged("HouseSystem");
            }
        }

        /// <summary>
        /// House systems
        /// </summary>
        public String[] HouseSystems { get; private set; }

        /// <summary>
        /// Command to calculation
        /// </summary>
        public RelayCommand DoCalculationCommand { get; private set; }

        /// <summary>
        /// Planets to calculate
        /// </summary>
        public List<Planet> Planets { get; private set; }
    }
}
