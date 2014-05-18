using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SwephNet.Tests
{
    [TestClass]
    public class SwephNetTest
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

    }
}
