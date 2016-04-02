using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class Issue17Test
    {
        [TestMethod]
        public void strcpy_VBsafe()
        {
            using (var sweph = new SwissEph())
            {
                double[] dummy = new double[12];
                String serr = null;

                // The issue raised a IndexOutOfRangeException
                sweph.swe_heliacal_ut(SwissEph.J2000, dummy, dummy, dummy, "moon", 0, 0, dummy, ref serr);

            }
        }
    }
}
