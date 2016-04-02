using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class Issue18Test
    {
        [TestMethod]
        public void LoadAsteroidData()
        {
            using (var swe = new SwissEph())
            {
                swe.OnLoadFile += (s, e) => {
                    var asm = this.GetType().Assembly;
                    String sr = e.FileName.Replace("[ephe]", @"SwissEphNet.Tests.files").Replace("/", ".").Replace("\\", ".");
                    e.File = asm.GetManifestResourceStream(sr);
                };

                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;

                // The issue raised a FormatException
                swe.swe_calc_ut(tjd, SwissEph.SE_AST_OFFSET + 5, SwissEph.SEFLG_SWIEPH, xx, ref serr);
                Assert.AreEqual(130.764380973775, xx[0], 0.000000000001);
                Assert.AreEqual(-1.04454870127634, xx[1], 0.0000000000001);
                Assert.AreEqual(3.07938963784905, xx[2], 0.0000000000001);
                Assert.AreEqual(0, xx[3], 0.0000000000001);
                Assert.AreEqual(0, xx[4], 0.0000000000001);
                Assert.AreEqual(0, xx[5], 0.0000000000001);
            }
        }
    }
}
