using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using SwephNet;

namespace SwephNet.Tests
{
    [TestClass]
    public class SwephTest
    {

        [TestMethod]
        public void TestCreate() {
            using (var swe = new Sweph()) {
                Assert.IsNotNull(swe.Date);
            }
        }

        [TestMethod]
        public void TestLoadFile()
        {
            using (var swe = new Sweph())
            {
                Assert.IsNull(swe.LoadFile(null));
                Assert.IsNull(swe.LoadFile("file"));
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == "file")
                    {
                        e.File = new MemoryStream(System.Text.Encoding.Default.GetBytes("This is the file content."));
                    }
                };
                Assert.IsNull(swe.LoadFile(null));
                Assert.IsNull(swe.LoadFile("test"));
                var str = swe.LoadFile("file");
                Assert.IsNotNull(str);
                using (var r = new StreamReader(str))
                {
                    Assert.AreEqual("This is the file content.", r.ReadToEnd());
                }
            }
        }

        [TestMethod]
        public void TestTrace()
        {
            using (var swe = new Sweph())
            {
                List<String> messages = new List<string>();
                swe.Trace(null);
                swe.Trace("Message 1");
                swe.OnTrace += (s, e) => {
                    messages.Add(e.Message);
                };
                swe.Trace(null);
                swe.Trace("Message 2");
                swe.Trace("Message {0}", 3);
                CollectionAssert.AreEqual(new String[]{
                    null,
                    "Message 2",
                    "Message 3",
                }, messages.ToArray());
            }
        }

    }
}
