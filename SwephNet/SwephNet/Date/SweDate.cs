using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Date management engine
    /// </summary>
    public class SweDate
    {

        #region Public constants

        /// <summary>
        /// 2000 January 1.5
        /// </summary>
        public const double J2000 = 2451545.0;

        /// <summary>
        /// 1950 January 0.923 
        /// </summary>
        public const double B1950 = 2433282.42345905;

        /// <summary>
        /// 1900 January 0.5
        /// </summary>
        public const double J1900 = 2415020.0;

        /// <summary>
        /// First Julian Day of the Gregorian calendar : October 15, 1582
        /// </summary>
        public const double GregorianFirstJD = 2299160.5;

        #endregion

        #region Date informations

        /// <summary>
        /// Get the day of week of a Julian Day
        /// </summary>
        public static WeekDay DayOfWeek(double jd) {
            return (WeekDay)((((int)Math.Floor(jd - 2433282 - 1.5) % 7) + 7) % 7);
        }

        #endregion

        #region Date conversions

        /// <summary>
        /// Get the hour decimal value
        /// </summary>
        /// <param name="hour">Hour</param>
        /// <param name="minute">Minute</param>
        /// <param name="second">Second</param>
        /// <returns>The hour in decimal valeu</returns>
        public static double GetHourValue(int hour, int minute, int second) {
            return (Double)hour + (minute / 60.0) + (second / 3600.0);
        }

        /// <summary>
        /// Get default calendar from a Julian Day
        /// </summary>
        /// <remarks>
        /// Gregorian calendar start at October 15, 1582
        /// </remarks>
        public static DateCalendar GetCalendar(double jd) {
            return jd >= GregorianFirstJD ? DateCalendar.Gregorian : DateCalendar.Julian;
        }

        /// <summary>
        /// Get default calendar from a date
        /// </summary>
        /// <remarks>
        /// Gregorian calendar start at October 15, 1582
        /// </remarks>
        public static DateCalendar GetCalendar(int year, int month, int day) {
            int date = (year * 10000) + (month * 100) + day;
            return date >= 15821115 ? DateCalendar.Gregorian : DateCalendar.Julian;
        }

        /// <summary>
        /// This function returns the absolute Julian day number (JD) 
        /// for a given date. 
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <param name="hour">Hour</param>
        /// <param name="minute">Minute</param>
        /// <param name="second">Second</param>
        /// <param name="calendar">Calendar of conversion</param>
        /// <returns>The julian day value as Universal Time</returns>
        public static double DateToJulianDay(
            int year, int month, int day,
            int hour, int minute, int second,
            DateCalendar calendar) {
            return DateToJulianDay(year, month, day, GetHourValue(hour, minute, second), calendar);
        }

        /// <summary>
        /// This function returns the absolute Julian day number (JD) 
        /// for a given date. 
        /// </summary>
        /// <remarks>
        /// <para>Base on swe_julday()</para>
        /// <para>
        /// The Julian day number is a system of numbering all days continously
        /// within the time range of known human history. It should be familiar
        /// to every astrological or astronomical programmer. The time variable
        /// in astronomical theories is usually expressed in Julian days or
        /// Julian centuries (36525 days per century) relative to some start day;
        /// the start day is called 'the epoch'.
        /// The Julian day number is a double representing the number of
        /// days since JD = 0.0 on 1 Jan -4712, 12:00 noon (in the Julian calendar).
        /// </para>
        /// <para>
        /// Midnight has always a JD with fraction .5, because traditionally
        /// the astronomical day started at noon. This was practical because
        /// then there was no change of date during a night at the telescope.
        /// From this comes also the fact the noon ephemerides were printed
        /// before midnight ephemerides were introduced early in the 20th century.
        /// </para>
        /// <para>
        /// NOTE: The Julian day number must not be confused with the Julian 
        /// calendar system.
        /// </para>
        /// <para>
        /// Be aware the we always use astronomical year numbering for the years
        /// before Christ, not the historical year numbering.
        /// Astronomical years are done with negative numbers, historical
        /// years with indicators BC or BCE (before common era).
        /// Year 0 (astronomical)  	= 1 BC
        /// year -1 (astronomical) 	= 2 BC
        /// etc.
        /// </para>
        /// <para>
        /// Original author: Marc Pottenger, Los Angeles.
        /// with bug fix for year &lt; -4711   15-aug-88 by Alois Treindl
        /// (The parameter sequence m,d,y still indicates the US origin,
        /// be careful because the similar function date_conversion() uses
        /// other parameter sequence and also Astrodienst relative juldate.)
        /// </para>
        /// <para>
        /// References: Oliver Montenbruck, Grundlagen der Ephemeridenrechnung,
        /// Verlag Sterne und Weltraum (1987), p.49 ff
        /// </para>
        /// </remarks>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <param name="hour">Hour in decimal</param>
        /// <param name="calendar">Calendar of conversion</param>
        /// <returns>The julian day value as Universal Time</returns>
        public static double DateToJulianDay(
            int year, int month, int day,
            double hour, DateCalendar calendar) {
            double jd;
            double u, u0, u1, u2;
            u = year;
            if (month < 3) u -= 1;
            u0 = u + 4712.0;
            u1 = month + 1.0;
            if (u1 < 4) u1 += 12.0;
            jd = Math.Floor(u0 * 365.25)
               + Math.Floor(30.6 * u1 + 0.000001)
               + day + hour / 24.0 - 63.5;
            if (calendar == DateCalendar.Gregorian) {
                u2 = Math.Floor(Math.Abs(u) / 100) - Math.Floor(Math.Abs(u) / 400);
                if (u < 0.0) u2 = -u2;
                jd = jd - u2 + 2;
                if ((u < 0.0) && (u / 100 == Math.Floor(u / 100)) && (u / 400 != Math.Floor(u / 400)))
                    jd -= 1;
            }
            return jd;
        }

        /// <summary>
        /// Convert a Julian Day to a DateTime
        /// </summary>
        /// <remarks>
        /// <para>Based on swe_rev_jul()</para>
        /// <para>
        /// swe_revjul() is the inverse function to swe_julday(), see the description there.
        /// </para>
        /// <para>
        /// Be aware the we use astronomical year numbering for the years
        /// before Christ, not the historical year numbering.
        /// Astronomical years are done with negative numbers, historical
        /// years with indicators BC or BCE (before common era).
        /// Year  0 (astronomical)  	= 1 BC historical year
        /// year -1 (astronomical) 	= 2 BC historical year
        /// year -234 (astronomical) 	= 235 BC historical year
        /// etc.
        /// </para>
        /// <para>
        /// Original author Mark Pottenger, Los Angeles.
        /// with bug fix for year < -4711 16-aug-88 Alois Treindl
        /// </para>
        /// </remarks>
        /// <param name="jd">The Julian Day</param>
        /// <param name="calendar">The calendar</param>
        /// <param name="year">Year result</param>
        /// <param name="month">Month result</param>
        /// <param name="day">Day result</param>
        /// <param name="hour">Hour result</param>
        /// <param name="minute">Minute result</param>
        /// <param name="second">Second result</param>
        /// <returns></returns>
        public static void JulianDayToDate(double jd, DateCalendar calendar,
            out int year, out int month, out int day,
            out int hour, out int minute, out int second) {
            double u0, u1, u2, u3, u4;
            u0 = jd + 32082.5;
            if (calendar == DateCalendar.Gregorian) {
                u1 = u0 + Math.Floor(u0 / 36525.0) - Math.Floor(u0 / 146100.0) - 38.0;
                if (jd >= 1830691.5) u1 += 1;
                u0 = u0 + Math.Floor(u1 / 36525.0) - Math.Floor(u1 / 146100.0) - 38.0;
            }
            u2 = Math.Floor(u0 + 123.0);
            u3 = Math.Floor((u2 - 122.2) / 365.25);
            u4 = Math.Floor((u2 - Math.Floor(365.25 * u3)) / 30.6001);
            month = (int)(u4 - 1.0);
            if (month > 12) month -= 12;
            day = (int)(u2 - Math.Floor(365.25 * u3) - Math.Floor(30.6001 * u4));
            year = (int)(u3 + Math.Floor((u4 - 2.0) / 12.0) - 4800);
            var jut = (jd - Math.Floor(jd + 0.5) + 0.5) * 24.0;
            jut += 0.5 / 3600.0;
            hour = (int)jut;
            minute = (int)Math.Floor(((jut * 60.0)) % 60);
            second = (int)Math.Floor(((jut * 3600.0)) % 60);
        }

        /// <summary>
        /// Convert a DateUT to a Julian Day
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="calendar">Calendar to use</param>
        /// <returns>The Julian Day</returns>
        public static double DateToJulianDay(UniversalTime date, DateCalendar calendar) {
            return DateToJulianDay(date.Year, date.Month, date.Day, date.Hours, date.Minutes, date.Seconds, calendar);
        }

        /// <summary>
        /// Convert a DateUT to a Julian Day
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>The Julian Day</returns>
        public static double DateToJulianDay(UniversalTime date) {
            return DateToJulianDay(
                date.Year, date.Month, date.Day,
                date.Hours, date.Minutes, date.Seconds,
                GetCalendar(date.Year, date.Month, date.Day)
                );
        }

        /// <summary>
        /// Convert a Julian Day to a DateUT
        /// </summary>
        /// <param name="jd">The Julian Day to convert</param>
        /// <param name="calendar">Calendar to use</param>
        /// <returns>The DateTime</returns>
        public static UniversalTime JulianDayToDate(double jd, DateCalendar calendar) {
            int year, month, day, hour, minute, second;
            JulianDayToDate(jd, calendar, out year, out month, out day, out hour, out minute, out second);
            return new UniversalTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Convert a Julian Day to a DateUT
        /// </summary>
        /// <param name="jd">The Julian Day to convert</param>
        /// <returns>The DateTime</returns>
        public static UniversalTime JulianDayToDate(double jd) {
            int year, month, day, hour, minute, second;
            JulianDayToDate(jd, GetCalendar(jd), out year, out month, out day, out hour, out minute, out second);
            return new UniversalTime(year, month, day, hour, minute, second);
        }

        #endregion

    }

}
