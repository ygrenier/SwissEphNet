using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{

    [TestClass]
    public class CTest
    {
    
        [TestMethod]
        public void TestAtof() {
            Assert.AreEqual(0.0, C.atof(null));
            Assert.AreEqual(0.0, C.atof(""));
            Assert.AreEqual(0.0, C.atof("test"));
            Assert.AreEqual(0.0, C.atof("0"));
            Assert.AreEqual(0.0, C.atof("0.0"));
            Assert.AreEqual(1.0, C.atof("1"));
            Assert.AreEqual(1.2, C.atof("1.2"));
            Assert.AreEqual(1.0, C.atof("+1"));
            Assert.AreEqual(1.2, C.atof("+1.2"));
            Assert.AreEqual(-1.0, C.atof("-1"));
            Assert.AreEqual(-1.2, C.atof("-1.2"));
        }

    }
}
