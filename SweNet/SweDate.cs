using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Date management engine
    /// </summary>
    public class SweDate
    {

        #region Constants

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
        /// <param name="date">Date to convert</param>
        /// <param name="calendar">Calendar of conversion</param>
        /// <returns>The julian day value as Universal Time</returns>
        public static double DateTimeToJulianDay(DateTime date, DateCalendar calendar) {
            double jd;
            double u, u0, u1, u2;
            u = date.Year;
            if (date.Month < 3) u -= 1;
            u0 = u + 4712.0;
            u1 = date.Month + 1.0;
            if (u1 < 4) u1 += 12.0;
            jd = Math.Floor(u0 * 365.25)
               + Math.Floor(30.6 * u1 + 0.000001)
               + date.Day + date.GetHourValue() / 24.0 - 63.5;
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
        /// <param name="jd"></param>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public static DateTime JulianDayToDateTime(double jd, DateCalendar calendar) {
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
            var jmon = (int)(u4 - 1.0);
            if (jmon > 12) jmon -= 12;
            var jday = (int)(u2 - Math.Floor(365.25 * u3) - Math.Floor(30.6001 * u4));
            var jyear = (int)(u3 + Math.Floor((u4 - 2.0) / 12.0) - 4800);
            var jut = (jd - Math.Floor(jd + 0.5) + 0.5) * 24.0;
            var jhour = (int)jut;
            var jmin = (int)((jut * 60.0) % 60.0);
            var jsec = (int)((jut * 3600.0) % 60.0);
            return new DateTime(jyear, jmon, jday, jhour, jmin, jsec);
        }

    }
}
