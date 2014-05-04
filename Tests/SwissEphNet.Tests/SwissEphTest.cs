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

        [TestMethod]
        public void TestVersion() {
            using (var target = new SwissEph()) {
                Assert.AreEqual("2.00.00", target.swe_version());
                Assert.AreEqual("2.00.00-net-0001", target.swe_dotnet_version());
            }
        }

    }
}
