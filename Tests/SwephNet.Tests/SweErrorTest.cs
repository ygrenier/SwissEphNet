using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    /// <summary>
    /// Description résumée pour SweErrorTest
    /// </summary>
    [TestClass]
    public class SweErrorTest
    {
        [TestMethod]
        public void TestCreate()
        {

            var target = new SweError("Message");
            Assert.AreEqual("Message", target.Message);
            Assert.IsNull(target.InnerException);

            target = new SweError("Message {0}", 2);
            Assert.AreEqual("Message 2", target.Message);
            Assert.IsNull(target.InnerException);

            var ex = new ArgumentException();

            target = new SweError(null, "Message");
            Assert.AreEqual("Message", target.Message);
            Assert.IsNull(target.InnerException);

            target = new SweError(null, "Message {0}", 2);
            Assert.AreEqual("Message 2", target.Message);
            Assert.IsNull(target.InnerException);

            target = new SweError(ex, "Message");
            Assert.AreEqual("Message", target.Message);
            Assert.AreSame(ex, target.InnerException);

            target = new SweError(ex, "Message {0}", 2);
            Assert.AreEqual("Message 2", target.Message);
            Assert.AreSame(ex, target.InnerException);

        }
    }
}
