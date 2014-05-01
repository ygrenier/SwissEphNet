using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class TraceEventArgsTest
    {
        [TestMethod]
        public void TestCreate() {
            var target = new TraceEventArgs("message");
            Assert.AreEqual("message", target.Message);
        }
    }
}
