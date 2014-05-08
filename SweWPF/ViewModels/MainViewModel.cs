using SweNet;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// Main viewmodel 
    /// </summary>
    public class MainViewModel : ViewModel
    {
        Dictionary<Type, object> _Services = new Dictionary<Type, object>();

        public MainViewModel() {
            Config = new ConfigViewModel();
            Input = new InputViewModel();
            Result = new CalculationResultViewModel();
            DoCalculationCommand = new RelayCommand(() => {
                DoCalculation();
            });
        }

        /// <summary>
        /// Do calculation
        /// </summary>
        public void DoCalculation() {
            Result.Apply(
                    GetService<Services.ICalcService>().Calculate(
                        Config.CreateConfigurationData(), 
                        Input.CreateInputData()
                    )
                );
        }

        /// <summary>
        /// Get a service
        /// </summary>
        public T GetService<T>() {
            object r = null;
            if (_Services.TryGetValue(typeof(T), out r))
                return (T)r;
            if (typeof(T) == typeof(Services.ICalcService)) {
                r = new Services.CalcService();
                _Services[typeof(T)] = r;
            }
            return (T)r;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public ConfigViewModel Config { get; private set; }

        /// <summary>
        /// Input informations
        /// </summary>
        public InputViewModel Input { get; private set; }

        /// <summary>
        /// Calculation result
        /// </summary>
        public CalculationResultViewModel Result { get; private set; }

        /// <summary>
        /// Command to calculation
        /// </summary>
        public RelayCommand DoCalculationCommand { get; private set; }

    }

}
