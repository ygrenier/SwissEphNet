using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class Issue15Test
    {
        [TestMethod]
        public void TestSideral()
        {
            using (var sweph = new SwissEph())
            {
                int jday = 1, jmon = 1, jyear = 2001;
                double jut = 0;
                double[] x2 = new double[6];
                Int32 iflag, iflgret;
                iflag = SwissEph.SEFLG_MOSEPH | SwissEph.SEFLG_SIDEREAL | SwissEph.SEFLG_NONUT;
                string snam = string.Empty, serr = String.Empty;

                var tjd = sweph.swe_julday(jyear, jmon, jday, jut, SwissEph.SE_GREG_CAL);
                var te = tjd + sweph.swe_deltat(tjd);

                double delta = 0.000001;

                Assert.AreEqual(2451910.5, tjd);
                Assert.AreEqual(2451910.500742, te, delta);

                sweph.swe_set_sid_mode(SwissEph.SE_SIDM_LAHIRI, 0, 0);
                double ayanamsa = sweph.swe_get_ayanamsa(te);

                Assert.AreEqual(23.871032, ayanamsa, delta);

                // TODO Uncomment to tests

                //iflgret = sweph.swe_calc(te, SwissEph.SE_SUN, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(256.766845, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_MOON, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(324.839238, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_MERCURY, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(260.404949, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_VENUS, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(303.100189, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_MARS, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(191.071250, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_JUPITER, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(38.323488, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_SATURN, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(30.724496, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_URANUS, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(294.783510, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_NEPTUNE, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(281.460643, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_PLUTO, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(229.901526, x2[0], delta);

                //iflgret = sweph.swe_calc(te, SwissEph.SE_MEAN_NODE, iflag, x2, ref serr);
                //Assert.AreEqual(iflgret, iflag);
                //Assert.AreEqual(81.818882, x2[0], delta);
            }

        }
    }
}
