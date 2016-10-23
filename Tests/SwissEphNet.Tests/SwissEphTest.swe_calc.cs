using System;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [Fact]
        public void Test_swe_calc() {
            using (var swe = new SwissEph()) {
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;
                swe.swe_calc(tjd, SwissEph.SE_SUN, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.Equal(142.779826288975, xx[0], 12);
                Assert.Equal(-1.57665386141208E-05, xx[1], 12);
                Assert.Equal(1.01267253822512, xx[2], 12);
                Assert.Equal(0, xx[3], 12);
                Assert.Equal(0, xx[4], 12);
                Assert.Equal(0, xx[5], 12);
            }
        }
    }

}
