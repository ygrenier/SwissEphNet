using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {

        [Fact]
        public void TestJulDay() {
            using (var swe = new SwissEph()) {
                Assert.Equal(0.0, swe.swe_julday(-4713, 11, 24, 12.0, SwissEph.SE_GREG_CAL));

                Assert.Equal(0.0, swe.swe_julday(-4712, 1, 1, 12.0, SwissEph.SE_JUL_CAL));

                Assert.Equal(2000000.0, swe.swe_julday(763, 9, 18, 12.0, SwissEph.SE_GREG_CAL));

                Assert.Equal(2000000.0, swe.swe_julday(763, 9, 14, 12.0, SwissEph.SE_JUL_CAL));

                Assert.Equal(1063884, swe.swe_julday(-1800, 9, 18, 12.0, SwissEph.SE_GREG_CAL));

                Assert.Equal(1063865, swe.swe_julday(-1800, 9, 14, 12.0, SwissEph.SE_JUL_CAL));

                Assert.Equal(2442275.47916667, swe.swe_julday(1974, 8, 15, 23 + 30 / 60.0 + 0 / 3600.0, SwissEph.SE_GREG_CAL), 8);
                Assert.Equal(2456774.20375, swe.swe_julday(2014, 4, 26, SwissEph.GetHourValue(16, 53, 24), SwissEph.SE_GREG_CAL));
            }
        }

        [Fact]
        public void TestRevjul() {
            using (var swe = new SwissEph()) {
                int y = 0, m = 0, d = 0; double ut = 0;

                swe.swe_revjul(0, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.Equal(-4713, y);
                Assert.Equal(11, m);
                Assert.Equal(24, d);
                Assert.Equal(12.0, ut);

                swe.swe_revjul(0, SwissEph.SE_JUL_CAL, ref y, ref m, ref d, ref ut);
                Assert.Equal(-4712, y);
                Assert.Equal(1, m);
                Assert.Equal(1, d);
                Assert.Equal(12.0, ut);

                swe.swe_revjul(2000000, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.Equal(763, y);
                Assert.Equal(9, m);
                Assert.Equal(18, d);
                Assert.Equal(12.0, ut);

                swe.swe_revjul(2000000, SwissEph.SE_JUL_CAL, ref y, ref m, ref d, ref ut);
                Assert.Equal(763, y);
                Assert.Equal(9, m);
                Assert.Equal(14, d);
                Assert.Equal(12.0, ut);

                swe.swe_revjul(2456774.20375, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.Equal(2014, y);
                Assert.Equal(4, m);
                Assert.Equal(26, d);
                Assert.Equal(16.8899999968708, ut, 13);
                Assert.Equal(16.0, Math.Floor(ut));
                Assert.Equal(1013, Math.Floor(ut * 60.0));
                Assert.Equal(53.0, (Math.Floor(ut * 60.0)) % 60.0);
                Assert.Equal(60803, Math.Floor(ut * 3600.0));
                Assert.Equal(23.9999887347221, (ut * 3600.0) % 60.0, 13);
                Assert.Equal(23, Math.Floor(ut * 3600.0) % 60.0);


                swe.swe_revjul(2442275.47916667, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.Equal(1974, y);
                Assert.Equal(8, m);
                Assert.Equal(15, d);
                Assert.Equal(23.5000000745058, ut, 13);
                Assert.Equal(23.0, Math.Floor(ut));
                Assert.Equal(1410.0, Math.Floor(ut * 60.0));
                Assert.Equal(30.0, (Math.Floor(ut * 60.0)) % 60.0);
                Assert.Equal(84600, Math.Floor(ut * 3600.0));
                Assert.Equal(0, Math.Floor(ut * 3600.0) % 60.0);
            }
        }

        [Fact]
        public void LoadDeltaT()
        {
            using (var swe = new SwissEph())
            {
                var tjd_ut = swe.swe_julday(2017, 8, 26, 11.25, SwissEph.SE_GREG_CAL);
                Assert.Equal(0.00079744, swe.swe_deltat(tjd_ut), 8);
            }
            using (var swe = new SwissEph())
            {
                var tjd_ut = swe.swe_julday(2017, 8, 26, 11.25, SwissEph.SE_GREG_CAL);
                bool isLoaded = false;
                swe.OnLoadFile += (s, e) =>
                {
                    if (e.FileName == "[ephe]\\sedeltat.txt")
                    {
                        var asm = this.GetType().GetAssembly();
                        String sr = e.FileName.Replace("[ephe]", @"SwissEphNet.Tests.files").Replace("/", ".").Replace("\\", ".");
                        e.File = asm.GetManifestResourceStream(sr);
                        isLoaded = true;
                    }
                };
                Assert.Equal(0.00079744, swe.swe_deltat(tjd_ut), 8);
                Assert.True(isLoaded);
            }
        }

        //[Fact]
        //public void TestDeltaT_with_ESPENAK_MEEUS_2006() {
        //    using (var swe = new SwissEph()) {
        //        swe.ESPENAK_MEEUS_2006 = true;
        //        double deltaPrec = 0.00000000000001;

        //        Assert.Equal(1.5716511059188, swe.swe_deltat(0.0), deltaPrec);

        //        Assert.Equal(0.0374254553961889, swe.swe_deltat(2000000.0), deltaPrec);
        //        Assert.Equal(0.0374253886123893, swe.swe_deltat(2000000.25), deltaPrec);
        //        Assert.Equal(0.0374253218286385, swe.swe_deltat(2000000.5), deltaPrec);
        //        Assert.Equal(0.0374252550449363, swe.swe_deltat(2000000.75), deltaPrec);
        //        Assert.Equal(0.000848297829347124, swe.swe_deltat(2317746.13090277789), deltaPrec);

        //        // 2415020.0
        //        var vals = new Dictionary<double, double>() {
        //            {2400000.0,8.85169749775391E-05},
        //            {2400200.0,8.89367796449606E-05},
        //            {2400400.0,8.94708209388948E-05},
        //            {2400800.0,8.87232218904813E-05},
        //            {2402000.0,7.08931645254769E-05},
        //            {2404000.0,1.81148433665968E-05},
        //            {2408000.0,-6.40563578305767E-05},
        //            {2410000.0,-6.57993558330751E-05},
        //            {2412000.0,-7.15340361147556E-05},
        //            {2414000.0,-6.50989531127749E-05},
        //            {2418000.0,9.19707007809117E-05},
        //            //{2420000.0,8.85169749775391E-05},
        //            //{2430000.0,8.85169749775391E-05},
        //            //{2440000.0,8.85169749775391E-05},
        //            //{2450000.0,8.85169749775391E-05},
        //            //{2460000.0,8.85169749775391E-05},
        //            //{2470000.0,8.85169749775391E-05},
        //            //{2480000.0,8.85169749775391E-05},
        //            //{2490000.0,8.85169749775391E-05},

        //            //{2500000.0,8.85169749775391E-05},
        //            //{2510000.0,8.85169749775391E-05},
        //            //{2520000.0,8.85169749775391E-05},
        //            //{2530000.0,8.85169749775391E-05},
        //            //{2540000.0,8.85169749775391E-05},
        //            //{2550000.0,8.85169749775391E-05},
        //            //{2560000.0,8.85169749775391E-05},
        //            //{2570000.0,8.85169749775391E-05},
        //            //{2580000.0,8.85169749775391E-05},
        //            //{2590000.0,8.85169749775391E-05},

        //            //{2600000.0,8.85169749775391E-05},
        //            //{2610000.0,8.85169749775391E-05},
        //            //{2620000.0,8.85169749775391E-05},
        //            //{2630000.0,8.85169749775391E-05},
        //            //{2640000.0,8.85169749775391E-05},
        //            //{2650000.0,8.85169749775391E-05},
        //            //{2660000.0,8.85169749775391E-05},
        //            //{2670000.0,8.85169749775391E-05},
        //            //{2680000.0,8.85169749775391E-05},
        //            //{2690000.0,8.85169749775391E-05},

        //            //{2700000.0,8.85169749775391E-05},
        //            //{2710000.0,8.85169749775391E-05},
        //            //{2720000.0,8.85169749775391E-05},
        //            //{2730000.0,8.85169749775391E-05},
        //            //{2740000.0,8.85169749775391E-05},
        //            //{2750000.0,8.85169749775391E-05},
        //            //{2760000.0,8.85169749775391E-05},
        //            //{2770000.0,8.85169749775391E-05},
        //            //{2780000.0,8.85169749775391E-05},
        //            //{2790000.0,8.85169749775391E-05},

        //            //{2800000.0,8.85169749775391E-05},
        //            //{2810000.0,8.85169749775391E-05},
        //            //{2820000.0,8.85169749775391E-05},
        //            //{2830000.0,8.85169749775391E-05},
        //            //{2840000.0,8.85169749775391E-05},
        //            //{2850000.0,8.85169749775391E-05},
        //            //{2860000.0,8.85169749775391E-05},
        //            //{2870000.0,8.85169749775391E-05},
        //            //{2880000.0,8.85169749775391E-05},
        //            //{2890000.0,8.85169749775391E-05},

        //            //{2900000.0,8.85169749775391E-05},
        //            //{2910000.0,8.85169749775391E-05},
        //            //{2920000.0,8.85169749775391E-05},
        //            //{2930000.0,8.85169749775391E-05},
        //            //{2940000.0,8.85169749775391E-05},
        //            //{2950000.0,8.85169749775391E-05},
        //            //{2960000.0,8.85169749775391E-05},
        //            //{2970000.0,8.85169749775391E-05},
        //            //{2980000.0,8.85169749775391E-05},
        //            //{2990000.0,8.85169749775391E-05},

        //        };
        //        foreach (var kvp in vals) {
        //            Assert.Equal(kvp.Value, swe.swe_deltat(kvp.Key), deltaPrec, String.Format("deltat({0})", kvp.Key));
        //        }

        //        Assert.Equal(0.101230433035332, swe.swe_deltat(3000000), deltaPrec);
        //        Assert.Equal(0.101230598229371, swe.swe_deltat(3000000.5), deltaPrec);
        //        Assert.Equal(0.101230680826441, swe.swe_deltat(3000000.75), deltaPrec);

        //    }
        //}

        //[Fact]
        //public void TestDeltaT_without_ESPENAK_MEEUS_2006() {
        //    using (var swe = new SwissEph()) {
        //        swe.ESPENAK_MEEUS_2006 = false;
        //        double deltaPrec = 0.000000000001;

        //        Assert.Equal(1.5716511059188, swe.swe_deltat(0.0), deltaPrec);

        //        Assert.Equal(0.0375610997366034, swe.swe_deltat(2000000.0), deltaPrec);
        //        Assert.Equal(0.0375610327085927, swe.swe_deltat(2000000.25), deltaPrec);
        //        Assert.Equal(0.0375609656805818, swe.swe_deltat(2000000.5), deltaPrec);
        //        Assert.Equal(0.0375608986525707, swe.swe_deltat(2000000.75), deltaPrec);
        //        Assert.Equal(0.000848297829347124, swe.swe_deltat(2317746.13090277789), deltaPrec);

        //        var tjd = SwissEph.J2000 + (365.25 * (1610 - 2000.0));
        //        Assert.Equal(0.00138947083317893, swe.swe_deltat(tjd), deltaPrec);


        //        Assert.Equal(0.101230433035332, swe.swe_deltat(3000000), deltaPrec);
        //        Assert.Equal(0.101230598229371, swe.swe_deltat(3000000.5), deltaPrec);
        //        Assert.Equal(0.101230680826441, swe.swe_deltat(3000000.75), deltaPrec);

        //    }
        //}

        [Fact]
        public void Test_swe_utc_time_zone() {
            using (var swe = new SwissEph()) {
                int year = 1974, month = 8, day = 16, hour = 0, min = 30; double sec = 0;

                // local to utc
                swe.swe_utc_time_zone(year, month, day, hour, min, sec, +2.0, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(8, month);
                Assert.Equal(15, day);
                Assert.Equal(22, hour);
                Assert.Equal(30, min);
                Assert.Equal(0.0, sec);

                // utc to local
                swe.swe_utc_time_zone(year, month, day, hour, min, sec, -2.0, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(30, min);
                Assert.Equal(0.0, sec);

                // check leap sec
                sec = 61;
                swe.swe_utc_time_zone(year, month, day, hour, min, sec, -2.0, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(2, hour);
                Assert.Equal(30, min);
                Assert.Equal(60.9999999999998, sec, 13);

            }
        }

        [Fact]
        public void Test_swe_utc_to_jd() {
            using (var swe = new SwissEph()) {
                int year = 1974, month = 8, day = 16, hour = 0, min = 30; double sec = 0;
                string serr = null; double[] dret = new double[2];

                var res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2442275.5213563, dret[0], 7);
                Assert.Equal(2442275.52083417, dret[1], 7);
                Assert.Null(serr);

                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_JUL_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2442288.5213563, dret[0], 7);
                Assert.Equal(2442288.52083377, dret[1], 7);
                Assert.Null(serr);

                // leap second
                res = swe.swe_utc_to_jd(year, 12, 31, 23, 59, 60, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2442413.50052296, dret[0], 7);
                Assert.Equal(2442413.49999659, dret[1], 7);
                Assert.Null(serr);

                res = swe.swe_utc_to_jd(year, 12, 31, 23, 59, 60, SwissEph.SE_JUL_CAL, dret, ref serr);
                Assert.Equal(SwissEph.ERR, res);
                Assert.Equal("invalid time (no leap second!): 23:59:60.00", serr);
                serr = null;

                // Before 1972
                year = 1960;
                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2437162.52122023, dret[0], 7);
                Assert.Equal(2437162.52083333, dret[1], 7);
                Assert.Null(serr);

                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_JUL_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2437175.52122041, dret[0], 7);
                Assert.Equal(2437175.52083333, dret[1], 7);
                Assert.Null(serr);

                // Date > today (after last leap date)
                year = 2030;
                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2462729.5216884, dret[0], 7);
                Assert.Equal(2462729.52083333, dret[1], 7);
                Assert.Null(serr);

                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_JUL_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2462742.5216885, dret[0], 7);
                Assert.Equal(2462742.52083333, dret[1], 7);
                Assert.Null(serr);

                // Errors
                year = 1974;
                res = swe.swe_utc_to_jd(year, 2, 31, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.ERR, res);
                Assert.Equal("invalid date: year = 1974, month = 2, day = 31", serr);

                res = swe.swe_utc_to_jd(year, month, day, hour, 62, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.ERR, res);
                Assert.Equal("invalid time: 0:62:0.00", serr);

            }
        }

        [Fact]
        public void Test_swe_jdet_to_utc() {
            using (var swe = new SwissEph()) {
                int year = 0, month = 0, day = 0, hour = 0, min = 0; double sec = 0;

                swe.swe_jdet_to_utc(2442275.5213563, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(30, min);
                Assert.Equal(0.000335276126861572, sec, 14);

                swe.swe_jdet_to_utc(2442288.5213563, SwissEph.SE_JUL_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(30, min);
                Assert.Equal(0.000335276126861572, sec, 14);

                // leap second
                swe.swe_jdet_to_utc(2442413.50052296, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(12, month);
                Assert.Equal(31, day);
                Assert.Equal(23, hour);
                Assert.Equal(59, min);
                Assert.Equal(59.9997586011887, sec, 12);
                swe.swe_jdet_to_utc(2442413.50052299, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(12, month);
                Assert.Equal(31, day);
                Assert.Equal(23, hour);
                Assert.Equal(59, min);
                Assert.Equal(60.0022987127304, sec, 12);
                swe.swe_jdet_to_utc(2442413.50055, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1975, year);
                Assert.Equal(1, month);
                Assert.Equal(1, day);
                Assert.Equal(0, hour);
                Assert.Equal(0, min);
                Assert.Equal(1.33598148822784, sec, 12);

                // Before 1972
                swe.swe_jdet_to_utc(2437162.52122023, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1960, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(29, min);
                Assert.Equal(59.9986857175827, sec, 12);

                swe.swe_jdet_to_utc(2437175.52122041, SwissEph.SE_JUL_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1960, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(29, min);
                Assert.Equal(59.9987661838531, sec, 12);

                // Date > today (after last leap date)
                swe.swe_jdet_to_utc(2462729.5218584, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(2030, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(30, min);
                Assert.Equal(14.687642455101, sec, 12);

                swe.swe_jdet_to_utc(2462742.52185908, SwissEph.SE_JUL_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(2030, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(30, min);
                Assert.Equal(14.739422500134, sec, 12);

            }
        }

        [Fact]
        public void Test_swe_jdut1_to_utc() {
            using (var swe = new SwissEph()) {
                int year = 0, month = 0, day = 0, hour = 0, min = 0; double sec = 0;

                swe.swe_jdut1_to_utc(2442275.5213563, SwissEph.SE_GREG_CAL, ref year, ref month, ref day, ref hour, ref min, ref sec);
                Assert.Equal(1974, year);
                Assert.Equal(8, month);
                Assert.Equal(16, day);
                Assert.Equal(0, hour);
                Assert.Equal(30, min);
                Assert.Equal(45.1117783784866, sec, 10);

            }
        }

        [Fact]
        public void Test_swe_date_conversion() {
            using (var swe = new SwissEph()) {
                int year = 1974, month = 8, day = 16; double hour = 0.5; double tjd = 0.0;

                var res = swe.swe_date_conversion(year, month, day, hour, 'g', ref tjd);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2442275.52083333, tjd, 8);

                res = swe.swe_date_conversion(year, month, day, hour, 'j', ref tjd);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2442288.52083333, tjd, 8);

                res = swe.swe_date_conversion(year, month, 32, hour, 'j', ref tjd);
                Assert.Equal(SwissEph.ERR, res);
            }
        }

        [Fact]
        public void Test_LoadingLeapSeconds() {
            int year = 2019, month = 12, day = 31, hour = 23, min = 59; double sec = 60;
            string serr = null; double[] dret = new double[2]; int res;

            // The date is not a standard leap second
            using (var swe = new SwissEph()) {
                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.ERR, res);
                Assert.Equal("invalid time (no leap second!): 23:59:60.00", serr);
            }

            // We include the date as leap second in fake loading
            String content = @"
# comments
19720630
19721231
19731231
19741231
19751231
19761231
19771231
19781231
19791231
19810630
19820630
19830630
19850630
19871231
19891231
19901231
19920630
19930630
19940630
19951231
19970630
19981231
20051231
20081231

#
20101231
20111231
20121231
20131231
20141231
20141231
20161231
20171231
20181231
20191231
20201231
20211231
20221231
20231231
20241231
20251231
20261231
20271231
20281231
20291231
20301231
20311231
20321231
20331231
20341231
20351231
20361231
20371231

";
            serr = null;
            using (var swe = new SwissEph()) {
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == @"[ephe]\seleapsec.txt") {
                        e.File = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(content));
                    }
                };

                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2458849.50082389, dret[0], 8);
                Assert.Equal(2458849.50001255, dret[1], 8);
                Assert.Null(serr);
            }

            // We adding a lot of lines in 'file' for code coverage purpose
            StringBuilder sb = new StringBuilder(content);
            for (int i = 0; i < 100; i++) {
                sb.AppendFormat("{0}1231", 2038 + i).AppendLine();
            }
            content = sb.ToString();
            serr = null;
            using (var swe = new SwissEph()) {
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == @"[ephe]\seleapsec.txt") {
                        e.File = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(content));
                    }
                };

                res = swe.swe_utc_to_jd(year, month, day, hour, min, sec, SwissEph.SE_GREG_CAL, dret, ref serr);
                Assert.Equal(SwissEph.OK, res);
                Assert.Equal(2458849.50082389, dret[0], 8);
                Assert.Equal(2458849.50001255, dret[1], 8);
                Assert.Null(serr);
            }
        }

        [Fact]
        public void Test_swe_day_of_week() {
            using (var target = new SwissEph()) {
                Assert.Equal(0, target.swe_day_of_week(0));
                Assert.Equal(5, target.swe_day_of_week(2456774.20375));
                Assert.Equal(6, target.swe_day_of_week(2456775.20375));
                Assert.Equal(0, target.swe_day_of_week(2456776.20375));
                Assert.Equal(1, target.swe_day_of_week(2456777.20375));
                Assert.Equal(2, target.swe_day_of_week(2456778.20375));
                Assert.Equal(3, target.swe_day_of_week(2456779.20375));
                Assert.Equal(4, target.swe_day_of_week(2456780.20375));
                Assert.Equal(5, target.swe_day_of_week(2456781.20375));
            }
        }

    }
}
