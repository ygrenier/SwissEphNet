using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{
    [TestClass]
    public class LatitudeTest
    {

        [TestMethod]
        public void TestBase() {
            Latitude l = new Latitude();
            Assert.AreEqual(0, l.Degrees);
            Assert.AreEqual(0, l.Minutes);
            Assert.AreEqual(0, l.Seconds);
            Assert.AreEqual(LatitudePolarity.North, l.Polarity);
            Assert.AreEqual(0.0, l.Value, 0.00000000001);
            Assert.AreEqual("0N00'00\"", l.ToString());
        }

        [TestMethod]
        public void TestFromValue() {
            Double value = 278.123456789;
            Latitude l = new Latitude(value);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.North, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98N07'24\"", l.ToString());

            value = -98.123456789;
            l = new Latitude(value);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.South, l.Polarity);
            Assert.AreEqual(-98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98S07'24\"", l.ToString());
        }

        [TestMethod]
        public void TestFromComponent1() {
            Latitude l = new Latitude(98, 7, 24);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.North, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98N07'24\"", l.ToString());

            l = new Latitude(-98, 7, 24);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.South, l.Polarity);
            Assert.AreEqual(-98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98S07'24\"", l.ToString());
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent1_ErrDeg() {
            new Latitude(198, 7, 24);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent1_ErrMin() {
            new Latitude(98, 77, 24);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent1_ErrSec() {
            new Latitude(98, 7, -24);
        }

        [TestMethod]
        public void TestFromComponent2() {
            Latitude l = new Latitude(98, 7, 24, LatitudePolarity.North);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.North, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98N07'24\"", l.ToString());

            l = new Latitude(98, 7, 24, LatitudePolarity.South);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.South, l.Polarity);
            Assert.AreEqual(-98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98S07'24\"", l.ToString());
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent2_ErrDeg() {
            new Latitude(198, 7, 24, LatitudePolarity.North);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent2_ErrMin() {
            new Latitude(98, 77, 24, LatitudePolarity.North);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent2_ErrSec() {
            new Latitude(98, 7, -24, LatitudePolarity.North);
        }

        [TestMethod]
        public void TestExplicitCast() {
            Double value = 278.123456789;

            Latitude l = value;

            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LatitudePolarity.North, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98N07'24\"", l.ToString());

            value = l;
            Assert.AreEqual(98.1233333333333, value, 0.00000000001);

        }

    }
}
