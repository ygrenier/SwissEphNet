using SweNet;
using System;
using System.Collections.Generic;
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
        private SweNet.SwissEph _Sweph;

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
        private SweNet.SwissEph CreateNewSweph() {
            var result = new SweNet.SwissEph();
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

            // Initialize engine
            Sweph.swe_set_topo(input.Longitude, input.Latitude, input.Altitude);

            // Dates and Times
            result.JulianDay = Sweph.JulianDay(input.DateUTC);
            result.DateUTC = Sweph.DateUT(result.JulianDay);
            result.EphemerisTime = Sweph.EphemerisTime(result.JulianDay);
            result.SideralTime = Sweph.swe_sidtime(result.JulianDay) + (input.Longitude / 15.0);
            if (result.SideralTime >= 24.0) result.SideralTime -= 24.0;
            if (result.SideralTime < 0.0) result.SideralTime += 24.0;
            //var armc = sidt * 15;

            // Calculation
            String serr = null;
            Double[] x=new double[24];
            var iflag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
            var iflgret = Sweph.swe_calc(result.EphemerisTime, SwissEph.SE_ECL_NUT, iflag, x, ref serr);
            result.TrueEclipticObliquity = x[0];
            result.MeanEclipticObliquity = x[1];
            result.NutationLongitude = x[2];
            result.NutationObliquity = x[3];

            // Planets

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
        public SweNet.SwissEph Sweph {
            get { return _Sweph ?? (_Sweph = CreateNewSweph()); }
        }

        /// <summary>
        /// Command to calculation
        /// </summary>
        public RelayCommand DoCalculationCommand { get; private set; }

    }

}
