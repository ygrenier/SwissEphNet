using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [TestMethod]
        public void Test_swe_set_topo()
        {
            using (var swe = new SwissEph())
            {
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xin1 = new double[6], 
                    xin2 = new double[6],
                    xin3 = new double[6]
                    ; String serr = null;

                swe.swe_calc(tjd, SwissEph.SE_SUN, 33024, xin1, ref serr);
                Assert.AreEqual(0, xin1[0], 0.0000000000001);
                Assert.AreEqual(0, xin1[1], 0.0000000000001);
                Assert.AreEqual(0, xin1[2], 0.0000000000001);

                swe.swe_set_topo(geopos[0], geopos[1], geopos[2]);
                swe.swe_calc(tjd, SwissEph.SE_SUN, 33024, xin2, ref serr);
                Assert.AreEqual(142.781800447353, xin2[0], 0.000000000001);
                Assert.AreEqual(5.71307641569538E-05, xin2[1], 0.000000000001);
                Assert.AreEqual(1.01269577772231, xin2[2], 0.000000000001);

                swe.swe_calc(tjd, SwissEph.SE_SUN, 33024, xin3, ref serr);
                Assert.AreEqual(142.781800447353, xin3[0], 0.000000000001);
                Assert.AreEqual(5.71307641569538E-05, xin3[1], 0.0000000000001);
                Assert.AreEqual(1.01269577772231, xin3[2], 0.0000000000001);
            }
        }

    }
}
