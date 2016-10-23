using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class LoadFileEventArgsTest
    {
        [TestMethod]
        public void TestCreate() {
            var target = new LoadFileEventArgs("file");
            Assert.AreEqual("file", target.FileName);
            Assert.IsNull(target.File);

            var stream = new System.IO.MemoryStream();
            target.File = stream;

            Assert.AreSame(stream, target.File);

        }
    }
}
