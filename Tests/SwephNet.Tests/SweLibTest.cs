using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    [TestClass]
    public class SweLibTest
    {

        [TestMethod]
        public void TestDegNorm()
        {
            Assert.AreEqual(0, SweLib.DegNorm(0));
            Assert.AreEqual(180, SweLib.DegNorm(180));
            Assert.AreEqual(180, SweLib.DegNorm(-180));
            Assert.AreEqual(0, SweLib.DegNorm(360));
            Assert.AreEqual(0, SweLib.DegNorm(-360));
            Assert.AreEqual(90, SweLib.DegNorm(450));
            Assert.AreEqual(270, SweLib.DegNorm(-450));
        }

        [TestMethod]
        public void TestDegToRad()
        {
            double delta = 0.00000000000001;
            Assert.AreEqual(0, SweLib.DegToRad(0), delta);
            Assert.AreEqual(3.14159265358979, SweLib.DegToRad(180), delta);
            Assert.AreEqual(-3.14159265358979, SweLib.DegToRad(-180), delta);
            Assert.AreEqual(6.28318530717959, SweLib.DegToRad(360), delta);
            Assert.AreEqual(-6.28318530717959, SweLib.DegToRad(-360), delta);
            Assert.AreEqual(7.85398163397448, SweLib.DegToRad(450), delta);
            Assert.AreEqual(-7.85398163397448, SweLib.DegToRad(-450), delta);
        }

    }
}
