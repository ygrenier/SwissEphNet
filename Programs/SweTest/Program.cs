#define MSDOS

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

/* 
  $Header: /users/dieter/sweph/RCS/swetest.c,v 1.78 2010/06/25 07:22:10 dieter Exp $
  swetest.c	A test program
   
  Authors: Dieter Koch and Alois Treindl, Astrodienst Zuerich

**************************************************************/

#region Licence
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
#endregion

using SwissEphNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SweTest
{
    class Program
    {
        #region C

        #region Strings
        /* attention: Microsoft Compiler does not accept strings > 2048 char */
        static string infocmd0 = @"\n\
  Swetest computes a complete set of geocentric planetary positions,\n\
  for a given date or a sequence of dates.\n\
  Input can either be a date or an absolute julian day number.\n\
  0:00 (midnight).\n\
  With the proper options, swetest can be used to output a printed\n\
  ephemeris and transfer the data into other programs like spreadsheets\n\
  for graphical display.\n\
  Version: $Header: /users/dieter/sweph/RCS/swetest.c,v 1.78 2010/06/25 07:22:10 dieter Exp $\n\
\n";
        static string infocmd1 = @"\n\
  Command line options:\n\
     help commands:\n\
        -?, -h  display whole info\n\
        -hcmd   display commands\n\
        -hplan  display planet numbers\n\
        -hform  display format characters\n\
        -hdate  display input date format\n\
        -hexamp  display examples\n\
        -glp  report file location of library\n\
     input time formats:\n\
        -bDATE  begin date; e.g. -b1.1.1992 if\n\
                Note: the date format is day month year (European style).\n\
        -bj...  begin date as an absolute Julian day number; e.g. -bj2415020.5\n\
        -j...   same as -bj\n\
        -tHH.MMSS  input time (as Ephemeris Time)\n\
        -ut     input date is Universal Time\n\
	-utHH:MM:SS input time (as Universal Time)\n\
	-utHH.MMSS input time (as Universal Time)\n\
     output time for eclipses, occultations, risings/settings is UT by default\n\
        -lmt    output date/time is LMT (with -geopos)\n\
        -lat    output date/time is LAT (with -geopos)\n\
     object, number of steps, step with\n\
        -pSEQ   planet sequence to be computed.\n\
                See the letter coding below.\n\
        -dX     differential ephemeris: print differential ephemeris between\n\
                body X and each body in list given by -p\n\
                example: -p2 -d0 -fJl -n366 -b1.1.1992 prints the longitude\n\
                distance between SUN (planet 0) and MERCURY (planet 2)\n\
                for a full year starting at 1 Jan 1992.\n\
    -DX	midpoint ephemeris, works the same way as the differential\n\
        mode -d described above, but outputs the midpoint position.\n\
        -nN     output data for N consecutive days; if no -n option\n\
                is given, the default is 1. If the option -n without a\n\
                number is given, the default is 20.\n\
        -sN     timestep N days, default 1. This option is only meaningful\n\
                when combined with option -n.\n\
                If an 'm' is appended, the time step is in minutes instead of days, \n\
                for example -s15m for a time step of 15 minutes.\n\
                If an 's' is appended, the time step is in seconds instead of days, \n\
                for example -s1s for a time step of 1 second.\n\
";
        static string infocmd2 = @"\
     output format:\n\
        -fSEQ   use SEQ as format sequence for the output columns;\n\
                default is PLBRS.\n\
        -head   don\'t print the header before the planet data. This option\n\
                is useful when you want to paste the output into a\n\
                spreadsheet for displaying graphical ephemeris.\n\
        +head   header before every step (with -s..) \n\
        -gPPP   use PPP as gap between output columns; default is a single\n\
                blank.  -g followed by white space sets the\n\
                gap to the TAB character; which is useful for data entry\n\
                into spreadsheets.\n\
        -hor	list data for multiple planets 'horizontally' in same line.\n\
		all columns of -fSEQ are repeated except time colums tTJyY.\n\
     astrological house system:\n\
        -house[long,lat,hsys]	\n\
        include house cusps. The longitude, latitude (degrees with\n\
        DECIMAL fraction) and house system letter can be given, with\n\
        commas separated, + for east and north. If none are given,\n\
        Greenwich UK and Placidus is used: 0.00,51.50,p.\n\
		The output lists 12 house cusps, Asc, MC, ARMC, Vertex,\n\
		Equatorial Ascendant, co-Ascendant as defined by Walter Koch, \n\
		co-Ascendant as defined by Michael Munkasey, and Polar Ascendant. \n\
        Houses can only be computed if option -ut is given.\n\
                   A  equal\n\
                   B  Alcabitius\n\
                   C  Campanus\n\
                   D  equal / MC\n\
                   E  equal = A\n\
                   F  Carter poli-equatorial\n\
                   G  36 Gauquelin sectors\n\
                   H  horizon / azimuth\n\
                   I  Sunshine\n\
                   i  Sunshine alternative\n\
                   K  Koch\n\
                   L  Pullen S-delta\n\
                   M  Morinus\n\
                   N  Whole sign, Aries = 1st house\n\
                   O  Porphyry\n\
                   P  Placidus\n\
                   Q  Pullen S-ratio\n\
                   R  Regiomontanus\n\
                   S  Sripati\n\
                   T  Polich/Page (""topocentric"")\n\
                   U  Krusinski-Pisa-Goelzer\n\
                   V  equal Vehlow\n\
                   W  equal, whole sign\n\
                   X  axial rotation system/ Meridian houses\n\
                   Y  APC houses\n\
		 The use of lower case letters is deprecated. They will have a\n\
		 different meaning in future releases of Swiss Ephemeris.\n\
        -hsy[hsys]	\n\
		 house system to be used (for house positions of planets)\n\
		 for long, lat, hsys, see -house\n\
		 The use of lower case letters is deprecated. They will have a\n\
		 different meaning in future releases of Swiss Ephemeris.\n\
";
        static string infocmd3 = @"\
        -geopos[long,lat,elev]	\n\
        Geographic position. Can be used for azimuth and altitude\n\
                or topocentric or house cups calculations.\n\
                The longitude, latitude (degrees with DECIMAL fraction)\n\
        and elevation (meters) can be given, with\n\
        commas separated, + for east and north. If none are given,\n\
        Greenwich is used: 0,51.5,0\n\
     sidereal astrology:\n\
    -ay..   ayanamsha, with number of method, e.g. ay0 for Fagan/Bradley\n\
    -sid..    sidereal, with number of method (see below)\n\
    -sidt0..  sidereal, projection on ecliptic of t0 \n\
    -sidsp..  sidereal, projection on solar system plane \n\
           number of ayanamsha method:\n\
       0 for Fagan/Bradley\n\
       1 for Lahiri\n\
       2 for De Luce\n\
       3 for Raman\n\
       4 for Ushashashi\n\
       5 for Krishnamurti\n\
       6 for Djwhal Khul\n\
       7 for Yukteshwar\n\
       8 for J.N. Bhasin\n\
       9 for Babylonian/Kugler 1\n\
       10 for Babylonian/Kugler 2\n\
       11 for Babylonian/Kugler 3\n\
       12 for Babylonian/Huber\n\
       13 for Babylonian/Eta Piscium\n\
       14 for Babylonian/Aldebaran = 15 Tau\n\
       15 for Hipparchos\n\
       16 for Sassanian\n\
       17 for Galact. Center = 0 Sag\n\
       18 for J2000\n\
       19 for J1900\n\
       20 for B1950\n\
       21 for Suryasiddhanta\n\
       22 for Suryasiddhanta, mean Sun\n\
       23 for Aryabhata\n\
       24 for Aryabhata, mean Sun\n\
       25 for SS Citra\n\
       26 for SS Revati\n\
       27 for True Citra\n\
       28 for True Revati\n\
       29 for True Pushya\n\
	   30 for Galactic (Gil Brand)\n\
	   31 for Galactic Equator (IAU1958)\n\
	   32 for Galactic Equator\n\
	   33 for Galactic Equator mid-Mula\n\
	   34 for Skydram (Mardyks)\n\
	   35 for True Mula (Chandra Hari)\n\
	   36 Dhruva/Gal.Center/Mula (Wilhelm)\n\
	   37 Aryabhata 522\n\
	   38 Babylonian/Britton\n\
     ephemeris specifications:\n\
        -edirPATH change the directory of the ephemeris files \n\
        -eswe   swiss ephemeris\n\
        -ejpl   jpl ephemeris (DE431), or with ephemeris file name\n\
                -ejplde200.eph \n\
        -emos   moshier ephemeris\n\
        -true             true positions\n\
        -noaberr          no aberration\n\
        -nodefl           no gravitational light deflection\n\
    -noaberr -nodefl  astrometric positions\n\
        -j2000            no precession (i.e. J2000 positions)\n\
        -icrs             ICRS (use Internat. Celestial Reference System)\n\
        -nonut            no nutation \n\
";
        static string infocmd4 = @"\
        -speed            calculate high precision speed \n\
        -speed3           'low' precision speed from 3 positions \n\
                          do not use this option. -speed parameter\n\
              is faster and more precise \n\
    -iXX	          force iflag to value XX\n\
        -testaa96         test example in AA 96, B37,\n\
                          i.e. venus, j2450442.5, DE200.\n\
                          attention: use precession IAU1976\n\
                          and nutation 1980 (s. swephlib.h)\n\
        -testaa95\n\
        -testaa97\n\
        -roundsec         round to seconds\n\
        -roundmin         round to minutes\n\
     observer position:\n\
        -hel    compute heliocentric positions\n\
        -bary   compute barycentric positions (bar. earth instead of node) \n\
        -topo[long,lat,elev]	\n\
        topocentric positions. The longitude, latitude (degrees with\n\
        DECIMAL fraction) and elevation (meters) can be given, with\n\
        commas separated, + for east and north. If none are given,\n\
        Zuerich is used: 8.55,47.38,400\n\
     orbital elements:\n\
        -orbel  compute osculating orbital elements relative to the\n\
	        mean ecliptic J2000. (Note, all values, including time of\n\
		pericenter vary considerably depending on the date for which the\n\
		osculating ellipse is calculated\n\
\n\
     special events:\n\
        -solecl solar eclipse\n\
                output 1st line:\n\
                  eclipse date,\n\
                  time of maximum (UT),\n\
                  core shadow width (negative with total eclipses),\n\
                  fraction of solar diameter that is eclipsed\n\
          Julian day number (6-digit fraction) of maximum\n\
                output 2nd line:\n\
                  start and end times for partial and total phase\n\
                output 3rd line:\n\
                  geographical longitude and latitude of maximum eclipse,\n\
                  totality duration at that geographical position,\n\
                output with -local, see below.\n\
        -occult occultation of planet or star by the moon. Use -p to \n\
                specify planet (-pf -xfAldebaran for stars) \n\
                output format same as with -solecl\n\
";
        static string infocmd5 = @"\
        -lunecl lunar eclipse\n\
                output 1st line:\n\
                  eclipse date,\n\
                  time of maximum (UT),\n\
          Julian day number (6-digit fraction) of maximum\n\
                output 2nd line:\n\
                  6 contacts for start and end of penumbral, partial, and\n\
                  total phase\n\
        -local  only with -solecl or -occult, if the next event of this\n\
                kind is wanted for a given geogr. position.\n\
                Use -geopos[long,lat,elev] to specify that position.\n\
                If -local is not set, the program \n\
                searches for the next event anywhere on earth.\n\
                output 1st line:\n\
                  eclipse date,\n\
                  time of maximum,\n\
                  fraction of solar diameter that is eclipsed\n\
                output 2nd line:\n\
                  local eclipse duration,\n\
                  local four contacts,\n\
        -hev[type] heliacal events,\n\
        type 1 = heliacal rising\n\
        type 2 = heliacal setting\n\
        type 3 = evening first\n\
        type 4 = morning last\n\
            type 0 or missing = all four events are listed.\n\
        -rise   rising and setting of a planet or star.\n\
                Use -geopos[long,lat,elev] to specify geographical position.\n\
        -metr   southern and northern meridian transit of a planet of star\n\
                Use -geopos[long,lat,elev] to specify geographical position.\n\
     specifications for eclipses:\n\
        -total  total eclipse (only with -solecl, -lunecl)\n\
        -partial partial eclipse (only with -solecl, -lunecl)\n\
        -annular annular eclipse (only with -solecl)\n\
        -anntot annular-total (hybrid) eclipse (only with -solecl)\n\
        -penumbral penumbral lunar eclipse (only with -lunecl)\n\
        -central central eclipse (only with -solecl, nonlocal)\n\
        -noncentral non-central eclipse (only with -solecl, nonlocal)\n\
";
        static string infocmd6 = @"\
     specifications for risings and settings:\n\
        -norefrac   neglect refraction (with option -rise)\n\
        -disccenter find rise of disc center (with option -rise)\n\
        -discbottom find rise of disc bottom (with option -rise)\n\
    -hindu      hindu version of sunrise (with option -rise)\n\
     specifications for heliacal events:\n\
        -at[press,temp,rhum,visr]:\n\
                pressure in hPa\n\
            temperature in degrees Celsius\n\
            relative humidity in %\n\
            visual range, interpreted as follows:\n\
              > 1 : meteorological range in km\n\
              1>visr>0 : total atmospheric coefficient (ktot)\n\
              = 0 : calculated from press, temp, rhum\n\
            Default values are -at1013.25,15,40,0\n\
         -obs[age,SN] age of observer and Snellen ratio\n\
                Default values are -obs36,1\n\
         -opt[age,SN,binocular,magn,diam,transm]\n\
                age and SN as with -obs\n\
            0 monocular or 1 binocular\n\
            telescope magnification\n\
            optical aperture in mm\n\
            optical transmission\n\
            Default values: -opt36,1,1,1,0,0 (naked eye)\n\
     backward search:\n\
        -bwd\n";
        /* characters still available:
          bcgijklruvx
         */
        static string infoplan = @"\n\
  Planet selection letters:\n\
     planetary lists:\n\
        d (default) main factors 0123456789mtABCcg\n\
        p main factors as above, plus main asteroids DEFGHI\n\
        h ficticious factors J..X\n\
        a all factors\n\
        (the letters above can only appear as a single letter)\n\n\
     single body numbers/letters:\n\
        0 Sun (character zero)\n\
        1 Moon (character 1)\n\
        2 Mercury\n\
        3 Venus\n\
        4 Mars\n\
        5 Jupiter\n\
        6 Saturn\n\
        7 Uranus\n\
        8 Neptune\n\
        9 Pluto\n\
        m mean lunar node\n\
        t true lunar node\n\
        n nutation\n\
        o obliquity of ecliptic\n\
    q delta t\n\
    y time equation\n\
        A mean lunar apogee (Lilith, Black Moon) \n\
        B osculating lunar apogee \n\
        c intp. lunar apogee \n\
        g intp. lunar perigee \n\
        C Earth (in heliocentric or barycentric calculation)\n\
     dwarf planets, plutoids\n\
        F Ceres\n\
    9 Pluto\n\
    s -xs136199   Eris\n\
    s -xs136472   Makemake\n\
    s -xs136108   Haumea\n\
     some minor planets:\n\
        D Chiron\n\
        E Pholus\n\
        G Pallas \n\
        H Juno \n\
        I Vesta \n\
        s minor planet, with MPC number given in -xs\n\
     fixed stars:\n\
        f fixed star, with name or number given in -xf option\n\
    f -xfSirius   Sirius\n\
     fictitious objects:\n\
        J Cupido \n\
        K Hades \n\
        L Zeus \n\
        M Kronos \n\
        N Apollon \n\
        O Admetos \n\
        P Vulkanus \n\
        Q Poseidon \n\
        R Isis (Sevin) \n\
        S Nibiru (Sitchin) \n\
        T Harrington \n\
        U Leverrier's Neptune\n\
        V Adams' Neptune\n\
        W Lowell's Pluto\n\
        X Pickering's Pluto\n\
        Y Vulcan\n\
        Z White Moon\n\
    w Waldemath's dark Moon\n\
        z hypothetical body, with number given in -xz\n\
     sidereal time:\n\
        x sidereal time\n\
        e print a line of labels\n\
          \n";
        /* characters still available 
           CcEeMmOoqWwz
        */
        static string infoform = @"\n\
  Output format SEQ letters:\n\
  In the standard setting five columns of coordinates are printed with\n\
  the default format PLBRS. You can change the default by providing an\n\
  option like -fCCCC where CCCC is your sequence of columns.\n\
  The coding of the sequence is like this:\n\
        y year\n\
        Y year.fraction_of_year\n\
        p planet index\n\
        P planet name\n\
        J absolute juldate\n\
        T date formatted like 23.02.1992 \n\
        t date formatted like 920223 for 1992 february 23\n\
        L longitude in degree ddd mm'ss\""\n\
        l longitude decimal\n\
        Z longitude ddsignmm'ss\""\n\
        S speed in longitude in degree ddd:mm:ss per day\n\
        SS speed for all values specified in fmt\n\
        s speed longitude decimal (degrees/day)\n\
        ss speed for all values specified in fmt\n\
        B latitude degree\n\
        b latitude decimal\n\
        R distance decimal in AU\n\
        r distance decimal in AU, Moon in seconds parallax\n\
        q relative distance (1000=nearest, 0=furthest)\n\
        A right ascension in hh:mm:ss\n\
        a right ascension hours decimal\n\
        D declination degree\n\
        d declination decimal\n\
        I azimuth degree\n\
        i azimuth decimal\n\
        H altitude degree\n\
        h altitude decimal\n\
        K altitude (with refraction) degree\n\
        k altitude (with refraction) decimal\n\
        G house position in degrees\n\
        g house position in degrees decimal\n\
        j house number 1.0 - 12.99999\n\
        X x-, y-, and z-coordinates ecliptical\n\
        x x-, y-, and z-coordinates equatorial\n\
        U unit vector ecliptical\n\
        u unit vector equatorial\n\
        Q l, b, r, dl, db, dr, a, d, da, dd\n\
    n nodes (mean): ascending/descending (Me - Ne); longitude decimal\n\
    N nodes (osculating): ascending/descending, longitude; decimal\n\
    f apsides (mean): perihel, aphel, second focal point; longitude dec.\n\
    F apsides (osc.): perihel, aphel, second focal point; longitude dec.\n\
    + phase angle\n\
    - phase\n\
    * elongation\n\
    / apparent diameter of disc (without refraction)\n\
    = magnitude\n";
        static string infoform2 = @"\
        v (reserved)\n\
        V (reserved)\n\
    ";
        static string infodate = @"\n\
  Date entry:\n\
  In the interactive mode, when you are asked for a start date,\n\
  you can enter data in one of the following formats:\n\
\n\
        1.2.1991        three integers separated by a nondigit character for\n\
                        day month year. Dates are interpreted as Gregorian\n\
                        after 4.10.1582 and as Julian Calendar before.\n\
                        Time is always set to midnight (0 h).\n\
                        If the three letters jul are appended to the date,\n\
                        the Julian calendar is used even after 1582.\n\
                        If the four letters greg are appended to the date,\n\
                        the Gregorian calendar is used even before 1582.\n\
\n\
        j2400123.67     the letter j followed by a real number, for\n\
                        the absolute Julian daynumber of the start date.\n\
                        Fraction .5 indicates midnight, fraction .0\n\
                        indicates noon, other times of the day can be\n\
                        chosen accordingly.\n\
\n\
        <RETURN>        repeat the last entry\n\
        \n\
        .               stop the program\n\
\n\
        +20             advance the date by 20 days\n\
\n\
        -10             go back in time 10 days\n";
        static string infoexamp = @"\n\
\n\
  Examples:\n\
\n\
    swetest -p2 -b1.12.1900 -n15 -s2\n\
    ephemeris of Mercury (-p2) starting on 1 Dec 1900,\n\
    15 positions (-n15) in two-day steps (-s2)\n\
\n\
    swetest -p2 -b1.12.1900 -n15 -s2 -fTZ -roundsec -g, -head\n\
    same, but output format =  date and zodiacal position (-fTZ),\n\
    separated by comma (-g,) and rounded to seconds (-roundsec),\n\
    without header (-head).\n\
\n\
    swetest -ps -xs433 -b1.12.1900\n\
    position of asteroid 433 Eros (-ps -xs433)\n\
\n\
    swetest -pf -xfAldebaran -b1.1.2000\n\
    position of fixed star Aldebaran \n\
\n\
    swetest -p1 -d0 -b1.12.1900 -n10 -fPTl -head\n\
    angular distance of moon (-p1) from sun (-d0) for 10\n\
    consecutive days (-n10).\n\
\n\
    swetest -p6 -DD -b1.12.1900 -n100 -s5 -fPTZ -head -roundmin\n\
      Midpoints between Saturn (-p6) and Chiron (-DD) for 100\n\
      consecutive steps (-n100) with 5-day steps (-s5) with\n\
      longitude in degree-sign format (-f..Z) rounded to minutes (-roundmin)\n\
\n\
    swetest -b5.1.2002 -p -house12.05,49.50,K -ut12:30\n\
	Koch houses for a location in Germany at a given date and time\n\
\n\
    swetest -b1.1.2016  -g -fTlbR -p0123456789Dmte -hor -n366 -roundsec\n\
	tabular ephemeris (all planets Sun - Pluto, Chiron, mean node, true node)\n\
	in one horizontal row, tab-separated, for 366 days. For each planet\n\
	list longitude, latitude and geocentric distance.\n";
        #endregion

        /**************************************************************/

        //#include "swephexp.h" 	/* this includes  "sweodef.h" */

        /*
         * programmers warning: It looks much worse than it is!
         * Originally swetest.c was a small and simple test program to test
         * the main functions of the Swiss Ephemeris and to demonstrate
         * its precision.
         * It compiles on Unix, on MSDOS and as a non-GUI utility on 16-bit
         * and 32-bit windows.
         * This portability has forced us into some clumsy constructs, which
         * end to hide the actual simplicity of the use of Swiss Ephemeris.
         * For example, the mechanism implemented here in swetest.c to find
         * the binary ephemeris files overrides the much simpler mechanism
         * inside the SwissEph library. This was necessary because we wanted
         * swetest.exe to run directly off the CDROM and search with some
         * intelligence for ephemeris files already installed on a system.
         */

        //#if MSDOS
        //#  include <direct.h>
        //#  include <dos.h>
        //#  ifdef _MSC_VER
        //#    include <sys\types.h>
        //#  endif
        //#if __MINGW32__
        //#  include <sys/stat.h>
        //#else
        //#  include <sys\stat.h>
        //#endif
        //#  include <float.h>
        //#else
        //# ifdef MACOS
        //#  include <console.h>
        //# else
        //#  include <sys/stat.h>
        //# endif
        //#endif

        const double J2000 = 2451545.0;  /* 2000 January 1.5 */
        static double square_sum(CPointer<double> x) { return (x[0] * x[0] + x[1] * x[1] + x[2] * x[2]); }
        const int SEFLG_EPHMASK = SwissEph.SEFLG_EPHMASK;//(SEFLG_JPLEPH | SEFLG_SWIEPH | SEFLG_MOSEPH);

        const int BIT_ROUND_SEC = 1;
        const int BIT_ROUND_MIN = 2;
        const int BIT_ZODIAC = 4;
        const int BIT_LZEROES = 8;

        const int BIT_TIME_LZEROES = 8;
        const int BIT_TIME_LMT = 16;
        const int BIT_TIME_LAT = 32;

        const string PLSEL_D = "0123456789mtA";
        const string PLSEL_P = "0123456789mtABCcgDEFGHI";
        const string PLSEL_H = "JKLMNOPQRSTUVWXYZw";
        const string PLSEL_A = "0123456789mtABCcgDEFGHIJKLMNOPQRSTUVWXYZw";

        const char DIFF_DIFF = 'd';
        const char DIFF_MIDP = 'D';
        const int MODE_HOUSE = 1;
        const int MODE_LABEL = 2;

        const int SEARCH_RANGE_LUNAR_CYCLES = 20000;

        static int OUTPUT_EXTRA_PRECISION = 0;

        static string se_pname = String.Empty;
        static string[] zod_nam = new String[]{"ar", "ta", "ge", "cn", "le", "vi",
                                  "li", "sc", "sa", "cp", "aq", "pi"};

        static string star = "algol", star2 = String.Empty;
        static string sastno = "433";
        static string shyp = "1";
        //static char *dms(double x, int32 iflag);
        //static int make_ephemeris_path(int32 iflag, char *argv0, char *ephepath);
        //static int letter_to_ipl(int letter);
        //static int print_line(int mode, AS_BOOL is_first);
        //static int do_special_event(double tjd, int32 ipl, char* star, int32 special_event, int32 special_mode, double* geopos, double* datm, double* dobs, char* serr);
        //static int32 orbital_elements(double tjd_et, int32 ipl, int32 iflag, char* serr);
        //static char *hms_from_tjd(double x);
        //static void do_printf(char *info);
        //static char *hms(double x, int32 iflag);
        //static void remove_whitespace(char *s);
        //#if MSDOS
        //static int cut_str_any(char *s, char *cutlist, char *cpos[], int nmax);
        //#endif

        /* globals shared between main() and print_line() */
        static string fmt = "PLBRS";
        static string gap = " ";
        static double t, te, tut, jut = 0;
        static int jmon, jday, jyear;
        static int ipl = SwissEph.SE_SUN, ipldiff = SwissEph.SE_SUN, nhouses = 12;
        static string spnam = string.Empty, spnam2 = string.Empty, serr = string.Empty;
        static string serr_save = string.Empty, serr_warn = string.Empty;
        static int gregflag = SwissEph.SE_GREG_CAL;
        static int diff_mode = 0;
        static bool universal_time = false;
        static Int32 round_flag = 0;
        static Int32 time_flag = 0;
        static bool short_output = false;
        static bool list_hor = false;
        static Int32 special_event = 0;
        static Int32 special_mode = 0;
        static bool do_orbital_elements = false;
        static bool hel_using_AV = false;
        static double[] x = new double[6], x2 = new double[6], xequ = new double[6], xcart = new double[6],
            xcartq = new double[6], xobl = new double[6], xaz = new double[6], xt = new double[6], xsv = new double[6];
        static double hpos, hpos2, hposj, armc;
        static int hpos_meth = 0;
        static double[] geopos = new double[10];
        static double[] attr = new double[20], tret = new double[20], datm = new double[4], dobs = new double[6];
        static Int32 iflag = 0, iflag2;              /* external flag: helio, geo... */
        static string[] hs_nam = new string[]
        { "undef", "Ascendant", "MC", "ARMC", "Vertex", "equat. Asc.", "co-Asc. W.Koch", "co-Asc Munkasey", "Polar Asc." };
        static int direction = 1;
        static bool direction_flag = false;
        static bool step_in_minutes = false;
        static bool step_in_seconds = false;
        static Int32 helflag = 0;
        static double tjd = 2415020.5;
        static Int32 nstep = 1, istep;
        static Int32 search_flag = 0;
        static string sout = String.Empty;
        static Int32 whicheph = SwissEph.SEFLG_SWIEPH;
        static char psp;
        static Int32 norefrac = 0;
        static Int32 disccenter = 0;
        static string ephepath = String.Empty;
        static Int32 discbottom = 0;
        /* for test of old models only */
        static Int32[] astro_models = new Int32[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static bool do_set_astro_models = false;

        const int SP_LUNAR_ECLIPSE = 1;
        const int SP_SOLAR_ECLIPSE = 2;
        const int SP_OCCULTATION = 3;
        const int SP_RISE_SET = 4;
        const int SP_MERIDIAN_TRANSIT = 5;
        const int SP_HELIACAL = 6;

        const int SP_MODE_HOW = 2;       /* an option for Lunar */
        const int SP_MODE_LOCAL = 8;       /* an option for Solar */
        const int SP_MODE_HOCAL = 4096;

        const int ECL_LUN_PENUMBRAL = 1;       /* eclipse types for hocal list */
        const int ECL_LUN_PARTIAL = 2;
        const int ECL_LUN_TOTAL = 3;
        const int ECL_SOL_PARTIAL = 4;
        const int ECL_SOL_ANNULAR = 5;
        const int ECL_SOL_TOTAL = 6;

        static SwissEph sweph = null;

        static int main_test(int argc, string[] argv)
        {
            string sdate_save = String.Empty;
            string s1 = String.Empty, s2 = String.Empty;
            string sp/*, sp2*/; int spi, sp2i;
            char spno;
            string plsel = PLSEL_D;
            //#if HPUNIX
            //  char hostname[80];
            //#endif
            int i, j, n, iflag_f = -1, iflgt;
            int line_count, line_limit = 32000;
            double daya;
            double top_long = 0.0;	/* Greenwich UK */
            double top_lat = 51.5;
            double top_elev = 0;
            bool have_geopos = false;
            char ihsy = 'P';
            bool do_houses = false;
            string fname = String.Empty;
            string sdate = String.Empty;
            string begindate = null;
            string stimein = string.Empty;
            Int32 iflgret;
            bool is_first = true;
            bool with_header = true;
            bool with_glp = false;
            bool with_header_always = false;
            bool do_ayanamsa = false;
            Int32 sid_mode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
            double t2, tstep = 1, thour = 0;
            double delt;
            datm[0] = 1013.25; datm[1] = 15; datm[2] = 40; datm[3] = 0;
            dobs[0] = 0; dobs[1] = 0;
            dobs[2] = 0; dobs[3] = 0; dobs[4] = 0; dobs[5] = 0;
            serr = serr_save = serr_warn = sdate_save = String.Empty;
            //# ifdef MACOS
            //  argc = ccommand(&argv); /* display the arguments window */    
            //# endif
            stimein = string.Empty;
            using (sweph = new SwissEph())
            {
                sweph.OnLoadFile += sweph_OnLoadFile;
                ephepath = @".;C:\\sweph\ephe";
                fname = SwissEph.SE_FNAME_DFT;
                for (i = 1; i < argc; i++)
                {
                    if (argv[i].StartsWith("-ut"))
                    {
                        universal_time = true;
                        if (argv[i].Length > 3)
                        {
                            //stimein = string.Empty;
                            //strncat(stimein, argv[i] + 3, 30);
                            stimein = argv[i].Substring(3);
                            if (stimein.Length > 30)
                                stimein = stimein.Substring(0, 30);
                        }
                    }
                    else if (argv[i].StartsWith("-glp"))
                    {
                        with_glp = true;
                    }
                    else if (argv[i].StartsWith("-hor"))
                    {
                        list_hor = true;
                    }
                    else if (argv[i].StartsWith("-head"))
                    {
                        with_header = false;
                    }
                    else if (argv[i].StartsWith("+head"))
                    {
                        with_header_always = true;
                    }
                    else if (String.Compare(argv[i], "-j2000") == 0)
                    {
                        iflag |= SwissEph.SEFLG_J2000;
                    }
                    else if (String.Compare(argv[i], "-icrs") == 0)
                    {
                        iflag |= SwissEph.SEFLG_ICRS;
                    }
                    else if (argv[i].StartsWith("-ay"))
                    {
                        do_ayanamsa = true;
                        sid_mode = int.Parse(argv[i] + 3);
                        //sweph.swe_set_sid_mode(sid_mode, 0, 0);
                    }
                    else if (argv[i].StartsWith("-sidt0"))
                    {
                        iflag |= SwissEph.SEFLG_SIDEREAL;
                        sid_mode = int.Parse(argv[i] + 6);
                        if (sid_mode == 0)
                            sid_mode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
                        sid_mode |= SwissEph.SE_SIDBIT_ECL_T0;
                        //sweph.swe_set_sid_mode(sid_mode, 0, 0);
                    }
                    else if (argv[i].StartsWith("-sidsp"))
                    {
                        iflag |= SwissEph.SEFLG_SIDEREAL;
                        sid_mode = int.Parse(argv[i] + 6);
                        if (sid_mode == 0)
                            sid_mode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
                        sid_mode |= SwissEph.SE_SIDBIT_SSY_PLANE;
                        //sweph.swe_set_sid_mode(sid_mode, 0, 0);
                    }
                    else if (argv[i].StartsWith("-sid"))
                    {
                        iflag |= SwissEph.SEFLG_SIDEREAL;
                        sid_mode = int.Parse(argv[i] + 4);
                        //if (sid_mode > 0)
                        //    sweph.swe_set_sid_mode(sid_mode, 0, 0);
                    }
                    else if (String.Compare(argv[i], "-jplhora") == 0)
                    {
                        iflag |= SwissEph.SEFLG_JPLHOR_APPROX;
                    }
                    else if (String.Compare(argv[i], "-jplhor") == 0)
                    {
                        iflag |= SwissEph.SEFLG_JPLHOR;
                    }
                    else if (argv[i].StartsWith("-j"))
                    {
                        begindate = argv[i] + 1;
                    }
                    else if (argv[i].StartsWith("-ejpl"))
                    {
                        whicheph = SwissEph.SEFLG_JPLEPH;
                        if (argv[i].Length > 5)
                        {
                            fname = argv[i].Substring(5);
                        }
                    }
                    else if (argv[i].StartsWith("-edir"))
                    {
                        if (argv[i].Length > 5)
                        {
                            ephepath = argv[i].Substring(5);
                        }
                    }
                    else if (String.Compare(argv[i], "-eswe") == 0)
                    {
                        whicheph = SwissEph.SEFLG_SWIEPH;
                    }
                    else if (String.Compare(argv[i], "-emos") == 0)
                    {
                        whicheph = SwissEph.SEFLG_MOSEPH;
                    }
                    else if (argv[i].StartsWith("-helflag"))
                    {
                        helflag = int.Parse(argv[i] + 8);
                        if (helflag >= SwissEph.SE_HELFLAG_AV)
                            hel_using_AV = true;
                    }
                    else if (String.Compare(argv[i], "-hel") == 0)
                    {
                        iflag |= SwissEph.SEFLG_HELCTR;
                    }
                    else if (String.Compare(argv[i], "-bary") == 0)
                    {
                        iflag |= SwissEph.SEFLG_BARYCTR;
                    }
                    else if (argv[i].StartsWith("-house"))
                    {
                        sout = String.Empty;
                        C.sscanf(argv[i].Substring(6), "%lf,%lf,%c", ref top_long, ref top_lat, ref sout);
                        top_elev = 0;
                        if (!String.IsNullOrEmpty(sout)) ihsy = sout[0];
                        do_houses = true;
                        have_geopos = true;
                    }
                    else if (argv[i].StartsWith("-hsy"))
                    {
                        ihsy = argv[i].Length > 4 ? argv[i][4] : '\0';
                        if (ihsy == '\0') ihsy = 'P';
                        if (argv[i].Length > 5)
                            hpos_meth = int.Parse(argv[i].Substring(5));
                        have_geopos = true;
                    }
                    else if (argv[i].StartsWith("-topo"))
                    {
                        iflag |= SwissEph.SEFLG_TOPOCTR;
                        C.sscanf(argv[i].Substring(5), "%lf,%lf,%lf", ref top_long, ref top_lat, ref top_elev);
                        have_geopos = true;
                    }
                    else if (argv[i].StartsWith("-geopos"))
                    {
                        C.sscanf(argv[i].Substring(7), "%lf,%lf,%lf", ref top_long, ref top_lat, ref top_elev);
                        have_geopos = true;
                    }
                    else if (String.Compare(argv[i], "-true") == 0)
                    {
                        iflag |= SwissEph.SEFLG_TRUEPOS;
                    }
                    else if (String.Compare(argv[i], "-noaberr") == 0)
                    {
                        iflag |= SwissEph.SEFLG_NOABERR;
                    }
                    else if (String.Compare(argv[i], "-nodefl") == 0)
                    {
                        iflag |= SwissEph.SEFLG_NOGDEFL;
                    }
                    else if (String.Compare(argv[i], "-nonut") == 0)
                    {
                        iflag |= SwissEph.SEFLG_NONUT;
                    }
                    else if (String.Compare(argv[i], "-speed3") == 0)
                    {
                        iflag |= SwissEph.SEFLG_SPEED3;
                    }
                    else if (String.Compare(argv[i], "-speed") == 0)
                    {
                        iflag |= SwissEph.SEFLG_SPEED;
                    }
                    else if (argv[i].StartsWith("-testaa"))
                    {
                        whicheph = SwissEph.SEFLG_JPLEPH;
                        fname = SwissEph.SE_FNAME_DE200;
                        if (String.Compare(argv[i].Substring(7), "95") == 0)
                            begindate = "j2449975.5";
                        if (String.Compare(argv[i].Substring(7), "96") == 0)
                            begindate = "j2450442.5";
                        if (String.Compare(argv[i].Substring(7), "97") == 0)
                            begindate = "j2450482.5";
                        fmt = "PADRu";
                        universal_time = false;
                        plsel = "3";
                    }
                    else if (argv[i].StartsWith("-lmt"))
                    {
                        universal_time = true;
                        time_flag |= BIT_TIME_LMT;
                        if (argv[i].Length > 4)
                        {
                            stimein = string.Empty;
                            stimein += argv[i].Substring(4, Math.Min(30, argv[i].Length - 4));
                        }
                    }
                    else if (String.Compare(argv[i], "-lat") == 0)
                    {
                        universal_time = true;
                        time_flag |= BIT_TIME_LAT;
                    }
                    else if (String.Compare(argv[i], "-lunecl") == 0)
                    {
                        special_event = SP_LUNAR_ECLIPSE;
                    }
                    else if (String.Compare(argv[i], "-solecl") == 0)
                    {
                        special_event = SP_SOLAR_ECLIPSE;
                        have_geopos = true;
                    }
                    else if (String.Compare(argv[i], "-short") == 0)
                    {
                        short_output = true;
                    }
                    else if (String.Compare(argv[i], "-occult") == 0)
                    {
                        special_event = SP_OCCULTATION;
                        have_geopos = true;
                    }
                    else if (String.Compare(argv[i], "-hocal") == 0)
                    {
                        /* used to create a listing for inclusion in hocal.c source code */
                        special_mode |= SP_MODE_HOCAL;
                    }
                    else if (String.Compare(argv[i], "-how") == 0)
                    {
                        special_mode |= SP_MODE_HOW;
                    }
                    else if (String.Compare(argv[i], "-total") == 0)
                    {
                        search_flag |= SwissEph.SE_ECL_TOTAL;
                    }
                    else if (String.Compare(argv[i], "-annular") == 0)
                    {
                        search_flag |= SwissEph.SE_ECL_ANNULAR;
                    }
                    else if (String.Compare(argv[i], "-anntot") == 0)
                    {
                        search_flag |= SwissEph.SE_ECL_ANNULAR_TOTAL;
                    }
                    else if (String.Compare(argv[i], "-partial") == 0)
                    {
                        search_flag |= SwissEph.SE_ECL_PARTIAL;
                    }
                    else if (String.Compare(argv[i], "-penumbral") == 0)
                    {
                        search_flag |= SwissEph.SE_ECL_PENUMBRAL;
                    }
                    else if (String.Compare(argv[i], "-noncentral") == 0)
                    {
                        search_flag &= ~SwissEph.SE_ECL_CENTRAL;
                        search_flag |= SwissEph.SE_ECL_NONCENTRAL;
                    }
                    else if (String.Compare(argv[i], "-central") == 0)
                    {
                        search_flag &= ~SwissEph.SE_ECL_NONCENTRAL;
                        search_flag |= SwissEph.SE_ECL_CENTRAL;
                    }
                    else if (String.Compare(argv[i], "-local") == 0)
                    {
                        special_mode |= SP_MODE_LOCAL;
                    }
                    else if (String.Compare(argv[i], "-rise") == 0)
                    {
                        special_event = SP_RISE_SET;
                        have_geopos = true;
                    }
                    else if (String.Compare(argv[i], "-norefrac") == 0)
                    {
                        norefrac = 1;
                    }
                    else if (String.Compare(argv[i], "-disccenter") == 0)
                    {
                        disccenter = 1;
                    }
                    else if (String.Compare(argv[i], "-hindu") == 0)
                    {
                        norefrac = 1;
                        disccenter = 1;
                    }
                    else if (String.Compare(argv[i], "-discbottom") == 0)
                    {
                        discbottom = 1;
                    }
                    else if (String.Compare(argv[i], "-metr") == 0)
                    {
                        special_event = SP_MERIDIAN_TRANSIT;
                        have_geopos = true;
                        /* secret test feature for dieter */
                    }
                    else if (argv[i].StartsWith("-prec"))
                    {
                        j = 0;
                        //astro_models[j] = atoi(argv[i] + 5);
                        //sp = argv[i];
                        //while ((sp2 = strchr(sp, ',')) != NULL)
                        //{
                        //    sp = sp2 + 1;
                        //    j++;
                        //    astro_models[j] = atoi(sp);
                        //}
                        var parts = argv[i].Substring(5).Split(',');
                        foreach (var p in parts)
                        {
                            int xx = 0;
                            int.TryParse(p, out xx);
                            j++;
                            astro_models[j] = xx;
                        }
                        do_set_astro_models = true;
                    }
                    else if (argv[i].StartsWith("-hev"))
                    {
                        special_event = SP_HELIACAL;
                        search_flag = 0;
                        if (argv[i].Length > 4)
                            search_flag = int.Parse(argv[i].Substring(4));
                        have_geopos = true;
                        if (argv[i].Contains("AV")) hel_using_AV = true;
                    }
                    else if (argv[i].StartsWith("-at"))
                    {
                        C.sscanf(argv[i].Substring(3), "%lf,%lf,%lf,%lf", ref datm[0], ref datm[1], ref datm[2], ref datm[3]);
                        sp = argv[i].Substring(3);
                        j = 0;
                        var parts = sp.Split(',');
                        while (j < 4 && j < parts.Length)
                        {
                            datm[j] = C.atof(parts[j]);
                            //sp = strchr(sp, ',');
                            //if (sp != NULL) sp += 1;
                            j++;
                        }
                    }
                    else if (argv[i].StartsWith("-obs"))
                    {
                        C.sscanf(argv[i].Substring(4), "%lf,%lf", ref (dobs[0]), ref (dobs[1]));
                    }
                    else if (argv[i].StartsWith("-opt"))
                    {
                        C.sscanf(argv[i].Substring(4), "%lf,%lf,%lf,%lf,%lf,%lf", ref (dobs[0]), ref (dobs[1]), ref (dobs[2]), ref (dobs[3]), ref (dobs[4]), ref (dobs[5]));
                    }
                    else if (argv[i].StartsWith("-orbel"))
                    {
                        do_orbital_elements = true;
                    }
                    else if (String.Compare(argv[i], "-bwd") == 0)
                    {
                        direction = -1;
                        direction_flag = true;
                    }
                    else if (argv[i].StartsWith("-p"))
                    {
                        spno = argv[i][2];
                        switch (spno)
                        {
                            case 'd':
                                /*
                                case '\0':
                                case ' ':  
                                */
                                plsel = PLSEL_D; break;
                            case 'p': plsel = PLSEL_P; break;
                            case 'h': plsel = PLSEL_H; break;
                            case 'a': plsel = PLSEL_A; break;
                            default: plsel = spno.ToString(); break;
                        }
                    }
                    else if (argv[i].StartsWith("-xs"))
                    {
                        /* number of asteroid */
                        sastno = argv[i].Substring(3);
                    }
                    else if (argv[i].StartsWith("-xf"))
                    {
                        /* name or number of fixed star */
                        star = argv[i].Substring(3);
                    }
                    else if (argv[i].StartsWith("-xz"))
                    {
                        /* number of hypothetical body */
                        shyp = argv[i].Substring(3);
                    }
                    else if (argv[i].StartsWith("-x"))
                    {
                        /* name or number of fixed star */
                        star = argv[i].Substring(2);
                    }
                    else if (argv[i].StartsWith("-n"))
                    {
                        nstep = int.Parse(argv[i].Substring(2));
                        if (nstep == 0)
                            nstep = 20;
                    }
                    else if (argv[i].StartsWith("-i"))
                    {
                        iflag_f = int.Parse(argv[i].Substring(2));
                        if ((iflag_f & SwissEph.SEFLG_XYZ) != 0)
                            fmt = "PX";
                    }
                    else if (argv[i].StartsWith("-s"))
                    {
                        tstep = double.Parse(argv[i].Substring(2));
                        //if (*(argv[i] + strlen(argv[i]) - 1) == 'm')
                        //    step_in_minutes = TRUE;
                        //if (*(argv[i] + strlen(argv[i]) - 1) == 's')
                        //    step_in_seconds = TRUE;
                        if (argv[i].EndsWith("m"))
                            step_in_minutes = true;
                        if (argv[i].EndsWith("s"))
                            step_in_seconds = true;
                    }
                    else if (argv[i].StartsWith("-b"))
                    {
                        begindate = argv[i].Substring(2);
                    }
                    else if (argv[i].StartsWith("-f"))
                    {
                        fmt = argv[i].Substring(2);
                    }
                    else if (argv[i].StartsWith("-g"))
                    {
                        gap = argv[i].Substring(2);
                        if (String.IsNullOrEmpty(gap)) gap = "\t";
                    }
                    else if (argv[i].StartsWith("-d") || argv[i].StartsWith("-D"))
                    {
                        diff_mode = argv[i][1];	/* 'd' or 'D' */
                        sp = argv[i].Substring(2);
                        ipldiff = letter_to_ipl(String.IsNullOrEmpty(sp) ? '\0' : sp[0]);
                        if (ipldiff < 0) ipldiff = SwissEph.SE_SUN;
                        spnam2 = sweph.swe_get_planet_name(ipldiff);
                    }
                    else if (String.Compare(argv[i], "-roundsec") == 0)
                    {
                        round_flag |= BIT_ROUND_SEC;
                    }
                    else if (String.Compare(argv[i], "-roundmin") == 0)
                    {
                        round_flag |= BIT_ROUND_MIN;
                        /*} else if (strncmp(argv[i], "-timeout", 8) == 0) {
                              swe_set_timeout(atoi(argv[i]) + 8);*/
                    }
                    else if (argv[i].StartsWith("-t"))
                    {
                        if (argv[i].Length > 2)
                        {
                            stimein = argv[i].Substring(2, Math.Min(30, argv[i].Length - 2));
                        }
                    }
                    else if (argv[i].StartsWith("-h") || argv[i].StartsWith("-?"))
                    {
                        sp = argv[i].Length > 2 ? argv[i].Substring(2, 1) : String.Empty;
                        if (sp == "c" || sp == String.Empty)
                        {
                            Console.Write(infocmd0);
                            Console.Write(infocmd1);
                            Console.Write(infocmd2);
                            Console.Write(infocmd3);
                            Console.Write(infocmd4);
                            Console.Write(infocmd5);
                            Console.Write(infocmd6);
                        }
                        if (sp == "p" || sp == String.Empty)
                            Console.Write(infoplan);
                        if (sp == "f" || sp == String.Empty)
                        {
                            Console.Write(infoform);
                            Console.Write(infoform2);
                        }
                        if (sp == "d" || sp == String.Empty)
                            Console.Write(infodate);
                        if (sp == "e" || sp == String.Empty)
                            Console.Write(infoexamp);
                        goto end_main;
                    }
                    else
                    {
                        sout = "illegal option ";
                        sout += argv[i];
                        sout += "\n";
                        Console.Write(sout);
                        return 1;
                    }
                }
                if (special_event == SP_OCCULTATION ||
                    special_event == SP_RISE_SET ||
                    special_event == SP_MERIDIAN_TRANSIT ||
                    special_event == SP_HELIACAL
                    )
                {
                    ipl = letter_to_ipl(string.IsNullOrEmpty(plsel) ? '\0' : plsel[0]);
                    if (plsel == "f")
                        ipl = SwissEph.SE_FIXSTAR;
                    else
                        star = String.Empty;
                    if (special_event == SP_OCCULTATION && ipl == 1)
                        ipl = 2; /* no occultation of moon by moon */
                }
                if (!string.IsNullOrEmpty(stimein))
                {
                    if ((spi = stimein.IndexOf(':')) >= 0)
                    {
                        s1 = String.Concat(stimein.Substring(0, spi), ".", stimein.Substring(spi + 1));
                        if ((spi = s1.IndexOf(':')) >= 0)
                        {
                            s1 = String.Concat(s1.Substring(0, spi), s1.Substring(spi + 1));
                        }
                    }
                    thour = double.Parse(s1);
                    thour += (thour < 0 ? -.00005 : .00005);
                    /* h.mmss -> decimal */
                    t = (thour % 1) * 100;
                    j = (int)t;
                    t = (int)((t % 1.0) * 100);
                    thour = (int)thour + j / 60.0 + t / 3600.0;
                }
                //#if HPUNIX
                //  gethostname (hostname, 80);
                //  if (strstr(hostname, "as10") != NULL) 
                //    line_limit = 1000;
                //#endif
#if MSDOS
                Console.OutputEncoding = Encoding.UTF8;
                //SetConsoleOutputCP(65001);	// set console to utf-8,
                // works only from Windows Vista upwards, not on XP.
#endif
                if (with_header)
                {
                    for (i = 0; i < argc; i++)
                    {
                        Console.Write(argv[i]);
                        Console.Write(" ");
                    }
                }
                iflag = (iflag & ~SEFLG_EPHMASK) | whicheph;
                if (fmt.IndexOfAny("SsQ".ToCharArray()) >= 0 && (iflag & SwissEph.SEFLG_SPEED3) == 0)
                    iflag |= SwissEph.SEFLG_SPEED;
                if (String.IsNullOrEmpty(ephepath))
                {
                    if (make_ephemeris_path(iflag, argv[0], ref ephepath) == SwissEph.ERR)
                    {
                        iflag = (iflag & ~SwissEph.SEFLG_EPHMASK) | SwissEph.SEFLG_MOSEPH;
                        whicheph = SwissEph.SEFLG_MOSEPH;
                    }
                }
                if (whicheph != SwissEph.SEFLG_MOSEPH)
                    sweph.swe_set_ephe_path(ephepath);
                if ((whicheph & SwissEph.SEFLG_JPLEPH) != 0)
                    sweph.swe_set_jpl_file(fname);
                /* the following is only a test feature */
                if (do_set_astro_models)
                    sweph.swe_set_astro_models(astro_models); /* secret test feature for dieter */
                if ((iflag & SwissEph.SEFLG_SIDEREAL) != 0 || do_ayanamsa)
                    sweph.swe_set_sid_mode(sid_mode, 0, 0);
                geopos[0] = top_long;
                geopos[1] = top_lat;
                geopos[2] = top_elev;
                sweph.swe_set_topo(top_long, top_lat, top_elev);
                /*swe_set_tid_acc(-25.82);  * to test delta t output */
                while (true)
                {
                    serr = serr_save = serr_warn = String.Empty;
                    if (begindate == null)
                    {
                        Console.Write("\nDate ?");
                        sdate = String.Empty;
                        sdate = Console.ReadLine();
                        if (sdate == null) goto end_main;
                    }
                    else
                    {
                        sdate = String.Empty;
                        sdate = begindate;
                        begindate = ".";  /* to exit afterwards */
                    }
                    if (String.Compare(sdate, "-bary") == 0)
                    {
                        iflag = iflag & ~SwissEph.SEFLG_HELCTR;
                        iflag |= SwissEph.SEFLG_BARYCTR;
                        sdate = String.Empty;
                    }
                    else if (String.Compare(sdate, "-hel") == 0)
                    {
                        iflag = iflag & ~SwissEph.SEFLG_BARYCTR;
                        iflag |= SwissEph.SEFLG_HELCTR;
                        sdate = String.Empty;
                    }
                    else if (String.Compare(sdate, "-geo") == 0)
                    {
                        iflag = iflag & ~SwissEph.SEFLG_BARYCTR;
                        iflag = iflag & ~SwissEph.SEFLG_HELCTR;
                        sdate = String.Empty;
                    }
                    else if (String.Compare(sdate, "-ejpl") == 0)
                    {
                        iflag &= ~SwissEph.SEFLG_EPHMASK;
                        iflag |= SwissEph.SEFLG_JPLEPH;
                        sdate = String.Empty;
                    }
                    else if (String.Compare(sdate, "-eswe") == 0)
                    {
                        iflag &= ~SwissEph.SEFLG_EPHMASK;
                        iflag |= SwissEph.SEFLG_SWIEPH;
                        sdate = String.Empty;
                    }
                    else if (String.Compare(sdate, "-emos") == 0)
                    {
                        iflag &= ~SwissEph.SEFLG_EPHMASK;
                        iflag |= SwissEph.SEFLG_MOSEPH;
                        sdate = String.Empty;
                    }
                    else if (sdate.StartsWith("-xs"))
                    {
                        /* number of asteroid */
                        sastno = sdate.Substring(3);
                        sdate = String.Empty;
                    }
                    sp = sdate;
                    if (sp.StartsWith("."))
                    {
                        goto end_main;
                    }
                    else if (String.IsNullOrEmpty(sp))
                    {
                        sdate = sdate_save;
                    }
                    else
                    {
                        sdate_save = sdate;
                    }
                    if (String.IsNullOrEmpty(sdate))
                    {
                        sdate = C.sprintf("j%f", tjd);
                    }
                    if (sp.StartsWith("j"))
                    {   /* it's a day number */
                        if ((sp2i = sp.IndexOf(',')) >= 0)
                            //*sp2 = '.';
                            sp = String.Concat(sp.Substring(0, sp2i), '.', sp.Substring(sp2i + 1));
                        sdate = sp;
                        C.sscanf(sp.Substring(1), "%lf", ref tjd);
                        if (tjd < 2299160.5)
                            gregflag = SwissEph.SE_JUL_CAL;
                        else
                            gregflag = SwissEph.SE_GREG_CAL;
                        if (sp.Contains("jul"))
                            gregflag = SwissEph.SE_JUL_CAL;
                        else if (sp.Contains("greg"))
                            gregflag = SwissEph.SE_GREG_CAL;
                        sweph.swe_revjul(tjd, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    }
                    else if (sp.StartsWith("+"))
                    {
                        n = int.Parse(sp);
                        if (n == 0) n = 1;
                        tjd += n;
                        sweph.swe_revjul(tjd, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    }
                    else if (sp.StartsWith("-"))
                    {
                        n = int.Parse(sp);
                        if (n == 0) n = -1;
                        tjd += n;
                        sweph.swe_revjul(tjd, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    }
                    else
                    {
                        if (C.sscanf(sp, "%d%*c%d%*c%d", ref jday, ref jmon, ref jyear) < 1) return 1;
                        if ((Int32)jyear * 10000L + (Int32)jmon * 100L + (Int32)jday < 15821015L)
                            gregflag = SwissEph.SE_JUL_CAL;
                        else
                            gregflag = SwissEph.SE_GREG_CAL;
                        if (sp.Contains("jul"))
                            gregflag = SwissEph.SE_JUL_CAL;
                        else if (sp.Contains("greg"))
                            gregflag = SwissEph.SE_GREG_CAL;
                        jut = 0;
                        tjd = sweph.swe_julday(jyear, jmon, jday, jut, gregflag);
                        tjd += thour / 24.0;
                    }
                    if (special_event > 0)
                    {
                        do_special_event(tjd, ipl, star, special_event, special_mode, geopos, datm, dobs, ref serr);
                        //swe_close();
                        return SwissEph.OK;
                    }
                    line_count = 0;
                    for (t = tjd, istep = 1; istep <= nstep; t += tstep, istep++)
                    {
                        if (step_in_minutes)
                            t = tjd + (istep - 1) * tstep / 1440;
                        if (step_in_seconds)
                            t = tjd + (istep - 1) * tstep / 86400;
                        if (t < 2299160.5)
                            gregflag = SwissEph.SE_JUL_CAL;
                        else
                            gregflag = SwissEph.SE_GREG_CAL;
                        if (sdate.Contains("jul"))
                            gregflag = SwissEph.SE_JUL_CAL;
                        else if (sdate.Contains("greg"))
                            gregflag = SwissEph.SE_GREG_CAL;
                        t2 = t;
                        sweph.swe_revjul(t2, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                        if (with_header)
                        {
                            if (with_glp)
                                printf("\npath: %s", sweph.swe_get_library_path());
                            printf("\ndate (dmy) %d.%d.%d", jday, jmon, jyear);
                            if (gregflag != 0)
                                Console.Write(" greg.");
                            else
                                Console.Write(" jul.");
                            t2 = jut + (jut < 0 ? -0.5 : 0.5) / 3600.0;
                            printf("  % 2d:", (int)t2);
                            t2 = (t2 - (Int32)t2) * 60;
                            printf("%02d:", (int)t2);
                            t2 = (t2 - (Int32)t2) * 60;
                            printf("%02d", (int)t2);
                            if (universal_time)
                            {
                                if ((time_flag & BIT_TIME_LMT) != 0)
                                    printf(" LMT");
                                else
                                    printf(" UT");
                            }
                            else
                            {
                                printf(" ET");
                            }
                            printf("\t\tversion %s", sweph.swe_version());
                        }
                        delt = sweph.swe_deltat_ex(t, iflag, ref serr);
                        if (universal_time)
                        {
                            if ((time_flag & BIT_TIME_LMT) != 0)
                            {
                                if (with_header)
                                {
                                    printf("\nLMT: %.9f", t);
                                    t -= geopos[0] / 15.0 / 24.0;
                                }
                            }
                            if (with_header)
                            {
                                printf("\nUT:  %.9f", t);
                            }
                            if (with_header)
                            {
                                printf("     delta t: %f sec", delt * 86400.0);
                            }
                            te = t + delt;
                            tut = t;
                        }
                        else
                        {
                            te = t;
                            tut = t - delt;
                        }
                        iflgret = sweph.swe_calc(te, SwissEph.SE_ECL_NUT, iflag, xobl, ref serr);
                        if (with_header)
                        {
                            printf("\nTT:  %.9f", te);
                            if ((iflag & SwissEph.SEFLG_SIDEREAL) != 0)
                            {
                                if (sweph.swe_get_ayanamsa_ex(te, iflag, out daya, ref serr) == SwissEph.ERR)
                                {
                                    printf("   error in swe_get_ayanamsa_ex(): %s\n", serr);
                                    return 1;
                                }
                                printf("   ayanamsa = %s (%s)", dms(daya, round_flag), sweph.swe_get_ayanamsa_name(sid_mode));
                            }
                            if (have_geopos)
                            {
                                printf("\ngeo. long %f, lat %f, alt %f", geopos[0], geopos[1], geopos[2]);
                            }
                            if (iflag_f >= 0)
                                iflag = iflag_f;
                            if (plsel.IndexOf('o') < 0)
                            {
                                printf("\n%-15s %s", "Epsilon (true)", dms(xobl[0], round_flag));
                            }
                            if (plsel.IndexOf('n') < 0)
                            {
                                Console.Write("\nNutation        ");
                                Console.Write(dms(xobl[2], round_flag));
                                Console.Write(gap);
                                Console.Write(dms(xobl[3], round_flag));
                            }
                            printf("\n");
                            if (do_houses)
                            {
                                var shsy = sweph.swe_house_name(ihsy);
                                if (!universal_time)
                                {
                                    do_houses = false;
                                    printf("option -house requires option -ut for Universal Time\n");
                                }
                                else
                                {
                                    s1 = dms(top_long, round_flag);
                                    s2 = dms(top_lat, round_flag);
                                    printf("Houses system %c (%s) for long=%s, lat=%s\n", ihsy, shsy, s1, s2);
                                }
                            }
                        }
                        if (with_header && !with_header_always)
                            with_header = false;
                        if (do_ayanamsa)
                        {
                            if (sweph.swe_get_ayanamsa_ex(te, iflag, out daya, ref serr) == SwissEph.ERR)
                            {
                                printf("   error in swe_get_ayanamsa_ex(): %s\n", serr);
                                return 1;
                            }
                            Console.Write("Ayanamsa");
                            Console.Write(gap);
                            Console.Write(dms(daya, round_flag));
                            Console.Write("\n");
                            /*printf("Ayanamsa%s%s\n", gap, dms(daya, round_flag));*/
                            continue;
                        }
                        if (t == tjd && plsel.IndexOf('e') >= 0)
                        {
                            if (list_hor)
                            {
                                is_first = true;
                                //for (psp = plsel; *psp != '\0'; psp++)
                                for (var pspi = 0; pspi < plsel.Length; pspi++)
                                {
                                    psp = plsel[pspi];
                                    if (psp == 'e') continue;
                                    ipl = letter_to_ipl(psp);
                                    spnam = string.Empty;
                                    if (ipl >= SwissEph.SE_SUN && ipl <= SwissEph.SE_VESTA)
                                        spnam = sweph.swe_get_planet_name(ipl);
                                    print_line(MODE_LABEL, is_first);
                                    is_first = false;
                                }
                                printf("\n");
                            }
                            else
                            {
                                print_line(MODE_LABEL, true);
                            }
                        }
                        is_first = true;
                        for (var pspi = 0; pspi < plsel.Length; pspi++)
                        {
                            psp = plsel[pspi];
                            if (psp == 'e') continue;
                            ipl = letter_to_ipl(psp);
                            if (ipl == -2)
                            {
                                printf("illegal parameter -p%s\n", plsel);
                                return 1;
                            }
                            if (psp == 'f')
                                ipl = SwissEph.SE_FIXSTAR;
                            else if (psp == 's')
                                ipl = int.Parse(sastno) + 10000;
                            else if (psp == 'z')
                                ipl = int.Parse(shyp) + SwissEph.SE_FICT_OFFSET_1;
                            if ((iflag & SwissEph.SEFLG_HELCTR) != 0)
                            {
                                if (ipl == SwissEph.SE_SUN
                                      || ipl == SwissEph.SE_MEAN_NODE || ipl == SwissEph.SE_TRUE_NODE
                                      || ipl == SwissEph.SE_MEAN_APOG || ipl == SwissEph.SE_OSCU_APOG)
                                    continue;
                            }
                            else if ((iflag & SwissEph.SEFLG_BARYCTR) != 0)
                            {
                                if (ipl == SwissEph.SE_MEAN_NODE || ipl == SwissEph.SE_TRUE_NODE
                                      || ipl == SwissEph.SE_MEAN_APOG || ipl == SwissEph.SE_OSCU_APOG)
                                    continue;
                            }
                            else
                            {         /* geocentric */
                                if (ipl == SwissEph.SE_EARTH && !do_orbital_elements)
                                    continue;
                            }
                            /* ecliptic position */
                            if (iflag_f >= 0)
                                iflag = iflag_f;
                            if (ipl == SwissEph.SE_FIXSTAR)
                            {
                                iflgret = sweph.swe_fixstar(star, te, iflag, x, ref serr);
                                /* magnitude, etc. */
                                if (iflgret != SwissEph.ERR && fmt.IndexOf('=') >= 0)
                                {
                                    double mag = 0;
                                    iflgret = sweph.swe_fixstar_mag(ref star, ref mag, ref serr);
                                    attr[4] = mag;
                                }
                                se_pname = star;
                            }
                            else
                            {
                                iflgret = sweph.swe_calc(te, ipl, iflag, x, ref serr);
                                /* phase, magnitude, etc. */
                                if (iflgret != SwissEph.ERR && fmt.IndexOfAny("+-*/=".ToCharArray()) >= 0)
                                    iflgret = sweph.swe_pheno(te, ipl, iflag, attr, ref serr);
                                se_pname = sweph.swe_get_planet_name(ipl);
                            }
                            if (psp == 'q')
                            {/* delta t */
                                x[0] = sweph.swe_deltat_ex(te, iflag, ref serr) * 86400;
                                x[1] = x[2] = x[3] = 0;
                                se_pname = "Delta T";
                            }
                            if (psp == 'x')
                            {/* sidereal time */
                                x[0] = sweph.swe_degnorm(sweph.swe_sidtime(tut) * 15 + geopos[0]);
                                x[1] = x[2] = x[3] = 0;
                                se_pname = "Sidereal Time";
                            }
                            if (psp == 'o')
                            {/* ecliptic is wanted, remove nutation */
                                x[2] = x[3] = 0;
                                se_pname = "Ecl. Obl.";
                            }
                            if (psp == 'n')
                            {/* nutation is wanted, remove ecliptic */
                                x[0] = x[2];
                                x[1] = x[3];
                                x[2] = x[3] = 0;
                                se_pname = "Nutation";
                            }
                            if (psp == 'y')
                            {/* time equation */
                                iflgret = sweph.swe_time_equ(tut, out (x[0]), ref serr);
                                x[0] *= 86400; /* in seconds */;
                                x[1] = x[2] = x[3] = 0;
                                se_pname = "Time Equ.";
                            }
                            if (iflgret < 0)
                            {
                                if (String.Compare(serr, serr_save) != 0
                                  && (ipl == SwissEph.SE_SUN || ipl == SwissEph.SE_MOON
                                      || ipl == SwissEph.SE_MEAN_NODE || ipl == SwissEph.SE_TRUE_NODE
                                      || ipl == SwissEph.SE_CHIRON || ipl == SwissEph.SE_PHOLUS || ipl == SwissEph.SE_CUPIDO
                                      || ipl >= SwissEph.SE_AST_OFFSET || ipl == SwissEph.SE_FIXSTAR
                                      || psp == 'y'))
                                {
                                    Console.Write("error: ");
                                    Console.Write(serr);
                                    Console.Write("\n");
                                }
                                serr_save = serr;
                            }
                            else if (!String.IsNullOrEmpty(serr) && String.IsNullOrEmpty(serr_warn))
                            {
                                if (!serr.Contains("'seorbel.txt' not found"))
                                    serr_warn = serr;
                            }
                            if (diff_mode != 0)
                            {
                                iflgret = sweph.swe_calc(te, ipldiff, iflag, x2, ref serr);
                                if (iflgret < 0)
                                {
                                    Console.Write("error: ");
                                    Console.Write(serr);
                                    Console.Write("\n");
                                }
                                if (diff_mode == DIFF_DIFF)
                                {
                                    for (i = 1; i < 6; i++)
                                        x[i] -= x2[i];
                                    if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                        x[0] = sweph.swe_difdeg2n(x[0], x2[0]);
                                    else
                                        x[0] = sweph.swe_difrad2n(x[0], x2[0]);
                                }
                                else
                                {	/* DIFF_MIDP */
                                    for (i = 1; i < 6; i++)
                                        x[i] = (x[i] + x2[i]) / 2;
                                    if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                        x[0] = sweph.swe_deg_midp(x[0], x2[0]);
                                    else
                                        x[0] = sweph.swe_rad_midp(x[0], x2[0]);
                                }
                            }
                            /* equator position */
                            if (fmt.IndexOfAny("aADdQ".ToCharArray()) >= 0)
                            {
                                iflag2 = iflag | SwissEph.SEFLG_EQUATORIAL;
                                if (ipl == SwissEph.SE_FIXSTAR)
                                    iflgret = sweph.swe_fixstar(star, te, iflag2, xequ, ref serr);
                                else
                                    iflgret = sweph.swe_calc(te, ipl, iflag2, xequ, ref serr);
                                if (diff_mode != 0)
                                {
                                    iflgret = sweph.swe_calc(te, ipldiff, iflag2, x2, ref serr);
                                    if (diff_mode == DIFF_DIFF)
                                    {
                                        for (i = 1; i < 6; i++)
                                            xequ[i] -= x2[i];
                                        if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                            xequ[0] = sweph.swe_difdeg2n(xequ[0], x2[0]);
                                        else
                                            xequ[0] = sweph.swe_difrad2n(xequ[0], x2[0]);
                                    }
                                    else
                                    {	/* DIFF_MIDP */
                                        for (i = 1; i < 6; i++)
                                            xequ[i] = (xequ[i] + x2[i]) / 2;
                                        if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                            xequ[0] = sweph.swe_deg_midp(xequ[0], x2[0]);
                                        else
                                            xequ[0] = sweph.swe_rad_midp(xequ[0], x2[0]);
                                    }
                                }
                            }
                            /* azimuth and height */
                            if (fmt.IndexOfAny("IiHhKk".ToCharArray()) >= 0)
                            {
                                /* first, get topocentric equatorial positions */
                                iflgt = whicheph | SwissEph.SEFLG_EQUATORIAL | SwissEph.SEFLG_TOPOCTR;
                                if (ipl == SwissEph.SE_FIXSTAR)
                                    iflgret = sweph.swe_fixstar(star, te, iflgt, xt, ref serr);
                                else
                                    iflgret = sweph.swe_calc(te, ipl, iflgt, xt, ref serr);
                                /* to azimuth/height */
                                /* atmospheric pressure "0" has the effect that a value
                                 * of 1013.25 mbar is assumed at 0 m above sea level.
                                 * If the altitude of the observer is given (in geopos[2])
                                 * pressure is estimated according to that */
                                sweph.swe_azalt(tut, SwissEph.SE_EQU2HOR, geopos, datm[0], datm[1], xt, xaz);
                                if (diff_mode != 0)
                                {
                                    iflgret = sweph.swe_calc(te, ipldiff, iflgt, xt, ref serr);
                                    sweph.swe_azalt(tut, SwissEph.SE_EQU2HOR, geopos, datm[0], datm[1], xt, x2);
                                    if (diff_mode == DIFF_DIFF)
                                    {
                                        for (i = 1; i < 3; i++)
                                            xaz[i] -= x2[i];
                                        if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                            xaz[0] = sweph.swe_difdeg2n(xaz[0], x2[0]);
                                        else
                                            xaz[0] = sweph.swe_difrad2n(xaz[0], x2[0]);
                                    }
                                    else
                                    {	/* DIFF_MIDP */
                                        for (i = 1; i < 3; i++)
                                            xaz[i] = (xaz[i] + x2[i]) / 2;
                                        if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                            xaz[0] = sweph.swe_deg_midp(xaz[0], x2[0]);
                                        else
                                            xaz[0] = sweph.swe_rad_midp(xaz[0], x2[0]);
                                    }
                                }
                            }
                            /* ecliptic cartesian position */
                            if (fmt.IndexOfAny("XU".ToCharArray()) >= 0)
                            {
                                iflag2 = iflag | SwissEph.SEFLG_XYZ;
                                if (ipl == SwissEph.SE_FIXSTAR)
                                    iflgret = sweph.swe_fixstar(star, te, iflag2, xcart, ref serr);
                                else
                                    iflgret = sweph.swe_calc(te, ipl, iflag2, xcart, ref serr);
                                if (diff_mode != 0)
                                {
                                    iflgret = sweph.swe_calc(te, ipldiff, iflag2, x2, ref serr);
                                    if (diff_mode == DIFF_DIFF)
                                    {
                                        for (i = 0; i < 6; i++)
                                            xcart[i] -= x2[i];
                                    }
                                    else
                                    {
                                        xcart[i] = (xcart[i] + x2[i]) / 2;
                                    }
                                }
                            }
                            /* equator cartesian position */
                            if (fmt.IndexOfAny("xu".ToCharArray()) >= 0)
                            {
                                iflag2 = iflag | SwissEph.SEFLG_XYZ | SwissEph.SEFLG_EQUATORIAL;
                                if (ipl == SwissEph.SE_FIXSTAR)
                                    iflgret = sweph.swe_fixstar(star, te, iflag2, xcartq, ref serr);
                                else
                                    iflgret = sweph.swe_calc(te, ipl, iflag2, xcartq, ref serr);
                                if (diff_mode != 0)
                                {
                                    iflgret = sweph.swe_calc(te, ipldiff, iflag2, x2, ref serr);
                                    if (diff_mode == DIFF_DIFF)
                                    {
                                        for (i = 0; i < 6; i++)
                                            xcartq[i] -= x2[i];
                                    }
                                    else
                                    {
                                        xcartq[i] = (xcart[i] + x2[i]) / 2;
                                    }
                                }
                            }
                            /* house position */
                            if (fmt.IndexOfAny("gGj".ToCharArray()) >= 0)
                            {
                                armc = sweph.swe_degnorm(sweph.swe_sidtime(tut) * 15 + geopos[0]);
                                for (i = 0; i < 6; i++)
                                    xsv[i] = x[i];
                                if (hpos_meth == 1)
                                    xsv[1] = 0;
                                if (ipl == SwissEph.SE_FIXSTAR)
                                    star2 = star;
                                else
                                    star2 = String.Empty;
                                if (hpos_meth >= 2 && char.ToUpper(ihsy) == 'G')
                                {
                                    sweph.swe_gauquelin_sector(tut, ipl, star2, iflag, hpos_meth, geopos, 0, 0, ref hposj, ref serr);
                                }
                                else
                                {
                                    hposj = sweph.swe_house_pos(armc, geopos[1], xobl[0], ihsy, xsv, ref serr);
                                }
                                if (char.ToUpper(ihsy) == 'G')
                                    hpos = (hposj - 1) * 10;
                                else
                                    hpos = (hposj - 1) * 30;
                                if (diff_mode != 0)
                                {
                                    for (i = 0; i < 6; i++)
                                        xsv[i] = x2[i];
                                    if (hpos_meth == 1)
                                        xsv[1] = 0;
                                    hpos2 = sweph.swe_house_pos(armc, geopos[1], xobl[0], ihsy, xsv, ref serr);
                                    if (Char.ToUpper(ihsy) == 'G')
                                        hpos2 = (hpos2 - 1) * 10;
                                    else
                                        hpos2 = (hpos2 - 1) * 30;
                                    if (diff_mode == DIFF_DIFF)
                                    {
                                        if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                            hpos = sweph.swe_difdeg2n(hpos, hpos2);
                                        else
                                            hpos = sweph.swe_difrad2n(hpos, hpos2);
                                    }
                                    else
                                    {	/* DIFF_MIDP */
                                        if ((iflag & SwissEph.SEFLG_RADIANS) == 0)
                                            hpos = sweph.swe_deg_midp(hpos, hpos2);
                                        else
                                            hpos = sweph.swe_rad_midp(hpos, hpos2);
                                    }
                                }
                            }
                            spnam = se_pname;
                            print_line(0, is_first);
                            is_first = false;
                            if (!list_hor) line_count++;
                            if (do_orbital_elements)
                            {
                                orbital_elements(te, ipl, iflag, ref serr);
                                continue;
                            }
                            if (line_count >= line_limit)
                            {
                                printf("****** line count %d was exceeded\n", line_limit);
                                break;
                            }
                        }         /* for psp */
                        if (list_hor)
                        {
                            printf("\n");
                            line_count++;
                        }
                        if (do_houses)
                        {
                            double[] cusp = new double[100];
                            int iofs;
                            if (char.ToUpper(ihsy) == 'G')
                                nhouses = 36;
                            iofs = nhouses + 1;
                            iflgret = sweph.swe_houses_ex(t, iflag, top_lat, top_long, ihsy, cusp, cusp.GetPointer(iofs));
                            if (iflgret < 0)
                            {
                                if (String.Compare(serr, serr_save) != 0)
                                {
                                    Console.Write("error: ");
                                    Console.Write(serr);
                                    Console.Write("\n");
                                }
                                serr_save = serr;
                            }
                            else
                            {
                                is_first = true;
                                for (ipl = 1; ipl < iofs + 8; ipl++)
                                {
                                    x[0] = cusp[ipl];
                                    x[1] = 0;	/* latitude */
                                    x[2] = 1.0;	/* pseudo radius vector */
                                    if (ipl == iofs + 2)
                                    { /* armc is already equatorial! */
                                        xequ[0] = x[0];
                                        xequ[1] = x[1];
                                        xequ[2] = x[2];
                                    }
                                    else if (fmt.IndexOfAny("aADdQ".ToCharArray()) >= 0)
                                    {
                                        sweph.swe_cotrans(x, xequ, -xobl[0]);
                                    }
                                    if (fmt.IndexOfAny("IiHhKk".ToCharArray()) >= 0)
                                    {
                                        double[] gpos = new double[3];
                                        gpos[0] = top_long;
                                        gpos[1] = top_lat;
                                        gpos[2] = 0;
                                        sweph.swe_azalt(t, SwissEph.SE_ECL2HOR, gpos, datm[0], datm[1], x, xaz);
                                    }
                                    print_line(MODE_HOUSE, is_first);
                                    is_first = false;
                                    if (!list_hor) line_count++;
                                }
                                if (list_hor)
                                {
                                    printf("\n");
                                    line_count++;
                                }
                            }
                        }
                        if (line_count >= line_limit)
                            break;
                    }           /* for tjd */
                    if (!String.IsNullOrEmpty(serr_warn))
                    {
                        printf("\nwarning: ");
                        Console.Write(serr_warn);
                        printf("\n");
                    }
                }             /* while 1 */
                              /* close open files and free allocated space */
                end_main:
                //swe_close();
                return SwissEph.OK;
            }
        }

        static void sweph_OnLoadFile(object sender, LoadFileEventArgs e)
        {
            String fname = e.FileName.Replace("[ephe]", "").Trim('/', '\\');
            String[] paths = String.IsNullOrWhiteSpace(ephepath) ? new String[] { "" } : ephepath.Split(';');
            foreach (var path in paths)
            {
                String f = System.IO.Path.Combine(path, fname);
                if (System.IO.File.Exists(f))
                {
                    e.File = new System.IO.FileStream(f, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                }
            }
        }

        /* This function calculates the geocentric relative distance of a planet,
         * where the closest position has value 1000, and remotest position has 
         * value 0.
         * The value is returned as an integer. The algorithm does not allow 
         * much higher accuracy.
         *
         * With the Moon we measure the distance relative to the maximum and minimum
         * found between 12000 BCE and 16000 CE.
         * If the distance value were given relative to the momentary osculating 
         * ellipse, then the apogee would always have the value 1000 and the perigee
         * the value 0. It is certainly more interesting to know how much it is
         * relative to a greater time range.
         */
        static Int32 get_geocentric_relative_distance(double tjd_et, Int32 ipl, Int32 iflag, ref string serr)
        {
            Int32 iflagi = (iflag & (SEFLG_EPHMASK | SwissEph.SEFLG_HELCTR | SwissEph.SEFLG_BARYCTR));
            Int32 retval;
            double ar = 0;
            double[] xx = new double[6];
            double dmax = 0, dmin = 0, dtrue = 0;
            if (false && ipl == SwissEph.SE_MOON)
            {
                dmax = 0.002718774; // jd = 283030.8
                dmin = 0.002381834; // jd = -1006731.3
                if ((retval = sweph.swe_calc(tjd_et, SwissEph.SE_MOON, iflagi | SwissEph.SEFLG_J2000 | SwissEph.SEFLG_TRUEPOS, xx, ref serr)) == SwissEph.ERR)
                    return 0;
                dtrue = xx[2];
            }
            else
            {
                if (sweph.swe_orbit_max_min_true_distance(tjd_et, ipl, iflagi, ref dmax, ref dmin, ref dtrue, ref serr) == SwissEph.ERR)
                    return 0;
            }
            if (dmax - dmin == 0)
            {
                ar = 0;
            }
            else
            {
                ar = (1 - (dtrue - dmin) / (dmax - dmin)) * 1000.0;
                ar += 0.5; // rounding
            }
            return (Int32)ar;
        }

        /*
         * The string fmt contains a sequence of format specifiers;
         * each character in fmt creates a column, the columns are
         * sparated by the gap string.
         * Time columns tTJyY are only printed, if is_first is TRUE,
         * so that they are not repeated in list_hor (horizontal list) mode.
         * In list_hor mode, no newline is printed.
         */
        static int print_line(int mode, bool is_first)
        {
            //string sp, sp2;
            int spi = 0; char sp;
            int sp2i = 0; char sp2;
            double t2, ju2 = 0;
            double y_frac;
            double ar/*, sinp*/;
            double[] dret = new double[20];
            string slon = string.Empty;
            string pnam = string.Empty;
            bool is_house = ((mode & MODE_HOUSE) != 0);
            bool is_label = ((mode & MODE_LABEL) != 0);
            Int32 iflgret, dar;
            // build planet name column, just in case
            if (is_house)
            {
                if (ipl <= nhouses)
                {
                    pnam = C.sprintf("house %2d       ", ipl);
                }
                else
                {
                    pnam = C.sprintf("%-15s", hs_nam[ipl - nhouses]);
                }
            }
            else if (diff_mode == DIFF_DIFF)
            {
                pnam = C.sprintf("%.3s-%.3s", spnam, spnam2);
            }
            else if (diff_mode == DIFF_MIDP)
            {
                pnam = C.sprintf("%.3s/%.3s", spnam, spnam2);
            }
            else
            {
                pnam = C.sprintf("%-15s", spnam);
            }
            if (list_hor && fmt.IndexOf('P') >= 0)
            {
                slon = C.sprintf("%.8s %s", pnam, "long.");
            }
            else
            {
                slon = C.sprintf("%-14s", "long.");
            }
            for (spi = 0; spi < fmt.Length; spi++)
            {
                sp = fmt[spi];
                if (is_house && "bBsSrRxXuUQnNfF+-*/=".IndexOf(sp) >= 0) continue;
                //if (sp != fmt)
                if (spi > 0)
                    Console.Write(gap);
                //if (sp == fmt && list_hor && !is_first && strchr("yYJtT", *sp) == NULL)
                if (spi == 0 && list_hor && !is_first && "yYJtT".IndexOf(sp) >= 0)
                    //fputs(gap, stdout);
                    Console.Write(gap);
                switch (sp)
                {
                    case 'y':
                        if (list_hor && !is_first)
                        {
                            break;
                        }
                        if (is_label) { printf("year"); break; }
                        printf("%d", jyear);
                        break;
                    case 'Y':
                        if (list_hor && !is_first)
                        {
                            break;
                        }
                        if (is_label) { printf("year"); break; }
                        t2 = sweph.swe_julday(jyear, 1, 1, ju2, gregflag);
                        y_frac = (t - t2) / 365.0;
                        printf("%.2f", jyear + y_frac);
                        break;
                    case 'p':
                        if (is_label) { printf("obj.nr"); break; }
                        if (!is_house && diff_mode == DIFF_DIFF)
                        {
                            printf("%d-%d", ipl, ipldiff);
                        }
                        else if (!is_house && diff_mode == DIFF_MIDP)
                        {
                            printf("%d/%d", ipl, ipldiff);
                        }
                        else
                        {
                            printf("%d", ipl);
                        }
                        break;
                    case 'P':
                        if (is_label) { printf("%-15s", "name"); break; }
                        if (is_house)
                        {
                            if (ipl <= nhouses)
                            {
                                printf("house %2d       ", ipl);
                            }
                            else
                            {
                                printf("%-15s", hs_nam[ipl - nhouses]);
                            }
                        }
                        else if (diff_mode == DIFF_DIFF)
                        {
                            printf("%.3s-%.3s", spnam, spnam2);
                        }
                        else if (diff_mode == DIFF_MIDP)
                        {
                            printf("%.3s/%.3s", spnam, spnam2);
                        }
                        else
                        {
                            printf("%-15s", spnam);
                        }
                        break;
                    case 'J':
                        if (list_hor && !is_first)
                        {
                            break;
                        }
                        if (is_label) { printf("julday"); break; }
                        y_frac = (t - Math.Floor(t)) * 100;
                        if (Math.Floor(y_frac) != y_frac)
                        {
                            printf("%.5f", t);
                        }
                        else
                        {
                            printf("%.2f", t);
                        }
                        break;
                    case 'T':
                        if (list_hor && !is_first)
                        {
                            break;
                        }
                        if (is_label) { printf("date    "); break; }
                        printf("%02d.%02d.%d", jday, jmon, jyear);
                        if (jut != 0 || step_in_minutes || step_in_seconds)
                        {
                            int h, m, s;
                            s = (int)(jut * 3600 + 0.5);
                            h = (int)(s / 3600.0);
                            m = (int)((s % 3600) / 60.0);
                            s %= 60;
                            printf(" %d:%02d:%02d", h, m, s);
                            if (universal_time)
                                printf(" UT");
                            else
                                printf(" ET");
                        }
                        break;
                    case 't':
                        if (list_hor && !is_first)
                        {
                            break;
                        }
                        if (is_label) { printf("date"); break; }
                        printf("%02d%02d%02d", jyear % 100, jmon, jday);
                        break;
                    case 'L':
                        if (is_label) { printf(slon); break; }
                        if (psp == 'q' || psp == 'y') /* delta t or time equation */
                            goto ldec;
                        Console.Write(dms(x[0], round_flag));
                        break;
                    case 'l':
                        if (is_label) { printf(slon); break; }
                        ldec:
                        if (OUTPUT_EXTRA_PRECISION != 0)
                            printf("%# 11.9f", x[0]);
                        else
                            printf("%# 11.7f", x[0]);
                        break;
                    case 'G':
                        if (is_label) { printf("housPos"); break; }
                        Console.Write(dms(hpos, round_flag));
                        break;
                    case 'g':
                        if (is_label) { printf("housPos"); break; }
                        printf("%# 11.7f", hpos);
                        break;
                    case 'j':
                        if (is_label) { printf("houseNr"); break; }
                        printf("%# 11.7f", hposj);
                        break;
                    case 'Z':
                        if (is_label) { printf(slon); break; }
                        Console.Write(dms(x[0], round_flag | BIT_ZODIAC));
                        break;
                    case 'S':
                    case 's':
                        if (fmt.Length > spi + 1 && (fmt[spi + 1] == 'S' || fmt[spi + 1] == 's' || fmt.IndexOfAny("XUxu".ToCharArray()) >= 0))
                        {
                            for (sp2i = 0; sp2i < fmt.Length; sp2i++)
                            {
                                sp2 = fmt[sp2i];
                                if (sp2i > 0)
                                    Console.Write(gap);
                                switch (sp2)
                                {
                                    case 'L':   /* speed! */
                                    case 'Z':   /* speed! */
                                        if (is_label) { printf("lon/day"); break; }
                                        Console.Write(dms(x[3], round_flag));
                                        break;
                                    case 'l':   /* speed! */
                                        if (is_label) { printf("lon/day"); break; }
                                        printf("%11.7f", x[3]);
                                        break;
                                    case 'B':   /* speed! */
                                        if (is_label) { printf("lat/day"); break; }
                                        Console.Write(dms(x[4], round_flag));
                                        break;
                                    case 'b':   /* speed! */
                                        if (is_label) { printf("lat/day"); break; }
                                        printf("%11.7f", x[4]);
                                        break;
                                    case 'A':   /* speed! */
                                        if (is_label) { printf("RA/day"); break; }
                                        Console.Write(dms(xequ[3] / 15, round_flag | SwissEph.SEFLG_EQUATORIAL));
                                        break;
                                    case 'a':   /* speed! */
                                        if (is_label) { printf("RA/day"); break; }
                                        printf("%11.7f", xequ[3]);
                                        break;
                                    case 'D':   /* speed! */
                                        if (is_label) { printf("dcl/day"); break; }
                                        Console.Write(dms(xequ[4], round_flag));
                                        break;
                                    case 'd':   /* speed! */
                                        if (is_label) { printf("dcl/day"); break; }
                                        printf("%11.7f", xequ[4]);
                                        break;
                                    case 'R':   /* speed! */
                                    case 'r':   /* speed! */
                                        if (is_label) { printf("AU/day"); break; }
                                        if (OUTPUT_EXTRA_PRECISION != 0)
                                            printf("%# 16.11f", x[5]);
                                        else
                                            printf("%# 14.9f", x[5]);
                                        break;
                                    case 'U':   /* speed! */
                                    case 'X':   /* speed! */
                                        if (is_label)
                                        {
                                            Console.Write("speed_0");
                                            Console.Write(gap);
                                            Console.Write("speed_1");
                                            Console.Write(gap);
                                            Console.Write("speed_2");
                                            break;
                                        }
                                        if (sp == 'U')
                                            ar = Math.Sqrt(square_sum(xcart));
                                        else
                                            ar = 1;
                                        printf("%# 14.9f", xcart[3] / ar);
                                        Console.Write(gap);
                                        printf("%# 14.9f", xcart[4] / ar);
                                        Console.Write(gap);
                                        printf("%# 14.9f", xcart[5] / ar);
                                        break;
                                    case 'u':   /* speed! */
                                    case 'x':   /* speed! */
                                        if (is_label)
                                        {
                                            Console.Write("speed_0");
                                            Console.Write(gap);
                                            Console.Write("speed_1");
                                            Console.Write(gap);
                                            Console.Write("speed_2");
                                            break;
                                        }
                                        if (sp == 'u')
                                            ar = Math.Sqrt(square_sum(xcartq));
                                        else
                                            ar = 1;
                                        printf("%# 14.9f", xcartq[3] / ar);
                                        Console.Write(gap);
                                        printf("%# 14.9f", xcartq[4] / ar);
                                        Console.Write(gap);
                                        printf("%# 14.9f", xcartq[5] / ar);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (fmt[spi + 1] == 'S' || fmt[spi + 1] == 's')
                            {
                                spi++;
                                sp = fmt[spi];
                            }
                        }
                        else if (sp == 'S')
                        {
                            if (is_label) { printf("deg/day"); break; }
                            Console.Write(dms(x[3], round_flag));
                        }
                        else
                        {
                            if (is_label) { printf("deg/day"); break; }
                            printf("%# 11.7f", x[3]);
                        }
                        break;
                    case 'B':
                        if (is_label) { printf("lat.    "); break; }
                        Console.Write(dms(x[1], round_flag));
                        break;
                    case 'b':
                        if (is_label) { printf("lat.    "); break; }
                        if (OUTPUT_EXTRA_PRECISION != 0)
                            printf("%# 11.9f", x[1]);
                        else
                            printf("%# 11.7f", x[1]);
                        break;
                    case 'A':     /* right ascension */
                        if (is_label) { printf("RA      "); break; }
                        Console.Write(dms(xequ[0] / 15, round_flag | SwissEph.SEFLG_EQUATORIAL));
                        break;
                    case 'a':     /* right ascension */
                        if (is_label) { printf("RA      "); break; }
                        if (OUTPUT_EXTRA_PRECISION != 0)
                            printf("%# 11.9f", xequ[0]);
                        else
                            printf("%# 11.7f", xequ[0]);
                        break;
                    case 'D':     /* declination */
                        if (is_label) { printf("decl      "); break; }
                        Console.Write(dms(xequ[1], round_flag));
                        break;
                    case 'd':     /* declination */
                        if (is_label) { printf("decl      "); break; }
                        if (OUTPUT_EXTRA_PRECISION != 0)
                            printf("%# 11.9f", xequ[1]);
                        else
                            printf("%# 11.7f", xequ[1]);
                        break;
                    case 'I':     /* azimuth */
                        if (is_label) { printf("azimuth"); break; }
                        Console.Write(dms(xaz[0], round_flag));
                        break;
                    case 'i':     /* azimuth */
                        if (is_label) { printf("azimuth"); break; }
                        printf("%# 11.7f", xaz[0]);
                        break;
                    case 'H':     /* height */
                        if (is_label) { printf("height"); break; }
                        Console.Write(dms(xaz[1], round_flag));
                        break;
                    case 'h':     /* height */
                        if (is_label) { printf("height"); break; }
                        printf("%# 11.7f", xaz[1]);
                        break;
                    case 'K':     /* height (apparent) */
                        if (is_label) { printf("hgtApp"); break; }
                        Console.Write(dms(xaz[2], round_flag));
                        break;
                    case 'k':     /* height (apparent) */
                        if (is_label) { printf("hgtApp"); break; }
                        printf("%# 11.7f", xaz[2]);
                        break;
                    case 'R':
                        if (is_label) { printf("distAU   "); break; }
                        printf("%# 14.9f", x[2]);
                        break;
                    case 'r':
                        if (is_label) { printf("dist"); break; }
                        if (ipl == SwissEph.SE_MOON)
                        { /* for moon print parallax */
                            /* geocentric horizontal parallax: */
                            //if (false) {
                            //    sinp = 8.794 / x[2];    /* in seconds of arc */
                            //    ar = sinp * (1 + sinp * sinp * 3.917402e-12);
                            //    /* the factor is 1 / (3600^2 * (180/pi)^2 * 6) */
                            //    printf("%# 13.5f\" %# 13.5f'", ar, ar / 60.0);
                            //}
                            sweph.swe_pheno(te, ipl, iflag, dret, ref serr);
                            printf("%# 13.5f\"", dret[5] * 3600);
                        }
                        else
                        {
                            printf("%# 14.9f", x[2]);
                        }
                        break;
                    case 'q':
                        if (is_label) { printf("reldist"); break; }
                        dar = get_geocentric_relative_distance(te, ipl, iflag, ref serr);
                        printf("% 5d", dar);
                        break;
                    case 'U':
                    case 'X':
                        if (sp == 'U')
                            ar = Math.Sqrt(square_sum(xcart));
                        else
                            ar = 1;
                        printf("%# 14.9f", xcart[0] / ar);
                        Console.Write(gap);
                        printf("%# 14.9f", xcart[1] / ar);
                        Console.Write(gap);
                        printf("%# 14.9f", xcart[2] / ar);
                        break;
                    case 'u':
                    case 'x':
                        if (is_label)
                        {
                            Console.Write("x0");
                            Console.Write(gap);
                            Console.Write("x1");
                            Console.Write(gap);
                            Console.Write("x2");
                            break;
                        }
                        if (sp == 'u')
                            ar = Math.Sqrt(square_sum(xcartq));
                        else
                            ar = 1;
                        printf("%# 14.9f", xcartq[0] / ar);
                        Console.Write(gap);
                        printf("%# 14.9f", xcartq[1] / ar);
                        Console.Write(gap);
                        printf("%# 14.9f", xcartq[2] / ar);
                        break;
                    case 'Q':
                        if (is_label) { printf("Q"); break; }
                        printf("%-15s", spnam);
                        Console.Write(dms(x[0], round_flag));
                        Console.Write(dms(x[1], round_flag));
                        printf("  %# 14.9f", x[2]);
                        Console.Write(dms(x[3], round_flag));
                        Console.Write(dms(x[4], round_flag));
                        printf("  %# 14.9f\n", x[5]);
                        printf("               %s", dms(xequ[0], round_flag));
                        Console.Write(dms(xequ[1], round_flag));
                        printf("                %s", dms(xequ[3], round_flag));
                        Console.Write(dms(xequ[4], round_flag));
                        break;
                    case 'N':
                    case 'n':
                        {
                            double[] xasc = new double[6], xdsc = new double[6];
                            int imeth = (sp == char.ToLower(sp)) ? SwissEph.SE_NODBIT_MEAN : SwissEph.SE_NODBIT_OSCU;
                            iflgret = sweph.swe_nod_aps(te, ipl, iflag, imeth, xasc, xdsc, null, null, ref serr);
                            if (iflgret >= 0 && (ipl <= SwissEph.SE_NEPTUNE || sp == 'N'))
                            {
                                if (is_label)
                                {
                                    Console.Write("nodAsc");
                                    Console.Write(gap);
                                    Console.Write("nodDesc");
                                    break;
                                }
                                printf("%# 11.7f", xasc[0]);
                                Console.Write(gap);
                                printf("%# 11.7f", xdsc[0]);
                            }
                        };
                        break;
                    case 'F':
                    case 'f':
                        if (!is_house)
                        {
                            double[] xfoc = new double[6], xaph = new double[6], xper = new double[6];
                            int imeth = (sp == char.ToLower(sp)) ? SwissEph.SE_NODBIT_MEAN : SwissEph.SE_NODBIT_OSCU;
                            iflgret = sweph.swe_nod_aps(te, ipl, iflag, imeth, null, null, xper, xaph, ref serr);
                            if (iflgret >= 0 && (ipl <= SwissEph.SE_NEPTUNE || sp == 'F'))
                            {
                                if (is_label)
                                {
                                    Console.Write("peri");
                                    Console.Write(gap);
                                    Console.Write("apo");
                                    break;
                                }
                                printf("%# 11.7f", xper[0]);
                                Console.Write(gap);
                                printf("%# 11.7f", xaph[0]);
                            }
                            imeth |= SwissEph.SE_NODBIT_FOPOINT;
                            iflgret = sweph.swe_nod_aps(te, ipl, iflag, imeth, null, null, xper, xfoc, ref serr);
                            if (iflgret >= 0 && (ipl <= SwissEph.SE_NEPTUNE || sp == 'F'))
                            {
                                if (is_label)
                                {
                                    Console.Write(gap);
                                    Console.Write("focus");
                                    break;
                                }
                                Console.Write(gap);
                                printf("%# 11.7f", xfoc[0]);
                            }
                        };
                        break;
                    case '+':
                        if (is_house) break;
                        if (is_label) { printf("phase"); break; }
                        Console.Write(dms(attr[0], round_flag));
                        break;
                    case '-':
                        if (is_label) { printf("phase"); break; }
                        if (is_house) break;
                        printf("  %# 14.9f", attr[1]);
                        break;
                    case '*':
                        if (is_label) { printf("elong"); break; }
                        if (is_house) break;
                        Console.Write(dms(attr[2], round_flag));
                        break;
                    case '/':
                        if (is_label) { printf("diamet"); break; }
                        if (is_house) break;
                        Console.Write(dms(attr[3], round_flag));
                        break;
                    case '=':
                        if (is_label) { printf("magn"); break; }
                        if (is_house) break;
                        printf("  %# 6.2fm", attr[4]);
                        break;
                    case 'V': /* human design gates */
                    case 'v':
                        {
                            double xhds;
                            int igate, iline, ihex;
                            int[] hexa = new int[64] { 1, 43, 14, 34, 9, 5, 26, 11, 10, 58, 38, 54, 61, 60, 41, 19, 13, 49, 30, 55, 37, 63, 22, 36, 25, 17, 21, 51, 42, 3, 27, 24, 2, 23, 8, 20, 16, 35, 45, 12, 15, 52, 39, 53, 62, 56, 31, 33, 7, 4, 29, 59, 40, 64, 47, 6, 46, 18, 48, 57, 32, 50, 28, 44 };
                            if (is_label) { printf("hds"); break; }
                            if (is_house) break;
                            xhds = sweph.swe_degnorm(x[0] - 223.25);
                            ihex = (int)Math.Floor(xhds / 5.625);
                            iline = ((int)(Math.Floor(xhds / 0.9375))) % 6 + 1;
                            igate = hexa[ihex];
                            printf("%2d.%d", igate, iline);
                            if (sp == 'V')
                                printf(" %2d%%", sweph.swe_d2l(100 * ((xhds / 0.9375) % 1.0)));
                            break;
                        }
                }     /* switch */
            }       /* for sp */
            if (!list_hor)
                printf("\n");
            return SwissEph.OK;
        }

        static string dms(double xv, Int32 iflg)
        {
            int izod;
            Int32 k, kdeg, kmin, ksec;
            string c = SwissEph.ODEGREE_STRING;
            string /*sp,*/ s1 = string.Empty;
            string s;
            int sgn;
            if (double.IsNaN(xv))
                return "nan";
            if (xv >= 360)
                xv = 0;
            s = string.Empty;
            if ((iflg & SwissEph.SEFLG_EQUATORIAL) != 0)
                c = "h";
            if (xv < 0)
            {
                xv = -xv;
                sgn = -1;
            }
            else
            {
                sgn = 1;
            }
            if ((iflg & BIT_ROUND_MIN) != 0)
            {
                xv = sweph.swe_degnorm(xv + 0.5 / 60);
            }
            else if ((iflg & BIT_ROUND_SEC) != 0)
            {
                xv = sweph.swe_degnorm(xv + 0.5 / 3600);
            }
            else
            {
                /* rounding 0.9999999999 to 1 */
                if (OUTPUT_EXTRA_PRECISION != 0)
                {
                    xv += (xv < 0 ? -1 : 1) * 0.000000005 / 3600.0;
                }
                else
                {
                    xv += (xv < 0 ? -1 : 1) * 0.00005 / 3600.0;
                }
            }
            if ((iflg & BIT_ZODIAC) != 0)
            {
                izod = (int)(xv / 30);
                xv = (xv % 30.0);
                kdeg = (Int32)xv;
                s = C.sprintf(" %2d %s ", kdeg, zod_nam[izod]);
            }
            else
            {
                kdeg = (Int32)xv;
                s = C.sprintf("%3d%s", kdeg, c);
            }
            xv -= kdeg;
            xv *= 60;
            kmin = (Int32)xv;
            if ((iflg & BIT_ZODIAC) != 0 && (iflg & BIT_ROUND_MIN) != 0)
            {
                s1 = C.sprintf("%2d", kmin);
            }
            else
            {
                s1 = C.sprintf("%2d'", kmin);
            }
            s += s1;
            if ((iflg & BIT_ROUND_MIN) != 0)
                goto return_dms;
            xv -= kmin;
            xv *= 60;
            ksec = (Int32)xv;
            if ((iflg & BIT_ROUND_SEC) != 0)
            {
                s1 = C.sprintf("%2d\"", ksec);
            }
            else
            {
                s1 = C.sprintf("%2d", ksec);
            }
            s += s1;
            if ((iflg & BIT_ROUND_SEC) != 0)
                goto return_dms;
            xv -= ksec;
            if (OUTPUT_EXTRA_PRECISION != 0)
            {
                k = (Int32)(xv * 100000000);
                s1 = C.sprintf(".%08d", k);
            }
            else
            {
                k = (Int32)(xv * 10000);
                s1 = C.sprintf(".%04d", k);
            }
            s += s1;
            return_dms:;
            int spi;
            if (sgn < 0)
            {
                spi = s.IndexOfAny("0123456789".ToCharArray());
                s = String.Concat(s.Substring(0, spi - 1), '-', s.Substring(spi));
            }
            if ((iflg & BIT_LZEROES) != 0)
            {
                //while ((sp = strchr(s + 2, ' ')) != NULL) *sp = '0';
                s = s.Substring(0, 2) + s.Substring(2).Replace(' ', '0');
            }
            return (s);
        }

        static int letter_to_ipl(char letter)
        {
            if (letter >= '0' && letter <= '9')
                return letter - '0' + SwissEph.SE_SUN;
            if (letter >= 'A' && letter <= 'I')
                return letter - 'A' + SwissEph.SE_MEAN_APOG;
            if (letter >= 'J' && letter <= 'Z')
                return letter - 'J' + SwissEph.SE_CUPIDO;
            switch (letter)
            {
                case 'm': return SwissEph.SE_MEAN_NODE;
                case 'c': return SwissEph.SE_INTP_APOG;
                case 'g': return SwissEph.SE_INTP_PERG;
                case 'n':
                case 'o': return SwissEph.SE_ECL_NUT;
                case 't': return SwissEph.SE_TRUE_NODE;
                case 'f': return SwissEph.SE_FIXSTAR;
                case 'w': return SwissEph.SE_WALDEMATH;
                case 'e': /* swetest: a line of labels */
                case 'q': /* swetest: delta t */
                case 'y': /* swetest: time equation */
                case 'x': /* swetest: sidereal time */
                case 's': /* swetest: an asteroid, with number given in -xs[number] */
                case 'z': /* swetest: a fictitious body, number given in -xz[number] */
                case 'd': /* swetest: default (main) factors 0123456789mtABC */
                case 'p': /* swetest: main factors ('d') plus main asteroids DEFGHI */
                case 'h': /* swetest: fictitious factors JKLMNOPQRSTUVWXYZw */
                case 'a': /* swetest: all factors, like 'p'+'h' */
                    return -1;
            }
            return -2;
        }

        static Int32 ut_to_lmt_lat(double t_ut, double[] geopos, out double t_ret, ref string serr)
        {
            Int32 iflgret = SwissEph.OK;
            if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
            {
                t_ut += geopos[0] / 360.0;
                if ((time_flag & BIT_TIME_LAT) != 0)
                {
                    iflgret = sweph.swe_lmt_to_lat(t_ut, geopos[0], out t_ut, ref serr);
                }
            }
            t_ret = t_ut;
            return iflgret;
        }

        static Int32 orbital_elements(double tjd_et, Int32 ipl, Int32 iflag, ref string serr)
        {
            Int32 retval;
            double[] dret = new double[20]; double jut = 0;
            Int32 jyear = 0, jmon = 0, jday = 0;
            string sdateperi = string.Empty;
            retval = sweph.swe_get_orbital_elements(tjd_et, ipl, iflag, dret, ref serr);
            if (retval == SwissEph.ERR)
            {
                printf("%s\n", serr);
                return SwissEph.ERR;
            }
            else
            {
                sweph.swe_revjul(dret[14], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                sdateperi = C.sprintf("%2d.%02d.%04d,%s", jday, jmon, jyear, hms(jut, BIT_LZEROES));
                printf("semiaxis         \t%f\neccentricity     \t%f\ninclination      \t%f\nasc. node       \t%f\narg. pericenter  \t%f\npericenter       \t%f\n", dret[0], dret[1], dret[2], dret[3], dret[4], dret[5]);
                printf("mean longitude   \t%f\nmean anomaly     \t%f\necc. anomaly     \t%f\ntrue anomaly     \t%f\n", dret[9], dret[6], dret[8], dret[7]);
                printf("time pericenter  \t%f %s\ndist. pericenter \t%f\ndist. apocenter  \t%f\n", dret[14], sdateperi, dret[15], dret[16]);
                printf("mean daily motion\t%f\nsid. period (y)  \t%f\ntrop. period (y) \t%f\nsynodic cycle (d)\t%f\n", dret[11], dret[10], dret[12], dret[13]);
            }
            return SwissEph.OK;
        }

        static Int32 call_rise_set(double t_ut, Int32 ipl, string star, Int32 whicheph, Int32 special_mode, double[] geopos, ref string serr)
        {
            int ii;
            Int32 rsmi = 0;
            double[] tret = new double[10]; double tret1sv = 0;
            double t0, t1;
            Int32 retc = SwissEph.OK;
            sweph.swe_set_topo(geopos[0], geopos[1], geopos[2]);
            do_printf("\n");
            /* loop over days */
            for (ii = 0; ii < nstep; ii++, t_ut = tret1sv + 0.1)
            {
                sout = String.Empty;
                /* swetest -rise
                 * calculate and print rising and setting */
                if (special_event == SP_RISE_SET)
                {
                    /* rising */
                    rsmi = SwissEph.SE_CALC_RISE;
                    if (norefrac != 0) rsmi |= SwissEph.SE_BIT_NO_REFRACTION;
                    if (disccenter != 0) rsmi |= SwissEph.SE_BIT_DISC_CENTER;
                    if (discbottom != 0) rsmi |= SwissEph.SE_BIT_DISC_BOTTOM;
                    if (sweph.swe_rise_trans(t_ut, ipl, star, whicheph, rsmi, geopos, datm[0], datm[1], ref (tret[0]), ref serr) != SwissEph.OK)
                    {
                        do_printf(serr);
                        Environment.Exit(0);
                    }
                    /* setting */
                    rsmi = SwissEph.SE_CALC_SET;
                    if (norefrac != 0) rsmi |= SwissEph.SE_BIT_NO_REFRACTION;
                    if (disccenter != 0) rsmi |= SwissEph.SE_BIT_DISC_CENTER;
                    if (discbottom != 0) rsmi |= SwissEph.SE_BIT_DISC_BOTTOM;
                    if (sweph.swe_rise_trans(t_ut, ipl, star, whicheph, rsmi, geopos, datm[0], datm[1], ref (tret[1]), ref serr) != SwissEph.OK)
                    {
                        do_printf(serr);
                        Environment.Exit(0);
                    }
                    tret1sv = tret[1];
                    if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                    {
                        retc = ut_to_lmt_lat(tret[0], geopos, out (tret[0]), ref serr);
                        retc = ut_to_lmt_lat(tret[1], geopos, out (tret[1]), ref serr);
                    }
                    t0 = 0; t1 = 0;
                    sout = "rise     ";
                    if (tret[0] == 0 || tret[0] > tret[1])
                    {
                        sout = "         -                     ";
                    }
                    else
                    {
                        t0 = tret[0];
                        sweph.swe_revjul(tret[0], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                        sout += C.sprintf("%2d.%02d.%04d\t%s    ", jday, jmon, jyear, hms(jut, BIT_LZEROES));
                    }
                    sout += "set      ";
                    if (tret[1] == 0)
                    {
                        sout += "         -                     \n";
                    }
                    else
                    {
                        t1 = tret[1];
                        sweph.swe_revjul(tret[1], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                        sout += C.sprintf("%2d.%02d.%04d\t%s    ", jday, jmon, jyear, hms(jut, BIT_LZEROES));
                    }
                    if (t0 != 0 && t1 != 0)
                    {
                        t0 = (t1 - t0) * 24;
                        sout += C.sprintf("dt = %s", hms(t0, BIT_LZEROES));
                    }
                    sout += "\n";
                    do_printf(sout);
                }
                /* swetest -metr
                 * calculate and print transits over meridian (midheaven and lower
                 * midheaven */
                if (special_event == SP_MERIDIAN_TRANSIT)
                {
                    /* transit over midheaven */
                    if (sweph.swe_rise_trans(t_ut, ipl, star, whicheph, SwissEph.SE_CALC_MTRANSIT, geopos, datm[0], datm[1], ref (tret[0]), ref serr) != SwissEph.OK)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    /* transit over lower midheaven */
                    if (sweph.swe_rise_trans(t_ut, ipl, star, whicheph, SwissEph.SE_CALC_ITRANSIT, geopos, datm[0], datm[1], ref (tret[1]), ref serr) != SwissEph.OK)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    tret1sv = tret[1];
                    if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                    {
                        retc = ut_to_lmt_lat(tret[0], geopos, out (tret[0]), ref serr);
                        retc = ut_to_lmt_lat(tret[1], geopos, out (tret[1]), ref serr);
                    }
                    sout = "mtransit ";
                    if (tret[0] == 0) sout += "         -                     ";
                    else
                    {
                        sweph.swe_revjul(tret[0], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                        sout += C.sprintf("%2d.%02d.%04d\t%s    ", jday, jmon, jyear, hms(jut, BIT_LZEROES));
                    }
                    sout += "itransit ";
                    if (tret[1] == 0) sout += "         -                     \n";
                    else
                    {
                        sweph.swe_revjul(tret[1], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                        sout += C.sprintf("%2d.%02d.%04d\t%s\n", jday, jmon, jyear, hms(jut, BIT_LZEROES));
                    }
                    do_printf(sout);
                }
            }
            return retc;
        }

        static Int32 call_lunar_eclipse(double t_ut, Int32 whicheph, Int32 special_mode, double[] geopos, ref string serr)
        {
            int i, ii, retc, eclflag, ecl_type = 0;
            int ihou, imin, isec, isgn;
            double dfrc, dt; double[] attr = new double[30];
            string s1 = string.Empty, sout_short = string.Empty, sfmt = string.Empty;
            /* no selective eclipse type set, set all */
            if ((search_flag & SwissEph.SE_ECL_ALLTYPES_LUNAR) == 0)
                search_flag |= SwissEph.SE_ECL_ALLTYPES_LUNAR;
            do_printf("\n");
            for (ii = 0; ii < nstep; ii++, t_ut += direction)
            {
                sout = String.Empty;
                /* swetest -lunecl -how 
                 * type of lunar eclipse and percentage for a given time: */
                if ((special_mode & SP_MODE_HOW) != 0)
                {
                    if ((eclflag = sweph.swe_lun_eclipse_how(t_ut, whicheph, geopos, attr, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    else
                    {
                        if ((eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                        {
                            ecl_type = ECL_LUN_TOTAL;
                            sfmt = "total lunar eclipse: %f o/o \n";
                        }
                        else if ((eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                        {
                            ecl_type = ECL_LUN_PARTIAL;
                            sfmt = "partial lunar eclipse: %f o/o \n";
                        }
                        else if ((eclflag & SwissEph.SE_ECL_PENUMBRAL) != 0)
                        {
                            ecl_type = ECL_LUN_PENUMBRAL;
                            sfmt = "penumbral lunar eclipse: %f o/o \n";
                        }
                        else
                        {
                            sfmt = "no lunar eclipse \n";
                        }
                        sout = sfmt;
                        if (sfmt.IndexOf('%') >= 0)
                        {
                            sout = C.sprintf(sfmt, attr[0]);
                        }
                        do_printf(sout);
                    }
                    continue;
                }
                /* swetest -lunecl 
                 * find next lunar eclipse: */
                /* locally visible lunar eclipse */
                if ((special_mode & SP_MODE_LOCAL) != 0)
                {
                    if ((eclflag = sweph.swe_lun_eclipse_when_loc(t_ut, whicheph, geopos, tret, attr, direction_flag, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                    {
                        for (i = 0; i < 10; i++)
                        {
                            if (tret[i] != 0)
                                retc = ut_to_lmt_lat(tret[i], geopos, out (tret[i]), ref serr);
                        }
                    }
                    t_ut = tret[0];
                    if ((eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                    {
                        sout = "total   ";
                        ecl_type = ECL_LUN_TOTAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_PENUMBRAL) != 0)
                    {
                        sout = "penumb. ";
                        ecl_type = ECL_LUN_PENUMBRAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                    {
                        sout = "partial ";
                        ecl_type = ECL_LUN_PARTIAL;
                    }
                    sout = "lunar eclipse\t";
                    sweph.swe_revjul(t_ut, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    /*if ((eclflag = swe_lun_eclipse_how(t_ut, whicheph, geopos, attr, serr)) == ERR) {
                      do_printf(serr);
                      return ERR;
                    }*/
                    dt = (tret[3] - tret[2]) * 24 * 60;
                    s1 = C.sprintf("%d min %4.2f sec", (int)dt, (dt % 1.0) * 60);
                    /* short output: 
                     * date, time of day, umbral magnitude, umbral duration, saros series, member number */
                    sout_short = C.sprintf("%s\t%2d.%2d.%4d\t%s\t%.3f\t%s\t%d\t%d\n", sout, jday, jmon, jyear, hms(jut, 0), attr[8], s1, (int)attr[9], (int)attr[10]);
                    sout += C.sprintf("%2d.%02d.%04d\t%s\t%.4f/%.4f\tsaros %d/%d\t%.6f\tdt=%.2f\n", jday, jmon, jyear, hms(jut, BIT_LZEROES), attr[0], attr[1], (int)attr[9], (int)attr[10], t_ut, sweph.swe_deltat(t_ut) * 86400);
                    /* second line:
                     * eclipse times, penumbral, partial, total begin and end */
                    if ((eclflag & SwissEph.SE_ECL_PENUMBBEG_VISIBLE) != 0)
                        sout += C.sprintf("  %s ", hms_from_tjd(tret[6]));
                    else
                        sout += ("      -         ");
                    if ((eclflag & SwissEph.SE_ECL_PARTBEG_VISIBLE) != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[2]));
                    else
                        sout += ("    -         ");
                    if ((eclflag & SwissEph.SE_ECL_TOTBEG_VISIBLE) != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[4]));
                    else
                        sout += ("    -         ");
                    if ((eclflag & SwissEph.SE_ECL_TOTEND_VISIBLE) != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[5]));
                    else
                        sout += ("    -         ");
                    if ((eclflag & SwissEph.SE_ECL_PARTEND_VISIBLE) != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[3]));
                    else
                        sout += ("    -         ");
                    if ((eclflag & SwissEph.SE_ECL_PENUMBEND_VISIBLE) != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[7]));
                    else
                        sout += ("    -         ");
                    sout += C.sprintf("dt=%.1f", sweph.swe_deltat_ex(tret[0], whicheph, ref serr) * 86400.0);
                    sout += ("\n");
                    /* global lunar eclipse */
                }
                else
                {
                    if ((eclflag = sweph.swe_lun_eclipse_when(t_ut, whicheph, search_flag, tret, direction_flag, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    t_ut = tret[0];
                    if ((eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                    {
                        sout = ("total   ");
                        ecl_type = ECL_LUN_TOTAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_PENUMBRAL) != 0)
                    {
                        sout = ("penumb. ");
                        ecl_type = ECL_LUN_PENUMBRAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                    {
                        sout = ("partial ");
                        ecl_type = ECL_LUN_PARTIAL;
                    }
                    sout += ("lunar eclipse\t");
                    if ((eclflag = sweph.swe_lun_eclipse_how(t_ut, whicheph, geopos, attr, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                    {
                        for (i = 0; i < 10; i++)
                        {
                            if (tret[i] != 0)
                                retc = ut_to_lmt_lat(tret[i], geopos, out (tret[i]), ref serr);
                        }
                    }
                    t_ut = tret[0];
                    sweph.swe_revjul(t_ut, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    dt = (tret[3] - tret[2]) * 24 * 60;
                    s1 = C.sprintf("%d min %4.2f sec", (int)dt, (dt % 1.0) * 60);
                    /* short output: 
                     * date, time of day, umbral magnitude, umbral duration, saros series, member number */
                    sout_short = C.sprintf("%s\t%2d.%2d.%4d\t%s\t%.3f\t%s\t%d\t%d\n", sout, jday, jmon, jyear, hms(jut, 0), attr[8], s1, (int)attr[9], (int)attr[10]);
                    sout += C.sprintf("%2d.%02d.%04d\t%s\t%.4f/%.4f\tsaros %d/%d\t%.6f\tdt=%.2f\n", jday, jmon, jyear, hms(jut, BIT_LZEROES), attr[0], attr[1], (int)attr[9], (int)attr[10], t_ut, sweph.swe_deltat_ex(t_ut, whicheph, ref serr) * 86400);
                    /* second line:
                     * eclipse times, penumbral, partial, total begin and end */
                    sout += C.sprintf("  %s ", hms_from_tjd(tret[6]));
                    if (tret[2] != 0)
                        sout = C.sprintf("%s ", hms_from_tjd(tret[2]));
                    else
                        sout += ("   -         ");
                    if (tret[4] != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[4]));
                    else
                        sout += ("   -         ");
                    if (tret[5] != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[5]));
                    else
                        sout += ("   -         ");
                    if (tret[3] != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[3]));
                    else
                        sout += ("   -         ");
                    sout += C.sprintf("%s", hms_from_tjd(tret[7]));
                    sout += C.sprintf("dt=%.1f", sweph.swe_deltat_ex(tret[0], whicheph, ref serr) * 86400.0);
                    sout += "\n";
                    if ((special_mode & SP_MODE_HOCAL) != 0)
                    {
                        sweph.swe_split_deg(jut, SwissEph.SE_SPLIT_DEG_ROUND_MIN, out ihou, out imin, out isec, out dfrc, out isgn);
                        sout = C.sprintf("\"%04d %02d %02d %02d.%02d %d\",\n", jyear, jmon, jday, ihou, imin, ecl_type);
                    }
                }
                if (short_output)
                    do_printf(sout_short);
                else
                    do_printf(sout);
            }
            return SwissEph.OK;
        }

        static Int32 call_solar_eclipse(double t_ut, Int32 whicheph, Int32 special_mode, double[] geopos, ref string serr)
        {
            int i, ii, retc, eclflag, ecl_type = 0;
            double dt; double[] tret = new double[30], attr = new double[30], geopos_max = new double[3];
            string s1 = string.Empty, s2 = string.Empty, sout_short = string.Empty;
            bool has_found = false;
            /* no selective eclipse type set, set all */
            if ((search_flag & SwissEph.SE_ECL_ALLTYPES_SOLAR) == 0)
                search_flag |= SwissEph.SE_ECL_ALLTYPES_SOLAR;
            /* for local eclipses: set geographic position of observer */
            if ((special_mode & SP_MODE_LOCAL) != 0)
                sweph.swe_set_topo(geopos[0], geopos[1], geopos[2]);
            do_printf("\n");
            for (ii = 0; ii < nstep; ii++, t_ut += direction)
            {
                sout = String.Empty;
                /* swetest -solecl -local -geopos...
                 * find next solar eclipse observable from a given geographic position */
                if ((special_mode & SP_MODE_LOCAL) != 0)
                {
                    if ((eclflag = sweph.swe_sol_eclipse_when_loc(t_ut, whicheph, geopos, tret, attr, direction_flag, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    else
                    {
                        has_found = false;
                        t_ut = tret[0];
                        if ((search_flag & SwissEph.SE_ECL_TOTAL) != 0 && (eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                        {
                            sout = ("total   ");
                            has_found = true;
                            ecl_type = ECL_SOL_TOTAL;
                        }
                        if ((search_flag & SwissEph.SE_ECL_ANNULAR) != 0 && (eclflag & SwissEph.SE_ECL_ANNULAR) != 0)
                        {
                            sout = ("annular ");
                            has_found = true;
                            ecl_type = ECL_SOL_ANNULAR;
                        }
                        if ((search_flag & SwissEph.SE_ECL_PARTIAL) != 0 && (eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                        {
                            sout = ("partial ");
                            has_found = true;
                            ecl_type = ECL_SOL_PARTIAL;
                        }
                        if (!has_found)
                        {
                            ii--;
                        }
                        else
                        {
                            sweph.swe_calc(t_ut + sweph.swe_deltat_ex(t_ut, whicheph, ref serr), SwissEph.SE_ECL_NUT, 0, x, ref serr);
                            if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                            {
                                for (i = 0; i < 10; i++)
                                {
                                    if (tret[i] != 0)
                                        retc = ut_to_lmt_lat(tret[i], geopos, out (tret[i]), ref serr);
                                }
                            }
                            t_ut = tret[0];
                            sweph.swe_revjul(t_ut, gregflag, ref jyear, ref jmon, ref jday, ref jut);
                            dt = (tret[3] - tret[2]) * 24 * 60;
                            sout += C.sprintf("%2d.%02d.%04d\t%s\t%.4f/%.4f/%.4f\tsaros %d/%d\t%.6f\n", jday, jmon, jyear, hms(jut, BIT_LZEROES), attr[8], attr[0], attr[2], (int)attr[9], (int)attr[10], t_ut);
                            sout += C.sprintf("\t%d min %4.2f sec\t", (int)dt, (dt % 1.0) * 60);
                            if ((eclflag & SwissEph.SE_ECL_1ST_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[1]));
                            else
                                sout += ("   -         ");
                            if ((eclflag & SwissEph.SE_ECL_2ND_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[2]));
                            else
                                sout += ("   -         ");
                            if ((eclflag & SwissEph.SE_ECL_3RD_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[3]));
                            else
                                sout += ("   -         ");
                            if ((eclflag & SwissEph.SE_ECL_4TH_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[4]));
                            else
                                sout += ("   -         ");
                            //#if 0
                            //      sprintf(sout + strlen(sout), "\t%d min %4.2f sec   %s %s %s %s", 
                            //                (int) dt, fmod(dt, 1) * 60, 
                            //                strcpy(s1, hms(fmod(tret[1] + 0.5, 1) * 24, BIT_LZEROES)), 
                            //                strcpy(s3, hms(fmod(tret[2] + 0.5, 1) * 24, BIT_LZEROES)), 
                            //                strcpy(s4, hms(fmod(tret[3] + 0.5, 1) * 24, BIT_LZEROES)),
                            //                strcpy(s2, hms(fmod(tret[4] + 0.5, 1) * 24, BIT_LZEROES)));
                            //#endif
                            sout += C.sprintf("dt=%.1f", sweph.swe_deltat_ex(tret[0], whicheph, ref serr) * 86400.0);
                            sout += ("\n");
                            do_printf(sout);
                        }
                    }
                }   /* endif search_local */
                /* swetest -solecl
                 * find next solar eclipse observable from anywhere on earth */
                if (0 == (special_mode & SP_MODE_LOCAL))
                {
                    if ((eclflag = sweph.swe_sol_eclipse_when_glob(t_ut, whicheph, search_flag, tret, direction_flag, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    t_ut = tret[0];
                    if ((eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                    {
                        sout = ("total   ");
                        ecl_type = ECL_SOL_TOTAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_ANNULAR) != 0)
                    {
                        sout = ("annular ");
                        ecl_type = ECL_SOL_ANNULAR;
                    }
                    if ((eclflag & SwissEph.SE_ECL_ANNULAR_TOTAL) != 0)
                    {
                        sout = ("ann-tot ");
                        ecl_type = ECL_SOL_ANNULAR;	/* by Alois: what is this ? */
                    }
                    if ((eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                    {
                        sout = ("partial ");
                        ecl_type = ECL_SOL_PARTIAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_NONCENTRAL) != 0 && 0 == (eclflag & SwissEph.SE_ECL_PARTIAL))
                        sout += ("non-central ");
                    sweph.swe_sol_eclipse_where(t_ut, whicheph, geopos_max, attr, ref serr);
                    if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                    {
                        for (i = 0; i < 10; i++)
                        {
                            if (tret[i] != 0)
                                retc = ut_to_lmt_lat(tret[i], geopos, out (tret[i]), ref serr);
                        }
                    }
                    sweph.swe_revjul(tret[0], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    sout_short = C.sprintf("%s\t%2d.%2d.%4d\t%s\t%.3f", sout, jday, jmon, jyear, hms(jut, 0), attr[8]);
                    sout += C.sprintf("%2d.%02d.%04d\t%s\t%f km\t%.4f/%.4f/%.4f\tsaros %d/%d\t%.6f\n", jday, jmon, jyear, hms(jut, 0), attr[3], attr[8], attr[0], attr[2], (int)attr[9], (int)attr[10], tret[0]);
                    sout += C.sprintf("\t%s ", hms_from_tjd(tret[2]));
                    if (tret[4] != 0)
                    {
                        sout += C.sprintf("%s ", hms_from_tjd(tret[4]));
                    }
                    else
                    {
                        sout += ("   -         ");
                    }
                    if (tret[5] != 0)
                    {
                        sout += C.sprintf("%s ", hms_from_tjd(tret[5]));
                    }
                    else
                    {
                        sout += ("   -         ");
                    }
                    sout += C.sprintf("%s", hms_from_tjd(tret[3]));
                    sout += C.sprintf("dt=%.1f", sweph.swe_deltat_ex(tret[0], whicheph, ref serr) * 86400.0);
                    sout += "\n";
                    s1 = dms(geopos_max[0], BIT_ROUND_MIN);
                    s2 = dms(geopos_max[1], BIT_ROUND_MIN);
                    sout += C.sprintf("\t%s\t%s", s1, s2);
                    sout += ("\t");
                    sout_short += ("\t");
                    if (0 == (eclflag & SwissEph.SE_ECL_PARTIAL) && 0 == (eclflag & SwissEph.SE_ECL_NONCENTRAL))
                    {
                        if ((eclflag = sweph.swe_sol_eclipse_when_loc(t_ut - 10, whicheph, geopos_max, tret, attr, false, ref serr)) == SwissEph.ERR)
                        {
                            do_printf(serr);
                            return SwissEph.ERR;
                        }
                        if (Math.Abs(tret[0] - t_ut) > 2)
                        {
                            do_printf("when_loc returns wrong date\n");
                        }
                        dt = (tret[3] - tret[2]) * 24 * 60;
                        s1 = C.sprintf("%d min %4.2f sec", (int)dt, (dt % 1.0) * 60);
                        sout += (s1);
                        sout_short += (s1);
                    }
                    sout_short += C.sprintf("\t%d\t%d", (int)attr[9], (int)attr[10]);
                    sout += ("\n");
                    sout_short += ("\n");
                    if ((special_mode & SP_MODE_HOCAL) != 0)
                    {
                        int ihou, imin, isec, isgn;
                        double dfrc;
                        sweph.swe_split_deg(jut, SwissEph.SE_SPLIT_DEG_ROUND_MIN, out ihou, out imin, out isec, out dfrc, out isgn);
                        sout = C.sprintf("\"%04d %02d %02d %02d.%02d %d\",\n", jyear, jmon, jday, ihou, imin, ecl_type);
                    }
                    /*printf("len=%ld\n", strlen(sout));*/
                    if (short_output)
                        do_printf(sout_short);
                    else
                        do_printf(sout);
                }
            }
            return SwissEph.OK;
        }

        static Int32 call_lunar_occultation(double t_ut, Int32 ipl, string star, Int32 whicheph, Int32 special_mode, double[] geopos, ref string serr)
        {
            int i, ii, ecl_type = 0, eclflag, retc;
            double dt; double[] tret = new double[30], attr = new double[30], geopos_max = new double[3];
            string s1 = String.Empty, s2 = String.Empty;
            bool has_found = false;
            int nloops = 0;
            /* no selective eclipse type set, set all */
            if ((search_flag & SwissEph.SE_ECL_ALLTYPES_SOLAR) == 0)
                search_flag |= SwissEph.SE_ECL_ALLTYPES_SOLAR;
            /* for local occultations: set geographic position of observer */
            if ((special_mode & SP_MODE_LOCAL) != 0)
                sweph.swe_set_topo(geopos[0], geopos[1], geopos[2]);
            do_printf("\n");
            for (ii = 0; ii < nstep; ii++)
            {
                sout = String.Empty;
                nloops++;
                if (nloops > SEARCH_RANGE_LUNAR_CYCLES)
                {
                    serr = C.sprintf("event search ended after %d lunar cycles at jd=%f\n", SEARCH_RANGE_LUNAR_CYCLES, t_ut);
                    do_printf(serr);
                    return SwissEph.ERR;
                }
                if ((special_mode & SP_MODE_LOCAL) != 0)
                {
                    /* * local search for occultation, test one lunar cycle only (SE_ECL_ONE_TRY) */
                    if (ipl != SwissEph.SE_SUN)
                    {
                        search_flag &= ~(SwissEph.SE_ECL_ANNULAR | SwissEph.SE_ECL_ANNULAR_TOTAL);
                        if (search_flag == 0)
                            search_flag = SwissEph.SE_ECL_ALLTYPES_SOLAR;
                    }
                    if ((eclflag = sweph.swe_lun_occult_when_loc(t_ut, ipl, star, whicheph, geopos, tret, attr, direction_flag/*|SwissEph.SE_ECL_ONE_TRY*/, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    else if (eclflag == 0)
                    {  /* event not found, try next conjunction */
                        t_ut = tret[0] + direction * 10;  /* try again with start date increased by 10 */
                        ii--;
                    }
                    else
                    {
                        t_ut = tret[0];
                        if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                        {
                            for (i = 0; i < 10; i++)
                            {
                                if (tret[i] != 0)
                                    retc = ut_to_lmt_lat(tret[i], geopos, out (tret[i]), ref serr);
                            }
                        }
                        has_found = false;
                        sout = String.Empty;
                        if ((search_flag & SwissEph.SE_ECL_TOTAL) != 0 && (eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                        {
                            sout += ("total");
                            has_found = true;
                            ecl_type = ECL_SOL_TOTAL;
                        }
                        if ((search_flag & SwissEph.SE_ECL_ANNULAR) != 0 && (eclflag & SwissEph.SE_ECL_ANNULAR) != 0)
                        {
                            sout += ("annular");
                            has_found = true;
                            ecl_type = ECL_SOL_ANNULAR;
                        }
                        if ((search_flag & SwissEph.SE_ECL_PARTIAL) != 0 && (eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                        {
                            sout += ("partial");
                            has_found = true;
                            ecl_type = ECL_SOL_PARTIAL;
                        }
                        if (ipl != SwissEph.SE_SUN)
                        {
                            if ((eclflag & SwissEph.SE_ECL_OCC_BEG_DAYLIGHT) != 0 && (eclflag & SwissEph.SE_ECL_OCC_END_DAYLIGHT) != 0)
                                sout += ("(daytime)"); /* occultation occurs during the day */
                            else if ((eclflag & SwissEph.SE_ECL_OCC_BEG_DAYLIGHT) != 0)
                                sout += ("(sunset) "); /* occultation occurs during the day */
                            else if ((eclflag & SwissEph.SE_ECL_OCC_END_DAYLIGHT) != 0)
                                sout += ("(sunrise)"); /* occultation occurs during the day */
                        }
                        while (sout.Length < 17)
                            sout += (" ");
                        if (!has_found)
                        {
                            ii--;
                        }
                        else
                        {
                            sweph.swe_calc_ut(t_ut, SwissEph.SE_ECL_NUT, 0, x, ref serr);
                            sweph.swe_revjul(tret[0], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                            dt = (tret[3] - tret[2]) * 24 * 60;
                            sout += C.sprintf("%2d.%02d.%04d\t%s\t%fo/o\n", jday, jmon, jyear, hms(jut, BIT_LZEROES), attr[0]);
                            sout += C.sprintf("\t%d min %4.2f sec\t", (int)dt, (dt % 1.0) * 60);
                            if ((eclflag & SwissEph.SE_ECL_1ST_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[1]));
                            else
                                sout += ("   -         ");
                            if ((eclflag & SwissEph.SE_ECL_2ND_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[2]));
                            else
                                sout += ("   -         ");
                            if ((eclflag & SwissEph.SE_ECL_3RD_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[3]));
                            else
                                sout += ("   -         ");
                            if ((eclflag & SwissEph.SE_ECL_4TH_VISIBLE) != 0)
                                sout += C.sprintf("%s ", hms_from_tjd(tret[4]));
                            else
                                sout += ("   -         ");
                            //#if 0
                            //      sprintf(sout + strlen(sout), "\t%d min %4.2f sec   %s %s %s %s", 
                            //                (int) dt, fmod(dt, 1) * 60, 
                            //                strcpy(s1, hms(fmod(tret[1] + 0.5, 1) * 24, BIT_LZEROES)), 
                            //                strcpy(s3, hms(fmod(tret[2] + 0.5, 1) * 24, BIT_LZEROES)), 
                            //                strcpy(s4, hms(fmod(tret[3] + 0.5, 1) * 24, BIT_LZEROES)),
                            //                strcpy(s2, hms(fmod(tret[4] + 0.5, 1) * 24, BIT_LZEROES)));
                            //#endif
                            sout += C.sprintf("dt=%.1f", sweph.swe_deltat_ex(tret[0], whicheph, ref serr) * 86400.0);
                            sout += ("\n");
                            do_printf(sout);
                        }
                    }
                }   /* endif search_local */
                if (0 == (special_mode & SP_MODE_LOCAL))
                {
                    /* * global search for occultations, test one lunar cycle only (SE_ECL_ONE_TRY) */
                    if ((eclflag = sweph.swe_lun_occult_when_glob(t_ut, ipl, star, whicheph, search_flag, tret, direction_flag/*|SE_ECL_ONE_TRY*/, ref serr)) == SwissEph.ERR)
                    {
                        do_printf(serr);
                        return SwissEph.ERR;
                    }
                    if (eclflag == 0)
                    { /* no occltation was found at next conjunction, try next conjunction */
                        t_ut = tret[0] + direction;
                        ii--;
                        continue;
                    }
                    if ((eclflag & SwissEph.SE_ECL_TOTAL) != 0)
                    {
                        sout = ("total   ");
                        ecl_type = ECL_SOL_TOTAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_ANNULAR) != 0)
                    {
                        sout = ("annular ");
                        ecl_type = ECL_SOL_ANNULAR;
                    }
                    if ((eclflag & SwissEph.SE_ECL_ANNULAR_TOTAL) != 0)
                    {
                        sout = ("ann-tot ");
                        ecl_type = ECL_SOL_ANNULAR;	/* by Alois: what is this ? */
                    }
                    if ((eclflag & SwissEph.SE_ECL_PARTIAL) != 0)
                    {
                        sout = ("partial ");
                        ecl_type = ECL_SOL_PARTIAL;
                    }
                    if ((eclflag & SwissEph.SE_ECL_NONCENTRAL) != 0 && 0 == (eclflag & SwissEph.SE_ECL_PARTIAL))
                        sout += ("non-central ");
                    t_ut = tret[0];
                    sweph.swe_lun_occult_where(t_ut, ipl, star, whicheph, geopos_max, attr, ref serr);
                    if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                    {
                        for (i = 0; i < 10; i++)
                        {
                            if (tret[i] != 0)
                                retc = ut_to_lmt_lat(tret[i], geopos, out (tret[i]), ref serr);
                        }
                    }
                    sweph.swe_revjul(tret[0], gregflag, ref jyear, ref jmon, ref jday, ref jut);
                    sout += C.sprintf("%2d.%02d.%04d\t%s\t%f km\t%f o/o\n", jday, jmon, jyear, hms(jut, BIT_LZEROES), attr[3], attr[0]);
                    sout += C.sprintf("\t%s ", hms_from_tjd(tret[2]));
                    if (tret[4] != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[4]));
                    else
                        sout += ("   -         ");
                    if (tret[5] != 0)
                        sout += C.sprintf("%s ", hms_from_tjd(tret[5]));
                    else
                        sout += ("   -         ");
                    sout += C.sprintf("%s", hms_from_tjd(tret[3]));
                    sout += C.sprintf("dt=%.1f", sweph.swe_deltat_ex(tret[0], whicheph, ref serr) * 86400.0);
                    sout += "\n";
                    s1 = (dms(geopos_max[0], BIT_ROUND_MIN));
                    s2 = (dms(geopos_max[1], BIT_ROUND_MIN));
                    sout += C.sprintf("\t%s\t%s", s1, s2);
                    if (0 == (eclflag & SwissEph.SE_ECL_PARTIAL) && 0 == (eclflag & SwissEph.SE_ECL_NONCENTRAL))
                    {
                        if ((eclflag = sweph.swe_lun_occult_when_loc(t_ut - 10, ipl, star, whicheph, geopos_max, tret, attr, false, ref serr)) == SwissEph.ERR)
                        {
                            do_printf(serr);
                            return SwissEph.ERR;
                        }
                        if (Math.Abs(tret[0] - t_ut) > 2)
                            do_printf("when_loc returns wrong date\n");
                        dt = (tret[3] - tret[2]) * 24 * 60;
                        sout += C.sprintf("\t%d min %4.2f sec\t", (int)dt, (dt % 1.0) * 60);
                    }
                    sout += ("\n");
                    if ((special_mode & SP_MODE_HOCAL) != 0)
                    {
                        int ihou, imin, isec, isgn;
                        double dfrc;
                        sweph.swe_split_deg(jut, SwissEph.SE_SPLIT_DEG_ROUND_MIN, out ihou, out imin, out isec, out dfrc, out isgn);
                        sout = C.sprintf("\"%04d %02d %02d %02d.%02d %d\",\n", jyear, jmon, jday, ihou, imin, ecl_type);
                    }
                    do_printf(sout);
                }
                t_ut += direction;
            }
            return SwissEph.OK;
        }

        static void do_print_heliacal(double[] dret, Int32 event_type, string obj_name)
        {
            string[] sevtname = new string[]{"",
            "heliacal rising ",
            "heliacal setting",
            "evening first   ",
            "morning last    ",
            "evening rising  ",
            "morning setting ",};
            string stz = "UT";
            string stim0 = String.Empty, stim1 = String.Empty, stim2 = String.Empty;
            if ((time_flag & BIT_TIME_LMT) != 0)
                stz = "LMT";
            if ((time_flag & BIT_TIME_LAT) != 0)
                stz = "LAT";
            sout = String.Empty;
            sweph.swe_revjul(dret[0], gregflag, ref jyear, ref jmon, ref jday, ref jut);
            if (event_type <= 4)
            {
                if (hel_using_AV)
                {
                    stim0 = (hms_from_tjd(dret[0]));
                    remove_whitespace(ref stim0);
                    /* The following line displays only the beginning of visibility. */
                    sout += C.sprintf("%s %s: %d/%02d/%02d %s %s (%.5f)\n", obj_name, sevtname[event_type], jyear, jmon, jday, stim0, stz, dret[0]);
                }
                else
                {
                    /* display the moment of beginning and optimum visibility */
                    stim0 = (hms_from_tjd(dret[0]));
                    stim1 = (hms_from_tjd(dret[1]));
                    stim2 = (hms_from_tjd(dret[2]));
                    remove_whitespace(ref stim0);
                    remove_whitespace(ref stim1);
                    remove_whitespace(ref stim2);
                    sout += C.sprintf("%s %s: %d/%02d/%02d %s %s (%.5f), opt %s, end %s, dur %.1f min\n", obj_name, sevtname[event_type], jyear, jmon, jday, stim0, stz, dret[0], stim1, stim2, (dret[2] - dret[0]) * 1440);
                }
            }
            else
            {
                stim0 = (hms_from_tjd(dret[0]));
                remove_whitespace(ref stim0);
                sout += C.sprintf("%s %s: %d/%02d/%02d %s %s (%f)\n", obj_name, sevtname[event_type], jyear, jmon, jday, stim0, stz, dret[0]);
            }
            do_printf(sout);
        }

        static Int32 call_heliacal_event(double t_ut, Int32 ipl, string star, Int32 whicheph, Int32 special_mode, double[] geopos, double[] datm, double[] dobs, ref string serr)
        {
            int ii, retc, event_type = 0, retflag;
            double[] dret = new double[40]; double tsave1, tsave2 = 0;
            string obj_name;
            helflag |= whicheph;
            /* if invalid heliacal event type was required, set 0 for any type */
            if (search_flag < 0 || search_flag > 6)
                search_flag = 0;
            /* optical instruments used: */
            if (dobs[3] > 0)
                helflag |= SwissEph.SE_HELFLAG_OPTICAL_PARAMS;
            if (hel_using_AV)
                helflag |= SwissEph.SE_HELFLAG_AV;
            if (ipl == SwissEph.SE_FIXSTAR)
                obj_name = star;
            else
                obj_name = sweph.swe_get_planet_name(ipl);
            do_printf("\n");
            for (ii = 0; ii < nstep; ii++, t_ut = dret[0] + 1)
            {
                sout = String.Empty;
                if (search_flag > 0)
                    event_type = search_flag;
                else if (ipl == SwissEph.SE_MOON)
                    event_type = SwissEph.SE_EVENING_FIRST;
                else
                    event_type = SwissEph.SE_HELIACAL_RISING;
                retflag = sweph.swe_heliacal_ut(t_ut, geopos, datm, dobs, obj_name, event_type, helflag, dret, ref serr);
                if (retflag == SwissEph.ERR)
                {
                    do_printf(serr);
                    return SwissEph.ERR;
                }
                if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                {
                    retc = ut_to_lmt_lat(dret[0], geopos, out (dret[0]), ref serr);
                    retc = ut_to_lmt_lat(dret[1], geopos, out (dret[1]), ref serr);
                    retc = ut_to_lmt_lat(dret[2], geopos, out (dret[2]), ref serr);
                }
                do_print_heliacal(dret, event_type, obj_name);
                /* list all events within synodic cycle */
                if (search_flag == 0)
                {
                    if (ipl == SwissEph.SE_VENUS || ipl == SwissEph.SE_MERCURY)
                    {
                        /* we have heliacal rising (morning first), now find morning last */
                        event_type = SwissEph.SE_MORNING_LAST;
                        retflag = sweph.swe_heliacal_ut(dret[0], geopos, datm, dobs, obj_name, event_type, helflag, dret, ref serr);
                        if (retflag == SwissEph.ERR)
                        {
                            do_printf(serr);
                            return SwissEph.ERR;
                        }
                        if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                        {
                            retc = ut_to_lmt_lat(dret[0], geopos, out (dret[0]), ref serr);
                            retc = ut_to_lmt_lat(dret[1], geopos, out (dret[1]), ref serr);
                            retc = ut_to_lmt_lat(dret[2], geopos, out (dret[2]), ref serr);
                        }
                        do_print_heliacal(dret, event_type, obj_name);
                        tsave1 = dret[0];
                        /* mercury can have several evening appearances without any morning
                         * appearances in betweeen. We have to find out when the next 
                         * morning appearance is and then find all evening appearances 
                         * that take place before that */
                        if (ipl == SwissEph.SE_MERCURY)
                        {
                            event_type = SwissEph.SE_HELIACAL_RISING;
                            retflag = sweph.swe_heliacal_ut(dret[0], geopos, datm, dobs, obj_name, event_type, helflag, dret, ref serr);
                            if (retflag == SwissEph.ERR)
                            {
                                do_printf(serr);
                                return SwissEph.ERR;
                            }
                            tsave2 = dret[0];
                        }
                        //repeat_mercury:
                        /* evening first */
                        event_type = SwissEph.SE_EVENING_FIRST;
                        retflag = sweph.swe_heliacal_ut(tsave1, geopos, datm, dobs, obj_name, event_type, helflag, dret, ref serr);
                        if (retflag == SwissEph.ERR)
                        {
                            do_printf(serr);
                            return SwissEph.ERR;
                        }
                        if (ipl == SwissEph.SE_MERCURY && dret[0] > tsave2)
                            continue;
                        if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                        {
                            retc = ut_to_lmt_lat(dret[0], geopos, out (dret[0]), ref serr);
                            retc = ut_to_lmt_lat(dret[1], geopos, out (dret[1]), ref serr);
                            retc = ut_to_lmt_lat(dret[2], geopos, out (dret[2]), ref serr);
                        }
                        do_print_heliacal(dret, event_type, obj_name);
                    }
                    if (ipl == SwissEph.SE_MOON)
                    {
                        /* morning last */
                        event_type = SwissEph.SE_MORNING_LAST;
                        retflag = sweph.swe_heliacal_ut(dret[0], geopos, datm, dobs, obj_name, event_type, helflag, dret, ref serr);
                        if (retflag == SwissEph.ERR)
                        {
                            do_printf(serr);
                            return SwissEph.ERR;
                        }
                        if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                        {
                            retc = ut_to_lmt_lat(dret[0], geopos, out (dret[0]), ref serr);
                            retc = ut_to_lmt_lat(dret[1], geopos, out (dret[1]), ref serr);
                            retc = ut_to_lmt_lat(dret[2], geopos, out (dret[2]), ref serr);
                        }
                        do_print_heliacal(dret, event_type, obj_name);
                    }
                    else
                    {
                        /* heliacal setting (evening last) */
                        event_type = SwissEph.SE_HELIACAL_SETTING;
                        retflag = sweph.swe_heliacal_ut(dret[0], geopos, datm, dobs, obj_name, event_type, helflag, dret, ref serr);
                        if (retflag == SwissEph.ERR)
                        {
                            do_printf(serr);
                            return SwissEph.ERR;
                        }
                        if ((time_flag & (BIT_TIME_LMT | BIT_TIME_LAT)) != 0)
                        {
                            retc = ut_to_lmt_lat(dret[0], geopos, out (dret[0]), ref serr);
                            retc = ut_to_lmt_lat(dret[1], geopos, out (dret[1]), ref serr);
                            retc = ut_to_lmt_lat(dret[2], geopos, out (dret[2]), ref serr);
                        }
                        do_print_heliacal(dret, event_type, obj_name);
                        //if (false && ipl == SwissEph.SE_MERCURY) {
                        //    tsave1 = dret[0];
                        //    goto repeat_mercury;
                        //}
                    }
                }
            }
            return SwissEph.OK;
        }

        static int do_special_event(double tjd, Int32 ipl, string star, Int32 special_event, Int32 special_mode, double[] geopos, double[] datm, double[] dobs, ref string serr)
        {
            int retc = 0;
            /* risings, settings, meridian transits */
            if (special_event == SP_RISE_SET ||
                special_event == SP_MERIDIAN_TRANSIT)
                retc = call_rise_set(tjd, ipl, star, whicheph, special_mode, geopos, ref serr);
            /* lunar eclipses */
            if (special_event == SP_LUNAR_ECLIPSE)
                retc = call_lunar_eclipse(tjd, whicheph, special_mode, geopos, ref serr);
            /* solar eclipses */
            if (special_event == SP_SOLAR_ECLIPSE)
                retc = call_solar_eclipse(tjd, whicheph, special_mode, geopos, ref serr);
            /* occultations by the moon */
            if (special_event == SP_OCCULTATION)
                retc = call_lunar_occultation(tjd, ipl, star, whicheph, special_mode, geopos, ref serr);
            /* heliacal event */
            if (special_event == SP_HELIACAL)
                retc = call_heliacal_event(tjd, ipl, star, whicheph, special_mode, geopos, datm, dobs, ref serr);
            return retc;
        }

        static string hms_from_tjd(double tjd)
        {
            double x;
            /* tjd may be negative, 0h corresponds to day number 9999999.5 */
            x = (tjd % 1.0);  /* may be negative ! */
            x = ((x + 1.5) % 1.0); /* is positive day fraction */
            return C.sprintf("%s ", hms(x * 24, BIT_LZEROES));
        }

        static string hms(double x, Int32 iflag)
        {
            //static char s[AS_MAXCH], s2[AS_MAXCH], *sp;
            var c = SwissEph.ODEGREE_STRING;
            x += 0.5 / 36000.0; /* round to 0.1 sec */
            var s = dms(x, iflag);
            var spi = s.IndexOf(c);
            if (spi >= 0)
            {
                s = String.Concat(s.Substring(0, spi), ":", s.Substring(spi + 1));
                var s2 = s.Substring(spi + SwissEph.ODEGREE_STRING.Length);
                s = String.Concat(s.Substring(0, spi + 1), s2);
                s = String.Concat(s.Substring(0, spi + 3), ":", s.Substring(spi + 4));
            }
            return s;
        }

        static void do_printf(string info)
        {
            Console.Write(info);
        }

        /* make_ephemeris_path().
         * ephemeris path includes
         *   current working directory
         *   + program directory
         *   + default path from swephexp.h on current drive
         *   +                              on program drive
         *   +                              on drive C:
         */
        static int make_ephemeris_path(Int32 iflg, string argv0, ref string path)
        {
            int spi;
            char dirglue = SwissEph.DIR_GLUE;
            int pathlen = 0;
            /* current working directory */
            path = C.sprintf(".%c", SwissEph.PATH_SEPARATOR);
            /* program directory */
            spi = argv0.LastIndexOf(dirglue);
            if (spi >= 0)
            {
                pathlen = spi;
                path += argv0.Substring(0, pathlen);
                path += C.sprintf("%c", SwissEph.PATH_SEPARATOR);
            }
#if MSDOS
            {
                string[] cpos = new string[20];
                string s = string.Empty, s1 = String.Empty;
                string[] sp = new string[3];
                int i, j, np;
                s1 = SwissEph.SE_EPHE_PATH;
                //s1 = ".;sweph";
                cpos = s1.Split(new char[] { SwissEph.PATH_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                np = cpos.Length;
                /* 
                 * default path from swephexp.h
                 * - current drive
                 * - program drive
                 * - drive C
                 */
                s = String.Empty;
                /* current working drive */
                sp[0] = Directory.GetCurrentDirectory();
                if (String.IsNullOrWhiteSpace(sp[0]))
                {
                    printf("error in getcwd()\n");
                    Environment.Exit(1);
                }
                if (sp[0][0] == 'C')
                    sp[0] = null;
                /* program drive */
                if (argv0[0] != 'C' && (sp[0] == null || sp[0][0] != argv0[0]))
                    sp[1] = argv0;
                else
                    sp[1] = null;
                /* drive C */
                sp[2] = "C";
                for (i = 0; i < np; i++)
                {
                    s = cpos[i];
                    if (s[0] == '.')	/* current directory */
                        continue;
                    if (s[1] == ':')  /* drive already there */
                        continue;
                    for (j = 0; j < 3; j++)
                    {
                        if (sp[j] != null)
                            path += C.sprintf("%c:%s%c", sp[j][0], s, SwissEph.PATH_SEPARATOR);
                    }
                }
            }
#else
            if (strlen(path) + strlen(SE_EPHE_PATH) < AS_MAXCH - 1)
                strcat(path, SE_EPHE_PATH);
#endif
            return SwissEph.OK;
        }

        static void remove_whitespace(ref string s)
        {
            //char *sp, s1[AS_MAXCH];
            //if (s == NULL) return;
            //for (sp = s; *sp == ' '; sp++)
            //  ;
            //strcpy(s1, sp);
            //while (*(sp = s1 + strlen(s1) - 1) == ' ')
            //  *sp = '\0';
            //strcpy(s, s1);
            if (s == null) { s = null; return; }
            s = s.Trim(' ');
        }

        //#if MSDOS
        ///**************************************************************
        //cut the string s at any char in cutlist; put pointers to partial strings
        //into cpos[0..n-1], return number of partial strings;
        //if less than nmax fields are found, the first empty pointer is
        //set to NULL.
        //More than one character of cutlist in direct sequence count as one
        //separator only! cut_str_any("word,,,word2",","..) cuts only two parts,
        //cpos[0] = "word" and cpos[1] = "word2".
        //If more than nmax fields are found, nmax is returned and the
        //last field nmax-1 rmains un-cut.
        //**************************************************************/
        //static int cut_str_any(char *s, char *cutlist, char *cpos[], int nmax)
        //{
        //  int n = 1;
        //  cpos [0] = s;
        //  while (*s != '\0') {
        //    if ((strchr(cutlist, (int) *s) != NULL) && n < nmax) {
        //      *s = '\0';
        //      while (*(s + 1) != '\0' && strchr (cutlist, (int) *(s + 1)) != NULL) s++;
        //      cpos[n++] = s + 1;
        //    }
        //    if (*s == '\n' || *s == '\r') {	/* treat nl or cr like end of string */
        //      *s = '\0';
        //      break;
        //    }
        //    s++;
        //  }
        //  if (n < nmax) cpos[n] = NULL;
        //  return (n);
        //}	/* cutstr */
        //#endif

        static void printf(String format, params object[] args)
        {
            Console.Write(C.sprintf(format, args));
        }

        #endregion

        static int Main(string[] args)
        {
            var s = Environment.GetCommandLineArgs();
            //s = new String[] { s[0], "-b16.08.1974", "-n1", "-s1", "-fPLBRS", "-pp", "-ejpl" };
            //s = new String[] { s[0], "-b16.08.1974", "-n1", "-s1", "-fPLBRS", "-pp", "-emos" };
            //s = new String[] { s[0], "-b16.08.1974", "-n1", "-s1", "-fPLBRS", "-pah", "-ejplde431.eph" };
            var result = main_test(s.Length, s);
#if DEBUG
            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
#endif
            return result;
        }
    }

}
