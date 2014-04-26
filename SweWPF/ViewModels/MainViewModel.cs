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
            return new SweNet.SwissEph();
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

    }

}
