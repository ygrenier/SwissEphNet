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
        /// Create a new Julian Day from a DateTime
        /// </summary>
        /// <param name="date">Date source</param>
        /// <param name="calendar">Calendar source</param>
        public JulianDay(DateTime date, DateCalendar calendar)
            : this() {
            this.Calendar = calendar;
            this.Value = SweDate.DateTimeToJulianDay(date, calendar);
        }

        /// <summary>
        /// Returns the DateTime of this Julian Day
        /// </summary>
        public DateTime ToDateTime() {
            return SweDate.JulianDayToDateTime(Value, Calendar);
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
