using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    public class ConfigViewModel : ViewModel
    {
        private String _EphemerisPath;

        /// <summary>
        /// 
        /// </summary>
        public ConfigViewModel() {
            _EphemerisPath = @".;C:\sweph\ephe";
        }

        /// <summary>
        /// 
        /// </summary>
        public Models.Configuration CreateConfigurationData() {
            var result = new Models.Configuration();
            result.SearchPaths.AddRange(EphemerisPath.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            return result;
        }

        /// <summary>
        /// Ephemeris path
        /// </summary>
        public String EphemerisPath {
            get { return _EphemerisPath; }
            set {
                _EphemerisPath = value;
                RaisePropertyChanged("EphemerisPath");
            }
        }

    }

}
