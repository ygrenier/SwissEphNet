using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{
    [TestClass]
    public class JulianDayTest
    {
        [TestMethod]
        public void TestCreateEmpty() {
            JulianDay jd = new JulianDay();
            Assert.AreEqual(0.0, jd.Value);
            Assert.AreEqual(DateCalendar.Julian, jd.Calendar);
        }

        [TestMethod]
        public void TestCreateComponents() {
            var date = new DateUT(2014, 4, 26, 16, 53, 24);
            JulianDay jd = new JulianDay(date, DateCalendar.Gregorian);
            Assert.AreEqual(2456774.20375, jd.Value);
            Assert.AreEqual(DateCalendar.Gregorian, jd.Calendar);

            date = new DateUT(2014, 4, 26, 16, 53, 24);
            jd = new JulianDay(date, DateCalendar.Julian);
            Assert.AreEqual(2456787.20375, jd.Value);
            Assert.AreEqual(DateCalendar.Julian, jd.Calendar);

            date = new DateUT(1974, 8, 15, 23, 30, 00);
            jd = new JulianDay(date, DateCalendar.Gregorian);
            Assert.AreEqual(2442275.47916667, jd.Value, 0.00000001);

        }

        [TestMethod]
        public void TestCreateDouble() {
            var date = 2456774.20375;
            JulianDay jd = new JulianDay(date, DateCalendar.Gregorian);
            Assert.AreEqual(date, jd.Value);
            Assert.AreEqual(DateCalendar.Gregorian, jd.Calendar);

            jd = new JulianDay(date, DateCalendar.Julian);
            Assert.AreEqual(date, jd.Value);
            Assert.AreEqual(DateCalendar.Julian, jd.Calendar);
        }

        [TestMethod]
        public void TestToDateUT() {
            var date = new DateUT(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            Assert.AreEqual(date, jd.ToDateUT());
        }

        [TestMethod]
        public void TestToDateTime() {
            var date = new DateUT(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            Assert.AreEqual(date.ToDateTime(), jd.ToDateTime());
        }

        [TestMethod]
        public void TestCastToDouble() {
            var date = new DateUT(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            double cd = jd;
            Assert.AreEqual(cd, jd.Value);
        }

        [TestMethod]
        public void TestToString() {
            var date = new DateUT(2014, 4, 26, 16, 53, 24);
            var jd = new JulianDay(date, DateCalendar.Gregorian);
            Assert.AreEqual("2456774,20375", jd.ToString());
        }

    }
}
