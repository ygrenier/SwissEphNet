using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{
    [TestClass]
    public class SwissEphTestDate
    {

        [TestMethod]
        public void TestJulDay() {
            Assert.AreEqual(0.0, SwissEph.JulDay(-4713, 11, 24, 12.0, SwissEph.SE_GREG_CAL));

            Assert.AreEqual(0.0, SwissEph.JulDay(-4712, 1, 1, 12.0, SwissEph.SE_JUL_CAL));

            Assert.AreEqual(2000000.0, SwissEph.JulDay(763, 9, 18, 12.0, SwissEph.SE_GREG_CAL));

            Assert.AreEqual(2000000.0, SwissEph.JulDay(763, 9, 14, 12.0, SwissEph.SE_JUL_CAL));

            Assert.AreEqual(1063884, SwissEph.JulDay(-1800, 9, 18, 12.0, SwissEph.SE_GREG_CAL));

            Assert.AreEqual(1063865, SwissEph.JulDay(-1800, 9, 14, 12.0, SwissEph.SE_JUL_CAL));
        }

        [TestMethod]
        public void TestRevjul() {
            int y = 0, m = 0, d = 0; double ut = 0;

            SwissEph.RevJul(0, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
            Assert.AreEqual(-4713, y);
            Assert.AreEqual(11, m);
            Assert.AreEqual(24, d);
            Assert.AreEqual(12.0, ut);

            SwissEph.RevJul(0, SwissEph.SE_JUL_CAL, ref y, ref m, ref d, ref ut);
            Assert.AreEqual(-4712, y);
            Assert.AreEqual(1, m);
            Assert.AreEqual(1, d);
            Assert.AreEqual(12.0, ut);

            SwissEph.RevJul(2000000, SwissEph.SE_GREG_CAL, ref y, ref m, ref d, ref ut);
            Assert.AreEqual(763, y);
            Assert.AreEqual(9, m);
            Assert.AreEqual(18, d);
            Assert.AreEqual(12.0, ut);

            SwissEph.RevJul(2000000, SwissEph.SE_JUL_CAL, ref y, ref m, ref d, ref ut);
            Assert.AreEqual(763, y);
            Assert.AreEqual(9, m);
            Assert.AreEqual(14, d);
            Assert.AreEqual(12.0, ut);
        }

    }
}
