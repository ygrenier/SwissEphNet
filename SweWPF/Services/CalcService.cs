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
            char hsys = 'P';
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

                // Configuration
                sweph.Ephemeris = EphemerisMode.SwissEphemeris;
                sweph.PositionCenter = input.PositionCenter;

                // Initialize engine
                sweph.swe_set_topo(input.Longitude, input.Latitude, input.Altitude);

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
                //var iflag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
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
                        pi.HousePosition = sweph.swe_house_pos(result.ARMC, input.Latitude, result.TrueEclipticObliquity, hsys, x, ref serr);
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
                double[] cusps = new double[13], ascmc = new double[10];
                var hNames = new String[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
                var amNames = new String[] { "Ascendant", "MC", "ARMC", "Vertex", "Equatorial ascendant", 
                    "Co-ascendant (Walter Koch)", "Co-ascendant (Michael Munkasey)", "Polar ascendant (M. Munkasey)" };
                sweph.swe_houses_ex(result.EphemerisTime, input.Latitude, input.Longitude, 'P', cusps, ascmc);
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

        private static EphemerisResult SwissEphCalculation(Configuration config, InputCalculation input) {
            EphemerisResult result = new EphemerisResult();
            String star = String.Empty;
            char hsys = 'P';
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

            using (var sweph = new SwissEphNet.SwissEph()) {
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

                // Configuration
                int iflag = SwissEphNet.SwissEph.SEFLG_SPEED;
                // Ephemeris type
                //switch (input.Ephemeris) {
                //    case EphemerisMode.Moshier:
                //        _SwissFlag |= SwissEph.SEFLG_MOSEPH;
                //        break;
                //    case EphemerisMode.JPL:
                //        _SwissFlag |= SwissEph.SEFLG_JPLEPH;
                //        break;
                //    case EphemerisMode.SwissEphemeris:
                //    default:
                //        _SwissFlag |= SwissEph.SEFLG_SWIEPH;
                //        break;
                //}
                iflag |= SwissEph.SEFLG_SWIEPH;
                // Position center
                var sidmode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
                switch (input.PositionCenter) {
                    case PositionCenter.Topocentric:
                        iflag |= SwissEph.SEFLG_TOPOCTR;
                        break;
                    case PositionCenter.Heliocentric:
                        iflag |= SwissEph.SEFLG_HELCTR;
                        break;
                    case PositionCenter.Barycentric:
                        iflag |= SwissEph.SEFLG_BARYCTR;
                        break;
                    case PositionCenter.SiderealFagan:
                        iflag |= SwissEph.SEFLG_SIDEREAL;
                        sidmode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
                        break;
                    case PositionCenter.SiderealLahiri:
                        iflag |= SwissEph.SEFLG_SIDEREAL;
                        sidmode = SwissEph.SE_SIDM_LAHIRI;
                        break;
                    case PositionCenter.Geocentric:
                    default:
                        break;
                }
                //sweph.swe_set_sid_mode(sidmode, 0, 0);

                // Initialize engine
                if (input.PositionCenter == PositionCenter.Topocentric)
                    sweph.swe_set_topo(input.Longitude, input.Latitude, input.Altitude);

                // Dates and Times
                int iyear = 0, imonth = 0, iday = 0; double ihour = 0;
                if (input.DateUT != null) {
                    result.JulianDay = new JulianDay(sweph.swe_julday(input.DateUT.Value.Year, input.DateUT.Value.Month, input.DateUT.Value.Day, SwissEph.GetHourValue(input.DateUT.Value.Hours, input.DateUT.Value.Minutes, input.DateUT.Value.Seconds), SwissEph.SE_GREG_CAL));
                    sweph.swe_revjul(result.JulianDay, SwissEph.SE_GREG_CAL, ref iyear, ref imonth, ref iday, ref ihour);
                    result.DateUTC = new DateUT(iyear, imonth, iday, ihour);
                    result.EphemerisTime = new EphemerisTime(result.JulianDay, sweph.swe_deltat(result.JulianDay));
                } else if (input.DateET != null) {
                    result.JulianDay = new JulianDay(sweph.swe_julday(input.DateET.Value.Year, input.DateET.Value.Month, input.DateET.Value.Day, SwissEph.GetHourValue(input.DateET.Value.Hours, input.DateET.Value.Minutes, input.DateET.Value.Seconds), SwissEph.SE_GREG_CAL));
                    sweph.swe_revjul(result.JulianDay, SwissEph.SE_GREG_CAL, ref iyear, ref imonth, ref iday, ref ihour);
                    result.DateUTC = new DateUT(iyear, imonth, iday, ihour);
                    result.EphemerisTime = new EphemerisTime(result.JulianDay, 0.0);
                } else if (input.JulianDay != null) {
                    result.JulianDay = input.JulianDay.Value;
                    sweph.swe_revjul(result.JulianDay, SwissEph.SE_GREG_CAL, ref iyear, ref imonth, ref iday, ref ihour);
                    result.DateUTC = new DateUT(iyear, imonth, iday, ihour);
                    result.EphemerisTime = new EphemerisTime(result.JulianDay, sweph.swe_deltat(result.JulianDay));
                }
                result.SideralTime = sweph.swe_sidtime(result.JulianDay) + (input.Longitude / 15.0);
                if (result.SideralTime >= 24.0) result.SideralTime -= 24.0;
                if (result.SideralTime < 0.0) result.SideralTime += 24.0;

                // Calculation
                String serr = null;
                Double[] x = new double[24];
                //var iflag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
                var iflgret = sweph.swe_calc(result.EphemerisTime, Planet.EclipticNutation, iflag, x, ref serr);
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
                        iflgret = sweph.swe_fixstar(star, result.EphemerisTime, iflag, x, ref serr);
                        pi.PlanetName = star;
                    } else {
                        iflgret = sweph.swe_calc(result.EphemerisTime, planet, iflag, x, ref serr);
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
                        pi.HousePosition = sweph.swe_house_pos(result.ARMC, input.Latitude, result.TrueEclipticObliquity, hsys, x, ref serr);
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
                double[] cusps = new double[13], ascmc = new double[10];
                var hNames = new String[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
                var amNames = new String[] { "Ascendant", "MC", "ARMC", "Vertex", "Equatorial ascendant", 
                    "Co-ascendant (Walter Koch)", "Co-ascendant (Michael Munkasey)", "Polar ascendant (M. Munkasey)" };
                sweph.swe_houses_ex(result.EphemerisTime, iflag, input.Latitude, input.Longitude, 'P', cusps, ascmc);
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
            return SwissEphCalculation(config, input);
            //return SwephCalculation(config, input);
        }

    }

}
