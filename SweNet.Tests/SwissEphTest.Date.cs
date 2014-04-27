using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SweNet.Tests
{
    partial class SwissEphTest
    {

        [TestMethod]
        public void TestJulDay() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual(0.0, swe.swe_julday(-4713, 11, 24, 12.0, SwissEph.SE_GREG_CAL));

                Assert.AreEqual(0.0, swe.swe_julday(-4712, 1, 1, 12.0, SwissEph.SE_JUL_CAL));

                Assert.AreEqual(2000000.0, swe.swe_julday(763, 9, 18, 12.0, SwissEph.SE_GREG_CAL));

                Assert.AreEqual(2000000.0, swe.swe_julday(763, 9, 14, 12.0, SwissEph.SE_JUL_CAL));

                Assert.AreEqual(1063884, swe.swe_julday(-1800, 9, 18, 12.0, SwissEph.SE_GREG_CAL));

                Assert.AreEqual(1063865, swe.swe_julday(-1800, 9, 14, 12.0, SwissEph.SE_JUL_CAL));

                Assert.AreEqual(2442275.47916667, swe.swe_julday(1974, 8, 15, 23 + 30 / 60.0 + 0 / 3600.0, SwissEph.SE_GREG_CAL), 0.00000001);
                Assert.AreEqual(2456774.20375, swe.swe_julday(2014, 4, 26, SweDate.GetHourValue(16, 53, 24), SwissEph.SE_GREG_CAL));
            }
        }

        [TestMethod]
        public void TestRevjul() {
            using (var swe = new SwissEph()) {
                int y = 0, m = 0, d = 0; double ut = 0;

                swe.swe_revjul(0, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.AreEqual(-4713, y);
                Assert.AreEqual(11, m);
                Assert.AreEqual(24, d);
                Assert.AreEqual(12.0, ut);

                swe.swe_revjul(0, SwissEph.SE_JUL_CAL, ref y, ref m, ref d, ref ut);
                Assert.AreEqual(-4712, y);
                Assert.AreEqual(1, m);
                Assert.AreEqual(1, d);
                Assert.AreEqual(12.0, ut);

                swe.swe_revjul(2000000, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.AreEqual(763, y);
                Assert.AreEqual(9, m);
                Assert.AreEqual(18, d);
                Assert.AreEqual(12.0, ut);

                swe.swe_revjul(2000000, SwissEph.SE_JUL_CAL, ref y, ref m, ref d, ref ut);
                Assert.AreEqual(763, y);
                Assert.AreEqual(9, m);
                Assert.AreEqual(14, d);
                Assert.AreEqual(12.0, ut);

                swe.swe_revjul(2456774.20375, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.AreEqual(2014, y);
                Assert.AreEqual(4, m);
                Assert.AreEqual(26, d);
                Assert.AreEqual(16.8899999968708, ut, 0.0000000000001);
                Assert.AreEqual(16.0, Math.Floor(ut));
                Assert.AreEqual(1013, Math.Floor(ut * 60.0));
                Assert.AreEqual(53.0, (Math.Floor(ut * 60.0)) % 60.0);
                Assert.AreEqual(60803, Math.Floor(ut * 3600.0));
                Assert.AreEqual(23.9999887347221, (ut * 3600.0) % 60.0, 0.0000000000001);
                Assert.AreEqual(23, Math.Floor(ut * 3600.0) % 60.0);
                

                swe.swe_revjul(2442275.47916667, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
                Assert.AreEqual(1974, y);
                Assert.AreEqual(8, m);
                Assert.AreEqual(15, d);
                Assert.AreEqual(23.5000000745058, ut, 0.0000000000001);
                Assert.AreEqual(23.0, Math.Floor(ut));
                Assert.AreEqual(1410.0, Math.Floor(ut * 60.0));
                Assert.AreEqual(30.0, (Math.Floor(ut * 60.0)) % 60.0);
                Assert.AreEqual(84600, Math.Floor(ut * 3600.0));
                Assert.AreEqual(0, Math.Floor(ut * 3600.0) % 60.0);
            }
        }

        [TestMethod]
        public void TestDeltaT_with_ESPENAK_MEEUS_2006() {
            using (var swe = new SwissEph()) {
                swe.ESPENAK_MEEUS_2006 = true;
                double deltaPrec = 0.00000000000001;

                Assert.AreEqual(1.5716511059188, swe.swe_deltat(0.0), deltaPrec);

                Assert.AreEqual(0.0374254553961889, swe.swe_deltat(2000000.0), deltaPrec);
                Assert.AreEqual(0.0374253886123893, swe.swe_deltat(2000000.25), deltaPrec);
                Assert.AreEqual(0.0374253218286385, swe.swe_deltat(2000000.5), deltaPrec);
                Assert.AreEqual(0.0374252550449363, swe.swe_deltat(2000000.75), deltaPrec);
                Assert.AreEqual(0.000848297829347124, swe.swe_deltat(2317746.13090277789), deltaPrec);

                // 2415020.0
                var vals = new Dictionary<double, double>() {
                    {2400000.0,8.85169749775391E-05},
                    {2400200.0,8.89367796449606E-05},
                    {2400400.0,8.94708209388948E-05},
                    {2400800.0,8.87232218904813E-05},
                    {2402000.0,7.08931645254769E-05},
                    {2404000.0,1.81148433665968E-05},
                    {2408000.0,-6.40563578305767E-05},
                    {2410000.0,-6.57993558330751E-05},
                    {2412000.0,-7.15340361147556E-05},
                    {2414000.0,-6.50989531127749E-05},
                    {2418000.0,9.19707007809117E-05},
                    //{2420000.0,8.85169749775391E-05},
                    //{2430000.0,8.85169749775391E-05},
                    //{2440000.0,8.85169749775391E-05},
                    //{2450000.0,8.85169749775391E-05},
                    //{2460000.0,8.85169749775391E-05},
                    //{2470000.0,8.85169749775391E-05},
                    //{2480000.0,8.85169749775391E-05},
                    //{2490000.0,8.85169749775391E-05},

                    //{2500000.0,8.85169749775391E-05},
                    //{2510000.0,8.85169749775391E-05},
                    //{2520000.0,8.85169749775391E-05},
                    //{2530000.0,8.85169749775391E-05},
                    //{2540000.0,8.85169749775391E-05},
                    //{2550000.0,8.85169749775391E-05},
                    //{2560000.0,8.85169749775391E-05},
                    //{2570000.0,8.85169749775391E-05},
                    //{2580000.0,8.85169749775391E-05},
                    //{2590000.0,8.85169749775391E-05},

                    //{2600000.0,8.85169749775391E-05},
                    //{2610000.0,8.85169749775391E-05},
                    //{2620000.0,8.85169749775391E-05},
                    //{2630000.0,8.85169749775391E-05},
                    //{2640000.0,8.85169749775391E-05},
                    //{2650000.0,8.85169749775391E-05},
                    //{2660000.0,8.85169749775391E-05},
                    //{2670000.0,8.85169749775391E-05},
                    //{2680000.0,8.85169749775391E-05},
                    //{2690000.0,8.85169749775391E-05},

                    //{2700000.0,8.85169749775391E-05},
                    //{2710000.0,8.85169749775391E-05},
                    //{2720000.0,8.85169749775391E-05},
                    //{2730000.0,8.85169749775391E-05},
                    //{2740000.0,8.85169749775391E-05},
                    //{2750000.0,8.85169749775391E-05},
                    //{2760000.0,8.85169749775391E-05},
                    //{2770000.0,8.85169749775391E-05},
                    //{2780000.0,8.85169749775391E-05},
                    //{2790000.0,8.85169749775391E-05},

                    //{2800000.0,8.85169749775391E-05},
                    //{2810000.0,8.85169749775391E-05},
                    //{2820000.0,8.85169749775391E-05},
                    //{2830000.0,8.85169749775391E-05},
                    //{2840000.0,8.85169749775391E-05},
                    //{2850000.0,8.85169749775391E-05},
                    //{2860000.0,8.85169749775391E-05},
                    //{2870000.0,8.85169749775391E-05},
                    //{2880000.0,8.85169749775391E-05},
                    //{2890000.0,8.85169749775391E-05},

                    //{2900000.0,8.85169749775391E-05},
                    //{2910000.0,8.85169749775391E-05},
                    //{2920000.0,8.85169749775391E-05},
                    //{2930000.0,8.85169749775391E-05},
                    //{2940000.0,8.85169749775391E-05},
                    //{2950000.0,8.85169749775391E-05},
                    //{2960000.0,8.85169749775391E-05},
                    //{2970000.0,8.85169749775391E-05},
                    //{2980000.0,8.85169749775391E-05},
                    //{2990000.0,8.85169749775391E-05},

                };
                foreach (var kvp in vals) {
                    Assert.AreEqual(kvp.Value, swe.swe_deltat(kvp.Key), deltaPrec, String.Format("deltat({0})", kvp.Key));
                }

                Assert.AreEqual(0.101230433035332, swe.swe_deltat(3000000), deltaPrec);
                Assert.AreEqual(0.101230598229371, swe.swe_deltat(3000000.5), deltaPrec);
                Assert.AreEqual(0.101230680826441, swe.swe_deltat(3000000.75), deltaPrec);

            }
        }

        [TestMethod]
        public void TestDeltaT_without_ESPENAK_MEEUS_2006() {
            using (var swe = new SwissEph()) {
                swe.ESPENAK_MEEUS_2006 = false;
                double deltaPrec = 0.000000000001;

                Assert.AreEqual(1.5716511059188, swe.swe_deltat(0.0), deltaPrec);

                Assert.AreEqual(0.0375610997366034, swe.swe_deltat(2000000.0), deltaPrec);
                Assert.AreEqual(0.0375610327085927, swe.swe_deltat(2000000.25), deltaPrec);
                Assert.AreEqual(0.0375609656805818, swe.swe_deltat(2000000.5), deltaPrec);
                Assert.AreEqual(0.0375608986525707, swe.swe_deltat(2000000.75), deltaPrec);
                Assert.AreEqual(0.000848297829347124, swe.swe_deltat(2317746.13090277789), deltaPrec);

                var tjd = SwissEph.J2000 + (365.25 * (1610 - 2000.0));
                Assert.AreEqual(0.00138947083317893, swe.swe_deltat(tjd), deltaPrec);


                Assert.AreEqual(0.101230433035332, swe.swe_deltat(3000000), deltaPrec);
                Assert.AreEqual(0.101230598229371, swe.swe_deltat(3000000.5), deltaPrec);
                Assert.AreEqual(0.101230680826441, swe.swe_deltat(3000000.75), deltaPrec);

            }
        }

    }
}
