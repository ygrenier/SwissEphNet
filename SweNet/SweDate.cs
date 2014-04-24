using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Date management engine
    /// </summary>
    internal class SweDate
    {

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
        public double DateTimeToJulianDay(DateTime date, DateCalendar calendar) {
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
        public DateTime JulianDayToDateTime(double jd, DateCalendar calendar) {
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

        /// <summary>
        /// Returns the Ephemeris Time of a DateTime
        /// </summary>
        /// <remarks>
        /// The Ephemeris Time (UT) is the Julian Day + DeltaT.
        /// </remarks>
        /// <param name="date">Date to convert</param>
        /// <param name="calendar">Calendar of conversion</param>
        /// <returns>The julian day value as Ephemeris Time</returns>
        public double DateTimeToEphemerisTime(DateTime date, DateCalendar calendar) {
            var jdut = DateTimeToJulianDay(date, calendar);
            return jdut + DeltaT(jdut);
        } 

        /// <summary>
        /// Returns DeltaT (ET - UT) in days
        /// </summary>
        /// <param name="jd">Julian Day in UT</param>
        /// <returns></returns>
        public Double DeltaT(double jd) {
            throw new NotImplementedException("SweDate.DeltaT");
//            double ans = 0;
//            double B, Y, Ygreg, dd;
//            int iy;
//            /* read additional values from swedelta.txt */
//            bool use_espenak_meeus = ESPENAK_MEEUS_2006;
//            Y = 2000.0 + (jd - SweConst.J2000) / 365.25;
//            Ygreg = 2000.0 + (jd - SweConst.J2000) / 365.2425;
//            /* Before 1633 AD, if the macro ESPENAK_MEEUS_2006 is TRUE: 
//             * Polynomials by Espenak & Meeus 2006, derived from Stephenson & Morrison 
//             * 2004. 
//             * Note, Espenak & Meeus use their formulae only from 2000 BC on.
//             * However, they use the long-term formula of Morrison & Stephenson,
//             * which can be used even for the remoter past.
//             */
//            if (use_espenak_meeus && jd < 2317746.13090277789) {
//                return deltat_espenak_meeus_1620(tjd);
//            }
//            /* If the macro ESPENAK_MEEUS_2006 is FALSE:
//             * Before 1620, we follow Stephenson & Morrsion 2004. For the tabulated 
//             * values 1000 BC through 1600 AD, we use linear interpolation.
//             */
//            if (Y < TABSTART) {
//                if (Y < TAB2_END) {
//                    return deltat_stephenson_morrison_1600(jd);
//                } else {
//                    /* between 1600 and 1620:
//                     * linear interpolation between 
//                     * end of table dt2 and start of table dt */
//                    if (Y >= TAB2_END) {
//                        B = TABSTART - TAB2_END;
//                        iy = (TAB2_END - TAB2_START) / TAB2_STEP;
//                        dd = (Y - TAB2_END) / B;
//                        /*ans = dt2[iy] + dd * (dt[0] / 100.0 - dt2[iy]);*/
//                        ans = dt2[iy] + dd * (dt[0] - dt2[iy]);
//                        ans = adjust_for_tidacc(ans, Ygreg);
//                        return ans / 86400.0;
//                    }
//                }
//            }
//            /* 1620 - today + a few years (tabend):
//             * Besselian interpolation from tabulated values in table dt.
//             * See AA page K11.
//             */
//            if (Y >= TABSTART) {
//                return deltat_aa(jd);
//            }
//#if TRACE
//            System.Diagnostics.Debug.WriteLine("DeltaT(): {0}\t{0}\t", jd, ans);
//#endif
//            return ans / 86400.0;

        }

    }
}
