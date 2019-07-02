using System;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [Fact]
        public void Test_swe_fixstar()
        {
            using (var swe = new SwissEph())
            {
                string name = "aldebaran";
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;

                int iflag = swe.swe_fixstar(ref name, tjd, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.Equal(SwissEph.ERR, iflag);
                Assert.Equal("SwissEph file 'sefstars.txt' not found in PATH '[ephe]'", serr);

                swe.OnLoadFile += (s, e) =>
                {
                    if (string.Equals(e.FileName, "[ephe]\\sefstars.txt", StringComparison.OrdinalIgnoreCase))
                    {
                        e.File = ResourceFileHelpers.OpenResourceFile("sefstars.txt");
                    }
                };

                iflag = swe.swe_fixstar(ref name, tjd, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.NotEqual(SwissEph.ERR, iflag);
                Assert.Null(serr);

                Assert.Equal("Aldebaran,alTau", name);
                Assert.Equal(69.43785467706, xx[0], 11);
                Assert.Equal(-5.46862068665, xx[1], 11);
                Assert.Equal(4214356.43826371, xx[2], 8);
#if DEBUG
                Assert.Equal(0.00014896, xx[3], 8);
                Assert.Equal(1.723E-05, xx[4], 8);
                Assert.Equal(0.01522108, xx[5], 8);
#else
                Assert.Equal(0.00014887, xx[3], 8);
                Assert.Equal(1.724E-05, xx[4], 8);
                Assert.Equal(0.01536112, xx[5], 8);
#endif

                name = "test";
                iflag = swe.swe_fixstar(ref name, tjd, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.Equal(SwissEph.ERR, iflag);
                Assert.Equal("star test not found", serr);
            }
        }

        [Fact]
        public void Test_swe_fixstar_ut()
        {
            using (var swe = new SwissEph())
            {
                string name = "aldebaran";
                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;

                swe.OnLoadFile += (s, e) =>
                {
                    if (string.Equals(e.FileName, "[ephe]\\sefstars.txt", StringComparison.OrdinalIgnoreCase))
                    {
                        e.File = ResourceFileHelpers.OpenResourceFile("sefstars.txt");
                    }
                };

                int iflag = swe.swe_fixstar_ut(ref name, tjd, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.NotEqual(SwissEph.ERR, iflag);
                Assert.Null(serr);

                Assert.Equal("Aldebaran,alTau", name);
                Assert.Equal(69.43785475383, xx[0], 11);
                Assert.Equal(-5.46862068520, xx[1], 11);
                Assert.Equal(4214356.43827158, xx[2], 7);
                Assert.Equal(0.000151, xx[3], 6);
                Assert.Equal(1.7E-05, xx[4], 6);
#if DEBUG
                Assert.Equal(0.015532, xx[5], 6);
#else
                Assert.Equal(0.01536, xx[5], 6);
#endif
            }
        }

        [Fact]
        public void Test_swe_fixstar_mag()
        {
            using (var swe = new SwissEph())
            {
                string name = "aldebaran";
                double mag = 0; String serr = null;

                swe.OnLoadFile += (s, e) =>
                {
                    if (string.Equals(e.FileName, "[ephe]\\sefstars.txt", StringComparison.OrdinalIgnoreCase))
                    {
                        e.File = ResourceFileHelpers.OpenResourceFile("sefstars.txt");
                    }
                };

                int iflag = swe.swe_fixstar_mag(ref name, ref mag, ref serr);
                Assert.NotEqual(SwissEph.ERR, iflag);

                Assert.Equal("Aldebaran,alTau", name);
                Assert.Equal(0.86, mag, 12);
            }
        }

    }

}
