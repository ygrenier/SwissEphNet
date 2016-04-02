﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                    System.Diagnostics.Debug.WriteLine(String.Join("|", asm.GetManifestResourceNames()));
                    e.File = asm.GetManifestResourceStream(sr);
                };

                double tjd = swe.swe_julday(1974, 8, 16, 0.5, SwissEph.SE_GREG_CAL);
                double[] geopos = new double[] { 47.853333, 5.333889, 468 };
                double[] xx = new double[6]; String serr = null;

                // The issue raised a FormatException
                swe.swe_calc_ut(tjd, SwissEph.SE_AST_OFFSET + 5, SwissEph.SEFLG_SWIEPH, xx, ref serr);
                
            }
        }
    }
}
