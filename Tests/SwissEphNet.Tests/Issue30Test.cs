using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwissEphNet.Tests
{
    /// <summary>
    /// Issue: https://github.com/ygrenier/SwissEphNet/issues/30
    /// </summary>
    public class Issue30Test
    {
        [Fact]
        public void Test()
        {
            using (var sweph = new SwissEph())
            {
                double[] planetdata = new double[6];
                double[] dret = new double[2];
                int outdt = 0;
                int outmo = 0;
                int outdy = 0;
                int outhh = 0;
                int outmm = 0;
                double outss = 0;
                string serr = null;

                sweph.swe_utc_time_zone(2000, 10, 22, 17, 30, 25, 5.5, ref outdt, ref outmo, ref outdy, ref outhh, ref outmm, ref outss);
                sweph.swe_utc_to_jd(outdt, outmo, outdy, outhh, outmm, outss, SwissEph.SE_GREG_CAL, dret, ref serr);

                int ipl = 0;
                int iflag = SwissEph.SEFLG_TRUEPOS | SwissEph.SEFLG_SWIEPH | SwissEph.SEFLG_SPEED | SwissEph.SEFLG_TOPOCTR | SwissEph.SEFLG_SIDEREAL;
                double geolon = 78.70972;
                double geolat = 10.76536;
                double height = 89;

                sweph.swe_set_topo(geolon, geolat, height);
                sweph.swe_set_sid_mode(5, 0, 0);

                double ayanamsa = sweph.swe_get_ayanamsa_ut(dret[1]);

                sweph.swe_calc_ut(dret[1], ipl, iflag, planetdata, ref serr);

                double[] cusps = new double[13];
                double[] ascmc = new double[10];

                Assert.Equal(ayanamsa, sweph.swe_get_ayanamsa_ut(dret[1]));

                sweph.swe_houses(dret[1], geolat, geolon, 'P', cusps, ascmc);
                Assert.Equal(ayanamsa, sweph.swe_get_ayanamsa_ut(dret[1]));

                sweph.swe_houses_ex(dret[1], SwissEph.SEFLG_SIDEREAL, geolat, geolon, 'P', cusps, ascmc);

                // The issue change the value of swe_get_ayanamsa_ut after swe_houses_ex call.
                Assert.Equal(ayanamsa, sweph.swe_get_ayanamsa_ut(dret[1]));
            }

        }
    }
}
