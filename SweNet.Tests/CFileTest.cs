using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace SweNet.Tests
{
    [TestClass]
    public class CFileTest
    {

        static Stream BuildStream(String content, Encoding enc = null) {
            enc = enc ?? System.Text.Encoding.UTF8;
            var result = new MemoryStream();
            var bs = enc.GetBytes(content);
            result.Write(bs, 0, bs.Length);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        [TestMethod]
        public void TestCreate() {
            var stream = new System.IO.MemoryStream();
            using (var cfile = new CFile(stream)) {
                Assert.AreEqual(0, cfile.Length);
                Assert.AreEqual(0, cfile.Position);
                Assert.AreEqual(false, cfile.EOF);
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(true, cfile.EOF);
            }
        }

        [TestMethod]
        public void TestReadLine() {
            using (var cfile = new CFile(BuildStream(""))) {
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(" "))) {
                Assert.AreEqual(" ", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual("3", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual("3", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r4"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual("3", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("4", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r4\r\n"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual("3", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("4", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r4\r\n5"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("2", cfile.ReadLine());
                Assert.AreEqual("3", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("4", cfile.ReadLine());
                Assert.AreEqual("5", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n\n\n\r\r\r\n5"))) {
                Assert.AreEqual("1", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("", cfile.ReadLine());
                Assert.AreEqual("5", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadLineEncoded() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.AreEqual("Ã¨aÃ ", cfile.ReadLine());
                Assert.AreEqual("Ã¼Ã®", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.AreEqual("èaà", cfile.ReadLine());
                Assert.AreEqual("üî", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.AreEqual("èaà", cfile.ReadLine());
                Assert.AreEqual("üî", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual("èaà", cfile.ReadLine());
                Assert.AreEqual("üî", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual("Ã¨aÃ ", cfile.ReadLine());
                Assert.AreEqual("Ã¼Ã®", cfile.ReadLine());
                Assert.AreEqual(null, cfile.ReadLine());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

    }
}
