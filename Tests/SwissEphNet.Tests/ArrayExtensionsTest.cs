using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class ArrayExtensionsTest
    {
        [TestMethod]
        public void TestGetPointer() {
            var array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var target = array.GetPointer();
            Assert.AreEqual(10, target.Length);
            Assert.AreEqual(0, target[0]);

            target = array.GetPointer(5);
            Assert.AreEqual(5, target.Length);
            Assert.AreEqual(5, target[0]);

        }
    }
}
