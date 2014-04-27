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
            TimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
            TimeZone = TimeZoneInfo.Local;
            Date = new DateUT(DateTime.Now);
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
                return Date - TimeZone.BaseUtcOffset;
            }
        }

        /// <summary>
        /// Liste of time zones
        /// </summary>
        public List<TimeZoneInfo> TimeZones { get; private set; }

    }
}
