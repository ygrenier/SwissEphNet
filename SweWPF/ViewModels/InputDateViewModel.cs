using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// ViewModel for date selection
    /// </summary>
    public class InputDateViewModel : ViewModel
    {
        /// <summary>
        /// Create a new model
        /// </summary>
        public InputDateViewModel() {
            ListDateTypes = new List<Tuple<EphemerisDateType, string>>(new Tuple<EphemerisDateType, string>[]{
                new Tuple<EphemerisDateType, string>(EphemerisDateType.UniversalTime, "Universal Time"),
                new Tuple<EphemerisDateType, string>(EphemerisDateType.EphemerisTime, "Ephemeris Time"),
                new Tuple<EphemerisDateType, string>(EphemerisDateType.JulianDay, "Julian Day")
            });
            ListDayLights = new List<Tuple<DayLightMode, string>>(new Tuple<DayLightMode, string>[]{
                new Tuple<DayLightMode, string>(DayLightMode.DotNet, "From .Net time zone"),
                new Tuple<DayLightMode, string>(DayLightMode.Off, "Day light off"),
                new Tuple<DayLightMode, string>(DayLightMode.On, "Day light on")
            });
            TimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
            _TimeZone = TimeZoneInfo.Local;
            _DateType = EphemerisDateType.UniversalTime;
            _DayLight = DayLightMode.DotNet;
        }

        /// <summary>
        /// Date types
        /// </summary>
        public List<Tuple<EphemerisDateType,String>> ListDateTypes { get; private set; }

        /// <summary>
        /// Input date type
        /// </summary>
        public EphemerisDateType DateType {
            get { return _DateType; }
            set { 
                _DateType = value;
                RaisePropertyChanged("DateType");
            }
        }
        private EphemerisDateType _DateType;

        /// <summary>
        /// Date
        /// </summary>
        public DateUT Date {
            get { return _Date; }
            set { 
                _Date = value;
                RaisePropertyChanged("Date");
                RaisePropertyChanged("DateUTC");
            }
        }
        private DateUT _Date;

        /// <summary>
        /// Time zone
        /// </summary>
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
        private TimeZoneInfo _TimeZone;

        /// <summary>
        /// Daylight modes
        /// </summary>
        public List<Tuple<DayLightMode, String>> ListDayLights { get; private set; }

        /// <summary>
        /// Daylight mode
        /// </summary>
        public DayLightMode DayLight {
            get { return _DayLight; }
            set {
                _DayLight = value;
                RaisePropertyChanged("DayLight");
                RaisePropertyChanged("DateUTC");
            }
        }
        private DayLightMode _DayLight;

        /// <summary>
        /// Date UTC
        /// </summary>
        public DateUT DateUTC {
            get {
                TimeSpan daylight = TimeSpan.Zero;
                switch (DayLight) {
                    case DayLightMode.DotNet:
                        if (Date.Year > 0 && TimeZone.SupportsDaylightSavingTime && TimeZone.IsDaylightSavingTime(Date.ToDateTime()))
                            daylight = TimeSpan.FromHours(1);
                        break;
                    case DayLightMode.On:
                        daylight = TimeSpan.FromHours(1);
                        break;
                    case DayLightMode.Off:
                    default:
                        break;
                }
                return Date - (TimeZone.BaseUtcOffset + daylight);
            }
        }

        /// <summary>
        /// Julian day
        /// </summary>
        public JulianDay JulianDay {
            get { return _JulianDay; }
            set {
                _JulianDay = value;
                RaisePropertyChanged("JulianDay");
                RaisePropertyChanged("JdValue");
            }
        }
        private JulianDay _JulianDay;

        /// <summary>
        /// Value of JulianDay
        /// </summary>
        public double JdValue {
            get { return _JulianDay; }
            set { JulianDay =new SweNet.JulianDay(value); }
        }

        /// <summary>
        /// Liste of time zones
        /// </summary>
        public List<TimeZoneInfo> TimeZones { get; private set; }

    }

}
