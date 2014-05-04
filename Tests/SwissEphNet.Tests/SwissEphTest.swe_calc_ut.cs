using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [TestMethod]
        public void Test_swe_calc_ut() {
            using (var swe = new SwissEph()) {
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;
                swe.swe_calc_ut(tjd, SwissEph.SE_SUN, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.AreEqual(142.780326430735, xx[0], 0.000000000001);
                Assert.AreEqual(-1.93227682119264E-05, xx[1], 0.0000000000001);
                Assert.AreEqual(1.01267244282576, xx[2], 0.0000000000001);
                Assert.AreEqual(0, xx[3], 0.0000000000001);
                Assert.AreEqual(0, xx[4], 0.0000000000001);
                Assert.AreEqual(0, xx[5], 0.0000000000001);
            }
        }
    }

}
