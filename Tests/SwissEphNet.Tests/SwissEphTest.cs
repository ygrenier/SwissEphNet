using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public partial class SwissEphTest
    {
        [TestMethod]
        public void TestConstructor() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(true, target.ESPENAK_MEEUS_2006);
            }
        }
    }
}
