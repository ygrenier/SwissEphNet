using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{
    [TestClass]
    public class LongitudeTest
    {

        [TestMethod]
        public void TestBase() {
            Longitude l = new Longitude();
            Assert.AreEqual(0, l.Degrees);
            Assert.AreEqual(0, l.Minutes);
            Assert.AreEqual(0, l.Seconds);
            Assert.AreEqual(LongitudePolarity.East, l.Polarity);
            Assert.AreEqual(0.0, l.Value, 0.00000000001);
            Assert.AreEqual("0E00'00\"", l.ToString());
        }

        [TestMethod]
        public void TestFromValue() {
            Double value = 278.123456789;
            Longitude l = new Longitude(value);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.East, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98E07'24\"", l.ToString());

            value = -98.123456789;
            l = new Longitude(value);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.West, l.Polarity);
            Assert.AreEqual(-98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98W07'24\"", l.ToString());
        }

        [TestMethod]
        public void TestFromComponent1() {
            Longitude l = new Longitude(98, 7, 24);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.East, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98E07'24\"", l.ToString());

            l = new Longitude(-98, 7, 24);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.West, l.Polarity);
            Assert.AreEqual(-98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98W07'24\"", l.ToString());
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent1_ErrDeg() {
            new Longitude(198, 7, 24);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent1_ErrMin() {
            new Longitude(98, 77, 24);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent1_ErrSec() {
            new Longitude(98, 7, -24);
        }

        [TestMethod]
        public void TestFromComponent2() {
            Longitude l = new Longitude(98, 7, 24, LongitudePolarity.East);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.East, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98E07'24\"", l.ToString());

            l = new Longitude(98, 7, 24, LongitudePolarity.West);
            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.West, l.Polarity);
            Assert.AreEqual(-98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98W07'24\"", l.ToString());
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent2_ErrDeg() {
            new Longitude(198, 7, 24, LongitudePolarity.East);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent2_ErrMin() {
            new Longitude(98, 77, 24, LongitudePolarity.East);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromComponent2_ErrSec() {
            new Longitude(98, 7, -24, LongitudePolarity.East);
        }

        [TestMethod]
        public void TestExplicitCast() {
            Double value = 278.123456789;

            Longitude l = value;

            Assert.AreEqual(98, l.Degrees);
            Assert.AreEqual(7, l.Minutes);
            Assert.AreEqual(24, l.Seconds);
            Assert.AreEqual(LongitudePolarity.East, l.Polarity);
            Assert.AreEqual(98.1233333333333, l.Value, 0.00000000001);
            Assert.AreEqual("98E07'24\"", l.ToString());

            value = l;
            Assert.AreEqual(98.1233333333333, value, 0.00000000001);

        }

    }
}
