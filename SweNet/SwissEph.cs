using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Swiss Ephemeris C conversion
    /// </summary>
    public partial class SwissEph : IDisposable
    {
        #region Ctors & Dest

        /// <summary>
        /// Create a new context
        /// </summary>
        public SwissEph() {
            pnoint2jpl = PNOINT2JPL.ToArray();
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

        /// <summary>
        /// Return current version
        /// </summary>
        public String Version() {
            String sdummy = null;
            return swe_version(ref sdummy);
        }

        #region Configuration

        /// <summary>
        /// Set sideral mode
        /// </summary>
        /// <param name="sid_mode"></param>
        /// <param name="t0"></param>
        /// <param name="ayan_t0"></param>
        public void SetSidMode(Int32 sid_mode, double t0, double ayan_t0) {
            swe_set_sid_mode(sid_mode, t0, ayan_t0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geopos"></param>
        public void SetTopo(GeoPosition geopos) {
            SetTopo(geopos.Longitude, geopos.Latitude, geopos.Altitude);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geolong"></param>
        /// <param name="geolat"></param>
        /// <param name="geoalt"></param>
        public void SetTopo(double geolong, double geolat, double geoalt) {
            swe_set_topo(geolong, geolat, geoalt);
        }

        /// <summary>
        /// Set the ephemeris path
        /// </summary>
        /// <param name="path"></param>
        public void SetEphePath(String path) {
            swe_set_ephe_path(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fname"></param>
        public void SetJplFile(String fname) {
            swe_set_jpl_file(fname);
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

        /// <summary>
        /// sidereal time, without eps and nut as parameters.
        /// tjd must be UT !!!
        /// for more informsation, see comment with swe_sidtime0()
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <returns></returns>
        public double SidTime(double tjd_ut) {
            return swe_sidtime(tjd_ut);
        }

        /// <summary>
        /// Equation of Time
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="E"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public int TimeEqu(double tjd_ut, ref double E, out string serr) {
            serr = null;
            return swe_time_equ(tjd_ut, out E, ref serr);
        }

        /// <summary>
        /// Magn [-]
        /// </summary>
        /// <param name="JDNDaysUTStart"></param>
        /// <param name="dgeo"></param>
        /// <param name="datm"></param>
        /// <param name="dobs"></param>
        /// <param name="ObjectNameIn"></param>
        /// <param name="TypeEvent"></param>
        /// <param name="helflag"></param>
        /// <param name="dret"></param>
        /// <param name="serr_ret"></param>
        /// <returns></returns>
        public Int32 HeliacalUt(double JDNDaysUTStart, double[] dgeo, double[] datm, double[] dobs, string ObjectNameIn, Int32 TypeEvent, Int32 helflag, double[] dret, out string serr_ret) {
            return swe_heliacal_ut(JDNDaysUTStart, dgeo, datm, dobs, ObjectNameIn, TypeEvent, helflag, dret, out serr_ret);
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

        #region Houses

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hsys"></param>
        /// <returns></returns>
        public String HouseName(char hsys) {
            return swe_house_name(hsys);
        }

        /// <summary>
        /// Computes the house position of a planet or another point,
        /// in degrees: 0 - 30 = 1st house, 30 - 60 = 2nd house, etc.
        /// </summary>
        /// <param name="armc"></param>
        /// <param name="geolat"></param>
        /// <param name="eps"></param>
        /// <param name="hsys"></param>
        /// <param name="xpin"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public double HousePos(double armc, double geolat, double eps, char hsys, double[] xpin, out string serr) {
            serr = null;
            return swe_house_pos(armc, geolat, eps, hsys, xpin, ref serr);
        }

        /// <summary>
        /// cusps are returned in double cusp[13], or cusp[37] with house system 'G'.
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="iflag"></param>
        /// <param name="geolat"></param>
        /// <param name="geolon"></param>
        /// <param name="hsys"></param>
        /// <param name="cusp"></param>
        /// <param name="ascmc"></param>
        /// <returns></returns>
        public int HousesEx(double tjd_ut, Int32 iflag, double geolat, double geolon, char hsys, CPointer<double> cusp, CPointer<double> ascmc) {
            return swe_houses_ex(tjd_ut, iflag, geolat, geolon, hsys, cusp, ascmc);
        }

        #endregion

        #region Calculation

        /// <summary>
        /// Reduce x modulo 360 degrees
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double DegNorm(double x) {
            return swe_degnorm(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public double DifDeg2n(double p1, double p2) {
            return swe_difdeg2n(p1, p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public double DifRad2n(double p1, double p2) {
            return swe_difrad2n(p1, p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x0"></param>
        /// <returns></returns>
        public double DegMidP(double x1, double x0) {
            return swe_deg_midp(x1, x0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x0"></param>
        /// <returns></returns>
        public double RadMidP(double x1, double x0) {
            return swe_rad_midp(x1, x0);
        }

        /// <summary>
        /// It checks whether a position for the same planet, the same t, and the
        /// same flag bits has already been computed. 
        /// If yes, this position is returned. Otherwise it is computed.
        /// </summary>
        /// <remarks>
        /// -> If the SEFLG_SPEED flag has been specified, the speed will be returned
        /// at offset 3 of position array x[]. Its precision is probably better 
        /// than 0.002"/day.
        /// -> If the SEFLG_SPEED3 flag has been specified, the speed will be computed
        /// from three positions. This speed is less accurate than SEFLG_SPEED,
        /// i.e. better than 0.1"/day. And it is much slower. It is used for 
        /// program tests only.
        /// -> If no speed flag has been specified, no speed will be returned.
        /// </remarks>
        /// <param name="tjd"></param>
        /// <param name="ipl"></param>
        /// <param name="iflag"></param>
        /// <param name="xx"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public int Calc(double tjd, int ipl, Int32 iflag, double[] xx, out string serr) {
            return swe_calc(tjd, ipl, iflag, xx, out serr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tjd"></param>
        /// <param name="ipl"></param>
        /// <param name="iflag"></param>
        /// <param name="xx"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public int CalcUt(double tjd, int ipl, Int32 iflag, double[] xx, out string serr) {
            return swe_calc_ut(tjd, ipl, iflag, xx, out serr);
        }

        /// <summary>
        /// get fixstar positions
        /// </summary>
        /// <param name="star">
        /// name of star or line number in star file 
        /// (start from 1, don't count comment).
        /// If no error occurs, the name of the star is returned
        /// in the format trad_name, nomeclat_name
        /// </param>
        /// <param name="tjd">absolute julian day</param>
        /// <param name="iflag">s. swecalc(); speed bit does not function</param>
        /// <param name="xx">pointer for returning the ecliptic coordinates</param>
        /// <param name="serr">error return string</param>
        /// <returns></returns>
        public int Fixstar(String star, double tjd, int iflag, double[] xx, out string serr) {
            serr = null;
            return swe_fixstar(star, tjd, iflag, xx, ref serr);
        }

        /// <summary>
        /// get fixstar magnitude
        /// </summary>
        /// <param name="star"></param>
        /// <param name="mag"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public int FixstarMag(ref string star, ref double mag, out string serr) {
            serr = null;
            return swe_fixstar_mag(ref star, ref mag, ref serr);
        }

        /// <summary>
        /// function calculates planetary phenomena
        /// </summary>
        /// <param name="tjd"></param>
        /// <param name="ipl"></param>
        /// <param name="iflag"></param>
        /// <param name="attr"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 Pheno(double tjd, Int32 ipl, Int32 iflag, double[] attr, out string serr) {
            serr = null;
            return swe_pheno(tjd, ipl, iflag, attr, ref serr);
        }

        /// <summary>
        /// the ayanamsa (precession in longitude) 
        /// according to Newcomb's definition: 360 -
        /// longitude of the vernal point of t referred to the
        /// ecliptic of t0.
        /// </summary>
        /// <param name="tjd"></param>
        /// <returns></returns>
        public double GetAyanamsa(double tjd) {
            return swe_get_ayanamsa(tjd);
        }

        /// <summary>
        /// Computes azimut and height, from either ecliptic or 
        /// equatorial coordinates
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="calc_flag"></param>
        /// <param name="geopos"></param>
        /// <param name="atpress"></param>
        /// <param name="attemp"></param>
        /// <param name="xin"></param>
        /// <param name="xaz"></param>
        public void Azalt(double tjd_ut,
              Int32 calc_flag,
              double[] geopos,
              double atpress,
              double attemp,
              double[] xin,
              double[] xaz) {
            swe_azalt(tjd_ut, calc_flag, geopos, atpress, attemp, xin, xaz);
        }

        /// <summary>
        /// function finds the gauquelin sector position of a planet or fixed star
        /// </summary>
        /// <param name="t_ut"></param>
        /// <param name="ipl"></param>
        /// <param name="starname"></param>
        /// <param name="iflag"></param>
        /// <param name="imeth"></param>
        /// <param name="geopos"></param>
        /// <param name="atpress"></param>
        /// <param name="attemp"></param>
        /// <param name="dgsect"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 GauquelinSector(double t_ut, Int32 ipl, string starname, Int32 iflag, Int32 imeth, double[] geopos, double atpress, double attemp, ref double dgsect, out string serr) {
            serr = null;
            return swe_gauquelin_sector(t_ut, ipl, starname, iflag, imeth, geopos, atpress, attemp, ref dgsect, ref serr);
        }

        /// <summary>
        /// Nodes and apsides of planets and moon
        /// </summary>
        /// <param name="tjd_et"></param>
        /// <param name="ipl"></param>
        /// <param name="iflag"></param>
        /// <param name="method"></param>
        /// <param name="xnasc"></param>
        /// <param name="xndsc"></param>
        /// <param name="xperi"></param>
        /// <param name="xaphe"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 NodAps(double tjd_et, Int32 ipl, Int32 iflag, Int32 method,
                              double[] xnasc, double[] xndsc, double[] xperi, double[] xaphe,
                              out string serr) {
            serr = null;
            return swe_nod_aps(tjd_et, ipl, iflag, method, xnasc, xndsc, xperi, xaphe, ref serr);
        }

        /// <summary>
        /// rise, set, and meridian transits of sun, moon, planets, and stars
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="ipl"></param>
        /// <param name="starname"></param>
        /// <param name="epheflag"></param>
        /// <param name="rsmi"></param>
        /// <param name="geopos"></param>
        /// <param name="atpress"></param>
        /// <param name="attemp"></param>
        /// <param name="tret"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 RiseTrans(double tjd_ut, Int32 ipl, string starname, Int32 epheflag, Int32 rsmi,
                       double[] geopos, double atpress, double attemp, ref double tret, out string serr) {
            serr = null;
            return swe_rise_trans(tjd_ut, ipl, starname, epheflag, rsmi, geopos, atpress, attemp, ref tret, ref serr);
        }

        /// <summary>
        /// Computes attributes of a lunar eclipse for given tjd and geopos
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="ifl"></param>
        /// <param name="geopos"></param>
        /// <param name="attr"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LunEclipseHow(double tjd_ut, Int32 ifl, double[] geopos, double[] attr, out string serr) {
            serr = null;
            return swe_lun_eclipse_how(tjd_ut, ifl, geopos, attr, ref serr);
        }

        /// <summary>
        /// When is the next lunar eclipse?
        /// </summary>
        /// <param name="tjd_start"></param>
        /// <param name="ifl"></param>
        /// <param name="ifltype"></param>
        /// <param name="tret"></param>
        /// <param name="backward"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LunEclipseWhen(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, out string serr) {
            serr = null;
            return swe_lun_eclipse_when(tjd_start, ifl, ifltype, tret, backward ? 1 : 0, ref serr);
        }

        /// <summary>
        /// When is the next lunar eclipse, observable at a geographic position?
        /// </summary>
        /// <param name="tjd_start"></param>
        /// <param name="ifl"></param>
        /// <param name="geopos"></param>
        /// <param name="tret"></param>
        /// <param name="attr"></param>
        /// <param name="backward"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LunEclipseWhenLoc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, out string serr) {
            serr = null;
            return swe_lun_eclipse_when_loc(tjd_start, ifl, geopos, tret, attr, backward ? 1 : 0, ref serr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="ipl"></param>
        /// <param name="starname"></param>
        /// <param name="ifl"></param>
        /// <param name="geopos"></param>
        /// <param name="attr"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LunOccultWhere(double tjd_ut, Int32 ipl, string starname, Int32 ifl, double[] geopos, double[] attr, out string serr) {
            serr = null;
            return swe_lun_occult_where(tjd_ut, ipl, starname, ifl, geopos, attr, ref serr);
        }

        /// <summary>
        /// When is the next solar eclipse at a given geographical position?
        /// </summary>
        /// <param name="tjd_start"></param>
        /// <param name="ipl"></param>
        /// <param name="starname"></param>
        /// <param name="ifl"></param>
        /// <param name="geopos"></param>
        /// <param name="tret"></param>
        /// <param name="attr"></param>
        /// <param name="backward"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LunOccultWhenLoc(double tjd_start, Int32 ipl, string starname, Int32 ifl,
             double[] geopos, double[] tret, double[] attr, bool backward, out string serr) {
            serr = null;
            return swe_lun_occult_when_loc(tjd_start, ipl, starname, ifl, geopos, tret, attr, backward ? 1 : 0, ref serr);
        }

        /// <summary>
        /// When is the next lunar occultation anywhere on earth?
        /// </summary>
        /// <param name="tjd_start"></param>
        /// <param name="ipl"></param>
        /// <param name="starname"></param>
        /// <param name="ifl"></param>
        /// <param name="ifltype"></param>
        /// <param name="tret"></param>
        /// <param name="backward"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LunOccultWhenGlob(double tjd_start, Int32 ipl, string starname, Int32 ifl, Int32 ifltype,
             double[] tret, bool backward, out string serr) {
            serr = null;
            return swe_lun_occult_when_glob(tjd_start, ipl, starname, ifl, ifltype, tret, backward ? 1 : 0, ref serr);
        }

        /// <summary>
        /// Computes geographic location and type of solar eclipse for a given tjd 
        /// </summary>
        /// <param name="tjd_ut"></param>
        /// <param name="ifl"></param>
        /// <param name="geopos"></param>
        /// <param name="attr"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 SolEclipseWhere(double tjd_ut, Int32 ifl, double[] geopos, double[] attr, out string serr) {
            serr = null;
            return swe_sol_eclipse_where(tjd_ut, ifl, geopos, attr, ref serr);
        }

        /// <summary>
        /// When is the next solar eclipse at a given geographical position?
        /// </summary>
        /// <param name="tjd_start"></param>
        /// <param name="ifl"></param>
        /// <param name="geopos"></param>
        /// <param name="tret"></param>
        /// <param name="attr"></param>
        /// <param name="backward"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 SolEclipseWhenLoc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, out string serr) {
            serr = null;
            return swe_sol_eclipse_when_loc(tjd_start, ifl, geopos, tret, attr, backward ? 1 : 0, ref serr);
        }

        /// <summary>
        /// When is the next solar eclipse anywhere on earth?
        /// </summary>
        /// <param name="tjd_start"></param>
        /// <param name="ifl"></param>
        /// <param name="ifltype"></param>
        /// <param name="tret"></param>
        /// <param name="backward"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 SolEclipseWhenGlob(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, out string serr) {
            serr = null;
            return swe_sol_eclipse_when_glob(tjd_start, ifl, ifltype, tret, backward ? 1 : 0, ref serr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tjd_lmt"></param>
        /// <param name="geolon"></param>
        /// <param name="tjd_lat"></param>
        /// <param name="serr"></param>
        /// <returns></returns>
        public Int32 LmtToLat(double tjd_lmt, double geolon, out double tjd_lat, out string serr) {
            return swe_lmt_to_lat(tjd_lmt, geolon, out tjd_lat, out serr);
        }

        /// <summary>
        /// function for splitting centiseconds
        /// </summary>
        /// <param name="ddeg"></param>
        /// <param name="roundflag"></param>
        /// <param name="ideg"></param>
        /// <param name="imin"></param>
        /// <param name="isec"></param>
        /// <param name="dsecfr"></param>
        /// <param name="isgn"></param>
        public void SplitDeg(double ddeg, Int32 roundflag, out Int32 ideg, out Int32 imin, out Int32 isec, out double dsecfr, out Int32 isgn) {
            swe_split_deg(ddeg, roundflag, out ideg, out imin, out isec, out dsecfr, out isgn);
        }

        /// <summary>
        /// conversion between ecliptical and equatorial polar coordinates
        /// </summary>
        /// <param name="xpo"></param>
        /// <param name="xpn"></param>
        /// <param name="eps"></param>
        public void CoTrans(double[] xpo, double[] xpn, double eps) {
            swe_cotrans(xpo, xpn, eps);
        }

        /// <summary>
        /// double to int32 with rounding, no overflow check
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Int32 D2l(double x) {
            return swe_d2l(x);
        }

        #endregion

    }

}
