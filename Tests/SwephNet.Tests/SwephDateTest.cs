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

    }
}
