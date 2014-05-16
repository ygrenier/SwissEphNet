using SwephNet;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{

    [TestClass]
    public class UniversalTimeTest
    {

        [TestMethod]
        public void TestCreateEmpty() {
            UniversalTime date = new UniversalTime();

            Assert.AreEqual(0, date.Year);
            Assert.AreEqual(0, date.Month);
            Assert.AreEqual(0, date.Day);
            Assert.AreEqual(0, date.Hours);
            Assert.AreEqual(0, date.Minutes);
            Assert.AreEqual(0, date.Seconds);

        }

        [TestMethod]
        public void TestCreateFromDateTime() {
            var dt = DateTime.Now;
            UniversalTime date = new UniversalTime(dt);

            Assert.AreEqual(dt.Year, date.Year);
            Assert.AreEqual(dt.Month, date.Month);
            Assert.AreEqual(dt.Day, date.Day);
            Assert.AreEqual(dt.Hour, date.Hours);
            Assert.AreEqual(dt.Minute, date.Minutes);
            Assert.AreEqual(dt.Second, date.Seconds);

        }

        [TestMethod]
        public void TestCreateFromDateTimeOffset() {
            var dt = DateTimeOffset.Now;
            UniversalTime date = new UniversalTime(dt);

            Assert.AreEqual(dt.Year, date.Year);
            Assert.AreEqual(dt.Month, date.Month);
            Assert.AreEqual(dt.Day, date.Day);
            Assert.AreEqual(dt.Hour, date.Hours);
            Assert.AreEqual(dt.Minute, date.Minutes);
            Assert.AreEqual(dt.Second, date.Seconds);

        }

        [TestMethod]
        public void TestCreateFromComponents() {
            var dt = DateTime.Now;
            UniversalTime date = new UniversalTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

            Assert.AreEqual(dt.Year, date.Year);
            Assert.AreEqual(dt.Month, date.Month);
            Assert.AreEqual(dt.Day, date.Day);
            Assert.AreEqual(dt.Hour, date.Hours);
            Assert.AreEqual(dt.Minute, date.Minutes);
            Assert.AreEqual(dt.Second, date.Seconds);

            date = new UniversalTime(dt.Year, dt.Month, dt.Day, dt.GetHourValue());

            Assert.AreEqual(dt.Year, date.Year);
            Assert.AreEqual(dt.Month, date.Month);
            Assert.AreEqual(dt.Day, date.Day);
            Assert.AreEqual(dt.Hour, date.Hours);
            Assert.AreEqual(dt.Minute, date.Minutes);
            Assert.AreEqual(dt.Second, date.Seconds);

        }

        [TestMethod]
        public void TestToDateTime() {
            var dt = DateTime.Now;
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second); // Remove milliseconds
            UniversalTime date = new UniversalTime(dt);

            Assert.AreEqual(dt, date.ToDateTime());
        }

        [TestMethod]
        public void TestToDateTimeOffset() {
            var dt = DateTimeOffset.Now;
            dt = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, TimeSpan.Zero); // Remove milliseconds
            UniversalTime date = new UniversalTime(dt);

            Assert.AreEqual(dt, date.ToDateTimeOffset());
        }

        [TestMethod]
        public void TestCompareTo() {
            UniversalTime date = new UniversalTime(1974, 8, 16, 0, 30, 0);

            Assert.AreEqual(0, date.CompareTo(new UniversalTime(1974, 8, 16, 0, 30, 0)));
            Assert.AreEqual(-1, date.CompareTo(new UniversalTime(1975, 8, 16, 0, 30, 0)));
            Assert.AreEqual(1, date.CompareTo(new UniversalTime(1973, 8, 16, 0, 30, 0)));
            Assert.AreEqual(7, date.CompareTo(new UniversalTime(1974, 1, 16, 0, 30, 0)));
            Assert.AreEqual(-4, date.CompareTo(new UniversalTime(1974, 12, 16, 0, 30, 0)));
            Assert.AreEqual(6, date.CompareTo(new UniversalTime(1974, 8, 10, 0, 30, 0)));
            Assert.AreEqual(-14, date.CompareTo(new UniversalTime(1974, 8, 30, 0, 30, 0)));
            Assert.AreEqual(-1, date.CompareTo(new UniversalTime(1974, 8, 16, 1, 30, 0)));
            Assert.AreEqual(20, date.CompareTo(new UniversalTime(1974, 8, 16, 0, 10, 0)));
            Assert.AreEqual(-20, date.CompareTo(new UniversalTime(1974, 8, 16, 0, 50, 0)));
            Assert.AreEqual(-10, date.CompareTo(new UniversalTime(1974, 8, 16, 0, 30, 10)));

            Assert.AreEqual(0, date.CompareTo(new DateTime(1974, 8, 16, 0, 30, 0, 500)));
            Assert.AreEqual(0, date.CompareTo(new DateTimeOffset(1974, 8, 16, 0, 30, 0, 500, TimeSpan.FromHours(1))));
            Assert.AreEqual(-1, date.CompareTo(1));
            object o = new UniversalTime(1974, 8, 16, 0, 30, 0);
            Assert.AreEqual(0, date.CompareTo(o));
        }

        [TestMethod]
        public void TestCompare() {
            UniversalTime date0 = new UniversalTime(1974, 8, 16, 0, 30, 0);
            UniversalTime date1 = new UniversalTime(1974, 8, 10, 0, 30, 0);
            UniversalTime date2 = new UniversalTime(1974, 8, 20, 0, 30, 0);

            Assert.AreEqual(0, UniversalTime.Compare(date0, date0));
            Assert.AreEqual(6, UniversalTime.Compare(date0, date1));
            Assert.AreEqual(-4, UniversalTime.Compare(date0, date2));

        }

        [TestMethod]
        public void TestCompareOperator() {
            UniversalTime date0 = new UniversalTime(1974, 8, 16, 0, 30, 0);
            UniversalTime date1 = new UniversalTime(1974, 8, 10, 0, 30, 0);
            UniversalTime date2 = new UniversalTime(1974, 8, 20, 0, 30, 0);

            Assert.AreEqual(true, date0 == new UniversalTime(1974, 8, 16, 0, 30, 0));
            Assert.AreEqual(false, date0 != new UniversalTime(1974, 8, 16, 0, 30, 0));
            Assert.AreEqual(false, date0 < new UniversalTime(1974, 8, 16, 0, 30, 0));
            Assert.AreEqual(false, date0 > new UniversalTime(1974, 8, 16, 0, 30, 0));
            Assert.AreEqual(true, date0 <= new UniversalTime(1974, 8, 16, 0, 30, 0));
            Assert.AreEqual(true, date0 >= new UniversalTime(1974, 8, 16, 0, 30, 0));

            Assert.AreEqual(false, date0 == date1);
            Assert.AreEqual(true, date0 != date1);
            Assert.AreEqual(false, date0 < date1);
            Assert.AreEqual(true, date0 > date1);
            Assert.AreEqual(false, date0 <= date1);
            Assert.AreEqual(true, date0 >= date1);

            Assert.AreEqual(false, date0 == date2);
            Assert.AreEqual(true, date0 != date2);
            Assert.AreEqual(true, date0 < date2);
            Assert.AreEqual(false, date0 > date2);
            Assert.AreEqual(true, date0 <= date2);
            Assert.AreEqual(false, date0 >= date2);

        }

        [TestMethod]
        public void TestEquals() {
            UniversalTime date0 = new UniversalTime(1974, 8, 16, 0, 30, 0);
            UniversalTime date1 = new UniversalTime(1974, 8, 10, 0, 30, 0);
            UniversalTime date2 = new UniversalTime(1974, 8, 20, 0, 30, 0);

            Assert.AreEqual(true, date0.Equals(date0));
            Assert.AreEqual(false, date0.Equals(date1));
            Assert.AreEqual(false, date0.Equals(date2));

            object o = date0;
            Assert.AreEqual(true, date0.Equals(o));
            o = date1;
            Assert.AreEqual(false, date0.Equals(o));
            o = 123;
            Assert.AreEqual(false, date0.Equals(o));

        }

        [TestMethod]
        public void TestGetHashCode() {
            UniversalTime date0 = new UniversalTime(1974, 8, 16, 0, 30, 0);

            Assert.AreEqual(0, new UniversalTime().GetHashCode());
            Assert.AreEqual(1968, date0.GetHashCode());

        }

        [TestMethod]
        public void TestAddSubOperator() {
            UniversalTime date0 = new UniversalTime(1974, 8, 16, 0, 30, 0);

            Assert.AreEqual(new UniversalTime(1974, 8, 16, 5, 3, 0), date0 + TimeSpan.FromMinutes(273));
            Assert.AreEqual(new UniversalTime(1974, 8, 15, 19, 57, 0), date0 - TimeSpan.FromMinutes(273));

        }

        [TestMethod]
        public void TestToString() {
            UniversalTime date0 = new UniversalTime(1974, 8, 16, 0, 30, 0);
            UniversalTime date1 = new UniversalTime(1974, 8, 18, 15, 3, 25);

            Assert.AreEqual("16/08/1974 00:30:00", date0.ToString());
            Assert.AreEqual("16/08/1974 00:30:00", date0.ToString(""));
            Assert.AreEqual("16/08/1974 00:30:00", date0.ToString("", null));
            Assert.AreEqual("16/08/1974 00:30:00", date0.ToString("", System.Globalization.CultureInfo.GetCultureInfo("fr-FR")));
            Assert.AreEqual("16/08/1974 00:30:00", date0.ToString("", System.Globalization.CultureInfo.GetCultureInfo("en-us")));

            Assert.AreEqual("d 16 8 74 \\", date0.ToString(@"\d d M y \"));

            var culture = System.Globalization.CultureInfo.GetCultureInfo("en-us");
            Assert.AreEqual("16 16 Fri Friday Friday", date0.ToString("d dd ddd dddd ddddd", culture));
            Assert.AreEqual("18 18 Sun Sunday Sunday", date1.ToString("d dd ddd dddd ddddd", culture));
            Assert.AreEqual("16 16 ven. vendredi vendredi", date0.ToString("d dd ddd dddd ddddd", System.Globalization.CultureInfo.GetCultureInfo("fr-FR")));

            Assert.AreEqual("8 08 Aug August August", date0.ToString("M MM MMM MMMM MMMMM", culture));

            Assert.AreEqual("74 74 1974 1974 1974", date0.ToString("y yy yyy yyyy yyyyy", culture));

            Assert.AreEqual("0 00 00 00 00", date0.ToString("h hh hhh hhhh hhhhh", culture));
            Assert.AreEqual("3 03 03 03 03", date1.ToString("h hh hhh hhhh hhhhh", culture));
            Assert.AreEqual("0 00 00 00 00", date0.ToString("H HH HHH HHHH HHHHH", culture));
            Assert.AreEqual("15 15 15 15 15", date1.ToString("H HH HHH HHHH HHHHH", culture));

            Assert.AreEqual("30 30 30 30 30", date0.ToString("m mm mmm mmmm mmmmm", culture));
            Assert.AreEqual("3 03 03 03 03", date1.ToString("m mm mmm mmmm mmmmm", culture));

            Assert.AreEqual("0 00 00 00 00", date0.ToString("s ss sss ssss sssss", culture));
            Assert.AreEqual("25 25 25 25 25", date1.ToString("s ss sss ssss sssss", culture));

            Assert.AreEqual("A AM AM", date0.ToString("t tt ttt", culture));
            Assert.AreEqual("P PM PM", date1.ToString("t tt ttt", culture));
        }

    }
}
