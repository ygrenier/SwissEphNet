using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [Fact]
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
                Assert.Equal(0, xin1[0], 12);
                Assert.Equal(0, xin1[1], 12);
                Assert.Equal(0, xin1[2], 12);

                swe.swe_set_topo(geopos[0], geopos[1], geopos[2]);
                swe.swe_calc(tjd, SwissEph.SE_SUN, 33024, xin2, ref serr);
                Assert.Equal(142.781802332174, xin2[0], 12);
                Assert.Equal(6.0673071E-05, xin2[1], 12);
                Assert.Equal(1.012695777714, xin2[2], 12);

                swe.swe_calc(tjd, SwissEph.SE_SUN, 33024, xin3, ref serr);
                Assert.Equal(142.781802332174, xin3[0], 12);
                Assert.Equal(6.0673071E-05, xin3[1], 12);
                Assert.Equal(1.012695777714, xin3[2], 12);
            }
        }

    }
}
