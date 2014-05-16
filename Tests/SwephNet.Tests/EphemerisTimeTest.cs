using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    [TestClass]
    public class EphemerisTimeTest
    {

        [TestMethod]
        public void TestCreateEmpty() {
            EphemerisTime et = new EphemerisTime();
            Assert.AreEqual(0.0, et.JulianDay.Value);
            Assert.AreEqual(0.0, et.DeltaT);
            Assert.AreEqual(0.0, et.Value);
        }

        [TestMethod]
        public void TestCreate() {
            var date = new UniversalTime(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            var dt = 0.456;
            EphemerisTime et = new EphemerisTime(jd, dt);
            Assert.AreEqual(jd, et.JulianDay);
            Assert.AreEqual(dt, et.DeltaT);
            Assert.AreEqual(jd.Value + dt, et.Value);
        }

        [TestMethod]
        public void TestCastToDouble() {
            var date = new UniversalTime(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            var dt = 0.456;
            EphemerisTime et = new EphemerisTime(jd, dt);
            double cd = et;
            Assert.AreEqual(cd, et.Value);
        }

        [TestMethod]
        public void TestToString() {
            var date = new UniversalTime(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            var dt = 0.456;
            EphemerisTime et = new EphemerisTime(jd, dt);
            Assert.AreEqual("2456774,65975", et.ToString());
        }

    }
}
