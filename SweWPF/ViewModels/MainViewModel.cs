using SweNet;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// Main viewmodel 
    /// </summary>
    public class MainViewModel : ViewModel, IDisposable
    {
        private ChildViewModel _CurrentChild;
        private Sweph _Sweph;

        public MainViewModel() {
            DoCalculationCommand = new RelayCommand(() => {
                DoCalculation((InputViewModel)CurrentChild);
            }, () => CurrentChild is InputViewModel);
            NavigateTo(new InputViewModel());
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing && _Sweph != null) {
                _Sweph.Dispose();
                _Sweph = null;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Create new Sweph context with current configuration
        /// </summary>
        private Sweph CreateNewSweph() {
            var result = new Sweph();
            result.OnLoadFile += Sweph_OnLoadFile;
            return result;
        }

        void Sweph_OnLoadFile(object sender, LoadFileEventArgs e) {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwephData");
            var fName = System.IO.Path.Combine(path, e.FileName);
            if (System.IO.File.Exists(fName)) {
                e.File = new System.IO.FileStream(fName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            }
        }

        public void DoCalculation(InputViewModel input) {
            CalculationResultViewModel result = new CalculationResultViewModel();
            String star = String.Empty;
            char hsys = 'P';
            result.Planets.Clear();

            // Initialize engine
            Sweph.swe_set_topo(input.Longitude, input.Latitude, input.Altitude);

            // Dates and Times
            result.JulianDay = Sweph.JulianDay(input.DateUTC);
            result.DateUTC = Sweph.DateUT(result.JulianDay);
            result.EphemerisTime = Sweph.EphemerisTime(result.JulianDay);
            result.SideralTime = Sweph.swe_sidtime(result.JulianDay) + (input.Longitude / 15.0);
            if (result.SideralTime >= 24.0) result.SideralTime -= 24.0;
            if (result.SideralTime < 0.0) result.SideralTime += 24.0;

            // Calculation
            String serr = null;
            Double[] x=new double[24];
            var iflag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
            var iflgret = Sweph.swe_calc(result.EphemerisTime, Planet.EclipticNutation, iflag, x, ref serr);
            result.TrueEclipticObliquity = x[0];
            result.MeanEclipticObliquity = x[1];
            result.NutationLongitude = x[2];
            result.NutationObliquity = x[3];

            // Planets
            foreach (var planet in input.Planets) {
                if (planet == Planet.Earth) continue;   // Exclude Earth if geo or topo
                serr = null;
                var pi = new PlanetInfos() {
                    Planet = planet
                };
                result.Planets.Add(pi);
                // Ecliptic position
                if (planet == Planet.FixedStar) {
                    iflgret = Sweph.swe_fixstar(star, result.EphemerisTime, iflag, x, ref serr);
                    pi.PlanetName = star;
                } else {
                    iflgret = Sweph.swe_calc(result.EphemerisTime, planet, iflag, x, ref serr);
                    pi.PlanetName = Sweph.swe_get_planet_name(planet);
                    if (planet.IsAsteroid) {
                        pi.PlanetName = String.Format("#{0}", planet);
                    }
                }
                if (iflgret >= 0) {
                    pi.Longitude = x[0];
                    pi.Latitude = x[1];
                    pi.Distance = x[2];
                    pi.LongitudeSpeed = x[3];
                    pi.LatitudeSpeed = x[4];
                    pi.DistanceSpeed = x[5];
                    pi.HousePosition = Sweph.swe_house_pos(result.ARMC, input.Latitude, result.TrueEclipticObliquity, hsys, x, ref serr);
                    if (pi.HousePosition == 0)
                        iflgret = SwissEph.ERR;
                }
                if (iflgret < 0) {
                    if (!String.IsNullOrEmpty(serr)) {
                        pi.ErrorMessage = serr;
                    }
                } else if (!String.IsNullOrEmpty(serr) && String.IsNullOrEmpty(pi.WarnMessage))
                    pi.WarnMessage = serr;
            }

            NavigateTo(result);
        }

        /// <summary>
        /// Navigate to a child model
        /// </summary>
        /// <param name="model"></param>
        public void NavigateTo(ChildViewModel model) {
            if (model == null) throw new ArgumentNullException("model");
            if (model.MainModel != this)
                model.Start(this);
            CurrentChild = model;
            DoCalculationCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Current Child
        /// </summary>
        public ChildViewModel CurrentChild {
            get { return _CurrentChild; }
            private set {
                if (_CurrentChild != value) {
                    _CurrentChild = value;
                    RaisePropertyChanged("CurrentChild");
                }
            }
        }

        /// <summary>
        /// Sweph context
        /// </summary>
        public Sweph Sweph {
            get { return _Sweph ?? (_Sweph = CreateNewSweph()); }
        }

        /// <summary>
        /// Command to calculation
        /// </summary>
        public RelayCommand DoCalculationCommand { get; private set; }

    }

}
