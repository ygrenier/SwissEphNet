using SweNet;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{

    [TestClass]
    public class DateUTTest
    {

        [TestMethod]
        public void TestCreateEmpty() {
            DateUT date = new DateUT();

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
            DateUT date = new DateUT(dt);

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
            DateUT date = new DateUT(dt);

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
            DateUT date = new DateUT(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

            Assert.AreEqual(dt.Year, date.Year);
            Assert.AreEqual(dt.Month, date.Month);
            Assert.AreEqual(dt.Day, date.Day);
            Assert.AreEqual(dt.Hour, date.Hours);
            Assert.AreEqual(dt.Minute, date.Minutes);
            Assert.AreEqual(dt.Second, date.Seconds);

            date = new DateUT(dt.Year, dt.Month, dt.Day, dt.GetHourValue());

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
            DateUT date = new DateUT(dt);

            Assert.AreEqual(dt, date.ToDateTime());
        }

        [TestMethod]
        public void TestToDateTimeOffset() {
            var dt = DateTimeOffset.Now;
            dt = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, TimeSpan.Zero); // Remove milliseconds
            DateUT date = new DateUT(dt);

            Assert.AreEqual(dt, date.ToDateTimeOffset());
        }

    }
}
