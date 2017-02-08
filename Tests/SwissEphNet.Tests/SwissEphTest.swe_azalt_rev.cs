using System;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [Fact]
        public void Test_swe_azalt_rev() {
            using (var swe = new SwissEph()) {
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xin = new double[] { 249.64242931513104, -32.8340765334715 }; 
                double[] xaz = new double[6];
                swe.swe_azalt_rev(tjd, SwissEph.SE_ECL2HOR, geopos, xin, xaz);
                Assert.Equal(142.77982440444, xaz[0], 12);
                Assert.Equal(-1.93084572176467E-05, xaz[1], 12);
                Assert.Equal(0, xaz[2], 12);
            }
        }
    }

}
