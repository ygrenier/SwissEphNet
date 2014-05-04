using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [TestMethod]
        public void Test_swe_azalt_rev() {
            using (var swe = new SwissEph()) {
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xin = new double[] { 249.64242931513104, -32.8340765334715 }; 
                double[] xaz = new double[6];
                swe.swe_azalt_rev(tjd, SwissEph.SE_ECL2HOR, geopos, xin, xaz);
                Assert.AreEqual(142.779824404441, xaz[0], 0.000000000001);
                Assert.AreEqual(-1.93084572176467E-05, xaz[1], 0.0000000000001);
                Assert.AreEqual(0, xaz[2], 0.0000000000001);
            }
        }
    }

}
