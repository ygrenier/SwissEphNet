using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SwissEphNet.Tests
{

    public class Issue18Test
    {
        [Fact]
        public void LoadAsteroidData()
        {
            using (var swe = new SwissEph())
            {
                swe.OnLoadFile += (s, e) => {
                    var asm = this.GetType().GetAssembly();
                    String sr = e.FileName.Replace("[ephe]", @"SwissEphNet.Tests.files").Replace("/", ".").Replace("\\", ".");
                    e.File = asm.GetManifestResourceStream(sr);
                };

                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;

                // The issue raised a FormatException
                swe.swe_calc_ut(tjd, SwissEph.SE_AST_OFFSET + 5, SwissEph.SEFLG_SWIEPH, xx, ref serr);
                Assert.Equal(130.764380953384, xx[0], 12);
                Assert.Equal(-1.0445487020471, xx[1], 13);
                Assert.Equal(3.0793896379558, xx[2], 13);
                Assert.Equal(0, xx[3], 13);
                Assert.Equal(0, xx[4], 13);
                Assert.Equal(0, xx[5], 13);
            }
        }
    }
}
