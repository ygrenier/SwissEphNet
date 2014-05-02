/*
   This is a port of the Swiss Ephemeris Free Edition, Version 2.00.00
   of Astrodienst AG, Switzerland from the original C Code to .Net. For
   copyright see the original copyright notices below and additional
   copyright notes in the file named LICENSE, or - if this file is not
   available - the copyright notes at http://www.astro.ch/swisseph/ and
   following.
   
   For any questions or comments regarding this port, you should
   ONLY contact me and not Astrodienst, as the Astrodienst AG is not involved
   in this port in any way.

   Yanos : ygrenier@ygrenier.com
*/

/************************************************************
  $Header: /home/dieter/sweph/RCS/swephexp.h,v 1.75 2009/04/08 07:19:08 dieter Exp $
  SWISSEPH: exported definitions and constants 

  This file represents the standard application interface (API)
  to the Swiss Ephemeris.

  A C programmer needs only to include this file, and link his code
  with the SwissEph library.

  The function calls are documented in the Programmer's documentation,
  which is online in HTML format.

  Structure of this file:
    Public API definitions
    Internal developer's definitions
    Public API functions.

  Authors: Dieter Koch and Alois Treindl, Astrodienst Zurich

************************************************************/
/* Copyright (C) 1997 - 2008 Astrodienst AG, Switzerland.  All rights reserved.

  License conditions
  ------------------

  This file is part of Swiss Ephemeris.

  Swiss Ephemeris is distributed with NO WARRANTY OF ANY KIND.  No author
  or distributor accepts any responsibility for the consequences of using it,
  or for whether it serves any particular purpose or works at all, unless he
  or she says so in writing.  

  Swiss Ephemeris is made available by its authors under a dual licensing
  system. The software developer, who uses any part of Swiss Ephemeris
  in his or her software, must choose between one of the two license models,
  which are
  a) GNU public license version 2 or later
  b) Swiss Ephemeris Professional License

  The choice must be made before the software developer distributes software
  containing parts of Swiss Ephemeris to others, and before any public
  service using the developed software is activated.

  If the developer choses the GNU GPL software license, he or she must fulfill
  the conditions of that license, which includes the obligation to place his
  or her whole software project under the GNU GPL or a compatible license.
  See http://www.gnu.org/licenses/old-licenses/gpl-2.0.html

  If the developer choses the Swiss Ephemeris Professional license,
  he must follow the instructions as found in http://www.astro.com/swisseph/ 
  and purchase the Swiss Ephemeris Professional Edition from Astrodienst
  and sign the corresponding license contract.

  The License grants you the right to use, copy, modify and redistribute
  Swiss Ephemeris, but only under certain conditions described in the License.
  Among other things, the License requires that the copyright notices and
  this notice be preserved on all copies.

  Authors of the Swiss Ephemeris: Dieter Koch and Alois Treindl

  The authors of Swiss Ephemeris have no control or influence over any of
  the derived works, i.e. over software or services created by other
  programmers which use Swiss Ephemeris functions.

  The names of the authors or of the copyright holder (Astrodienst) must not
  be used for promoting any software, product or service which uses or contains
  the Swiss Ephemeris. This copyright notice is the ONLY place where the
  names of the authors can legally appear, except in cases where they have
  given special permission in writing.

  The trademarks 'Swiss Ephemeris' and 'Swiss Ephemeris inside' may be used
  for promoting such software, products or services.
*/
namespace SwissEphNet
{
    using SwissEphNet.CPort;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// SwissEph export : Public part
    /// </summary>
    partial class SwissEph
    {

        /***********************************************************
         * definitions for use also by non-C programmers
         ***********************************************************/

        /* values for gregflag in swe_julday() and swe_revjul() */
        public const int SE_JUL_CAL = 0;
        public const int SE_GREG_CAL = 1;

        /*
         * planet numbers for the ipl parameter in swe_calc()
         */
        public const int SE_ECL_NUT = -1;

        public const int SE_SUN = 0;
        public const int SE_MOON = 1;
        public const int SE_MERCURY = 2;
        public const int SE_VENUS = 3;
        public const int SE_MARS = 4;
        public const int SE_JUPITER = 5;
        public const int SE_SATURN = 6;
        public const int SE_URANUS = 7;
        public const int SE_NEPTUNE = 8;
        public const int SE_PLUTO = 9;
        public const int SE_MEAN_NODE = 10;
        public const int SE_TRUE_NODE = 11;
        public const int SE_MEAN_APOG = 12;
        public const int SE_OSCU_APOG = 13;
        public const int SE_EARTH = 14;
        public const int SE_CHIRON = 15;
        public const int SE_PHOLUS = 16;
        public const int SE_CERES = 17;
        public const int SE_PALLAS = 18;
        public const int SE_JUNO = 19;
        public const int SE_VESTA = 20;
        public const int SE_INTP_APOG = 21;
        public const int SE_INTP_PERG = 22;

        public const int SE_NPLANETS = 23;

        public const int SE_AST_OFFSET = 10000;
        public const int SE_VARUNA = (SE_AST_OFFSET + 20000);

        public const int SE_FICT_OFFSET = 40;
        public const int SE_FICT_OFFSET_1 = 39;
        public const int SE_FICT_MAX = 999;
        public const int SE_NFICT_ELEM = 15;

        public const int SE_COMET_OFFSET = 1000;

        public const int SE_NALL_NAT_POINTS = (SE_NPLANETS + SE_NFICT_ELEM);

        /* Hamburger or Uranian "planets" */
        public const int SE_CUPIDO = 40;
        public const int SE_HADES = 41;
        public const int SE_ZEUS = 42;
        public const int SE_KRONOS = 43;
        public const int SE_APOLLON = 44;
        public const int SE_ADMETOS = 45;
        public const int SE_VULKANUS = 46;
        public const int SE_POSEIDON = 47;
        /* other fictitious bodies */
        public const int SE_ISIS = 48;
        public const int SE_NIBIRU = 49;
        public const int SE_HARRINGTON = 50;
        public const int SE_NEPTUNE_LEVERRIER = 51;
        public const int SE_NEPTUNE_ADAMS = 52;
        public const int SE_PLUTO_LOWELL = 53;
        public const int SE_PLUTO_PICKERING = 54;
        public const int SE_VULCAN = 55;
        public const int SE_WHITE_MOON = 56;
        public const int SE_PROSERPINA = 57;
        public const int SE_WALDEMATH = 58;

        public const int SE_FIXSTAR = -10;

        public const int SE_ASC = 0;
        public const int SE_MC = 1;
        public const int SE_ARMC = 2;
        public const int SE_VERTEX = 3;
        public const int SE_EQUASC = 4;	/* "equatorial ascendant" */
        public const int SE_COASC1 = 5;	/* "co-ascendant" (W. Koch) */
        public const int SE_COASC2 = 6;	/* "co-ascendant" (M. Munkasey) */
        public const int SE_POLASC = 7;	/* "polar ascendant" (M. Munkasey) */
        public const int SE_NASCMC = 8;

        ///*
        // * flag bits for parameter iflag in function swe_calc()
        // * The flag bits are defined in such a way that iflag = 0 delivers what one
        // * usually wants:
        // *    - the default ephemeris (SWISS EPHEMERIS) is used,
        // *    - apparent geocentric positions referring to the true equinox of date
        // *      are returned.
        // * If not only coordinates, but also speed values are required, use 
        // * flag = SEFLG_SPEED.
        // *
        // * The 'L' behind the number indicates that 32-bit integers (Long) are used.
        // */
        public const int SEFLG_JPLEPH = 1;       /* use JPL ephemeris */
        public const int SEFLG_SWIEPH = 2;       /* use SWISSEPH ephemeris */
        public const int SEFLG_MOSEPH = 4;       /* use Moshier ephemeris */

        public const int SEFLG_EPHMASK = (SEFLG_JPLEPH | SEFLG_SWIEPH | SEFLG_MOSEPH);

        public const int SEFLG_HELCTR = 8;      /* return heliocentric position */
        public const int SEFLG_TRUEPOS = 16;     /* return true positions, not apparent */
        public const int SEFLG_J2000 = 32;     /* no precession, i.e. give J2000 equinox */
        public const int SEFLG_NONUT = 64;     /* no nutation, i.e. mean equinox of date */
        public const int SEFLG_SPEED3 = 128;     /* speed from 3 positions (do not use it,
                                                    SEFLG_SPEED is faster and more precise.) */
        public const int SEFLG_SPEED = 256;     /* high precision speed  */
        public const int SEFLG_NOGDEFL = 512;     /* turn off gravitational deflection */
        public const int SEFLG_NOABERR = 1024;    /* turn off 'annual' aberration of light */
        public const int SEFLG_EQUATORIAL = (2 * 1024);    /* equatorial positions are wanted */
        public const int SEFLG_XYZ = (4 * 1024);    /* cartesian, not polar, coordinates */
        public const int SEFLG_RADIANS = (8 * 1024);    /* coordinates in radians, not degrees */
        public const int SEFLG_BARYCTR = (16 * 1024);   /* barycentric positions */
        public const int SEFLG_TOPOCTR = (32 * 1024);   /* topocentric positions */
        public const int SEFLG_SIDEREAL = (64 * 1024);   /* sidereal positions */
        public const int SEFLG_ICRS = (128 * 1024);   /* ICRS (DE406 reference frame) */
        public const int SEFLG_DPSIDEPS_1980 = (256 * 1024); /* reproduce JPL Horizons 
                                                                1962 - today to 0.002 arcsec. */
        public const int SEFLG_JPLHOR = SEFLG_DPSIDEPS_1980;
        public const int SEFLG_JPLHOR_APPROX = (512 * 1024);   /* approximate JPL Horizons 1962 - today */

        public const int SE_SIDBITS = 256;
        /* for projection onto ecliptic of t0 */
        public const int SE_SIDBIT_ECL_T0 = 256;
        /* for projection onto solar system plane */
        public const int SE_SIDBIT_SSY_PLANE = 512;

        /* sidereal modes (ayanamsas) */
        public const int SE_SIDM_FAGAN_BRADLEY = 0;
        public const int SE_SIDM_LAHIRI = 1;
        public const int SE_SIDM_DELUCE = 2;
        public const int SE_SIDM_RAMAN = 3;
        public const int SE_SIDM_USHASHASHI = 4;
        public const int SE_SIDM_KRISHNAMURTI = 5;
        public const int SE_SIDM_DJWHAL_KHUL = 6;
        public const int SE_SIDM_YUKTESHWAR = 7;
        public const int SE_SIDM_JN_BHASIN = 8;
        public const int SE_SIDM_BABYL_KUGLER1 = 9;
        public const int SE_SIDM_BABYL_KUGLER2 = 10;
        public const int SE_SIDM_BABYL_KUGLER3 = 11;
        public const int SE_SIDM_BABYL_HUBER = 12;
        public const int SE_SIDM_BABYL_ETPSC = 13;
        public const int SE_SIDM_ALDEBARAN_15TAU = 14;
        public const int SE_SIDM_HIPPARCHOS = 15;
        public const int SE_SIDM_SASSANIAN = 16;
        public const int SE_SIDM_GALCENT_0SAG = 17;
        public const int SE_SIDM_J2000 = 18;
        public const int SE_SIDM_J1900 = 19;
        public const int SE_SIDM_B1950 = 20;
        public const int SE_SIDM_SURYASIDDHANTA = 21;
        public const int SE_SIDM_SURYASIDDHANTA_MSUN = 22;
        public const int SE_SIDM_ARYABHATA = 23;
        public const int SE_SIDM_ARYABHATA_MSUN = 24;
        public const int SE_SIDM_SS_REVATI = 25;
        public const int SE_SIDM_SS_CITRA = 26;
        public const int SE_SIDM_TRUE_CITRA = 27;
        public const int SE_SIDM_TRUE_REVATI = 28;
        public const int SE_SIDM_USER = 255;

        public const int SE_NSIDM_PREDEF = 29;

        /* used for swe_nod_aps(): */
        public const int SE_NODBIT_MEAN = 1;   /* mean nodes/apsides */
        public const int SE_NODBIT_OSCU = 2;   /* osculating nodes/apsides */
        public const int SE_NODBIT_OSCU_BAR = 4;   /* same, but motion about solar system barycenter is considered */
        public const int SE_NODBIT_FOPOINT = 256;   /* focal point of orbit instead of aphelion */

        /* default ephemeris used when no ephemeris flagbit is set */
        public const int SEFLG_DEFAULTEPH = SEFLG_SWIEPH;

        public const int SE_MAX_STNAME = 256;	/* maximum size of fixstar name;
                                                 * the parameter star in swe_fixstar
                                                 * must allow twice this space for
                                                 * the returned star name.
                                                 */

        /* defines for eclipse computations */

        public const int SE_ECL_CENTRAL = 1;
        public const int SE_ECL_NONCENTRAL = 2;
        public const int SE_ECL_TOTAL = 4;
        public const int SE_ECL_ANNULAR = 8;
        public const int SE_ECL_PARTIAL = 16;
        public const int SE_ECL_ANNULAR_TOTAL = 32;
        public const int SE_ECL_PENUMBRAL = 64;
        public const int SE_ECL_ALLTYPES_SOLAR = (SE_ECL_CENTRAL | SE_ECL_NONCENTRAL | SE_ECL_TOTAL | SE_ECL_ANNULAR | SE_ECL_PARTIAL | SE_ECL_ANNULAR_TOTAL);
        public const int SE_ECL_ALLTYPES_LUNAR = (SE_ECL_TOTAL | SE_ECL_PARTIAL | SE_ECL_PENUMBRAL);
        public const int SE_ECL_VISIBLE = 128;
        public const int SE_ECL_MAX_VISIBLE = 256;
        public const int SE_ECL_1ST_VISIBLE = 512;	/* begin of partial eclipse */
        public const int SE_ECL_PARTBEG_VISIBLE = 512;	/* begin of partial eclipse */
        public const int SE_ECL_2ND_VISIBLE = 1024;	/* begin of total eclipse */
        public const int SE_ECL_TOTBEG_VISIBLE = 1024;	/* begin of total eclipse */
        public const int SE_ECL_3RD_VISIBLE = 2048;    /* end of total eclipse */
        public const int SE_ECL_TOTEND_VISIBLE = 2048;    /* end of total eclipse */
        public const int SE_ECL_4TH_VISIBLE = 4096;    /* end of partial eclipse */
        public const int SE_ECL_PARTEND_VISIBLE = 4096;    /* end of partial eclipse */
        public const int SE_ECL_PENUMBBEG_VISIBLE = 8192;    /* begin of penumbral eclipse */
        public const int SE_ECL_PENUMBEND_VISIBLE = 16384;   /* end of penumbral eclipse */
        public const int SE_ECL_OCC_BEG_DAYLIGHT = 8192;    /* occultation begins during the day */
        public const int SE_ECL_OCC_END_DAYLIGHT = 16384;   /* occultation ends during the day */
        public const int SE_ECL_ONE_TRY = (32 * 1024);
        /* check if the next conjunction of the moon with
         * a planet is an occultation; don't search further */

        /* for swe_rise_transit() */
        public const int SE_CALC_RISE = 1;
        public const int SE_CALC_SET = 2;
        public const int SE_CALC_MTRANSIT = 4;
        public const int SE_CALC_ITRANSIT = 8;
        public const int SE_BIT_DISC_CENTER = 256; /* to be or'ed to SE_CALC_RISE/SET,
                                                    * if rise or set of disc center is 
                                                    * required */
        public const int SE_BIT_DISC_BOTTOM = 8192; /* to be or'ed to SE_CALC_RISE/SET,
                                                     * if rise or set of lower limb of 
                                                     * disc is requried */
        public const int SE_BIT_NO_REFRACTION = 512; /* to be or'ed to SE_CALC_RISE/SET, 
                                                      * if refraction is to be ignored */
        public const int SE_BIT_CIVIL_TWILIGHT = 1024; /* to be or'ed to SE_CALC_RISE/SET */
        public const int SE_BIT_NAUTIC_TWILIGHT = 2048; /* to be or'ed to SE_CALC_RISE/SET */
        public const int SE_BIT_ASTRO_TWILIGHT = 4096; /* to be or'ed to SE_CALC_RISE/SET */
        public const int SE_BIT_FIXED_DISC_SIZE = (16 * 1024); /* or'ed to SE_CALC_RISE/SET:
                                                                * neglect the effect of distance on
                                                                * disc size */


        /* for swe_azalt() and swe_azalt_rev() */
        public const int SE_ECL2HOR = 0;
        public const int SE_EQU2HOR = 1;
        public const int SE_HOR2ECL = 0;
        public const int SE_HOR2EQU = 1;

        /* for swe_refrac() */
        public const int SE_TRUE_TO_APP = 0;
        public const int SE_APP_TO_TRUE = 1;

        /*
         * only used for experimenting with various JPL ephemeris files
         * which are available at Astrodienst's internal network
         */
        public const int SE_DE_NUMBER = 431;
        public const string SE_FNAME_DE200 = "de200.eph";
        public const string SE_FNAME_DE403 = "de403.eph";
        public const string SE_FNAME_DE404 = "de404.eph";
        public const string SE_FNAME_DE405 = "de405.eph";
        public const string SE_FNAME_DE406 = "de406.eph";
        public const string SE_FNAME_DE431 = "de431.eph";
        public const string SE_FNAME_DFT = SE_FNAME_DE431;
        public const string SE_FNAME_DFT2 = SE_FNAME_DE406;
        public const string SE_STARFILE_OLD = "fixstars.cat";
        public const string SE_STARFILE = "sefstars.txt";
        public const string SE_ASTNAMFILE = "seasnam.txt";
        public const string SE_FICTFILE = "seorbel.txt";

        /*
         * ephemeris path
         * this defines where ephemeris files are expected if the function
         * swe_set_ephe_path() is not called by the application.
         * Normally, every application should make this call to define its
         * own place for the ephemeris files.
         */
        /// <summary>
        /// SweNet : We create a pseudo constant for detect ephemeris path when loading
        /// </summary>
        public const String SE_EPHE_PATH = "[ephe]";


        /* defines for function swe_split_deg() (in swephlib.c) */
        public const int SE_SPLIT_DEG_ROUND_SEC = 1;
        public const int SE_SPLIT_DEG_ROUND_MIN = 2;
        public const int SE_SPLIT_DEG_ROUND_DEG = 4;
        public const int SE_SPLIT_DEG_ZODIACAL = 8;
        public const int SE_SPLIT_DEG_KEEP_SIGN = 16;	/* don't round to next sign, 
                                                         * e.g. 29.9999999 will be rounded
                                                         * to 29d59'59" (or 29d59' or 29d) */
        public const int SE_SPLIT_DEG_KEEP_DEG = 32;	/* don't round to next degree
                                                         * e.g. 13.9999999 will be rounded
                                                         * to 13d59'59" (or 13d59' or 13d) */

        /* for heliacal functions */
        public const int SE_HELIACAL_RISING = 1;
        public const int SE_HELIACAL_SETTING = 2;
        public const int SE_MORNING_FIRST = SE_HELIACAL_RISING;
        public const int SE_EVENING_LAST = SE_HELIACAL_SETTING;
        public const int SE_EVENING_FIRST = 3;
        public const int SE_MORNING_LAST = 4;
        public const int SE_ACRONYCHAL_RISING = 5;  /* still not implemented */
        public const int SE_ACRONYCHAL_SETTING = 6;  /* still not implemented */
        public const int SE_COSMICAL_SETTING = SE_ACRONYCHAL_SETTING;

        public const int SE_HELFLAG_LONG_SEARCH = 128;
        public const int SE_HELFLAG_HIGH_PRECISION = 256;
        public const int SE_HELFLAG_OPTICAL_PARAMS = 512;
        public const int SE_HELFLAG_NO_DETAILS = 1024;
        public const int SE_HELFLAG_SEARCH_1_PERIOD = (1 << 11);  /*  2048 */
        public const int SE_HELFLAG_VISLIM_DARK = (1 << 12);  /*  4096 */
        public const int SE_HELFLAG_VISLIM_NOMOON = (1 << 13);  /*  8192 */
        public const int SE_HELFLAG_VISLIM_PHOTOPIC = (1 << 14);  /* 16384 */
        public const int SE_HELFLAG_AV = (1 << 15);  /* 32768 */
        public const int SE_HELFLAG_AVKIND_VR = (1 << 15);  /* 32768 */
        public const int SE_HELFLAG_AVKIND_PTO = (1 << 16);
        public const int SE_HELFLAG_AVKIND_MIN7 = (1 << 17);
        public const int SE_HELFLAG_AVKIND_MIN9 = (1 << 18);
        public const int SE_HELFLAG_AVKIND = (SE_HELFLAG_AVKIND_VR | SE_HELFLAG_AVKIND_PTO | SE_HELFLAG_AVKIND_MIN7 | SE_HELFLAG_AVKIND_MIN9);
        public const double TJD_INVALID = 99999999.0;
        public const bool SIMULATE_VICTORVB = true;

        public const int SE_HELIACAL_LONG_SEARCH = 128;
        public const int SE_HELIACAL_HIGH_PRECISION = 256;
        public const int SE_HELIACAL_OPTICAL_PARAMS = 512;
        public const int SE_HELIACAL_NO_DETAILS = 1024;
        public const int SE_HELIACAL_SEARCH_1_PERIOD = (1 << 11);  /*  2048 */
        public const int SE_HELIACAL_VISLIM_DARK = (1 << 12);  /*  4096 */
        public const int SE_HELIACAL_VISLIM_NOMOON = (1 << 13);  /*  8192 */
        public const int SE_HELIACAL_VISLIM_PHOTOPIC = (1 << 14);  /* 16384 */
        public const int SE_HELIACAL_AVKIND_VR = (1 << 15);  /* 32768 */
        public const int SE_HELIACAL_AVKIND_PTO = (1 << 16);
        public const int SE_HELIACAL_AVKIND_MIN7 = (1 << 17);
        public const int SE_HELIACAL_AVKIND_MIN9 = (1 << 18);
        public const int SE_HELIACAL_AVKIND = (SE_HELFLAG_AVKIND_VR | SE_HELFLAG_AVKIND_PTO | SE_HELFLAG_AVKIND_MIN7 | SE_HELFLAG_AVKIND_MIN9);

        public const int SE_PHOTOPIC_FLAG = 0;
        public const int SE_SCOTOPIC_FLAG = 1;
        public const int SE_MIXEDOPIC_FLAG = 2;

        /// <summary>
        /// 2000 January 1.5
        /// </summary>
        public const double J2000 = 2451545.0;

        ///***********************************************************
        // * exported functions
        // ***********************************************************/

        public Int32 swe_heliacal_ut(double tjdstart_ut, double[] geopos, double[] datm, double[] dobs, string ObjectName, 
            Int32 TypeEvent, Int32 iflag, double[] dret, ref string serr) {
            return SweHel.swe_heliacal_ut(tjdstart_ut, geopos, datm, dobs, ObjectName, TypeEvent, iflag, dret, ref serr);
        }

        public Int32 swe_heliacal_pheno_ut(double tjd_ut, double[] geopos, double[] datm, double[] dobs, string ObjectName,
            Int32 TypeEvent, Int32 helflag, double[] darr, ref string serr) {
            return SweHel.swe_heliacal_pheno_ut(tjd_ut, geopos, datm, dobs, ObjectName, TypeEvent, helflag, darr, ref serr);
        }
        public Int32 swe_vis_limit_mag(double tjdut, double[] geopos, double[] datm, double[] dobs, string ObjectName, 
            Int32 helflag, double[] dret, ref string serr) {
            return SweHel.swe_vis_limit_mag(tjdut, geopos, datm, dobs, ObjectName, helflag, dret, ref serr);
        }
        /* the following are secret, for Victor Reijs' */
        public Int32 swe_heliacal_angle(double tjdut, double[] dgeo, double[] datm, double[] dobs, Int32 helflag, double mag,
            double azi_obj, double azi_sun, double azi_moon, double alt_moon, double[] dret, ref string serr) {
            return SweHel.swe_heliacal_angle(tjdut, dgeo, datm, dobs, helflag, mag, azi_obj, azi_sun, azi_moon, alt_moon, dret, ref serr);
        }
        public Int32 swe_topo_arcus_visionis(double tjdut, double[] dgeo, double[] datm, double[] dobs, Int32 helflag, double mag,
            double azi_obj, double alt_obj, double azi_sun, double azi_moon, double alt_moon, ref double dret, ref string serr) {
            return SweHel.swe_topo_arcus_visionis(tjdut, dgeo, datm, dobs, helflag, mag, azi_obj, alt_obj, azi_sun, azi_moon, alt_moon, ref dret, ref serr);
        }

        ///**************************** 
        // * exports from sweph.c 
        // ****************************/

        public string swe_version() { return Sweph.swe_version(); }
        
        /// <summary>
        /// planets, moon, nodes etc. 
        /// </summary>
        public Int32 swe_calc(double tjd, int ipl, Int32 iflag, double[] xx, ref string serr) {
            return Sweph.swe_calc(tjd, ipl, iflag, xx, ref serr);
        }

        public Int32 swe_calc_ut(double tjd_ut, Int32 ipl, Int32 iflag, double[] xx, ref string serr) {
            return Sweph.swe_calc(tjd_ut, ipl, iflag, xx, ref serr);
        }

        /// <summary>
        /// fixed stars
        /// </summary>
        public Int32 swe_fixstar(string star, double tjd, Int32 iflag, double[] xx, ref string serr) {
            return Sweph.swe_fixstar(star, tjd, iflag, xx, ref serr);
        }
        public Int32 swe_fixstar_ut(string star, double tjd_ut, Int32 iflag, double[] xx, ref string serr) {
            return Sweph.swe_fixstar_ut(star, tjd_ut, iflag, xx, ref serr);
        }
        public Int32 swe_fixstar_mag(ref string star, ref double mag, ref string serr) {
            return Sweph.swe_fixstar_mag(ref star, ref mag, ref serr);
        }

        /// <summary>
        /// Close Swiss Ephemeris
        /// </summary>
        public void swe_close() { Sweph.swe_close(); }

        /// <summary>
        /// set directory path of ephemeris files
        /// </summary>
        public void swe_set_ephe_path(String path) { Sweph.swe_set_ephe_path(path); }


        /// <summary>
        /// set file name of JPL file
        /// </summary>
        public void swe_set_jpl_file(string fname) { Sweph.swe_set_jpl_file(fname); }


        /// <summary>
        /// get planet name
        /// </summary>
        public string swe_get_planet_name(int ipl) { return Sweph.swe_get_planet_name(ipl); }

        /// <summary>
        /// set geographic position of observer
        /// </summary>
        public void swe_set_topo(double geolon, double geolat, double height) { Sweph.swe_set_topo(geolon, geolat, height); }


        /// <summary>
        /// set sidereal mode
        /// </summary>
        public void swe_set_sid_mode(Int32 sid_mode, double t0, double ayan_t0) { Sweph.swe_set_sid_mode(sid_mode, t0, ayan_t0); }

        /// <summary>
        /// get ayanamsa 
        /// </summary>
        public double swe_get_ayanamsa(double tjd_et) { return Sweph.swe_get_ayanamsa(tjd_et); }

        public double swe_get_ayanamsa_ut(double tjd_ut) { return Sweph.swe_get_ayanamsa_ut(tjd_ut); }

        public string swe_get_ayanamsa_name(Int32 isidmode) { return Sweph.swe_get_ayanamsa_name(isidmode); }


        ///**************************** 
        // * exports from swedate.c 
        // ****************************/

        public int swe_date_conversion(
                int y, int m, int d,         /* year, month, day */
                double utime,   /* universal time in hours (decimal) */
                char c,         /* calendar g[regorian]|j[ulian] */
                ref double tjd) {
            return SweDate.swe_date_conversion(y, m, d, utime, c, ref tjd);
        }

        public double swe_julday(int year, int mon, int mday, double hour, int gregflag) {
            return SweDate.swe_julday(year, mon, mday, hour, gregflag);
        }

        public void swe_revjul(double jd, int gregflag, ref int year, ref int mon, ref int mday, ref double hour) {
            SweDate.swe_revjul(jd, gregflag, ref year, ref mon, ref mday, ref hour);
        }

        //public Int32 swe_utc_to_jd(Int32 iyear, Int32 imonth, Int32 iday, 
        //        Int32 ihour, Int32 imin, double dsec,
        //        Int32 gregflag, ref double dret, ref string serr) {

        //}

        //ext_def(void) swe_jdet_to_utc(
        //        double tjd_et, int32 gregflag, 
        //    int32 *iyear, int32 *imonth, int32 *iday, 
        //    int32 *ihour, int32 *imin, double *dsec);

        //ext_def(void) swe_jdut1_to_utc(
        //        double tjd_ut, int32 gregflag, 
        //    int32 *iyear, int32 *imonth, int32 *iday, 
        //    int32 *ihour, int32 *imin, double *dsec);

        //ext_def(void) swe_utc_time_zone(
        //        int32 iyear, int32 imonth, int32 iday,
        //    int32 ihour, int32 imin, double dsec,
        //    double d_timezone,
        //    int32 *iyear_out, int32 *imonth_out, int32 *iday_out,
        //    int32 *ihour_out, int32 *imin_out, double *dsec_out);

        ///**************************** 
        // * exports from swehouse.c 
        // ****************************/

        public int swe_houses(double tjd_ut, double geolat, double geolon, char hsys, double[] cusps, double[] ascmc) {
            return SweHouse.swe_houses(tjd_ut, geolat, geolon, hsys, cusps, ascmc);
        }

        public int swe_houses_ex(double tjd_ut, Int32 iflag, double geolat, double geolon, char hsys, CPointer<double> hcusps, CPointer<double> ascmc) {
            return SweHouse.swe_houses_ex(tjd_ut, iflag, geolat, geolon, hsys, hcusps, ascmc);
        }

        public int swe_houses_armc(double armc, double geolat, double eps, char hsys, double[] cusps, double[] ascmc) {
            return SweHouse.swe_houses_armc(armc, geolat, eps, hsys, cusps, ascmc);
        }

        public double swe_house_pos(double armc, double geolon, double eps, char hsys, double[] xpin, ref string serr) { 
            return SweHouse.swe_house_pos(armc, geolon, eps, hsys, xpin, ref serr);
        }

        public string swe_house_name(char hsys) { return SweHouse.swe_house_name(hsys); }

        ///**************************** 
        // * exports from swecl.c 
        // ****************************/

        public Int32 swe_gauquelin_sector(double t_ut, Int32 ipl, String starname, Int32 iflag, Int32 imeth, double[] geopos,
            double atpress, double attemp, ref double dgsect, ref string serr) {
            return SweCL.swe_gauquelin_sector(t_ut, ipl, starname, iflag, imeth, geopos, atpress, attemp, ref dgsect, ref serr);
        }

        /// <summary>
        /// computes geographic location and attributes of solar
        /// eclipse at a given tjd
        /// </summary>
        public Int32 swe_sol_eclipse_where(double tjd, Int32 ifl, double[] geopos, double[] attr, ref string serr) {
            return SweCL.swe_sol_eclipse_where(tjd, ifl, geopos, attr, ref serr);
        }

        public Int32 swe_lun_occult_where(double tjd, Int32 ipl, string starname, Int32 ifl, double[] geopos, double[] attr, ref string serr) {
            return SweCL.swe_lun_occult_where(tjd, ipl, starname, ifl, geopos, attr, ref serr);
        }

        /// <summary>
        /// computes attributes of a solar eclipse for given tjd, geolon, geolat
        /// </summary>
        public Int32 swe_sol_eclipse_how(double tjd, Int32 ifl, double[] geopos, double[] attr, ref string serr) {
            return SweCL.swe_sol_eclipse_how(tjd, ifl, geopos, attr, ref serr) ;
        }
        
        /// <summary>
        /// finds time of next occultation globally
        /// </summary>
        public Int32 swe_lun_occult_when_glob(double tjd_start, Int32 ipl, string starname, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr) {
            return SweCL.swe_lun_occult_when_glob(tjd_start, ipl, starname, ifl, ifltype, tret, backward, ref serr);
        }

        /// <summary>
        /// finds time of next local eclipse
        /// </summary>
        public Int32 swe_sol_eclipse_when_loc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, ref string serr) {
            return SweCL.swe_sol_eclipse_when_loc(tjd_start, ifl, geopos, tret, attr, backward, ref serr);
        }

        public Int32 swe_lun_occult_when_loc(double tjd_start, Int32 ipl, String starname, Int32 ifl, double[] geopos, double[] tret,
            double[] attr, bool backward, ref string serr) {
                return SweCL.swe_lun_occult_when_loc(tjd_start, ipl, starname, ifl, geopos, tret, attr, backward, ref serr);
        }

        /// <summary>
        /// finds time of next eclipse globally
        /// </summary>
        public Int32 swe_sol_eclipse_when_glob(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr) {
            return SweCL.swe_sol_eclipse_when_glob(tjd_start, ifl, ifltype, tret, backward, ref serr);
        }

        /// <summary>
        /// computes attributes of a lunar eclipse for given tjd
        /// </summary>
        public Int32 swe_lun_eclipse_how(double tjd_ut, Int32 ifl, double[] geopos, double[] attr, ref string serr) {
            return SweCL.swe_lun_eclipse_how(tjd_ut, ifl, geopos, attr, ref serr);
        }

        public Int32 swe_lun_eclipse_when(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr) {
            return SweCL.swe_lun_eclipse_when(tjd_start, ifl, ifltype, tret, backward, ref serr);
        }

        public Int32 swe_lun_eclipse_when_loc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, ref string serr) {
            return SweCL.swe_lun_eclipse_when_loc(tjd_start, ifl, geopos, tret, attr, backward, ref serr);
        }

        /// <summary>
        /// planetary phenomena
        /// </summary>
        public Int32 swe_pheno(double tjd, Int32 ipl, Int32 iflag, double[] attr, ref string serr) {
            return SweCL.swe_pheno(tjd, ipl, iflag, attr, ref serr);
        }

        public Int32 swe_pheno_ut(double tjd_ut, Int32 ipl, Int32 iflag, double[] attr, ref string serr) {
            return SweCL.swe_pheno_ut(tjd_ut, ipl, iflag, attr, ref serr); 
        }

        public double swe_refrac(double inalt, double atpress, double attemp, Int32 calc_flag) {
            return SweCL.swe_refrac(inalt, atpress, attemp, calc_flag);
        }

        public double swe_refrac_extended(double inalt, double geoalt, double atpress, double attemp, double lapse_rate, Int32 calc_flag, double[] dret) {
            return SweCL.swe_refrac_extended(inalt, geoalt, atpress, attemp, lapse_rate, calc_flag, dret);
        }

        public void swe_set_lapse_rate(double lapse_rate) {
            SweCL.swe_set_lapse_rate(lapse_rate);
        }

        public void swe_azalt(double tjd_ut, Int32 calc_flag, double[] geopos, double atpress, double attemp, double[] xin, double[] xaz) { 
            SweCL.swe_azalt(tjd_ut, calc_flag, geopos, atpress, attemp, xin, xaz); 
        }

        public void swe_azalt_rev(double tjd_ut, Int32 calc_flag, double[] geopos, double[] xin, double[] xout) {
            SweCL.swe_azalt_rev(tjd_ut, calc_flag, geopos, xin, xout);
        }

        public Int32 swe_rise_trans(double tjd_ut, Int32 ipl, string starname, Int32 epheflag, Int32 rsmi,
            double[] geopos, double atpress, double attemp, ref double tret, ref string serr) {
                return SweCL.swe_rise_trans(tjd_ut, ipl, starname, epheflag, rsmi, geopos, atpress, attemp, ref tret, ref serr);
        }

        public Int32 swe_nod_aps(double tjd_et, Int32 ipl, Int32 iflag,
                              Int32 method,
                              double[] xnasc, double[] xndsc,
                              double[] xperi, double[] xaphe,
                              ref string serr) {
            return SweCL.swe_nod_aps(tjd_et, ipl, iflag, method, xnasc, xndsc, xperi, xaphe, ref serr);
        }


        public Int32 swe_nod_aps_ut(double tjd_ut, Int32 ipl, Int32 iflag,
                              Int32 method,
                              double[] xnasc, double[] xndsc,
                              double[] xperi, double[] xaphe,
                              ref string serr) {
            return SweCL.swe_nod_aps_ut(tjd_ut, ipl, iflag, method, xnasc, xndsc, xperi, xaphe, ref serr);
        }


        /**************************** 
         * exports from swephlib.c 
         ****************************/

        /* delta t */
        public double swe_deltat(double tjd) { return SwephLib.swe_deltat(tjd); }

        /// <summary>
        /// equation of time
        /// </summary>
        public int swe_time_equ(double tjd, out double e, ref string serr) { return Sweph.swe_time_equ(tjd, out e, ref serr); }
        public int swe_lmt_to_lat(double tjd_lmt, double geolon, out double tjd_lat, ref string serr) {
            return Sweph.swe_lmt_to_lat(tjd_lmt, geolon, out tjd_lat, ref serr); 
        }
        public int swe_lat_to_lmt(double tjd_lat, double geolon, out double tjd_lmt, ref string serr) {
            return Sweph.swe_lat_to_lmt(tjd_lat, geolon, out tjd_lmt, ref serr); 
        }


        /// <summary>
        /// sidereal time 
        /// </summary>
        public double swe_sidtime0(double tjd_ut, double ecl, double nut) { return SwephLib.swe_sidtime0(tjd_ut, ecl, nut); }
        public double swe_sidtime(double tjd_ut) { return SwephLib.swe_sidtime(tjd_ut); }


        /// <summary>
        /// coordinate transformation polar -> polar
        /// </summary>
        public void swe_cotrans(CPointer<double> xpo, CPointer<double> xpn, double eps) { SwephLib.swe_cotrans(xpo, xpn, eps); }
        public void swe_cotrans_sp(CPointer<double> xpo, CPointer<double> xpn, double eps) { SwephLib.swe_cotrans_sp(xpo, xpn, eps); }

        /// <summary>
        /// tidal acceleration to be used in swe_deltat()
        /// </summary>
        public double swe_get_tid_acc() { return SwephLib.swe_get_tid_acc(); }
        public void swe_set_tid_acc(double tidacc) { SwephLib.swe_set_tid_acc(tidacc); }

        public double swe_degnorm(double x) { return SwephLib.swe_degnorm(x); }

        public double swe_radnorm(double x) { return SwephLib.swe_radnorm(x); }
        public double swe_rad_midp(double x1, double x0) { return SwephLib.swe_rad_midp(x1, x0); }
        public double swe_deg_midp(double x1, double x0) { return SwephLib.swe_deg_midp(x1, x0); }

        public void swe_split_deg(double ddeg, Int32 roundflag, out Int32 ideg, out Int32 imin, out Int32 isec, out double dsecfr, out Int32 isgn) {
            SwephLib.swe_split_deg(ddeg, roundflag, out ideg, out imin, out isec, out dsecfr, out isgn);
        }

        ///******************************************************* 
        // * other functions from swephlib.c;
        // * they are not needed for Swiss Ephemeris,
        // * but may be useful to former Placalc users.
        // ********************************************************/

        /// <summary>
        /// normalize argument into interval [0..DEG360]
        /// </summary>
        public Int32 swe_csnorm(Int32 p) { return SwephLib.swe_csnorm(p); }

        /// <summary>
        /// distance in centisecs p1 - p2 normalized to [0..360[
        /// </summary>
        public Int32 swe_difcsn(Int32 p1, Int32 p2) { return SwephLib.swe_difcsn(p1, p2); }

        public double swe_difdegn(double p1, double p2) { return SwephLib.swe_difdegn(p1, p2); }

        /// <summary>
        /// distance in centisecs p1 - p2 normalized to [-180..180[
        /// </summary>
        public Int32 swe_difcs2n(Int32 p1, Int32 p2) { return SwephLib.swe_difcs2n(p1, p2); }

        public double swe_difdeg2n(double p1, double p2) { return SwephLib.swe_difdeg2n(p1, p2); }

        public double swe_difrad2n(double p1, double p2) { return SwephLib.swe_difrad2n(p1, p2); }

        /// <summary>
        /// round second, but at 29.5959 always down
        /// </summary>
        public Int32 swe_csroundsec(Int32 x) { return SwephLib.swe_csroundsec(x); }

        /// <summary>
        /// double to int32 with rounding, no overflow check
        /// </summary>
        public Int32 swe_d2l(double x) { return SwephLib.swe_d2l(x); }

        /// <summary>
        /// monday = 0, ... sunday = 6
        /// </summary>
        public int swe_day_of_week(double jd) { return SwephLib.swe_day_of_week(jd); }

        public string swe_cs2timestr(Int32 t, char sep, bool suppressZero, ref string a) { return SwephLib.swe_cs2timestr(t, sep, suppressZero, ref a); }

        public string swe_cs2lonlatstr(Int32 t, char pchar, char mchar, ref string s) { return SwephLib.swe_cs2lonlatstr(t, pchar, mchar, ref s); }

        public string swe_cs2degstr(Int32 t, ref string a) { return SwephLib.swe_cs2degstr(t, ref a); }

    }

}
