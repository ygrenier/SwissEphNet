using SweNet;
using SweWPF.Models;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Services
{

    /// <summary>
    /// Sweph calculation service implement
    /// </summary>
    public class CalcService : ICalcService
    {

        private static EphemerisResult SwephCalculation(Configuration config, InputCalculation input) {
            EphemerisResult result = new EphemerisResult();
            String star = String.Empty;
            List<String> searchPaths = new List<string>();

            // Initialize paths
            var paths = config.SearchPaths.Where(s => !String.IsNullOrWhiteSpace(s)).Union(new String[] { "." });
            foreach (var path in paths.Select(s => s.Trim())) {
                if (path == ".") {
                    searchPaths.Add(AppDomain.CurrentDomain.BaseDirectory);
                    searchPaths.Add(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwephData"));
                } else {
                    searchPaths.Add(path.Trim());
                }
            }

            using (var sweph = new SweNet.Sweph()) {
                sweph.OnLoadFile += (s, e) => {
                    var f = e.FileName.Replace("[ephe]", "").Trim('\\', '/');
                    foreach (var p in searchPaths) {
                        var fName = System.IO.Path.Combine(p, f);
                        if (System.IO.File.Exists(fName)) {
                            e.File = new System.IO.FileStream(fName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                            return;
                        }
                    }
                };

                // Prepare result
                result.Reset();

                // Initialize engine
                sweph.Ephemeris = input.EphemerisMode;
                if (sweph.Ephemeris == EphemerisMode.JPL)
                    sweph.swe_set_jpl_file(input.JplFile);
                sweph.PositionCenter = input.PositionCenter;
                if (sweph.PositionCenter == PositionCenter.Topocentric)
                    sweph.swe_set_topo(input.Longitude, input.Latitude, input.Altitude);
                sweph.HouseSystem = input.HouseSystem;

                // Dates and Times
                if (input.DateUT != null) {
                    result.JulianDay = sweph.JulianDay(input.DateUT.Value);
                    result.DateUTC = sweph.DateUT(result.JulianDay);
                    result.EphemerisTime = sweph.EphemerisTime(result.JulianDay);
                } else if (input.DateET != null) {
                    result.JulianDay = sweph.JulianDay(input.DateET.Value);
                    result.DateUTC = sweph.DateUT(result.JulianDay);
                    result.EphemerisTime = new EphemerisTime(result.JulianDay, 0.0);
                } else if (input.JulianDay != null) {
                    result.JulianDay = input.JulianDay.Value;
                    result.DateUTC = sweph.DateUT(result.JulianDay);
                    result.EphemerisTime = sweph.EphemerisTime(result.JulianDay);
                }
                result.SideralTime = sweph.swe_sidtime(result.JulianDay) + (input.Longitude / 15.0);
                if (result.SideralTime >= 24.0) result.SideralTime -= 24.0;
                if (result.SideralTime < 0.0) result.SideralTime += 24.0;

                // Calculation
                String serr = null;
                Double[] x = new double[24];
                var iflgret = sweph.swe_calc(result.EphemerisTime, Planet.EclipticNutation, x, ref serr);
                result.TrueEclipticObliquity = x[0];
                result.MeanEclipticObliquity = x[1];
                result.NutationLongitude = x[2];
                result.NutationObliquity = x[3];

                // Planets
                foreach (var planet in input.Planets) {
                    if (planet == Planet.Earth) continue;   // Exclude Earth if geo or topo
                    serr = null;
                    var pi = new PlanetValues() {
                        Planet = planet
                    };
                    result.Planets.Add(pi);
                    // Ecliptic position
                    if (planet == Planet.FixedStar) {
                        iflgret = sweph.swe_fixstar(star, result.EphemerisTime, x, ref serr);
                        pi.PlanetName = star;
                    } else {
                        iflgret = sweph.swe_calc(result.EphemerisTime, planet, x, ref serr);
                        pi.PlanetName = sweph.swe_get_planet_name(planet);
                        if (planet.IsAsteroid) {
                            pi.PlanetName = String.Format("#{0}", planet - Planet.FirstAsteroid);
                        }
                    }
                    if (iflgret >= 0) {
                        pi.Longitude = x[0];
                        pi.Latitude = x[1];
                        pi.Distance = x[2];
                        pi.LongitudeSpeed = x[3];
                        pi.LatitudeSpeed = x[4];
                        pi.DistanceSpeed = x[5];
                        pi.HousePosition = sweph.swe_house_pos(result.ARMC, input.Latitude, result.TrueEclipticObliquity, x, ref serr);
                        if (pi.HousePosition == 0)
                            iflgret = SwissEph.ERR;
                    }
                    if (iflgret < 0) {
                        if (!String.IsNullOrEmpty(serr)) {
                            pi.ErrorMessage = serr;
                        }
                    } else if (!String.IsNullOrEmpty(serr) && String.IsNullOrEmpty(pi.WarnMessage))
                        pi.WarnMessage = serr;
                }
                /*
                    //* equator position * /
                    if (fmt.IndexOfAny("aADdQ".ToCharArray()) >= 0) {
                        iflag2 = iflag | SwissEph.SEFLG_EQUATORIAL;
                        if (ipl == SwissEph.SE_FIXSTAR)
                            iflgret = sweph.swe_fixstar(star, tjd_et, iflag2, xequ, ref serr);
                        else
                            iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xequ, ref serr);
                    }
                    //* ecliptic cartesian position * /
                    if (fmt.IndexOfAny("XU".ToCharArray()) >= 0) {
                        iflag2 = iflag | SwissEph.SEFLG_XYZ;
                        if (ipl == SwissEph.SE_FIXSTAR)
                            iflgret = sweph.swe_fixstar(star, tjd_et, iflag2, xcart, ref serr);
                        else
                            iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xcart, ref serr);
                    }
                    //* equator cartesian position * /
                    if (fmt.IndexOfAny("xu".ToCharArray()) >= 0) {
                        iflag2 = iflag | SwissEph.SEFLG_XYZ | SwissEph.SEFLG_EQUATORIAL;
                        if (ipl == SwissEph.SE_FIXSTAR)
                            iflgret = sweph.swe_fixstar(star, tjd_et, iflag2, xcartq, ref serr);
                        else
                            iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xcartq, ref serr);
                    }
                    spnam = se_pname;
                 */

                // Houses
                double[] cusps = new double[input.HouseSystem == HouseSystem.GauquelinSector ? 37 : 13];
                double[] ascmc = new double[10];
                var hNames = new String[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
                var amNames = new String[] { "Ascendant", "MC", "ARMC", "Vertex", "Equatorial ascendant", 
                    "Co-ascendant (Walter Koch)", "Co-ascendant (Michael Munkasey)", "Polar ascendant (M. Munkasey)" };
                sweph.swe_houses_ex(result.JulianDay, input.Latitude, input.Longitude, cusps, ascmc);
                for (int i = 1; i <= 12; i++) {
                    result.Houses.Add(new HouseValues() {
                        House = i,
                        HouseName = hNames[i],
                        Cusp = cusps[i]
                    });
                }
                for (int i = 0; i < 7; i++) {
                    result.ASMCs.Add(new HouseValues() {
                        House = i,
                        HouseName = amNames[i],
                        Cusp = ascmc[i]
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Do an ephemeris calculation
        /// </summary>
        public EphemerisResult Calculate(Configuration config, InputCalculation input) {
            return SwephCalculation(config, input);
        }

    }

}
