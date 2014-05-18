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
        IDependencyContainer _Container;

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

        #region Tidal constants

        // for delta t: intrinsic tidal acceleration in the mean motion of the moon,
        // not given in the parameters list of the ephemeris files but computed
        // by Chapront/Chapront-Touzé/Francou A&A 387 (2002), p. 705.

        public const double Tidal26 = (-26.0);
        public const double TidalDE200 = (-23.8946);
        /// <summary>
        /// was (-25.8) until V. 1.76.2
        /// </summary>
        public const double TidalDE403 = (-25.580);
        /// <summary>
        /// was (-25.8) until V. 1.76.2
        /// </summary>
        public const double TidalDE404 = (-25.580);
        /// <summary>
        /// was (-25.7376) until V. 1.76.2
        /// </summary>
        public const double TidalDE405 = (-25.826);
        /// <summary>
        /// was (-25.7376) until V. 1.76.2
        /// </summary>
        public const double TidalDE406 = (-25.826);
        /// <summary>
        /// JPL Interoffice Memorandum 14-mar-2008 on DE421 Lunar Orbit
        /// </summary>
        public const double TidalDE421 = (-25.85);
        /// <summary>
        /// JPL Interoffice Memorandum 9-jul-2013 on DE430 Lunar Orbit
        /// </summary>
        public const double TidalDE430 = (-25.82);
        /// <summary>
        /// Waiting for information
        /// </summary>
        public const double TidalDE431 = (-25.82);
        /// <summary>
        /// Default tidal
        /// </summary>
        public const double TidalDefault = TidalDE431;

        #endregion

        #endregion

        #region Ctor & Dest.

        /// <summary>
        /// Create a new SweDate
        /// </summary>
        public SweDate(IDependencyContainer container)
        {
            _Container = container;
            // Define default provider if not exists
            if (!container.CanResolve<Date.IDeltaTRecordProvider>())
                container.Register<Date.IDeltaTRecordProvider, Date.DeltaTRecordFile>(false);
            TidalAcceleration = TidalDefault;
        }

        #endregion

        #region Date informations

        /// <summary>
        /// Get the day of week of a Julian Day
        /// </summary>
        public static WeekDay DayOfWeek(double jd)
        {
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
        public static double GetHourValue(int hour, int minute, int second)
        {
            return (Double)hour + (minute / 60.0) + (second / 3600.0);
        }

        /// <summary>
        /// Get default calendar from a Julian Day
        /// </summary>
        /// <remarks>
        /// Gregorian calendar start at October 15, 1582
        /// </remarks>
        public static DateCalendar GetCalendar(double jd)
        {
            return jd >= GregorianFirstJD ? DateCalendar.Gregorian : DateCalendar.Julian;
        }

        /// <summary>
        /// Get default calendar from a date
        /// </summary>
        /// <remarks>
        /// Gregorian calendar start at October 15, 1582
        /// </remarks>
        public static DateCalendar GetCalendar(int year, int month, int day)
        {
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
            DateCalendar calendar)
        {
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
            double hour, DateCalendar calendar)
        {
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
            if (calendar == DateCalendar.Gregorian)
            {
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
            out int hour, out int minute, out int second)
        {
            double u0, u1, u2, u3, u4;
            u0 = jd + 32082.5;
            if (calendar == DateCalendar.Gregorian)
            {
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
        public static double DateToJulianDay(UniversalTime date, DateCalendar calendar)
        {
            return DateToJulianDay(date.Year, date.Month, date.Day, date.Hours, date.Minutes, date.Seconds, calendar);
        }

        /// <summary>
        /// Convert a DateUT to a Julian Day
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>The Julian Day</returns>
        public static double DateToJulianDay(UniversalTime date)
        {
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
        public static UniversalTime JulianDayToDate(double jd, DateCalendar calendar)
        {
            int year, month, day, hour, minute, second;
            JulianDayToDate(jd, calendar, out year, out month, out day, out hour, out minute, out second);
            return new UniversalTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Convert a Julian Day to a DateUT
        /// </summary>
        /// <param name="jd">The Julian Day to convert</param>
        /// <returns>The DateTime</returns>
        public static UniversalTime JulianDayToDate(double jd)
        {
            int year, month, day, hour, minute, second;
            JulianDayToDate(jd, GetCalendar(jd), out year, out month, out day, out hour, out minute, out second);
            return new UniversalTime(year, month, day, hour, minute, second);
        }

        #endregion

        #region Delta T

        bool _InitializedDT = false;

        #region Datas

        // Table for -1000 through 1600, from Morrison & Stephenson (2004).
        static short[] TableDT2 = new short[]{
            /*-1000  -900  -800  -700  -600  -500  -400  -300  -200  -100*/
              25400,23700,22000,21000,19040,17190,15530,14080,12790,11640,
            /*    0   100   200   300   400   500   600   700   800   900*/
              10580, 9600, 8640, 7680, 6700, 5710, 4740, 3810, 2960, 2200,
            /* 1000  1100  1200  1300  1400  1500  1600,                 */
               1570, 1090,  740,  490,  320,  200,  120,  
        };
        const int StartDT2 = (-1000);
        const int EndDT2 = 1600;
        const int StepDT2 = 100;

        // Table from 1620
        const int StartDT = 1620;
        static double[] TableDT = new double[] {
            /* 1620.0 thru 1659.0 */
            124.00, 119.00, 115.00, 110.00, 106.00, 102.00, 98.00, 95.00, 91.00, 88.00,
            85.00, 82.00, 79.00, 77.00, 74.00, 72.00, 70.00, 67.00, 65.00, 63.00,
            62.00, 60.00, 58.00, 57.00, 55.00, 54.00, 53.00, 51.00, 50.00, 49.00,
            48.00, 47.00, 46.00, 45.00, 44.00, 43.00, 42.00, 41.00, 40.00, 38.00,
            /* 1660.0 thru 1699.0 */
            37.00, 36.00, 35.00, 34.00, 33.00, 32.00, 31.00, 30.00, 28.00, 27.00,
            26.00, 25.00, 24.00, 23.00, 22.00, 21.00, 20.00, 19.00, 18.00, 17.00,
            16.00, 15.00, 14.00, 14.00, 13.00, 12.00, 12.00, 11.00, 11.00, 10.00,
            10.00, 10.00, 9.00, 9.00, 9.00, 9.00, 9.00, 9.00, 9.00, 9.00,
            /* 1700.0 thru 1739.0 */
            9.00, 9.00, 9.00, 9.00, 9.00, 9.00, 9.00, 9.00, 10.00, 10.00,
            10.00, 10.00, 10.00, 10.00, 10.00, 10.00, 10.00, 11.00, 11.00, 11.00,
            11.00, 11.00, 11.00, 11.00, 11.00, 11.00, 11.00, 11.00, 11.00, 11.00,
            11.00, 11.00, 11.00, 11.00, 12.00, 12.00, 12.00, 12.00, 12.00, 12.00,
            /* 1740.0 thru 1779.0 */
            12.00, 12.00, 12.00, 12.00, 13.00, 13.00, 13.00, 13.00, 13.00, 13.00,
            13.00, 14.00, 14.00, 14.00, 14.00, 14.00, 14.00, 14.00, 15.00, 15.00,
            15.00, 15.00, 15.00, 15.00, 15.00, 16.00, 16.00, 16.00, 16.00, 16.00,
            16.00, 16.00, 16.00, 16.00, 16.00, 17.00, 17.00, 17.00, 17.00, 17.00,
            /* 1780.0 thru 1799.0 */
            17.00, 17.00, 17.00, 17.00, 17.00, 17.00, 17.00, 17.00, 17.00, 17.00,
            17.00, 17.00, 16.00, 16.00, 16.00, 16.00, 15.00, 15.00, 14.00, 14.00,
            /* 1800.0 thru 1819.0 */
            13.70, 13.40, 13.10, 12.90, 12.70, 12.60, 12.50, 12.50, 12.50, 12.50,
            12.50, 12.50, 12.50, 12.50, 12.50, 12.50, 12.50, 12.40, 12.30, 12.20,
            /* 1820.0 thru 1859.0 */
            12.00, 11.70, 11.40, 11.10, 10.60, 10.20, 9.60, 9.10, 8.60, 8.00,
            7.50, 7.00, 6.60, 6.30, 6.00, 5.80, 5.70, 5.60, 5.60, 5.60,
            5.70, 5.80, 5.90, 6.10, 6.20, 6.30, 6.50, 6.60, 6.80, 6.90,
            7.10, 7.20, 7.30, 7.40, 7.50, 7.60, 7.70, 7.70, 7.80, 7.80,
            /* 1860.0 thru 1899.0 */
            7.88, 7.82, 7.54, 6.97, 6.40, 6.02, 5.41, 4.10, 2.92, 1.82,
            1.61, .10, -1.02, -1.28, -2.69, -3.24, -3.64, -4.54, -4.71, -5.11,
            -5.40, -5.42, -5.20, -5.46, -5.46, -5.79, -5.63, -5.64, -5.80, -5.66,
            -5.87, -6.01, -6.19, -6.64, -6.44, -6.47, -6.09, -5.76, -4.66, -3.74,
            /* 1900.0 thru 1939.0 */
            -2.72, -1.54, -.02, 1.24, 2.64, 3.86, 5.37, 6.14, 7.75, 9.13,
            10.46, 11.53, 13.36, 14.65, 16.01, 17.20, 18.24, 19.06, 20.25, 20.95,
            21.16, 22.25, 22.41, 23.03, 23.49, 23.62, 23.86, 24.49, 24.34, 24.08,
            24.02, 24.00, 23.87, 23.95, 23.86, 23.93, 23.73, 23.92, 23.96, 24.02,
            /* 1940.0 thru 1979.0 */
            24.33, 24.83, 25.30, 25.70, 26.24, 26.77, 27.28, 27.78, 28.25, 28.71,
            29.15, 29.57, 29.97, 30.36, 30.72, 31.07, 31.35, 31.68, 32.18, 32.68,
            33.15, 33.59, 34.00, 34.47, 35.03, 35.73, 36.54, 37.43, 38.29, 39.20,
            40.18, 41.17, 42.23, 43.37, 44.49, 45.48, 46.46, 47.52, 48.53, 49.59,
            /* 1980.0 thru 1999.0 */
            50.54, 51.38, 52.17, 52.96, 53.79, 54.34, 54.87, 55.32, 55.82, 56.30,
            56.86, 57.57, 58.31, 59.12, 59.98, 60.78, 61.63, 62.30, 62.97, 63.47,
            /* 2000.0 thru 2009.0 */
            63.83, 64.09, 64.30, 64.47, 64.57, 64.69, 64.85, 65.15, 65.46, 65.78,      
            /* 2010.0 thru 2013.0 */
            66.07, 66.32, 66.60, 66.907,
            /* Extrapolated values, 2014 - 2017 */
            67.267,67.90, 68.40, 69.00, 69.50, 70.00,
        };

        #endregion

        /// <summary>
        /// Read delta t values from external file.
        /// record structure: year(whitespace)delta_t in 0.01 sec.
        /// </summary>
        /// <returns>Table size</returns>
        int InitDeltaT()
        {
            if (_InitializedDT) return TableDT.Length;
            _InitializedDT = true;
            // Get data provider
            var provider = _Container.Resolve<Date.IDeltaTRecordProvider>();
            var records = provider.GetRecords()
                .Where(r => r.Year >= StartDT && r.Year < 2050) // We limit the tab to 2050
                .OrderBy(r => r.Year)
                .ToList()
                ;
            if (records.Any())
            {
                // Calculate the new tab size
                int lastYear = records[records.Count - 1].Year;
                int newSize = lastYear - StartDT + 1;
                // Resize the table
                if (newSize > TableDT.Length)
                {
                    var dt = TableDT;
                    TableDT = new double[newSize];
                    Array.Copy(dt, 0, TableDT, 0, dt.Length);
                }
                // Update the table
                foreach (var rec in records)
                {
                    int tabIndex = rec.Year - StartDT;
                    TableDT[tabIndex] = rec.Value;
                }
            }
            return TableDT.Length;
        }

        protected double DeltatLongtermMorrisonStephenson(double tjd)
        {
            double Ygreg = 2000.0 + (tjd - J2000) / 365.2425;
            double u = (Ygreg - 1820) / 100.0;
            return (-20 + 32 * u * u);
        }

        protected double DeltatMorrisonStephenson1600(double tjd)
        {
            double ans = 0, ans2, ans3;
            double p, B, dd;
            double tjd0;
            int iy;
            double Y = 2000.0 + (tjd - J2000) / 365.2425;
            // before -1000:
            // formula by Stephenson&Morrison (2004; p. 335) but adjusted to fit the 
            // starting point of table dt2.
            if (Y < StartDT2)
            {
                //B = (Y - LTERM_EQUATION_YSTART) * 0.01;
                //ans = -20 + LTERM_EQUATION_COEFF * B * B;
                ans = DeltatLongtermMorrisonStephenson(tjd);
                ans = AdjustForTidacc(ans, Y);
                // transition from formula to table over 100 years
                if (Y >= StartDT2 - 100)
                {
                    //* starting value of table dt2:
                    ans2 = AdjustForTidacc(TableDT2[0], StartDT2);
                    // value of formula at epoch TAB2_START
                    // B = (TAB2_START - LTERM_EQUATION_YSTART) * 0.01;
                    // ans3 = -20 + LTERM_EQUATION_COEFF * B * B;
                    tjd0 = (StartDT2 - 2000) * 365.2425 + J2000;
                    ans3 = DeltatLongtermMorrisonStephenson(tjd0);
                    ans3 = AdjustForTidacc(ans3, Y);
                    dd = ans3 - ans2;
                    B = (Y - (StartDT2 - 100)) * 0.01;
                    // fit to starting point of table dt2.
                    ans = ans - dd * B;
                }
            }
            // between -1000 and 1600: 
            // linear interpolation between values of table dt2 (Stephenson&Morrison 2004)
            if (Y >= StartDT2 && Y < EndDT2)
            {
                double Yjul = 2000 + (tjd - 2451557.5) / 365.25;
                p = Math.Floor(Yjul);
                iy = (int)((p - StartDT2) / StepDT2);
                dd = (Yjul - (StartDT2 + StepDT2 * iy)) / StepDT2;
                ans = TableDT2[iy] + (TableDT2[iy + 1] - TableDT2[iy]) * dd;
                // correction for tidal acceleration used by our ephemeris
                ans = AdjustForTidacc(ans, Y);
            }
            ans /= 86400.0;
            return ans;
        }

        protected double DeltatEspenakMeeus1620(double tjd)
        {
            double ans = 0;
            double Ygreg;
            double u;
            //* double Y = 2000.0 + (tjd - J2000)/365.25;
            Ygreg = 2000.0 + (tjd - J2000) / 365.2425;
            if (Ygreg < -500)
            {
                ans = DeltatLongtermMorrisonStephenson(tjd);
            }
            else if (Ygreg < 500)
            {
                u = Ygreg / 100.0;
                ans = (((((0.0090316521 * u + 0.022174192) * u - 0.1798452) * u - 5.952053) * u + 33.78311) * u - 1014.41) * u + 10583.6;
            }
            else if (Ygreg < 1600)
            {
                u = (Ygreg - 1000) / 100.0;
                ans = (((((0.0083572073 * u - 0.005050998) * u - 0.8503463) * u + 0.319781) * u + 71.23472) * u - 556.01) * u + 1574.2;
            }
                // TODO Verify dead code
            else if (Ygreg < 1700)
            {
                u = Ygreg - 1600;
                ans = 120 - 0.9808 * u - 0.01532 * u * u + u * u * u / 7129.0;
            }
            else if (Ygreg < 1800)
            {
                u = Ygreg - 1700;
                ans = (((-u / 1174000.0 + 0.00013336) * u - 0.0059285) * u + 0.1603) * u + 8.83;
            }
            else if (Ygreg < 1860)
            {
                u = Ygreg - 1800;
                ans = ((((((0.000000000875 * u - 0.0000001699) * u + 0.0000121272) * u - 0.00037436) * u + 0.0041116) * u + 0.0068612) * u - 0.332447) * u + 13.72;
            }
            else if (Ygreg < 1900)
            {
                u = Ygreg - 1860;
                ans = ((((u / 233174.0 - 0.0004473624) * u + 0.01680668) * u - 0.251754) * u + 0.5737) * u + 7.62;
            }
            else if (Ygreg < 1920)
            {
                u = Ygreg - 1900;
                ans = (((-0.000197 * u + 0.0061966) * u - 0.0598939) * u + 1.494119) * u - 2.79;
            }
            else if (Ygreg < 1941)
            {
                u = Ygreg - 1920;
                ans = 21.20 + 0.84493 * u - 0.076100 * u * u + 0.0020936 * u * u * u;
            }
            else if (Ygreg < 1961)
            {
                u = Ygreg - 1950;
                ans = 29.07 + 0.407 * u - u * u / 233.0 + u * u * u / 2547.0;
            }
            else if (Ygreg < 1986)
            {
                u = Ygreg - 1975;
                ans = 45.45 + 1.067 * u - u * u / 260.0 - u * u * u / 718.0;
            }
            else if (Ygreg < 2005)
            {
                u = Ygreg - 2000;
                ans = ((((0.00002373599 * u + 0.000651814) * u + 0.0017275) * u - 0.060374) * u + 0.3345) * u + 63.86;
            }
            ans = AdjustForTidacc(ans, Ygreg);
            ans /= 86400.0;
            return ans;
        }

        protected double DeltatAA(double tjd)
        {
            double ans = 0, ans2, ans3;
            double p, B, B2, Y, dd;
            double[] d = new double[6];
            int i, iy, k;
            int tabsiz = InitDeltaT();
            int tabend = StartDT + tabsiz - 1;
            // Y = 2000.0 + (tjd - J2000)/365.25;
            Y = 2000.0 + (tjd - J2000) / 365.2425;
            if (Y <= tabend)
            {
                // Index into the table.
                p = Math.Floor(Y);
                iy = (int)(p - StartDT);
                // Zeroth order estimate is value at start of year
                ans = TableDT[iy];
                k = iy + 1;
                if (k >= tabsiz)
                    goto done; // No data, can't go on
                // The fraction of tabulation interval
                p = Y - p;
                // First order interpolated value
                ans += p * (TableDT[k] - TableDT[iy]);
                if ((iy - 1 < 0) || (iy + 2 >= tabsiz))
                    goto done; // can't do second differences
                // Make table of first differences
                k = iy - 2;
                for (i = 0; i < 5; i++)
                {
                    if ((k < 0) || (k + 1 >= tabsiz))
                        d[i] = 0;
                    else
                        d[i] = TableDT[k + 1] - TableDT[k];
                    k += 1;
                }
                // Compute second differences
                for (i = 0; i < 4; i++)
                    d[i] = d[i + 1] - d[i];
                B = 0.25 * p * (p - 1.0);
                ans += B * (d[1] + d[2]);
                if (iy + 2 >= tabsiz)
                    goto done;
                // Compute third differences
                for (i = 0; i < 3; i++)
                    d[i] = d[i + 1] - d[i];
                B = 2.0 * B / 3.0;
                ans += (p - 0.5) * B * d[1];
                if ((iy - 2 < 0) || (iy + 3 > tabsiz))
                    goto done;
                // Compute fourth differences
                for (i = 0; i < 2; i++)
                    d[i] = d[i + 1] - d[i];
                B = 0.125 * B * (p + 1.0) * (p - 2.0);
                ans += B * (d[0] + d[1]);
            done:
                ans = AdjustForTidacc(ans, Y);
                return ans / 86400.0;
            }
            // today - : 
            // Formula Stephenson (1997; p. 507),
            // with modification to avoid jump at end of AA table,
            // similar to what Meeus 1998 had suggested.
            // Slow transition within 100 years.
            B = 0.01 * (Y - 1820);
            ans = -20 + 31 * B * B;
            // slow transition from tabulated values to Stephenson formula:
            if (Y <= tabend + 100)
            {
                B2 = 0.01 * (tabend - 1820);
                ans2 = -20 + 31 * B2 * B2;
                ans3 = TableDT[tabsiz - 1];
                dd = (ans2 - ans3);
                ans += dd * (Y - (tabend + 100)) * 0.01;
            }
            return ans / 86400.0;
        }

        /// <summary>
        /// Returns DeltaT (ET - UT) in days
        /// </summary>
        /// <param name="jd">Julian Day in UT</param>
        /// <returns></returns>
        public double DeltaT(double jd)
        {
            double asY = 2000.0 + (jd - J2000) / 365.25;
            double asYGreg = 2000.0 + (jd - J2000) / 365.2425;
            double ans = 0;
            // Before 1633 AD and using UseEspenakMeeus2006
            // Polynomials by Espenak & Meeus 2006, derived from Stephenson & Morrison 
            // 2004. 
            // Note, Espenak & Meeus use their formulae only from 2000 BC on.
            // However, they use the long-term formula of Morrison & Stephenson,
            // which can be used even for the remoter past.
            if (UseEspenakMeeus2006 && jd < 2317746.13090277789)
            {
                return DeltatEspenakMeeus1620(jd);
            }
            // If the macro ESPENAK_MEEUS_2006 is FALSE:
            // Before 1620, we follow Stephenson & Morrsion 2004. For the tabulated 
            // values 1000 BC through 1600 AD, we use linear interpolation.
            if (asY < StartDT)
            {
                if (asY < EndDT2)
                {
                    return DeltatMorrisonStephenson1600(jd);
                }
                else
                {
                    // between 1600 and 1620:
                    // linear interpolation between 
                    // end of table dt2 and start of table dt
                    var B = StartDT - EndDT2;
                    var iy = (EndDT2 - StartDT2) / StepDT2;
                    var dd = (asY - EndDT2) / B;
                    ans = TableDT2[iy] + dd * (TableDT[0] - TableDT2[iy]);
                    ans = AdjustForTidacc(ans, asYGreg);
                    return ans / 86400.0;
                }
            }
            else
            {
                // 1620 - today + a few years (tabend):
                // Besselian interpolation from tabulated values in table dt.
                // See AA page K11.
                return DeltatAA(jd);
            }
        }
        
        #endregion

        #region Tidal acceleration

        /// <summary>
        /// Astronomical Almanac table is corrected by adding the expression
        ///     -0.000091 (ndot + 26)(year-1955)^2  seconds
        /// to entries prior to 1955 (AA page K8), where ndot is the secular
        /// tidal term in the mean motion of the Moon.
        ///
        /// Entries after 1955 are referred to atomic time standards and
        /// are not affected by errors in Lunar or planetary theory.
        /// </summary>
        public double AdjustForTidacc(double ans, double Y)
        {
            double B;
            if (Y < 1955.0)
            {
                B = (Y - 1955.0);
                ans += -0.000091 * (TidalAcceleration + 26.0) * B * B;
            }
            return ans;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicate if DeltaT calculation use Espenak Meeus calculation
        /// </summary>
        public bool UseEspenakMeeus2006 { get; set; }

        /// <summary>
        /// Current tidal acceleration
        /// </summary>
        public double TidalAcceleration { get; private set; }

        #endregion

    }

}
