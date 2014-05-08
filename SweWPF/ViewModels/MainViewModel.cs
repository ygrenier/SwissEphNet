using SweNet;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// Main viewmodel 
    /// </summary>
    public class MainViewModel : ViewModel, IDisposable
    {
        private Sweph _Sweph;
        private List<String> _SearchPaths = new List<string>();

        public MainViewModel() {
            Config = new ConfigViewModel();
            Input = new InputViewModel();
            Result = new CalculationResultViewModel();
            DoCalculationCommand = new RelayCommand(() => {
                DoCalculation();
            });
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing && _Sweph != null) {
                _Sweph.Dispose();
                _Sweph = null;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Create new Sweph context with current configuration
        /// </summary>
        private Sweph CreateNewSweph() {
            var result = new Sweph();
            result.OnLoadFile += Sweph_OnLoadFile;
            return result;
        }

        void Sweph_OnLoadFile(object sender, LoadFileEventArgs e) {
            var f = e.FileName.Replace("[ephe]", "").Trim('\\', '/');
            foreach (var p in _SearchPaths) {
                var fName = Path.Combine(p, f);
                if (System.IO.File.Exists(fName)) {
                    e.File = new System.IO.FileStream(fName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    return;
                }
            }
        }

        public void DoCalculation() {
            String star = String.Empty;
            char hsys = 'P';

            // Initialize paths
            String sourcePath = Config.EphemerisPath;
            if (String.IsNullOrWhiteSpace(sourcePath)) sourcePath = ".";
            _SearchPaths.Clear();
            foreach (var path in sourcePath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (String.IsNullOrWhiteSpace(path)) continue;
                if (path.Trim() == ".") {
                    _SearchPaths.Add(AppDomain.CurrentDomain.BaseDirectory);
                    _SearchPaths.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwephData"));
                } else {
                    _SearchPaths.Add(path.Trim());
                }
            }

            // Initialize result
            Result.Reset();

            // Initialize engine
            Sweph.swe_set_topo(Input.Longitude, Input.Latitude, Input.Altitude);

            // Dates and Times
            Result.JulianDay = Sweph.JulianDay(Input.DateUTC);
            Result.DateUTC = Sweph.DateUT(Result.JulianDay);
            Result.EphemerisTime = Sweph.EphemerisTime(Result.JulianDay);
            Result.SideralTime = Sweph.swe_sidtime(Result.JulianDay) + (Input.Longitude / 15.0);
            if (Result.SideralTime >= 24.0) Result.SideralTime -= 24.0;
            if (Result.SideralTime < 0.0) Result.SideralTime += 24.0;

            // Calculation
            String serr = null;
            Double[] x=new double[24];
            var iflag = SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED;
            var iflgret = Sweph.swe_calc(Result.EphemerisTime, Planet.EclipticNutation, iflag, x, ref serr);
            Result.TrueEclipticObliquity = x[0];
            Result.MeanEclipticObliquity = x[1];
            Result.NutationLongitude = x[2];
            Result.NutationObliquity = x[3];

            // Planets
            foreach (var planet in Input.Planets) {
                if (planet == Planet.Earth) continue;   // Exclude Earth if geo or topo
                serr = null;
                var pi = new PlanetInfos() {
                    Planet = planet
                };
                Result.Planets.Add(pi);
                // Ecliptic position
                if (planet == Planet.FixedStar) {
                    iflgret = Sweph.swe_fixstar(star, Result.EphemerisTime, iflag, x, ref serr);
                    pi.PlanetName = star;
                } else {
                    iflgret = Sweph.swe_calc(Result.EphemerisTime, planet, iflag, x, ref serr);
                    pi.PlanetName = Sweph.swe_get_planet_name(planet);
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
                    pi.HousePosition = Sweph.swe_house_pos(Result.ARMC, Input.Latitude, Result.TrueEclipticObliquity, hsys, x, ref serr);
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
            Sweph.swe_houses_ex(Result.EphemerisTime, iflag, Input.Latitude, Input.Longitude, 'P', cusps, ascmc);
            for (int i = 1; i <= 12; i++) {
                Result.Houses.Add(new HouseInfos() {
                    House = i,
                    HouseName = hNames[i],
                    Cusp = cusps[i]
                });
            }
            for (int i = 0; i < 7; i++) {
                Result.ASMCs.Add(new HouseInfos() {
                    House = i,
                    HouseName = amNames[i],
                    Cusp = ascmc[i]
                });
            }
        }

        /// <summary>
        /// Sweph context
        /// </summary>
        public Sweph Sweph {
            get { return _Sweph ?? (_Sweph = CreateNewSweph()); }
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public ConfigViewModel Config { get; private set; }

        /// <summary>
        /// Input informations
        /// </summary>
        public InputViewModel Input { get; private set; }

        /// <summary>
        /// Calculation result
        /// </summary>
        public CalculationResultViewModel Result { get; private set; }

        /// <summary>
        /// Command to calculation
        /// </summary>
        public RelayCommand DoCalculationCommand { get; private set; }

    }

}
