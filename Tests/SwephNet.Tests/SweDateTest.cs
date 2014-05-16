using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SwephNet;

namespace SwephNet.Tests
{
    [TestClass]
    public class SweDateTest
    {

        [TestMethod]
        public void TestDateToJulianDay() {
            using (var swe = new Sweph()) {

                Assert.AreEqual(0.0, SweDate.DateToJulianDay(new UniversalTime(-4713, 11, 24, 12, 0, 0), DateCalendar.Gregorian));
                Assert.AreEqual(-38.0, SweDate.DateToJulianDay(new UniversalTime(-4713, 11, 24, 12, 0, 0), DateCalendar.Julian));
                Assert.AreEqual(-38.0, SweDate.DateToJulianDay(new UniversalTime(-4713, 11, 24, 12, 0, 0)));
                Assert.AreEqual(0.0, SweDate.DateToJulianDay(-4713, 11, 24, 12, 0, 0, DateCalendar.Gregorian));
                Assert.AreEqual(0.0, SweDate.DateToJulianDay(-4713, 11, 24, 12.0, DateCalendar.Gregorian));
                Assert.AreEqual(0.0, SweDate.DateToJulianDay(-4712, 1, 1, 12.0, DateCalendar.Julian));

                Assert.AreEqual(2000000.0, SweDate.DateToJulianDay(763, 9, 18, 12.0, DateCalendar.Gregorian));
                Assert.AreEqual(2000000.0, SweDate.DateToJulianDay(763, 9, 14, 12.0, DateCalendar.Julian));

                Assert.AreEqual(1063884.0, SweDate.DateToJulianDay(-1800, 9, 18, 12.0, DateCalendar.Gregorian));
                Assert.AreEqual(1063865.0, SweDate.DateToJulianDay(-1800, 9, 14, 12.0, DateCalendar.Julian));
            }
        }

        [TestMethod]
        public void TestJulianDayToDate() {
            using (var swe = new Sweph()) {
                int y = 0, m = 0, d = 0, h, mi, s;

                SweDate.JulianDayToDate(0, DateCalendar.Gregorian, out y, out m, out d, out h, out mi, out s);
                Assert.AreEqual(-4713, y);
                Assert.AreEqual(11, m);
                Assert.AreEqual(24, d);
                Assert.AreEqual(12, h);
                Assert.AreEqual(0, mi);
                Assert.AreEqual(0, s);

                SweDate.JulianDayToDate(0, DateCalendar.Julian, out y, out m, out d, out h, out mi, out s);
                Assert.AreEqual(-4712, y);
                Assert.AreEqual(1, m);
                Assert.AreEqual(1, d);
                Assert.AreEqual(12, h);
                Assert.AreEqual(0, mi);
                Assert.AreEqual(0, s);

                SweDate.JulianDayToDate(2000000, DateCalendar.Gregorian, out y, out m, out d, out h, out mi, out s);
                Assert.AreEqual(763, y);
                Assert.AreEqual(9, m);
                Assert.AreEqual(18, d);
                Assert.AreEqual(12, h);
                Assert.AreEqual(0, mi);
                Assert.AreEqual(0, s);

                SweDate.JulianDayToDate(2000000, DateCalendar.Julian, out y, out m, out d, out h, out mi, out s);
                Assert.AreEqual(763, y);
                Assert.AreEqual(9, m);
                Assert.AreEqual(14, d);
                Assert.AreEqual(12, h);
                Assert.AreEqual(0, mi);
                Assert.AreEqual(0, s);

                var date = SweDate.JulianDayToDate(2000000, DateCalendar.Julian);
                Assert.AreEqual(763, date.Year);
                Assert.AreEqual(9, date.Month);
                Assert.AreEqual(14, date.Day);
                Assert.AreEqual(12, date.Hours);
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(0, date.Seconds);

                date = SweDate.JulianDayToDate(2000000);
                Assert.AreEqual(763, date.Year);
                Assert.AreEqual(9, date.Month);
                Assert.AreEqual(14, date.Day);
                Assert.AreEqual(12, date.Hours);
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(0, date.Seconds);
            }
        }

        [TestMethod]
        public void TestDayOfWeek() {
            Assert.AreEqual(WeekDay.Thursday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 1, 0, DateCalendar.Gregorian)));
            Assert.AreEqual(WeekDay.Friday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 2, 0, DateCalendar.Gregorian)));
            Assert.AreEqual(WeekDay.Saturday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 3, 0, DateCalendar.Gregorian)));
            Assert.AreEqual(WeekDay.Sunday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 4, 0, DateCalendar.Gregorian)));
            Assert.AreEqual(WeekDay.Monday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 5, 0, DateCalendar.Gregorian)));
            Assert.AreEqual(WeekDay.Tuesday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 6, 0, DateCalendar.Gregorian)));
            Assert.AreEqual(WeekDay.Wednesday, SweDate.DayOfWeek(SweDate.DateToJulianDay(2014, 5, 7, 0, DateCalendar.Gregorian)));
        }

        [TestMethod]
        public void TestGetCalendar() {
            Assert.AreEqual(DateCalendar.Julian, SweDate.GetCalendar(0));
            Assert.AreEqual(DateCalendar.Julian, SweDate.GetCalendar(-10000000));
            Assert.AreEqual(DateCalendar.Gregorian, SweDate.GetCalendar(+10000000));
            Assert.AreEqual(DateCalendar.Gregorian, SweDate.GetCalendar(SweDate.GregorianFirstJD));
            Assert.AreEqual(DateCalendar.Gregorian, SweDate.GetCalendar(SweDate.GregorianFirstJD + 1.0));
            Assert.AreEqual(DateCalendar.Julian, SweDate.GetCalendar(SweDate.GregorianFirstJD - 1.0));
        }

    }
}
