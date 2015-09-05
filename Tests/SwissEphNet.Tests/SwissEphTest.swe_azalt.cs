using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [TestMethod]
        public void Test_swe_azalt() {
            using (var swe = new SwissEph()) {
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xin = new double[6], xaz = new double[6]; String serr = null;
                swe.swe_calc(tjd, SwissEph.SE_SUN, SwissEph.SEFLG_MOSEPH, xin, ref serr);
                swe.swe_azalt(tjd, SwissEph.SE_ECL2HOR, geopos, 0, 0, xin, xaz);
                Assert.AreEqual(249.642429315131, xaz[0], 0.000000001);
                Assert.AreEqual(-32.8340765334715, xaz[1], 0.0000000001);
                Assert.AreEqual(-32.8340765334715, xaz[2], 0.0000000001);
            }
        }
    }

}
