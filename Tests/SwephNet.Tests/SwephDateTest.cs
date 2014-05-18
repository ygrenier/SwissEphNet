using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    [TestClass]
    public class SwephDateTest
    {

        [TestMethod]
        public void TestJulianDay()
        {
            using (var swe = new Sweph())
            {

                Assert.AreEqual(0.0, swe.JulianDay(new UniversalTime(-4713, 11, 24, 12, 0, 0), DateCalendar.Gregorian).Value);
                Assert.AreEqual(0.0, swe.JulianDay(-4713, 11, 24, 12, 0, 0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(0.0, swe.JulianDay(-4713, 11, 24, 12.0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(0.0, swe.JulianDay(-4712, 1, 1, 12.0, DateCalendar.Julian).Value);

                Assert.AreEqual(2000000.0, swe.JulianDay(763, 9, 18, 12.0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(2000000.0, swe.JulianDay(763, 9, 14, 12.0, DateCalendar.Julian).Value);

                Assert.AreEqual(1063884.0, swe.JulianDay(-1800, 9, 18, 12.0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(1063865.0, swe.JulianDay(-1800, 9, 14, 12.0, DateCalendar.Julian).Value);
            }
        }

        [TestMethod]
        public void TestDateUT()
        {
            using (var swe = new Sweph())
            {
                // From JulianDay
                Assert.AreEqual(new UniversalTime(-4712, 1, 1, 12.0), swe.DateUT(new JulianDay()));
                Assert.AreEqual(new UniversalTime(-4712, 1, 1, 12.0), swe.DateUT(new JulianDay(0)));
                Assert.AreEqual(new UniversalTime(-4713, 11, 24, 12, 0, 0), swe.DateUT(new JulianDay(0, DateCalendar.Gregorian)));
                Assert.AreEqual(new UniversalTime(-4712, 1, 1, 12.0), swe.DateUT(new JulianDay(0, DateCalendar.Julian)));

                // From EphemerisTime
                Assert.AreEqual(new UniversalTime(-4712, 1, 1, 12.0), swe.DateUT(new EphemerisTime()));
                Assert.AreEqual(new UniversalTime(-4712, 1, 1, 12.0), swe.DateUT(new EphemerisTime(new JulianDay(), 0)));
            }
        }

        [TestMethod]
        public void TestEphemerisTime()
        {
            using (var swe = new Sweph())
            {
                double prec = 0.00000001;
                Assert.AreEqual(1.5716511059188, swe.EphemerisTime(new JulianDay()), prec);
                Assert.AreEqual(2442275.52135549, swe.EphemerisTime(new JulianDay(new UniversalTime(1974, 8, 16, 0.5))), prec);

            }
        }

    }
}
