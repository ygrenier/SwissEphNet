using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Date Universal Time
    /// </summary>
    public struct DateUT
    {
        private int _Year, _Month, _Day, _Hours, _Minutes, _Seconds;

        /// <summary>
        /// New date from components
        /// </summary>
        public DateUT(int year, int month, int day, int hours, int minutes, int seconds)
            : this() {
            var jd = SweDate.DateToJulianDay(year, month, day, hours, minutes, seconds, DateCalendar.Julian);
            SweDate.JulianDayToDate(jd, DateCalendar.Julian, out _Year, out _Month, out _Day, out _Hours, out _Minutes, out _Seconds);
        }

        /// <summary>
        /// New date from DateTime
        /// </summary>
        public DateUT(DateTime date)
            : this(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second) {
        }

        /// <summary>
        /// New date from DateTimeOffset
        /// </summary>
        /// <param name="date"></param>
        public DateUT(DateTimeOffset date)
            : this(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second) {
        }

        /// <summary>
        /// Convert to a DateTime
        /// </summary>
        public DateTime ToDateTime() {
            return new DateTime(Year, Month, Day, Hours, Minutes, Seconds);
        }

        /// <summary>
        /// Convert to a DateTimeOffset
        /// </summary>
        public DateTimeOffset ToDateTimeOffset() {
            return new DateTimeOffset(Year, Month, Day, Hours, Minutes, Seconds, TimeSpan.Zero);
        }

        /// <summary>
        /// Day
        /// </summary>
        public int Day { get { return _Day; } }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get { return _Month; } }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get { return _Year; } }

        /// <summary>
        /// Hours
        /// </summary>
        public int Hours { get { return _Hours; } }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get { return _Minutes; } }

        /// <summary>
        /// Seconds
        /// </summary>
        public int Seconds { get { return _Seconds; } }
    }

}
