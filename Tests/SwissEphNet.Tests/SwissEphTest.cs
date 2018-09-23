using System;
using System.Globalization;
using Xunit;

namespace SwissEphNet.Tests
{

    public partial class SwissEphTest : IDisposable
    {
        CultureInfo _OldCulture;

        public SwissEphTest()
        {
#if NET_STANDARD
            _OldCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
#else
            _OldCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
#endif
        }

        public void Dispose()
        {
#if NET_STANDARD
            CultureInfo.CurrentCulture = _OldCulture;
            CultureInfo.CurrentUICulture = _OldCulture;
#else
            System.Threading.Thread.CurrentThread.CurrentCulture = _OldCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _OldCulture;
#endif
        }

        [Fact]
        public void TestConstructor() {
            using (var target = new SwissEph()) {
            }
        }

        [Fact]
        public void TestVersion() {
            using (var target = new SwissEph()) {
                Assert.Equal("2.06", target.swe_version());
                Assert.Equal("2.06.00-net-0023", target.swe_dotnet_version());
            }
        }

        [Fact]
        public void TestOnLoadFile() {
            // No file loading defined
            using (var target = new SwissEph()) {
                Assert.Equal("100: not found", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

            // File loading defined, but file not found
            using (var target = new SwissEph()) {
                target.OnLoadFile += (s, e) => {
                    e.File = null;
                };
                Assert.Equal("100: not found", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

            // File loading defined
            using (var target = new SwissEph()) {
                target.OnLoadFile += (s, e) => {
                    if (e.FileName == @"[ephe]\seasnam.txt") {
                        e.File = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(@"
000096  Aegle
000097  Klotho
000098  Ianthe
000099  Dike
000100  Hekate
000101  Helena
000102  Miriam
000103  Hera
"));
                    } else
                        e.File = null;
                };
                Assert.Equal("Hekate", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

        }

        [Fact(Skip = "")]
        public void Test_swe_heliacal_ut() {
            using (var target = new SwissEph()) {
                //public Int32 swe_heliacal_ut(double tjdstart_ut, double[] geopos, double[] datm, double[] dobs, string ObjectName, 
                //Int32 TypeEvent, Int32 iflag, double[] dret, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_heliacal_pheno_ut() {
            using (var target = new SwissEph()) {
                //public Int32 swe_heliacal_pheno_ut(double tjd_ut, double[] geopos, double[] datm, double[] dobs, string ObjectName,
                //Int32 TypeEvent, Int32 helflag, double[] darr, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_vis_limit_mag() {
            using (var target = new SwissEph()) {
                //public Int32 swe_vis_limit_mag(double tjdut, double[] geopos, double[] datm, double[] dobs, string ObjectName,
                //    Int32 helflag, double[] dret, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_heliacal_angle() {
            using (var target = new SwissEph()) {
                //public Int32 swe_heliacal_angle(double tjdut, double[] dgeo, double[] datm, double[] dobs, Int32 helflag, double mag,
                //    double azi_obj, double azi_sun, double azi_moon, double alt_moon, double[] dret, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_topo_arcus_visionis() {
            using (var target = new SwissEph()) {
                //public Int32 swe_topo_arcus_visionis(double tjdut, double[] dgeo, double[] datm, double[] dobs, Int32 helflag, double mag,
                //    double azi_obj, double alt_obj, double azi_sun, double azi_moon, double alt_moon, ref double dret, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_close() {
            using (var target = new SwissEph()) {
                //public void swe_close();
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_set_ephe_path() {
            using (var target = new SwissEph()) {
                //public void swe_set_ephe_path(String path);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_set_jpl_file() {
            using (var target = new SwissEph()) {
                //public void swe_set_jpl_file(string fname);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_set_sid_mode() {
            using (var target = new SwissEph()) {
                //public void swe_set_sid_mode(Int32 sid_mode, double t0, double ayan_t0);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_get_ayanamsa() {
            using (var target = new SwissEph()) {
                //public double swe_get_ayanamsa(double tjd_et);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_get_ayanamsa_ut() {
            using (var target = new SwissEph()) {
                //public double swe_get_ayanamsa_ut(double tjd_ut);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_get_ayanamsa_name() {
            using (var target = new SwissEph()) {
                //public string swe_get_ayanamsa_name(Int32 isidmode);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_houses() {
            using (var target = new SwissEph()) {
                //public int swe_houses(double tjd_ut, double geolat, double geolon, char hsys, double[] cusps, double[] ascmc);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_houses_ex() {
            using (var target = new SwissEph()) {
                //public int swe_houses_ex(double tjd_ut, Int32 iflag, double geolat, double geolon, char hsys, CPointer<double> hcusps, CPointer<double> ascmc);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_houses_armc() {
            using (var target = new SwissEph()) {
                //public int swe_houses_armc(double armc, double geolat, double eps, char hsys, double[] cusps, double[] ascmc);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_house_pos() {
            using (var target = new SwissEph()) {
                //public double swe_house_pos(double armc, double geolon, double eps, char hsys, double[] xpin, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_house_name() {
            using (var target = new SwissEph()) {
                //public string swe_house_name(char hsys);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_gauquelin_sector() {
            using (var target = new SwissEph()) {
                //public Int32 swe_gauquelin_sector(double t_ut, Int32 ipl, String starname, Int32 iflag, Int32 imeth, double[] geopos,
                //    double atpress, double attemp, ref double dgsect, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_sol_eclipse_where() {
            using (var target = new SwissEph()) {
                //public Int32 swe_sol_eclipse_where(double tjd, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lun_occult_where()
        {
            using (var target = new SwissEph())
            {
                //public Int32 swe_lun_occult_where(double tjd, Int32 ipl, string starname, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_sol_eclipse_how() {
            using (var target = new SwissEph()) {
                //public Int32 swe_sol_eclipse_how(double tjd, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            }
        }

        [Fact]
        public void Test_swe_lun_occult_when_glob() {
            //public Int32 swe_lun_occult_when_glob(double tjd_start, Int32 ipl, string starname, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Double[] tret = new double[12];
                String serr = null;
                var r = target.swe_lun_occult_when_glob(0.0, 0, null, 0, 0, tret, false, ref serr);
                Assert.Equal(-1, r);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_sol_eclipse_when_loc() {
            using (var target = new SwissEph()) {
                //public Int32 swe_sol_eclipse_when_loc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lun_occult_when_loc() {
            using (var target = new SwissEph()) {
                //public Int32 swe_lun_occult_when_loc(double tjd_start, Int32 ipl, String starname, Int32 ifl, double[] geopos, double[] tret,
                //    double[] attr, bool backward, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_sol_eclipse_when_glob() {
            using (var target = new SwissEph()) {
                //public Int32 swe_sol_eclipse_when_glob(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lun_eclipse_how() {
            using (var target = new SwissEph()) {
                //public Int32 swe_lun_eclipse_how(double tjd_ut, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lun_eclipse_when() {
            using (var target = new SwissEph()) {
                //public Int32 swe_lun_eclipse_when(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lun_eclipse_when_loc() {
            using (var target = new SwissEph()) {
                //public Int32 swe_lun_eclipse_when_loc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_pheno() {
            using (var target = new SwissEph()) {
                //public Int32 swe_pheno(double tjd, Int32 ipl, Int32 iflag, double[] attr, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_pheno_ut() {
            using (var target = new SwissEph()) {
                //public Int32 swe_pheno_ut(double tjd_ut, Int32 ipl, Int32 iflag, double[] attr, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_refrac() {
            using (var target = new SwissEph()) {
                //public double swe_refrac(double inalt, double atpress, double attemp, Int32 calc_flag);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_refrac_extended() {
            using (var target = new SwissEph()) {
                //public double swe_refrac_extended(double inalt, double geoalt, double atpress, double attemp, double lapse_rate, Int32 calc_flag, double[] dret);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_set_lapse_rate() {
            using (var target = new SwissEph()) {
                //public void swe_set_lapse_rate(double lapse_rate);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_rise_trans_true_hor() {
            using (var target = new SwissEph()) {
                //public Int32 swe_rise_trans_true_hor(double tjd_ut, Int32 ipl, string starname,
                //           Int32 epheflag, Int32 rsmi, double[] geopos, double atpress, double attemp,
                //           double horhgt, ref double tret, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_rise_trans() {
            using (var target = new SwissEph()) {
                //public Int32 swe_rise_trans(double tjd_ut, Int32 ipl, string starname, Int32 epheflag, Int32 rsmi,
                //    double[] geopos, double atpress, double attemp, ref double tret, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_nod_aps() {
            using (var target = new SwissEph()) {
                //public Int32 swe_nod_aps(double tjd_et, Int32 ipl, Int32 iflag,
                //                      Int32 method,
                //                      double[] xnasc, double[] xndsc,
                //                      double[] xperi, double[] xaphe,
                //                      ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_nod_aps_ut() {
            using (var target = new SwissEph()) {
                //public Int32 swe_nod_aps_ut(double tjd_ut, Int32 ipl, Int32 iflag,
                //                      Int32 method,
                //                      double[] xnasc, double[] xndsc,
                //                      double[] xperi, double[] xaphe,
                //                      ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_time_equ() {
            using (var target = new SwissEph()) {
                //public int swe_time_equ(double tjd, out double e, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lmt_to_lat() {
            using (var target = new SwissEph()) {
                //public int swe_lmt_to_lat(double tjd_lmt, double geolon, out double tjd_lat, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_lat_to_lmt() {
            using (var target = new SwissEph()) {
                //public int swe_lat_to_lmt(double tjd_lat, double geolon, out double tjd_lmt, ref string serr);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_sidtime0() {
            using (var target = new SwissEph()) {
                //public double swe_sidtime0(double tjd_ut, double ecl, double nut);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_sidtime() {
            using (var target = new SwissEph()) {
                //public double swe_sidtime(double tjd_ut);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_cotrans() {
            using (var target = new SwissEph()) {
                //public void swe_cotrans(CPointer<double> xpo, CPointer<double> xpn, double eps);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_cotrans_sp() {
            using (var target = new SwissEph()) {
                //public void swe_cotrans_sp(CPointer<double> xpo, CPointer<double> xpn, double eps);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_get_tid_acc() {
            using (var target = new SwissEph()) {
                //public double swe_get_tid_acc();
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_set_tid_acc() {
            using (var target = new SwissEph()) {
                //public void swe_set_tid_acc(double tidacc);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_degnorm() {
            using (var target = new SwissEph()) {
                //public double swe_degnorm(double x);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_radnorm() {
            using (var target = new SwissEph()) {
                //public double swe_radnorm(double x);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_rad_midp() {
            using (var target = new SwissEph()) {
                //public double swe_rad_midp(double x1, double x0);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_deg_midp() {
            using (var target = new SwissEph()) {
                //public double swe_deg_midp(double x1, double x0);
            }
        }

        [Fact(Skip = "")]
        public void Test_swe_split_deg() {
            using (var target = new SwissEph()) {
                //public void swe_split_deg(double ddeg, Int32 roundflag, out Int32 ideg, out Int32 imin, out Int32 isec, out double dsecfr, out Int32 isgn);
            }
        }

        [Fact]
        public void Test_swe_csnorm() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_csnorm(0 * SwissEph.DEG));
                Assert.Equal(3 * SwissEph.DEG, target.swe_csnorm(3 * SwissEph.DEG));
                Assert.Equal(357 * SwissEph.DEG, target.swe_csnorm(-3 * SwissEph.DEG));
                Assert.Equal(3 * SwissEph.DEG, target.swe_csnorm(363 * SwissEph.DEG));
                Assert.Equal(357 * SwissEph.DEG, target.swe_csnorm(-363 * SwissEph.DEG));
            }
        }

        [Fact]
        public void Test_swe_difcsn() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_difcsn(0, 0));
                Assert.Equal(129599997, target.swe_difcsn(0, 3));
                Assert.Equal(3, target.swe_difcsn(3, 0));
                Assert.Equal(0, target.swe_difcsn(3, 3));
            }
        }

        [Fact]
        public void Test_swe_difdegn() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_difdegn(0, 0));
                Assert.Equal(357, target.swe_difdegn(0, 3));
                Assert.Equal(3, target.swe_difdegn(3, 0));
                Assert.Equal(0, target.swe_difdegn(3, 3));
            }
        }

        [Fact]
        public void Test_swe_difcs2n() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_difcs2n(0, 0));
                Assert.Equal(-3, target.swe_difcs2n(0, 3));
                Assert.Equal(3, target.swe_difcs2n(3, 0));
                Assert.Equal(0, target.swe_difcs2n(3, 3));
            }
        }

        [Fact]
        public void Test_swe_difdeg2n() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_difdeg2n(0, 0));
                Assert.Equal(-3, target.swe_difdeg2n(0, 3));
                Assert.Equal(3, target.swe_difdeg2n(3, 0));
                Assert.Equal(0, target.swe_difdeg2n(3, 3));
            }
        }

        [Fact]
        public void Test_swe_difrad2n() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_difrad2n(0, 0));
                Assert.Equal(-3, target.swe_difrad2n(0, 3));
                Assert.Equal(3, target.swe_difrad2n(3, 0));
                Assert.Equal(0, target.swe_difrad2n(3, 3));
            }
        }

        [Fact]
        public void Test_swe_csroundsec() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_csroundsec(0));
                Assert.Equal(10440000, target.swe_csroundsec(29 * SwissEph.DEG));
                Assert.Equal(10799900, target.swe_csroundsec(10800000 - 40));
                Assert.Equal(10800000, target.swe_csroundsec(30 * SwissEph.DEG));
                Assert.Equal(-10439900, target.swe_csroundsec(-29 * SwissEph.DEG));
                Assert.Equal(-10799900, target.swe_csroundsec(-(10800000 - 40)));
                Assert.Equal(-10799900, target.swe_csroundsec(-30 * SwissEph.DEG));
                
                Assert.Equal(1200, target.swe_csroundsec(1234));
                Assert.Equal(9900, target.swe_csroundsec(9876));
                Assert.Equal(-1100, target.swe_csroundsec(-1234));
                Assert.Equal(-9800, target.swe_csroundsec(-9876));
            }
        }

        [Fact]
        public void Test_swe_d2l() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_d2l(0));
                Assert.Equal(123, target.swe_d2l(123.45));
                Assert.Equal(124, target.swe_d2l(123.987));
                Assert.Equal(-123, target.swe_d2l(-123.45));
                Assert.Equal(-124, target.swe_d2l(-123.987));
            }
        }

        [Fact]
        public void Test_swe_cs2timestr() {
            using (var target = new SwissEph()) {
                Assert.Equal("00-00-00", target.swe_cs2timestr(0, '-', false));
                Assert.Equal("00-00", target.swe_cs2timestr(0, '-', true));
                Assert.Equal("00:00:00", target.swe_cs2timestr(0, ':', false));
                Assert.Equal("00:00", target.swe_cs2timestr(0, ':', true));

                Assert.Equal("03:25:46", target.swe_cs2timestr(1234567, ':', false));
                Assert.Equal("03:25:46", target.swe_cs2timestr(1234567, ':', true));

                Assert.Equal("0-:.+:,+", target.swe_cs2timestr(-1234567, ':', false));
                Assert.Equal("0-:.+:,+", target.swe_cs2timestr(-1234567, ':', true));
            }
        }

        [Fact]
        public void Test_swe_cs2lonlatstr() {
            using (var target = new SwissEph()) {
                Assert.Equal("p000", target.swe_cs2lonlatstr(0, 'p', 'm'));
                Assert.Equal("p325'46", target.swe_cs2lonlatstr(1234567, 'p', 'm'));
                Assert.Equal("m325'46", target.swe_cs2lonlatstr(-1234567, 'p', 'm'));
            }
        }

        [Fact]
        public void Test_swe_cs2degstr() {
            using (var target = new SwissEph()) {
                Assert.Equal(" 0°00'00", target.swe_cs2degstr(0));
                Assert.Equal(" 3°25'45", target.swe_cs2degstr(1234567));
            }
        }
    }
}
