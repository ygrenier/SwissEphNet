using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Represents a Julian Day in Universal Time
    /// </summary>
    public struct JulianDay
    {

        /// <summary>
        /// Create a new Julian Day from his value
        /// </summary>
        /// <param name="val">The Julian Day value</param>
        /// <param name="calendar">The calendar source</param>
        public JulianDay(double val, DateCalendar calendar = DateCalendar.Gregorian)
            : this() {
            this.Calendar = calendar;
            this.Value = val;
        }

        /// <summary>
        /// Create a new Julian Day from a DateUT
        /// </summary>
        /// <param name="date">Date source</param>
        /// <param name="calendar">Calendar source</param>
        public JulianDay(DateUT date, DateCalendar calendar)
            : this() {
            this.Calendar = calendar;
            this.Value = SweDate.DateToJulianDay(date, calendar);
        }

        /// <summary>
        /// Returns the DateUT of this Julian Day
        /// </summary>
        public DateUT ToDateTime() {
            return SweDate.JulianDayToDate(Value, Calendar);
        }

        /// <summary>
        /// Implicit cast between Julian Day and double
        /// </summary>
        public static implicit operator Double(JulianDay jd) {
            return jd.Value;
        }

        /// <summary>
        /// Calendar
        /// </summary>
        public DateCalendar Calendar { get; private set; }

        /// <summary>
        /// The absolute Julian Day value
        /// </summary>
        public double Value { get; set; }

    }

}
