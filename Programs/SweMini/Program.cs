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
  $Header: /home/dieter/sweph/RCS/swemini.c,v 1.74 2008/06/16 10:07:20 dieter Exp $

  swemini.c	A minimal program to test the Swiss Ephemeris.

  Input: a date (in gregorian calendar, sequence day.month.year)
  Output: Planet positions at midnight Universal time, ecliptic coordinates,
          geocentric apparent positions relative to true equinox of date, as 
          usual in western astrology.
        
   
  Authors: Dieter Koch and Alois Treindl, Astrodienst Zurich

**************************************************************/
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwissEphNet;
using System.IO;
using System.Text.RegularExpressions;

namespace SweMini
{
    class Program
    {
        static int Main(string[] args)
        {
            return Main_Mini(args);
            //return Main_TestValues(args);
        }

        //static int Main_TestValues(string[] args) {
        //    int jyear = 2014,
        //        jmon = 4,
        //        jday = 21,
        //        jhour = 20,
        //        jmin = 41,
        //        jsec = 23;
        //    using (Sweph sweph = new Sweph()) {
        //        double jut = jhour + (jmin / 60.0) + (jsec / 3600.0);
        //        double tjd = sweph.swe_julday(jyear, jmon, jday, jut, SwissEph.SE_GREG_CAL);
        //        double deltat = sweph.swe_deltat(tjd);
        //        double te = tjd + deltat;
        //        printf("date: %02d.%02d.%d at %02d:%02d:%02d\nDeltat : %f\nJulian Day : %f\nEphemeris Time : %f\n", jday, jmon, jyear, jhour, jmin, jsec, deltat, tjd, te);

        //        var date = new DateUT(jyear, jmon, jday, jhour, jmin, jsec);
        //        var jd = sweph.JulianDay(date, DateCalendar.Gregorian);
        //        var et = sweph.EphemerisTime(jd);
        //        printf("date: %02d.%02d.%d at %02d:%02d:%02d\nDeltat : %f\nJulian Day : %f\nEphemeris Time : %f\n", 
        //            date.Day, date.Month, date.Year, date.Hours, date.Minutes, date.Seconds, et.DeltaT, jd.Value, et.Value);

        //    }

        //    Console.ReadKey();
        //    return 0;
        //}

        static int Main_Mini(string[] args)
        {
            string sdate = String.Empty, snam = String.Empty, serr = String.Empty;
            int jday = 1, jmon = 1, jyear = 2000;
            double jut = 0.0;
            double[] x2 = new double[6];
            Int32 iflag, iflgret;
            //int p;
            using (var swe = new SwissEph())
            {
                swe.swe_set_ephe_path(null);
                iflag = SwissEph.SEFLG_SPEED;
                swe.OnLoadFile += swe_OnLoadFile;
                while (true)
                {
                    Console.Write("\nDate (d.m.y) ? ");
                    sdate = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(sdate)) break;
                    /*
                     * stop if a period . is entered
                     */
                    if (sdate == ".")
                        return SwissEph.OK;
                    var match = Regex.Match(sdate, @"(\d+)\.(\d+)\.(\d+)");
                    if (!match.Success) continue;
                    jday = int.Parse(match.Groups[1].Value);
                    jmon = int.Parse(match.Groups[2].Value);
                    jyear = int.Parse(match.Groups[3].Value);
                    /*
                     * we have day, month and year and convert to Julian day number
                     */
                    var jd = swe.swe_julday(jyear, jmon, jday, jut, SwissEph.SE_GREG_CAL);
                    /*
                     * compute Ephemeris time from Universal time by adding delta_t
                     */
                    var te = jd + swe.swe_deltat(jd);
                    Console.WriteLine("date: {0:00}.{1:00}.{2:D4} at 0:00 Universal time", jday, jmon, jyear);
                    Console.WriteLine("planet     \tlongitude\tlatitude\tdistance\tspeed long.");
                    /*
                     * a loop over all planets
                     */
                    for (var p = SwissEph.SE_SUN; p <= SwissEph.SE_CHIRON; p++)
                    {
                        if (p == SwissEph.SE_EARTH) continue;
                        /*
                         * do the coordinate calculation for this planet p
                         */
                        iflgret = swe.swe_calc(te, p, iflag, x2, ref serr);
                        /*
                         * if there is a problem, a negative value is returned and an 
                         * errpr message is in serr.
                         */
                        if (iflgret < 0)
                            printf("error: %s\n", serr);
                        else if (iflgret != iflag)
                            printf("warning: iflgret != iflag. %s\n", serr);
                        /*
                         * get the name of the planet p
                         */
                        snam = swe.swe_get_planet_name(p);
                        /*
                         * print the coordinates
                         */
                        printf("%10s\t%11.7f\t%10.7f\t%10.7f\t%10.7f\n",
                           snam, x2[0], x2[1], x2[2], x2[3]);
                    }
                }
            }

#if DEBUG
            Console.Write("\nPress a key to quit...");
            Console.ReadKey();
#endif
            return 0;
        }

        static Stream SearchFile(String fileName)
        {
            fileName = fileName.Trim('/', '\\');
            var folders = new string[] {
                System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Datas"),
                @"C:\Temp\swisseph\swisseph\ephe"
            };
            foreach (var folder in folders)
            {
                var f = Path.Combine(folder, fileName);
                if (File.Exists(f))
                    return new System.IO.FileStream(f, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
            return null;
        }

        static void swe_OnLoadFile(object sender, LoadFileEventArgs e)
        {
            if (e.FileName.StartsWith("[ephe]"))
            {
                e.File = SearchFile(e.FileName.Replace("[ephe]", string.Empty));
            }
            else
            {
                var f = e.FileName;
                if (System.IO.File.Exists(f))
                    e.File = new System.IO.FileStream(f, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
        }

        public static void printf(string Format, params object[] Parameters)
        {
            Console.Write(C.sprintf(Format, Parameters));
        }

    }
}
