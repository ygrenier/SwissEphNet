using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Swiss Ephemeris context
    /// </summary>
    public partial class SwissEph : IDisposable
    {
        #region Ctors & Dest

        /// <summary>
        /// Create a new context
        /// </summary>
        public SwissEph() {
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing)
                swe_close();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        #endregion

        #region Dates

        /// <summary>
        /// This function returns the absolute Julian day number (JD)
        /// for a given calendar date.
        /// </summary>
        /// <param name="year">Year of the date.</param>
        /// <param name="month">Month of the date.</param>
        /// <param name="day">Day of the date.</param>
        /// <param name="hour">Hour as double with decimal fraction.</param>
        /// <param name="gregflag">
        /// If gregflag = SE_GREG_CAL (1), Gregorian calendar is assumed,
        /// if gregflag = SE_JUL_CAL (0),Julian calendar is assumed.
        /// </param>
        /// <returns>The Julian day number</returns>
        /// <remarks>
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
        /// NOTE: The Julian day number must not be confused with the Julian calendar system.
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
        ///             Verlag Sterne und Weltraum (1987), p.49 ff
        /// </para>
        /// <para>
        /// related functions: swe_revjul() reverse Julian day number: compute the
        ///                    calendar date from a given JD
        ///                    date_conversion() includes test for legal date values
        ///                    and notifies errors like 32 January.
        /// </para>
        /// </remarks>
        public double JulDay(int year, int month, int day, double hour, int gregflag) {
            return swe_julday(year, month, day, hour, gregflag);
        }

        /// <summary>
        /// swe_revjul() is the inverse function to swe_julday(), see the description
        /// there.
        /// Arguments are julian day number, calendar flag (0=julian, 1=gregorian)
        /// return values are the calendar day, month, year and the hour of
        /// the day with decimal fraction (0 .. 23.999999).
        /// </summary>
        /// <param name="jd">The Julian Date number</param>
        /// <param name="gregflag"></param>
        /// <param name="jyear"></param>
        /// <param name="jmon"></param>
        /// <param name="jday"></param>
        /// <param name="jut"></param>
        /// <remarks>
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
        public void RevJul(double jd, int gregflag,
                 ref int jyear, ref int jmon, ref int jday, ref double jut) {
            swe_revjul(jd, gregflag, ref jyear, ref jmon, ref jday, ref jut);
        }

        /// <summary>
        /// delta t from Julian day number
        /// </summary>
        /// <param name="tjd">The Julian Day number</param>
        /// <returns></returns>
        public double DeltaT(double tjd) {
            return swe_deltat(tjd);
        }

        #endregion

        #region Planets

        /// <summary>
        /// Get the planet name
        /// </summary>
        public string GetPlanetName(int ipl) {
            return swe_get_planet_name(ipl);
        }

        #endregion

    }

}
