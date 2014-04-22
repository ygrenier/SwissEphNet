using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace SweNet.Tests
{
    [TestClass]
    public class CFileTest
    {

        static Stream BuildStream(byte[] content) {
            var result = new MemoryStream();
            result.Write(content, 0, content.Length);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        static Stream BuildStream(String content, Encoding enc = null) {
            enc = enc ?? System.Text.Encoding.UTF8;
            var bs = enc.GetBytes(content);
            return BuildStream(enc.GetBytes(content));
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

        [TestMethod]
        public void TestRead() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(168, cfile.Read());
                Assert.AreEqual(97, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(160, cfile.Read());
                Assert.AreEqual(10, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(188, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(174, cfile.Read());
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.AreEqual(232, cfile.Read());
                Assert.AreEqual(97, cfile.Read());
                Assert.AreEqual(224, cfile.Read());
                Assert.AreEqual(10, cfile.Read());
                Assert.AreEqual(252, cfile.Read());
                Assert.AreEqual(238, cfile.Read());
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(168, cfile.Read());
                Assert.AreEqual(97, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(160, cfile.Read());
                Assert.AreEqual(10, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(188, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(174, cfile.Read());
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(232, cfile.Read());
                Assert.AreEqual(97, cfile.Read());
                Assert.AreEqual(224, cfile.Read());
                Assert.AreEqual(10, cfile.Read());
                Assert.AreEqual(252, cfile.Read());
                Assert.AreEqual(238, cfile.Read());
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(168, cfile.Read());
                Assert.AreEqual(97, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(160, cfile.Read());
                Assert.AreEqual(10, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(188, cfile.Read());
                Assert.AreEqual(195, cfile.Read());
                Assert.AreEqual(174, cfile.Read());
                Assert.AreEqual(-1, cfile.Read());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadChar() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(168, cfile.ReadChar());
                Assert.AreEqual(97, cfile.ReadChar());
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(160, cfile.ReadChar());
                Assert.AreEqual(10, cfile.ReadChar());
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(188, cfile.ReadChar());
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(174, cfile.ReadChar());
                Assert.AreEqual(0, cfile.ReadChar());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.AreEqual(232, cfile.ReadChar());
                Assert.AreEqual(97, cfile.ReadChar());
                Assert.AreEqual(224, cfile.ReadChar());
                Assert.AreEqual(10, cfile.ReadChar());
                Assert.AreEqual(252, cfile.ReadChar());
                Assert.AreEqual(238, cfile.ReadChar());
                Assert.AreEqual(0, cfile.ReadChar());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.AreEqual('è', cfile.ReadChar());
                Assert.AreEqual('a', cfile.ReadChar());
                Assert.AreEqual('à', cfile.ReadChar());
                Assert.AreEqual('\n', cfile.ReadChar());
                Assert.AreEqual('ü', cfile.ReadChar());
                Assert.AreEqual('î', cfile.ReadChar());
                Assert.AreEqual(0, cfile.ReadChar());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(232, cfile.ReadChar());
                Assert.AreEqual(97, cfile.ReadChar());
                Assert.AreEqual(224, cfile.ReadChar());
                Assert.AreEqual(10, cfile.ReadChar());
                Assert.AreEqual(252, cfile.ReadChar());
                Assert.AreEqual(238, cfile.ReadChar());
                Assert.AreEqual(0, cfile.ReadChar());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(168, cfile.ReadChar());
                Assert.AreEqual(97, cfile.ReadChar());
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(160, cfile.ReadChar());
                Assert.AreEqual(10, cfile.ReadChar());
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(188, cfile.ReadChar());
                Assert.AreEqual(195, cfile.ReadChar());
                Assert.AreEqual(174, cfile.ReadChar());
                Assert.AreEqual(0, cfile.ReadChar());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 195 }), Encoding.UTF8)) {
                Assert.AreEqual(65533, cfile.ReadChar());
                Assert.AreEqual(0, cfile.ReadChar());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadChars() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                CollectionAssert.AreEqual(new char[] { 'Ã', '¨', 'a' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { 'Ã', ' ', '\n' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { 'Ã', '¼', 'Ã', }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { '®' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(null, cfile.ReadChars(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                CollectionAssert.AreEqual(new char[] { 'è', 'a', 'à' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { '\n', 'ü', 'î' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(null, cfile.ReadChars(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                CollectionAssert.AreEqual(new char[] { 'è', 'a', 'à' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { '\n', 'ü', 'î' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(null, cfile.ReadChars(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                CollectionAssert.AreEqual(new char[] { 'è', 'a', 'à' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { '\n', 'ü', 'î' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(null, cfile.ReadChars(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                CollectionAssert.AreEqual(new char[] { 'Ã', '¨', 'a' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { 'Ã', ' ', '\n' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { 'Ã', '¼', 'Ã', }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(new char[] { '®' }, cfile.ReadChars(3));
                CollectionAssert.AreEqual(null, cfile.ReadChars(3));
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadString() {
            String str = null;
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("Ã¨a", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("Ã \n", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("Ã¼Ã", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual("®", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual(null, str);

                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("èaà", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("\nüî", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual(null, str);

                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("èaà", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("\nüî", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual(null, str);

                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("èaà", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("\nüî", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual(null, str);

                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("Ã¨a", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("Ã \n", str);

                str = "$$$";
                Assert.AreEqual(true, cfile.ReadString(ref str, 3));
                Assert.AreEqual("Ã¼Ã", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual("®", str);

                str = "$$$";
                Assert.AreEqual(false, cfile.ReadString(ref str, 3));
                Assert.AreEqual(null, str);

                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadByte() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.AreEqual(195, cfile.ReadByte());
                Assert.AreEqual(168, cfile.ReadByte());
                Assert.AreEqual(97, cfile.ReadByte());
                Assert.AreEqual(195, cfile.ReadByte());
                Assert.AreEqual(160, cfile.ReadByte());
                Assert.AreEqual(10, cfile.ReadByte());
                Assert.AreEqual(195, cfile.ReadByte());
                Assert.AreEqual(188, cfile.ReadByte());
                Assert.AreEqual(195, cfile.ReadByte());
                Assert.AreEqual(174, cfile.ReadByte());
                Assert.AreEqual(0, cfile.ReadByte());
                Assert.AreEqual(0, cfile.ReadByte());
                Assert.AreEqual(true, cfile.EOF);
            }
        }

        [TestMethod]
        public void TestReadBytes() {
            byte[] buff = new byte[3];
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 168, 97 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 160, 10}, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 188, 195 }, buff);
                Assert.AreEqual(1, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 174, 188, 195 }, buff);
                Assert.AreEqual(0, cfile.Read(buff, 0, 3));
                Assert.AreEqual(0, cfile.Read(buff, 0, 3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 232, 97, 224 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 10, 252, 238 }, buff);
                Assert.AreEqual(0, cfile.Read(buff, 0, 3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 168, 97 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 160, 10 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 188, 195 }, buff);
                Assert.AreEqual(1, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 174, 188, 195 }, buff);
                Assert.AreEqual(0, cfile.Read(buff, 0, 3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 232, 97, 224 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 10, 252, 238 }, buff);
                Assert.AreEqual(0, cfile.Read(buff, 0, 3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 168, 97 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 160, 10 }, buff);
                Assert.AreEqual(3, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 195, 188, 195 }, buff);
                Assert.AreEqual(1, cfile.Read(buff, 0, 3));
                CollectionAssert.AreEqual(new byte[] { 174, 188, 195 }, buff);
                Assert.AreEqual(0, cfile.Read(buff, 0, 3));
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadSByte() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-88, cfile.ReadSByte());
                Assert.AreEqual(97, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-96, cfile.ReadSByte());
                Assert.AreEqual(10, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-68, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-82, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.AreEqual(-24, cfile.ReadSByte());
                Assert.AreEqual(97, cfile.ReadSByte());
                Assert.AreEqual(-32, cfile.ReadSByte());
                Assert.AreEqual(10, cfile.ReadSByte());
                Assert.AreEqual(-4, cfile.ReadSByte());
                Assert.AreEqual(-18, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-88, cfile.ReadSByte());
                Assert.AreEqual(97, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-96, cfile.ReadSByte());
                Assert.AreEqual(10, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-68, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-82, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(-24, cfile.ReadSByte());
                Assert.AreEqual(97, cfile.ReadSByte());
                Assert.AreEqual(-32, cfile.ReadSByte());
                Assert.AreEqual(10, cfile.ReadSByte());
                Assert.AreEqual(-4, cfile.ReadSByte());
                Assert.AreEqual(-18, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-88, cfile.ReadSByte());
                Assert.AreEqual(97, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-96, cfile.ReadSByte());
                Assert.AreEqual(10, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-68, cfile.ReadSByte());
                Assert.AreEqual(-61, cfile.ReadSByte());
                Assert.AreEqual(-82, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0, 0x01, 0x10, 0x80, 0xF0, 0xFF }))) {
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(1, cfile.ReadSByte());
                Assert.AreEqual(16, cfile.ReadSByte());
                Assert.AreEqual(-128, cfile.ReadSByte());
                Assert.AreEqual(-16, cfile.ReadSByte());
                Assert.AreEqual(-1, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(0, cfile.ReadSByte());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadSBytes() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                CollectionAssert.AreEqual(new sbyte[] { -61, -88, 97 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -61, -96, 10 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -61, -68, -61 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -82 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(null, cfile.ReadSBytes(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                CollectionAssert.AreEqual(new sbyte[] { -24, 97, -32 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { 10, -4, -18 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(null, cfile.ReadSBytes(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                CollectionAssert.AreEqual(new sbyte[] { -61, -88, 97 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -61, -96, 10 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -61, -68, -61 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -82 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(null, cfile.ReadSBytes(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                CollectionAssert.AreEqual(new sbyte[] { -24, 97, -32 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { 10, -4, -18 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(null, cfile.ReadSBytes(3));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                CollectionAssert.AreEqual(new sbyte[] { -61, -88, 97 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -61, -96, 10 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -61, -68, -61 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(new sbyte[] { -82 }, cfile.ReadSBytes(3));
                CollectionAssert.AreEqual(null, cfile.ReadSBytes(3));
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadInt32() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual((int)0x78563412, cfile.ReadInt32());
                Assert.AreEqual(0x98BADCFE, (uint)cfile.ReadInt32());
                Assert.AreEqual(0, cfile.ReadInt32());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA }))) {
                Assert.AreEqual((int)0x78563412, cfile.ReadInt32());
                Assert.AreEqual(0, cfile.ReadInt32());
                Assert.AreEqual(0, cfile.ReadInt32());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual('a', cfile.ReadChar());
                Assert.AreEqual((int)0x78563412, cfile.ReadInt32());
                Assert.AreEqual(0x98BADCFE, (uint)cfile.ReadInt32());
                Assert.AreEqual(0, cfile.ReadInt32());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadUInt32() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual((uint)0x78563412, cfile.ReadUInt32());
                Assert.AreEqual(0x98BADCFE, cfile.ReadUInt32());
                Assert.AreEqual((uint)0, cfile.ReadUInt32());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA }))) {
                Assert.AreEqual((uint)0x78563412, cfile.ReadUInt32());
                Assert.AreEqual((uint)0, cfile.ReadUInt32());
                Assert.AreEqual((uint)0, cfile.ReadUInt32());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual('a', cfile.ReadChar());
                Assert.AreEqual((uint)0x78563412, cfile.ReadUInt32());
                Assert.AreEqual(0x98BADCFE, cfile.ReadUInt32());
                Assert.AreEqual((uint)0, cfile.ReadUInt32());
                Assert.AreEqual(true, cfile.EOF);
            }

        }

        [TestMethod]
        public void TestReadDouble() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual(4.69197536052338E+271, cfile.ReadDouble(), 1e+257);
                Assert.AreEqual(-1.50730608775746E-189, cfile.ReadDouble(), 1e-150);
                Assert.AreEqual(0, cfile.ReadDouble());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA}))) {
                Assert.AreEqual(4.69197536052338E+271, cfile.ReadDouble(), 1e+257);
                Assert.AreEqual(0, cfile.ReadDouble());
                Assert.AreEqual(0, cfile.ReadDouble());
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] {97,  0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual('a', cfile.ReadChar());
                Assert.AreEqual(4.69197536052338E+271, cfile.ReadDouble(), 1e+257);
                Assert.AreEqual(-1.50730608775746E-189, cfile.ReadDouble(), 1e-150);
                Assert.AreEqual(0, cfile.ReadInt32());
                Assert.AreEqual(true, cfile.EOF);
            }
        }

        [TestMethod]
        public void TestReadInt32s() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                var vals = cfile.ReadInt32s(4);
                Assert.AreEqual(2, vals.Length);
                Assert.AreEqual((int)0x78563412, vals[0]);
                Assert.AreEqual(0x98BADCFE, (uint)vals[1]);
                Assert.AreEqual(null, cfile.ReadInt32s(4));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA }))) {
                var vals = cfile.ReadInt32s(4);
                Assert.AreEqual(1, vals.Length);
                Assert.AreEqual((int)0x78563412, vals[0]);
                Assert.AreEqual(null, cfile.ReadInt32s(4));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual('a', cfile.ReadChar());
                var vals = cfile.ReadInt32s(4);
                Assert.AreEqual(2, vals.Length);
                Assert.AreEqual((int)0x78563412, vals[0]);
                Assert.AreEqual(0x98BADCFE, (uint)vals[1]);
                Assert.AreEqual(null, cfile.ReadInt32s(4));
                Assert.AreEqual(true, cfile.EOF);
            }
        }

        [TestMethod]
        public void TestReadDoubles() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                var vals = cfile.ReadDoubles(4);
                Assert.AreEqual(2, vals.Length);
                Assert.AreEqual(4.69197536052338E+271, vals[0], 1e+257);
                Assert.AreEqual(-1.50730608775746E-189, vals[1], 1e-150);
                Assert.AreEqual(null, cfile.ReadDoubles(4));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA }))) {
                var vals = cfile.ReadDoubles(4);
                Assert.AreEqual(1, vals.Length);
                Assert.AreEqual(4.69197536052338E+271, vals[0], 1e+257);
                Assert.AreEqual(null, cfile.ReadDoubles(4));
                Assert.AreEqual(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual('a', cfile.ReadChar());
                var vals = cfile.ReadDoubles(4);
                Assert.AreEqual(2, vals.Length);
                Assert.AreEqual(4.69197536052338E+271, vals[0], 1e+257);
                Assert.AreEqual(-1.50730608775746E-189, vals[1], 1e-150);
                Assert.AreEqual(null, cfile.ReadDoubles(4));
                Assert.AreEqual(true, cfile.EOF);
            }
        }
        
        [TestMethod]
        public void TestSeek() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.AreEqual(4, cfile.Seek(4, SeekOrigin.Current));
                Assert.AreEqual(0x98BADCFE, (uint)cfile.ReadInt32());
                Assert.AreEqual(0, cfile.Seek(0, SeekOrigin.Begin));
                Assert.AreEqual((int)0x78563412, cfile.ReadInt32());
            }

            using (var cfile = new CFile(null)) {
                Assert.AreEqual(0, cfile.Seek(4, SeekOrigin.Current));
            }

        }

    }
}
