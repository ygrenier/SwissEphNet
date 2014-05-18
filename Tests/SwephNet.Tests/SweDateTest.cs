using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SwephNet;
using System.IO;

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

        [TestMethod]
        public void TestGetTidAcc()
        {
            using (var swe = new Sweph())
            {
                Assert.AreEqual(-25.82, swe.Date.TidalAcceleration);
            }
        }

        [TestMethod]
        public void TestDeltaTWithEspenakMeeus2006()
        {
            using (var swe = new Sweph())
            {
                swe.Date.UseEspenakMeeus2006 = true;
                double deltaPrec = 0.00000000000001;

                Assert.AreEqual(1.5716511059188, swe.Date.DeltaT(0.0), deltaPrec);

                var vals = new Dictionary<double, double>() {
                    {swe.JulianDay(-1010, 2, 2, 12.5),0.2947146757992},
                    {swe.JulianDay(410, 2, 2, 12.5),0.0759254042309472},
                    {swe.JulianDay(1010, 2, 2, 12.5),0.017408820265673},
                    {swe.JulianDay(1650, 2, 2, 12.5),0.000536887552700585},
                    {swe.JulianDay(1750, 2, 2, 12.5),0.000143408234925189},
                    {swe.JulianDay(1850, 2, 2, 12.5),8.02298701914818E-05},
                    {swe.JulianDay(1870, 2, 2, 12.5),1.61408658417223E-05},
                    {swe.JulianDay(1910, 2, 2, 12.5),0.000121668188266053},
                    {swe.JulianDay(1930, 2, 2, 12.5),0.000277897213030198},
                    {swe.JulianDay(1950, 2, 2, 12.5),0.000337817215327823},
                    {swe.JulianDay(1970, 2, 2, 12.5),0.000466047488324088},
                    {swe.JulianDay(1990, 2, 2, 12.5),0.000658780771068347},
                    {swe.JulianDay(2000, 2, 2, 12.5),0.000739073662856691},
                };
                foreach (var kvp in vals)
                {
                    Assert.AreEqual(kvp.Value, swe.Date.DeltaT(kvp.Key), deltaPrec, String.Format("deltat({0})", kvp.Key));
                }

                Assert.AreEqual(0.0374254553961889, swe.Date.DeltaT(2000000.0), deltaPrec);
                Assert.AreEqual(0.0374253886123893, swe.Date.DeltaT(2000000.25), deltaPrec);
                Assert.AreEqual(0.0374253218286385, swe.Date.DeltaT(2000000.5), deltaPrec);
                Assert.AreEqual(0.0374252550449363, swe.Date.DeltaT(2000000.75), deltaPrec);
                Assert.AreEqual(0.000848297829347124, swe.Date.DeltaT(2317746.13090277789), deltaPrec);

                // 2415020.0
                vals = new Dictionary<double, double>() {
                    {2400000.0,8.85169749775391E-05},
                    {2400200.0,8.89367796449606E-05},
                    {2400400.0,8.94708209388948E-05},
                    {2400800.0,8.87232218904813E-05},
                    {2402000.0,7.08931645254769E-05},
                    {2404000.0,1.81148433665968E-05},
                    {2408000.0,-6.40563578305767E-05},
                    {2410000.0,-6.57993558330751E-05},
                    {2412000.0,-7.15340361147556E-05},
                    {2414000.0,-6.50989531127749E-05},
                    {2418000.0,9.19707007809117E-05},
                };
                foreach (var kvp in vals)
                {
                    Assert.AreEqual(kvp.Value, swe.Date.DeltaT(kvp.Key), deltaPrec, String.Format("deltat({0})", kvp.Key));
                }

                Assert.AreEqual(0.101230433035332, swe.Date.DeltaT(3000000), deltaPrec);
                Assert.AreEqual(0.101230598229371, swe.Date.DeltaT(3000000.5), deltaPrec);
                Assert.AreEqual(0.101230680826441, swe.Date.DeltaT(3000000.75), deltaPrec);

                Assert.AreEqual(0.000828297529544201, swe.Date.DeltaT(swe.JulianDay(2020, 1, 1, 10.5)), deltaPrec);

            }
        }

        [TestMethod]
        public void TestDeltaTWithoutEspenakMeeus2006()
        {
            using (var swe = new Sweph())
            {
                swe.Date.UseEspenakMeeus2006 = false;
                double deltaPrec = 0.000000000001;

                Assert.AreEqual(1.5716511059188, swe.Date.DeltaT(0.0), deltaPrec);

                Assert.AreEqual(0.294436185639733, swe.Date.DeltaT(swe.JulianDay(-1010, 2, 2, 12.5)), deltaPrec);

                Assert.AreEqual(0.0375610997366034, swe.Date.DeltaT(2000000.0), deltaPrec);
                Assert.AreEqual(0.0375610327085927, swe.Date.DeltaT(2000000.25), deltaPrec);
                Assert.AreEqual(0.0375609656805818, swe.Date.DeltaT(2000000.5), deltaPrec);
                Assert.AreEqual(0.0375608986525707, swe.Date.DeltaT(2000000.75), deltaPrec);
                Assert.AreEqual(0.000848297829347124, swe.Date.DeltaT(2317746.13090277789), deltaPrec);

                var tjd = SweDate.J2000 + (365.25 * (1610 - 2000.0));
                Assert.AreEqual(0.00138947083317893, swe.Date.DeltaT(tjd), deltaPrec);


                Assert.AreEqual(0.101230433035332, swe.Date.DeltaT(3000000), deltaPrec);
                Assert.AreEqual(0.101230598229371, swe.Date.DeltaT(3000000.5), deltaPrec);
                Assert.AreEqual(0.101230680826441, swe.Date.DeltaT(3000000.75), deltaPrec);

                Assert.AreEqual(0.000828297529544201, swe.Date.DeltaT(swe.JulianDay(2020, 1, 1, 10.5)), deltaPrec);

            }
        }

        [TestMethod]
        public void TestDeltaTWithLoadFile()
        {
            using (var swe = new Sweph())
            {
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == "sedeltat.txt")
                    {
                        e.File = new MemoryStream(System.Text.Encoding.Default.GetBytes(@"2000 30.0
2010 40.0
2020  50.0
2030 60.0
"));
                    }
                };

                double deltaPrec = 0.000000000001;

                Assert.AreEqual(0.294436185639733, swe.Date.DeltaT(swe.JulianDay(-1010, 2, 2, 12.5)), deltaPrec);
                Assert.AreEqual(0.0375610997366034, swe.Date.DeltaT(2000000.0), deltaPrec);
                Assert.AreEqual(0.000347222222222222, swe.Date.DeltaT(SweDate.J2000), deltaPrec);

                Assert.AreEqual(0.000578564998305795, swe.Date.DeltaT(swe.JulianDay(2020, 1, 1, 10.5)), deltaPrec);

            }
        }

        [TestMethod]
        public void TestDeltaTRecordFile()
        {
            using (var swe = new Sweph())
            {
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == "sedeltat.txt")
                    {
                        e.File = new MemoryStream(System.Text.Encoding.Default.GetBytes(@"
# Head

2000 30.0
    2001    31.25    
; 2002   30.0
2000-30.0

"));
                    }
                };

                var file = swe.Dependencies.Create<SwephNet.Date.DeltaTRecordFile>();
                var records = file.GetRecords().ToArray();
                Assert.AreEqual(2, records.Length);
                Assert.AreEqual(2000, records[0].Year);
                Assert.AreEqual(30.0, records[0].Value);
                Assert.AreEqual(2001, records[1].Year);
                Assert.AreEqual(31.25, records[1].Value);
            }
        }
        

    }
}
