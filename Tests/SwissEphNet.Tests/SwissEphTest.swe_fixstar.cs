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
                Assert.Equal(69.437854677212, xx[0], 11);
                Assert.Equal(-5.468620686643, xx[1], 11);
                Assert.Equal(1.0, xx[2], 12);
                Assert.Equal(0, xx[3], 12);
                Assert.Equal(0, xx[4], 12);
                Assert.Equal(0, xx[5], 12);

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
                Assert.Equal(69.439402029787, xx[0], 12);
                Assert.Equal(-5.469136284371, xx[1], 12);
                Assert.Equal(1.0, xx[2], 12);
                Assert.Equal(0, xx[3], 12);
                Assert.Equal(0, xx[4], 12);
                Assert.Equal(0, xx[5], 12);
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
