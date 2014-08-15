using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public partial class SwissEphTest
    {
        [TestMethod]
        public void TestConstructor() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(true, target.ESPENAK_MEEUS_2006);
            }
        }

        [TestMethod]
        public void TestVersion() {
            using (var target = new SwissEph()) {
                Assert.AreEqual("2.00.00", target.swe_version());
                Assert.AreEqual("2.00.00-net-0004", target.swe_dotnet_version());
            }
        }

        [TestMethod]
        public void TestOnLoadFile() {
            // No file loading defined
            using (var target = new SwissEph()) {
                Assert.AreEqual("100: not found", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

            // File loading defined, but file not found
            using (var target = new SwissEph()) {
                target.OnLoadFile += (s, e) => {
                    e.File = null;
                };
                Assert.AreEqual("100: not found", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
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
                Assert.AreEqual("Hekate", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

        }

        [TestMethod]
        public void Test_swe_heliacal_ut() {
            //public Int32 swe_heliacal_ut(double tjdstart_ut, double[] geopos, double[] datm, double[] dobs, string ObjectName, 
            //Int32 TypeEvent, Int32 iflag, double[] dret, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_heliacal_pheno_ut() {
            //public Int32 swe_heliacal_pheno_ut(double tjd_ut, double[] geopos, double[] datm, double[] dobs, string ObjectName,
            //Int32 TypeEvent, Int32 helflag, double[] darr, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_vis_limit_mag() {
            //public Int32 swe_vis_limit_mag(double tjdut, double[] geopos, double[] datm, double[] dobs, string ObjectName,
            //    Int32 helflag, double[] dret, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_heliacal_angle() {
            //public Int32 swe_heliacal_angle(double tjdut, double[] dgeo, double[] datm, double[] dobs, Int32 helflag, double mag,
            //    double azi_obj, double azi_sun, double azi_moon, double alt_moon, double[] dret, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_topo_arcus_visionis() {
            //public Int32 swe_topo_arcus_visionis(double tjdut, double[] dgeo, double[] datm, double[] dobs, Int32 helflag, double mag,
            //    double azi_obj, double alt_obj, double azi_sun, double azi_moon, double alt_moon, ref double dret, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_fixstar() {
            //public Int32 swe_fixstar(string star, double tjd, Int32 iflag, double[] xx, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_fixstar_ut() {
            //public Int32 swe_fixstar_ut(string star, double tjd_ut, Int32 iflag, double[] xx, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_fixstar_mag() {
            //public Int32 swe_fixstar_mag(ref string star, ref double mag, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_close() {
            //public void swe_close();
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_set_ephe_path() {
            //public void swe_set_ephe_path(String path);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_set_jpl_file() {
            //public void swe_set_jpl_file(string fname);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_set_sid_mode() {
            //public void swe_set_sid_mode(Int32 sid_mode, double t0, double ayan_t0);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_get_ayanamsa() {
            //public double swe_get_ayanamsa(double tjd_et);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_get_ayanamsa_ut() {
            //public double swe_get_ayanamsa_ut(double tjd_ut);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_get_ayanamsa_name() {
            //public string swe_get_ayanamsa_name(Int32 isidmode);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_houses() {
            //public int swe_houses(double tjd_ut, double geolat, double geolon, char hsys, double[] cusps, double[] ascmc);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_houses_ex() {
            //public int swe_houses_ex(double tjd_ut, Int32 iflag, double geolat, double geolon, char hsys, CPointer<double> hcusps, CPointer<double> ascmc);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_houses_armc() {
            //public int swe_houses_armc(double armc, double geolat, double eps, char hsys, double[] cusps, double[] ascmc);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_house_pos() {
            //public double swe_house_pos(double armc, double geolon, double eps, char hsys, double[] xpin, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_house_name() {
            //public string swe_house_name(char hsys);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_gauquelin_sector() {
            //public Int32 swe_gauquelin_sector(double t_ut, Int32 ipl, String starname, Int32 iflag, Int32 imeth, double[] geopos,
            //    double atpress, double attemp, ref double dgsect, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_sol_eclipse_where() {
            //public Int32 swe_sol_eclipse_where(double tjd, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lun_occult_where()
        {
            //public Int32 swe_lun_occult_where(double tjd, Int32 ipl, string starname, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            using (var target = new SwissEph())
            {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_sol_eclipse_how() {
            //public Int32 swe_sol_eclipse_how(double tjd, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lun_occult_when_glob() {
            //public Int32 swe_lun_occult_when_glob(double tjd_start, Int32 ipl, string starname, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Double[] tret = new double[12];
                String serr = null;
                var r = target.swe_lun_occult_when_glob(0.0, 0, null, 0, 0, tret, false, ref serr);
                Assert.AreEqual(-1, r);
            }
        }

        [TestMethod]
        public void Test_swe_sol_eclipse_when_loc() {
            //public Int32 swe_sol_eclipse_when_loc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lun_occult_when_loc() {
            //public Int32 swe_lun_occult_when_loc(double tjd_start, Int32 ipl, String starname, Int32 ifl, double[] geopos, double[] tret,
            //    double[] attr, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_sol_eclipse_when_glob() {
            //public Int32 swe_sol_eclipse_when_glob(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lun_eclipse_how() {
            //public Int32 swe_lun_eclipse_how(double tjd_ut, Int32 ifl, double[] geopos, double[] attr, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lun_eclipse_when() {
            //public Int32 swe_lun_eclipse_when(double tjd_start, Int32 ifl, Int32 ifltype, double[] tret, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lun_eclipse_when_loc() {
            //public Int32 swe_lun_eclipse_when_loc(double tjd_start, Int32 ifl, double[] geopos, double[] tret, double[] attr, bool backward, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_pheno() {
            //public Int32 swe_pheno(double tjd, Int32 ipl, Int32 iflag, double[] attr, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_pheno_ut() {
            //public Int32 swe_pheno_ut(double tjd_ut, Int32 ipl, Int32 iflag, double[] attr, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_refrac() {
            //public double swe_refrac(double inalt, double atpress, double attemp, Int32 calc_flag);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_refrac_extended() {
            //public double swe_refrac_extended(double inalt, double geoalt, double atpress, double attemp, double lapse_rate, Int32 calc_flag, double[] dret);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_set_lapse_rate() {
            //public void swe_set_lapse_rate(double lapse_rate);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_rise_trans_true_hor() {
            //public Int32 swe_rise_trans_true_hor(double tjd_ut, Int32 ipl, string starname,
            //           Int32 epheflag, Int32 rsmi, double[] geopos, double atpress, double attemp,
            //           double horhgt, ref double tret, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_rise_trans() {
            //public Int32 swe_rise_trans(double tjd_ut, Int32 ipl, string starname, Int32 epheflag, Int32 rsmi,
            //    double[] geopos, double atpress, double attemp, ref double tret, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_nod_aps() {
            //public Int32 swe_nod_aps(double tjd_et, Int32 ipl, Int32 iflag,
            //                      Int32 method,
            //                      double[] xnasc, double[] xndsc,
            //                      double[] xperi, double[] xaphe,
            //                      ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_nod_aps_ut() {
            //public Int32 swe_nod_aps_ut(double tjd_ut, Int32 ipl, Int32 iflag,
            //                      Int32 method,
            //                      double[] xnasc, double[] xndsc,
            //                      double[] xperi, double[] xaphe,
            //                      ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_time_equ() {
            //public int swe_time_equ(double tjd, out double e, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lmt_to_lat() {
            //public int swe_lmt_to_lat(double tjd_lmt, double geolon, out double tjd_lat, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_lat_to_lmt() {
            //public int swe_lat_to_lmt(double tjd_lat, double geolon, out double tjd_lmt, ref string serr);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_sidtime0() {
            //public double swe_sidtime0(double tjd_ut, double ecl, double nut);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_sidtime() {
            //public double swe_sidtime(double tjd_ut);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_cotrans() {
            //public void swe_cotrans(CPointer<double> xpo, CPointer<double> xpn, double eps);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_cotrans_sp() {
            //public void swe_cotrans_sp(CPointer<double> xpo, CPointer<double> xpn, double eps);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_get_tid_acc() {
            //public double swe_get_tid_acc();
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_set_tid_acc() {
            //public void swe_set_tid_acc(double tidacc);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_degnorm() {
            //public double swe_degnorm(double x);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_radnorm() {
            //public double swe_radnorm(double x);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_rad_midp() {
            //public double swe_rad_midp(double x1, double x0);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_deg_midp() {
            //public double swe_deg_midp(double x1, double x0);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_split_deg() {
            //public void swe_split_deg(double ddeg, Int32 roundflag, out Int32 ideg, out Int32 imin, out Int32 isec, out double dsecfr, out Int32 isgn);
            using (var target = new SwissEph()) {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Test_swe_csnorm() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_csnorm(0 * SwissEph.DEG));
                Assert.AreEqual(3 * SwissEph.DEG, target.swe_csnorm(3 * SwissEph.DEG));
                Assert.AreEqual(357 * SwissEph.DEG, target.swe_csnorm(-3 * SwissEph.DEG));
                Assert.AreEqual(3 * SwissEph.DEG, target.swe_csnorm(363 * SwissEph.DEG));
                Assert.AreEqual(357 * SwissEph.DEG, target.swe_csnorm(-363 * SwissEph.DEG));
            }
        }

        [TestMethod]
        public void Test_swe_difcsn() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_difcsn(0, 0));
                Assert.AreEqual(129599997, target.swe_difcsn(0, 3));
                Assert.AreEqual(3, target.swe_difcsn(3, 0));
                Assert.AreEqual(0, target.swe_difcsn(3, 3));
            }
        }

        [TestMethod]
        public void Test_swe_difdegn() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_difdegn(0, 0));
                Assert.AreEqual(357, target.swe_difdegn(0, 3));
                Assert.AreEqual(3, target.swe_difdegn(3, 0));
                Assert.AreEqual(0, target.swe_difdegn(3, 3));
            }
        }

        [TestMethod]
        public void Test_swe_difcs2n() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_difcs2n(0, 0));
                Assert.AreEqual(-3, target.swe_difcs2n(0, 3));
                Assert.AreEqual(3, target.swe_difcs2n(3, 0));
                Assert.AreEqual(0, target.swe_difcs2n(3, 3));
            }
        }

        [TestMethod]
        public void Test_swe_difdeg2n() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_difdeg2n(0, 0));
                Assert.AreEqual(-3, target.swe_difdeg2n(0, 3));
                Assert.AreEqual(3, target.swe_difdeg2n(3, 0));
                Assert.AreEqual(0, target.swe_difdeg2n(3, 3));
            }
        }

        [TestMethod]
        public void Test_swe_difrad2n() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_difrad2n(0, 0));
                Assert.AreEqual(-3, target.swe_difrad2n(0, 3));
                Assert.AreEqual(3, target.swe_difrad2n(3, 0));
                Assert.AreEqual(0, target.swe_difrad2n(3, 3));
            }
        }

        [TestMethod]
        public void Test_swe_csroundsec() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_csroundsec(0));
                Assert.AreEqual(10440000, target.swe_csroundsec(29 * SwissEph.DEG));
                Assert.AreEqual(10799900, target.swe_csroundsec(10800000 - 40));
                Assert.AreEqual(10800000, target.swe_csroundsec(30 * SwissEph.DEG));
                Assert.AreEqual(-10439900, target.swe_csroundsec(-29 * SwissEph.DEG));
                Assert.AreEqual(-10799900, target.swe_csroundsec(-(10800000 - 40)));
                Assert.AreEqual(-10799900, target.swe_csroundsec(-30 * SwissEph.DEG));
                
                Assert.AreEqual(1200, target.swe_csroundsec(1234));
                Assert.AreEqual(9900, target.swe_csroundsec(9876));
                Assert.AreEqual(-1100, target.swe_csroundsec(-1234));
                Assert.AreEqual(-9800, target.swe_csroundsec(-9876));
            }
        }

        [TestMethod]
        public void Test_swe_d2l() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(0, target.swe_d2l(0));
                Assert.AreEqual(123, target.swe_d2l(123.45));
                Assert.AreEqual(124, target.swe_d2l(123.987));
                Assert.AreEqual(-123, target.swe_d2l(-123.45));
                Assert.AreEqual(-124, target.swe_d2l(-123.987));
            }
        }

        [TestMethod]
        public void Test_swe_cs2timestr() {
            using (var target = new SwissEph()) {
                Assert.AreEqual("00-00-00", target.swe_cs2timestr(0, '-', false));
                Assert.AreEqual("00-00", target.swe_cs2timestr(0, '-', true));
                Assert.AreEqual("00:00:00", target.swe_cs2timestr(0, ':', false));
                Assert.AreEqual("00:00", target.swe_cs2timestr(0, ':', true));

                Assert.AreEqual("03:25:46", target.swe_cs2timestr(1234567, ':', false));
                Assert.AreEqual("03:25:46", target.swe_cs2timestr(1234567, ':', true));

                Assert.AreEqual("0-:.+:,+", target.swe_cs2timestr(-1234567, ':', false));
                Assert.AreEqual("0-:.+:,+", target.swe_cs2timestr(-1234567, ':', true));
            }
        }

        [TestMethod]
        public void Test_swe_cs2lonlatstr() {
            using (var target = new SwissEph()) {
                Assert.AreEqual("p000", target.swe_cs2lonlatstr(0, 'p', 'm'));
                Assert.AreEqual("p325'46", target.swe_cs2lonlatstr(1234567, 'p', 'm'));
                Assert.AreEqual("m325'46", target.swe_cs2lonlatstr(-1234567, 'p', 'm'));
            }
        }

        [TestMethod]
        public void Test_swe_cs2degstr() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(" 0°00'00", target.swe_cs2degstr(0));
                Assert.AreEqual(" 3°25'45", target.swe_cs2degstr(1234567));
            }
        }
    }
}
