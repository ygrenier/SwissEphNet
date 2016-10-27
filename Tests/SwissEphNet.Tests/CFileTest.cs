using System;
using System.Globalization;
using System.IO;
using System.Text;
using Xunit;

namespace SwissEphNet.Tests
{

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

        [Fact]
        public void TestCreate() {
            var stream = new System.IO.MemoryStream();
            using (var cfile = new CFile(stream)) {
                Assert.Equal(0, cfile.Length);
                Assert.Equal(0, cfile.Position);
                Assert.Equal(false, cfile.EOF);
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(true, cfile.EOF);
            }
        }

        [Fact]
        public void TestReadLine() {
            using (var cfile = new CFile(BuildStream(""))) {
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(" "))) {
                Assert.Equal(" ", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal("3", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal("3", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r4"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal("3", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("4", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r4\r\n"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal("3", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("4", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n2\r3\n\r4\r\n5"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("2", cfile.ReadLine());
                Assert.Equal("3", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("4", cfile.ReadLine());
                Assert.Equal("5", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("1\n\n\n\r\r\r\n5"))) {
                Assert.Equal("1", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("", cfile.ReadLine());
                Assert.Equal("5", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadLineEncoded() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal("Ã¨aÃ ", cfile.ReadLine());
                Assert.Equal("Ã¼Ã®", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal("èaà", cfile.ReadLine());
                Assert.Equal("üî", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal("èaà", cfile.ReadLine());
                Assert.Equal("üî", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal("èaà", cfile.ReadLine());
                Assert.Equal("üî", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal("Ã¨aÃ ", cfile.ReadLine());
                Assert.Equal("Ã¼Ã®", cfile.ReadLine());
                Assert.Equal(null, cfile.ReadLine());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestRead() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(195, cfile.Read());
                Assert.Equal(168, cfile.Read());
                Assert.Equal(97, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(160, cfile.Read());
                Assert.Equal(10, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(188, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(174, cfile.Read());
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal(232, cfile.Read());
                Assert.Equal(97, cfile.Read());
                Assert.Equal(224, cfile.Read());
                Assert.Equal(10, cfile.Read());
                Assert.Equal(252, cfile.Read());
                Assert.Equal(238, cfile.Read());
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal(195, cfile.Read());
                Assert.Equal(168, cfile.Read());
                Assert.Equal(97, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(160, cfile.Read());
                Assert.Equal(10, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(188, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(174, cfile.Read());
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(232, cfile.Read());
                Assert.Equal(97, cfile.Read());
                Assert.Equal(224, cfile.Read());
                Assert.Equal(10, cfile.Read());
                Assert.Equal(252, cfile.Read());
                Assert.Equal(238, cfile.Read());
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(195, cfile.Read());
                Assert.Equal(168, cfile.Read());
                Assert.Equal(97, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(160, cfile.Read());
                Assert.Equal(10, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(188, cfile.Read());
                Assert.Equal(195, cfile.Read());
                Assert.Equal(174, cfile.Read());
                Assert.Equal(-1, cfile.Read());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadChar() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(168, cfile.ReadChar());
                Assert.Equal(97, cfile.ReadChar());
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(160, cfile.ReadChar());
                Assert.Equal(10, cfile.ReadChar());
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(188, cfile.ReadChar());
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(174, cfile.ReadChar());
                Assert.Equal(0, cfile.ReadChar());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal(232, cfile.ReadChar());
                Assert.Equal(97, cfile.ReadChar());
                Assert.Equal(224, cfile.ReadChar());
                Assert.Equal(10, cfile.ReadChar());
                Assert.Equal(252, cfile.ReadChar());
                Assert.Equal(238, cfile.ReadChar());
                Assert.Equal(0, cfile.ReadChar());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal('è', cfile.ReadChar());
                Assert.Equal('a', cfile.ReadChar());
                Assert.Equal('à', cfile.ReadChar());
                Assert.Equal('\n', cfile.ReadChar());
                Assert.Equal('ü', cfile.ReadChar());
                Assert.Equal('î', cfile.ReadChar());
                Assert.Equal(0, cfile.ReadChar());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(232, cfile.ReadChar());
                Assert.Equal(97, cfile.ReadChar());
                Assert.Equal(224, cfile.ReadChar());
                Assert.Equal(10, cfile.ReadChar());
                Assert.Equal(252, cfile.ReadChar());
                Assert.Equal(238, cfile.ReadChar());
                Assert.Equal(0, cfile.ReadChar());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(168, cfile.ReadChar());
                Assert.Equal(97, cfile.ReadChar());
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(160, cfile.ReadChar());
                Assert.Equal(10, cfile.ReadChar());
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(188, cfile.ReadChar());
                Assert.Equal(195, cfile.ReadChar());
                Assert.Equal(174, cfile.ReadChar());
                Assert.Equal(0, cfile.ReadChar());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 195 }), Encoding.UTF8)) {
                Assert.Equal(65533, cfile.ReadChar());
                Assert.Equal(0, cfile.ReadChar());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadChars() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(new char[] { 'Ã', '¨', 'a' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { 'Ã', ' ', '\n' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { 'Ã', '¼', 'Ã', }, cfile.ReadChars(3));
                Assert.Equal(new char[] { '®' }, cfile.ReadChars(3));
                Assert.Equal(null, cfile.ReadChars(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal(new char[] { 'è', 'a', 'à' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { '\n', 'ü', 'î' }, cfile.ReadChars(3));
                Assert.Equal(null, cfile.ReadChars(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal(new char[] { 'è', 'a', 'à' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { '\n', 'ü', 'î' }, cfile.ReadChars(3));
                Assert.Equal(null, cfile.ReadChars(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(new char[] { 'è', 'a', 'à' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { '\n', 'ü', 'î' }, cfile.ReadChars(3));
                Assert.Equal(null, cfile.ReadChars(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(new char[] { 'Ã', '¨', 'a' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { 'Ã', ' ', '\n' }, cfile.ReadChars(3));
                Assert.Equal(new char[] { 'Ã', '¼', 'Ã', }, cfile.ReadChars(3));
                Assert.Equal(new char[] { '®' }, cfile.ReadChars(3));
                Assert.Equal(null, cfile.ReadChars(3));
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadString() {
            String str = null;
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("Ã¨a", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("Ã \n", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("Ã¼Ã", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal("®", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal(null, str);

                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("èaà", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("\nüî", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal(null, str);

                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("èaà", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("\nüî", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal(null, str);

                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("èaà", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("\nüî", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal(null, str);

                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("Ã¨a", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("Ã \n", str);

                str = "$$$";
                Assert.Equal(true, cfile.ReadString(ref str, 3));
                Assert.Equal("Ã¼Ã", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal("®", str);

                str = "$$$";
                Assert.Equal(false, cfile.ReadString(ref str, 3));
                Assert.Equal(null, str);

                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadByte() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(195, cfile.ReadByte());
                Assert.Equal(168, cfile.ReadByte());
                Assert.Equal(97, cfile.ReadByte());
                Assert.Equal(195, cfile.ReadByte());
                Assert.Equal(160, cfile.ReadByte());
                Assert.Equal(10, cfile.ReadByte());
                Assert.Equal(195, cfile.ReadByte());
                Assert.Equal(188, cfile.ReadByte());
                Assert.Equal(195, cfile.ReadByte());
                Assert.Equal(174, cfile.ReadByte());
                Assert.Equal(0, cfile.ReadByte());
                Assert.Equal(0, cfile.ReadByte());
                Assert.Equal(true, cfile.EOF);
            }
        }

        [Fact]
        public void TestReadBytes() {
            byte[] buff = new byte[3];
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 168, 97 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 160, 10}, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 188, 195 }, buff);
                Assert.Equal(1, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 174, 188, 195 }, buff);
                Assert.Equal(0, cfile.Read(buff, 0, 3));
                Assert.Equal(0, cfile.Read(buff, 0, 3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 232, 97, 224 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 10, 252, 238 }, buff);
                Assert.Equal(0, cfile.Read(buff, 0, 3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 168, 97 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 160, 10 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 188, 195 }, buff);
                Assert.Equal(1, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 174, 188, 195 }, buff);
                Assert.Equal(0, cfile.Read(buff, 0, 3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 232, 97, 224 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 10, 252, 238 }, buff);
                Assert.Equal(0, cfile.Read(buff, 0, 3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 168, 97 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 160, 10 }, buff);
                Assert.Equal(3, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 195, 188, 195 }, buff);
                Assert.Equal(1, cfile.Read(buff, 0, 3));
                Assert.Equal(new byte[] { 174, 188, 195 }, buff);
                Assert.Equal(0, cfile.Read(buff, 0, 3));
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadSByte() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-88, cfile.ReadSByte());
                Assert.Equal(97, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-96, cfile.ReadSByte());
                Assert.Equal(10, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-68, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-82, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal(-24, cfile.ReadSByte());
                Assert.Equal(97, cfile.ReadSByte());
                Assert.Equal(-32, cfile.ReadSByte());
                Assert.Equal(10, cfile.ReadSByte());
                Assert.Equal(-4, cfile.ReadSByte());
                Assert.Equal(-18, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-88, cfile.ReadSByte());
                Assert.Equal(97, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-96, cfile.ReadSByte());
                Assert.Equal(10, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-68, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-82, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(-24, cfile.ReadSByte());
                Assert.Equal(97, cfile.ReadSByte());
                Assert.Equal(-32, cfile.ReadSByte());
                Assert.Equal(10, cfile.ReadSByte());
                Assert.Equal(-4, cfile.ReadSByte());
                Assert.Equal(-18, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-88, cfile.ReadSByte());
                Assert.Equal(97, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-96, cfile.ReadSByte());
                Assert.Equal(10, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-68, cfile.ReadSByte());
                Assert.Equal(-61, cfile.ReadSByte());
                Assert.Equal(-82, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0, 0x01, 0x10, 0x80, 0xF0, 0xFF }))) {
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(1, cfile.ReadSByte());
                Assert.Equal(16, cfile.ReadSByte());
                Assert.Equal(-128, cfile.ReadSByte());
                Assert.Equal(-16, cfile.ReadSByte());
                Assert.Equal(-1, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(0, cfile.ReadSByte());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadSBytes() {
            using (var cfile = new CFile(BuildStream("èaà\nüî"))) {
                Assert.Equal(new sbyte[] { -61, -88, 97 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -61, -96, 10 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -61, -68, -61 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -82 }, cfile.ReadSBytes(3));
                Assert.Equal(null, cfile.ReadSBytes(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")))) {
                Assert.Equal(new sbyte[] { -24, 97, -32 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { 10, -4, -18 }, cfile.ReadSBytes(3));
                Assert.Equal(null, cfile.ReadSBytes(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî"), Encoding.UTF8)) {
                Assert.Equal(new sbyte[] { -61, -88, 97 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -61, -96, 10 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -61, -68, -61 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -82 }, cfile.ReadSBytes(3));
                Assert.Equal(null, cfile.ReadSBytes(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.GetEncoding("windows-1252")), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(new sbyte[] { -24, 97, -32 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { 10, -4, -18 }, cfile.ReadSBytes(3));
                Assert.Equal(null, cfile.ReadSBytes(3));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream("èaà\nüî", Encoding.UTF8), Encoding.GetEncoding("windows-1252"))) {
                Assert.Equal(new sbyte[] { -61, -88, 97 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -61, -96, 10 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -61, -68, -61 }, cfile.ReadSBytes(3));
                Assert.Equal(new sbyte[] { -82 }, cfile.ReadSBytes(3));
                Assert.Equal(null, cfile.ReadSBytes(3));
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadInt32() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal((int)0x78563412, cfile.ReadInt32());
                Assert.Equal(0x98BADCFE, (uint)cfile.ReadInt32());
                Assert.Equal(0, cfile.ReadInt32());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA }))) {
                Assert.Equal((int)0x78563412, cfile.ReadInt32());
                Assert.Equal(0, cfile.ReadInt32());
                Assert.Equal(0, cfile.ReadInt32());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal('a', cfile.ReadChar());
                Assert.Equal((int)0x78563412, cfile.ReadInt32());
                Assert.Equal(0x98BADCFE, (uint)cfile.ReadInt32());
                Assert.Equal(0, cfile.ReadInt32());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadUInt32() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal((uint)0x78563412, cfile.ReadUInt32());
                Assert.Equal(0x98BADCFE, cfile.ReadUInt32());
                Assert.Equal((uint)0, cfile.ReadUInt32());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA }))) {
                Assert.Equal((uint)0x78563412, cfile.ReadUInt32());
                Assert.Equal((uint)0, cfile.ReadUInt32());
                Assert.Equal((uint)0, cfile.ReadUInt32());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal('a', cfile.ReadChar());
                Assert.Equal((uint)0x78563412, cfile.ReadUInt32());
                Assert.Equal(0x98BADCFE, cfile.ReadUInt32());
                Assert.Equal((uint)0, cfile.ReadUInt32());
                Assert.Equal(true, cfile.EOF);
            }

        }

        [Fact]
        public void TestReadDouble() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal("4.69197536052338E+271", cfile.ReadDouble().ToString(CultureInfo.InvariantCulture));
                Assert.Equal(-1.50730608775746E-189, cfile.ReadDouble(), 15);
                Assert.Equal(0, cfile.ReadDouble());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA}))) {
                Assert.Equal("4.69197536052338E+271", cfile.ReadDouble().ToString(CultureInfo.InvariantCulture));
                Assert.Equal(0, cfile.ReadDouble());
                Assert.Equal(0, cfile.ReadDouble());
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] {97,  0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal('a', cfile.ReadChar());
                Assert.Equal("4.69197536052338E+271", cfile.ReadDouble().ToString(CultureInfo.InvariantCulture));
                Assert.Equal(-1.50730608775746E-189, cfile.ReadDouble(), 15);
                Assert.Equal(0, cfile.ReadInt32());
                Assert.Equal(true, cfile.EOF);
            }
        }

        [Fact]
        public void TestReadInt32s() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                var vals = cfile.ReadInt32s(4);
                Assert.Equal(2, vals.Length);
                Assert.Equal((int)0x78563412, vals[0]);
                Assert.Equal(0x98BADCFE, (uint)vals[1]);
                Assert.Equal(null, cfile.ReadInt32s(4));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA }))) {
                var vals = cfile.ReadInt32s(4);
                Assert.Equal(1, vals.Length);
                Assert.Equal((int)0x78563412, vals[0]);
                Assert.Equal(null, cfile.ReadInt32s(4));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal('a', cfile.ReadChar());
                var vals = cfile.ReadInt32s(4);
                Assert.Equal(2, vals.Length);
                Assert.Equal((int)0x78563412, vals[0]);
                Assert.Equal(0x98BADCFE, (uint)vals[1]);
                Assert.Equal(null, cfile.ReadInt32s(4));
                Assert.Equal(true, cfile.EOF);
            }
        }

        [Fact]
        public void TestReadDoubles() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                var vals = cfile.ReadDoubles(4);
                Assert.Equal(2, vals.Length);
                Assert.Equal("4.69197536052338E+271", vals[0].ToString(CultureInfo.InvariantCulture));
                Assert.Equal(-1.50730608775746E-189, vals[1], 15);
                Assert.Equal(null, cfile.ReadDoubles(4));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA }))) {
                var vals = cfile.ReadDoubles(4);
                Assert.Equal(1, vals.Length);
                Assert.Equal("4.69197536052338E+271", vals[0].ToString(CultureInfo.InvariantCulture));
                Assert.Equal(null, cfile.ReadDoubles(4));
                Assert.Equal(true, cfile.EOF);
            }

            using (var cfile = new CFile(BuildStream(new byte[] { 97, 0x12, 0x34, 0x56, 0x78, 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal('a', cfile.ReadChar());
                var vals = cfile.ReadDoubles(4);
                Assert.Equal(2, vals.Length);
                Assert.Equal("4.69197536052338E+271", vals[0].ToString(CultureInfo.InvariantCulture));
                Assert.Equal(-1.50730608775746E-189, vals[1], 15);
                Assert.Equal(null, cfile.ReadDoubles(4));
                Assert.Equal(true, cfile.EOF);
            }
        }

        [Fact]
        public void TestSeek() {
            using (var cfile = new CFile(BuildStream(new byte[] { 0x12, 0x34, 0x56, 0x78, 0xFE, 0xDC, 0xBA, 0x98 }))) {
                Assert.Equal(0, cfile.Seek(4, SeekOrigin.Current));
                Assert.Equal(0x98BADCFE, (uint)cfile.ReadInt32());
                Assert.Equal(0, cfile.Seek(0, SeekOrigin.Begin));
                Assert.Equal((int)0x78563412, cfile.ReadInt32());
            }

            using (var cfile = new CFile(null)) {
                Assert.Equal(-1, cfile.Seek(4, SeekOrigin.Current));
            }

        }

    }
}
