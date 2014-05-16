using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{

    [TestClass]
    public class SideralTimeTest
    {

        [TestMethod]
        public void TestCreateEmpty() {
            SideralTime time = new SideralTime();
            Assert.AreEqual(0, time.Value);
        }

        [TestMethod]
        public void TestCreate() {
            SideralTime time = new SideralTime(12.3456789);
            Assert.AreEqual(12.3456789, time.Value);
        }

        [TestMethod]
        public void TestToString() {
            SideralTime time = new SideralTime(12.3456789);
            Assert.AreEqual("12:20:44", time.ToString());
        }

    }
}
