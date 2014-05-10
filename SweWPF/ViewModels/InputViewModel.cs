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
            InputDate = new InputDateViewModel();
            Planets = new List<Planet>();
            InputDate.Date = new DateUT(DateTime.Now);
            //InputDate.Date = new DateUT(1974, 8, 16, 0, 30, 0);
            Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            HouseSystems = new string[] { "Placidus", "Campanus", "Regiomontanus", "Koch", "Equal", "Vehlow equal", "Horizon", "B=Alcabitus" };
            HouseSystem = HouseSystems[0];
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
                Altitude = this.Altitude,
                HouseSystem = this.HouseSystem,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                TimeZone = this.InputDate.TimeZone
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


        public InputDateViewModel InputDate { get; private set; }

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
        public String HouseSystem {
            get { return _HouseSystem; }
            set {
                _HouseSystem = value;
                RaisePropertyChanged("HouseSystem");
            }
        }
        private String _HouseSystem;

        /// <summary>
        /// House systems
        /// </summary>
        public String[] HouseSystems { get; private set; }

        /// <summary>
        /// Planets to calculate
        /// </summary>
        public List<Planet> Planets { get; private set; }

    }
}
