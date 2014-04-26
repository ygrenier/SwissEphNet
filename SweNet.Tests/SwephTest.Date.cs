using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SweNet.Tests
{
    partial class SwephTest
    {

        [TestMethod]
        public void TestJulianDay() {
            using (var swe = new Sweph()) {

                Assert.AreEqual(0.0, swe.JulianDay(new DateUT(-4713, 11, 24, 12, 0, 0), DateCalendar.Gregorian).Value);
                Assert.AreEqual(0.0, swe.JulianDay(-4713, 11, 24, 12, 0, 0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(0.0, swe.JulianDay(-4713, 11, 24, 12.0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(0.0, swe.JulianDay(-4712, 1, 1, 12.0, DateCalendar.Julian).Value);

                Assert.AreEqual(2000000.0, swe.JulianDay(763, 9, 18, 12.0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(2000000.0, swe.JulianDay(763, 9, 14, 12.0, DateCalendar.Julian).Value);

                Assert.AreEqual(1063884.0, swe.JulianDay(-1800, 9, 18, 12.0, DateCalendar.Gregorian).Value);
                Assert.AreEqual(1063865.0, swe.JulianDay(-1800, 9, 14, 12.0, DateCalendar.Julian).Value);
            }
        }

    }
}
