using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{

    [TestClass]
    public class GeoPositionTest
    {

        [TestMethod]
        public void TestCreateEmpty() {
            GeoPosition pos = new GeoPosition();
            Assert.AreEqual(0, pos.Longitude.Value);
            Assert.AreEqual(0, pos.Latitude.Value);
            Assert.AreEqual(0, pos.Altitude);
        }

        [TestMethod]
        public void TestCreate() {
            GeoPosition pos = new GeoPosition(46.72, -5.23, 12);
            Assert.AreEqual(46.72, pos.Longitude.Value);
            Assert.AreEqual(-5.23, pos.Latitude.Value);
            Assert.AreEqual(12, pos.Altitude);
        }

        [TestMethod]
        public void TestToString() {
            GeoPosition pos = new GeoPosition(46.72, -5.23, 12);
            Assert.AreEqual("46E43'12\", 5S13'48\", 12 m", pos.ToString());
        }

    }
}
