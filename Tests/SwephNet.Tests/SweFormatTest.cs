using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    [TestClass]
    public class SweFormatTest
    {

        [TestMethod]
        public void TestFormatAsDegrees() {
            Assert.AreEqual("   0°  0'  0,0000", SweFormat.FormatAsDegrees(0));
            Assert.AreEqual(" 123° 27' 24,4080", SweFormat.FormatAsDegrees(123.45678));
            Assert.AreEqual("-123° 27' 24,4080", SweFormat.FormatAsDegrees(-123.45678));
        }

        [TestMethod]
        public void TestFormatAsTime() {
            Assert.AreEqual("00:00:00", SweFormat.FormatAsTime(0));
            Assert.AreEqual("123:27:24", SweFormat.FormatAsTime(123.45678));
            Assert.AreEqual("-123:27:24", SweFormat.FormatAsTime(-123.45678));
        }

        [TestMethod]
        public void TestFormatAsHour() {
            Assert.AreEqual(" 0 h 00 m 00 s", SweFormat.FormatAsHour(0));
            Assert.AreEqual("123 h 27 m 24 s", SweFormat.FormatAsHour(123.45678));
            Assert.AreEqual("-123 h 27 m 24 s", SweFormat.FormatAsHour(-123.45678));
        }

    }
}
