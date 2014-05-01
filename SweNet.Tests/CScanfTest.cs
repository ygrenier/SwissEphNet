using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwissEphNet;

namespace SweNet.Tests
{
    [TestClass]
    public class CScanfTest
    {

        [TestMethod]
        public void TestScanfChar() {
            char r1 = ' ';

            Assert.AreEqual(0, C.sscanf("", "%c", ref r1));
            Assert.AreEqual(' ', r1);

            Assert.AreEqual(0, C.sscanf("1", "%2c", ref r1));
            Assert.AreEqual(' ', r1);

            Assert.AreEqual(1, C.sscanf("123", "%c", ref r1));
            Assert.AreEqual('1', r1);

            char[] r2 = null;
            Assert.AreEqual(1, C.sscanf("abc", "%2c", ref r2));
            CollectionAssert.AreEqual(new char[] { 'a', 'b' }, r2);

        }

        [TestMethod]
        public void TestScanfByte() {
            sbyte r1 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%hhd", ref r1));
            Assert.AreEqual(123, r1);

            byte r2 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%hhu", ref r2));
            Assert.AreEqual(123, r2);
        }

        [TestMethod]
        public void TestScanfInt16() {
            Int16 r1 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%hd", ref r1));
            Assert.AreEqual(123, r1);

            UInt16 r2 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%hu", ref r2));
            Assert.AreEqual(123, r2);
        }

        [TestMethod]
        public void TestScanfInt32() {
            int r1 = 0;

            Assert.AreEqual(0, C.sscanf("", "%d", ref r1));
            Assert.AreEqual(0, r1);

            Assert.AreEqual(0, C.sscanf("[ ", "[%d", ref r1));
            Assert.AreEqual(0, r1);

            Assert.AreEqual(1, C.sscanf("  123 ", "%d", ref r1));
            Assert.AreEqual(123, r1);

            Assert.AreEqual(1, C.sscanf("  321 ", "%d %f", ref r1));
            Assert.AreEqual(321, r1);

            Assert.AreEqual(1, C.sscanf("+123 ", "%d", ref r1));
            Assert.AreEqual(123, r1);

            Assert.AreEqual(1, C.sscanf("-123 ", "%d", ref r1));
            Assert.AreEqual(-123, r1);

            Assert.AreEqual(1, C.sscanf("040", "%d", ref r1));
            Assert.AreEqual(40, r1);

            Assert.AreEqual(1, C.sscanf("0x40", "%d", ref r1));
            Assert.AreEqual(0, r1);

            Assert.AreEqual(1, C.sscanf("123 ", "%2d", ref r1));
            Assert.AreEqual(12, r1);

            Assert.AreEqual(1, C.sscanf("123", "%5d", ref r1));
            Assert.AreEqual(123, r1);

        }

        [TestMethod]
        public void TestScanfUInt32() {
            uint r1 = 0;

            Assert.AreEqual(0, C.sscanf("", "%u", ref r1));
            Assert.AreEqual((uint)0, r1);

            Assert.AreEqual(1, C.sscanf("  123 ", "%u", ref r1));
            Assert.AreEqual((uint)123, r1);

            Assert.AreEqual(1, C.sscanf("+123 ", "%u", ref r1));
            Assert.AreEqual((uint)123, r1);

            Assert.AreEqual(1, C.sscanf("040", "%u", ref r1));
            Assert.AreEqual((uint)40, r1);

            Assert.AreEqual(1, C.sscanf("0x40", "%u", ref r1));
            Assert.AreEqual((uint)0, r1);

            Assert.AreEqual(1, C.sscanf("123 ", "%2u", ref r1));
            Assert.AreEqual((uint)12, r1);

            Assert.AreEqual(1, C.sscanf("123", "%4u", ref r1));
            Assert.AreEqual((uint)123, r1);

        }

        [TestMethod]
        public void TestScanfInt64() {
            Int64 r1 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%ld", ref r1));
            Assert.AreEqual(123, r1);

            Assert.AreEqual(1, C.sscanf("123", "%lld", ref r1));
            Assert.AreEqual(123, r1);
        }

        [TestMethod]
        public void TestScanfUInt64() {
            UInt64 r1 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%lu", ref r1));
            Assert.AreEqual((UInt64)123, r1);

            Assert.AreEqual(1, C.sscanf("123", "%llu", ref r1));
            Assert.AreEqual((UInt64)123, r1);
        }

        [TestMethod]
        public void TestScanfFloat() {
            Single r1 = 0; Double r2 = 0;

            Assert.AreEqual(1, C.sscanf("123", "%f", ref r1));
            Assert.AreEqual(123.0, r1);
            Assert.AreEqual(1, C.sscanf("123.456", "%f", ref r1));
            Assert.AreEqual((Single)123.456, r1);

            Assert.AreEqual(2, C.sscanf("123.456-9876.543", "%f-%lf", ref r1, ref r2));
            Assert.AreEqual((Single)123.456, r1);
            Assert.AreEqual(9876.543, r2);

            r1 = 0; 
            Assert.AreEqual(0, C.sscanf("", "%f", ref r1));
            Assert.AreEqual((Single)0, r1);

            Assert.AreEqual(0, C.sscanf("[ ", "[%f", ref r1));
            Assert.AreEqual((Single)0, r1);

            Assert.AreEqual(1, C.sscanf("  123 ", "%f", ref r1));
            Assert.AreEqual((Single)123, r1);

            Assert.AreEqual(1, C.sscanf("+123 ", "%f", ref r1));
            Assert.AreEqual((Single)123, r1);

            Assert.AreEqual(1, C.sscanf("-123 ", "%f", ref r1));
            Assert.AreEqual((Single)(-123), r1);

            Assert.AreEqual(1, C.sscanf("040", "%f", ref r1));
            Assert.AreEqual((Single)40, r1);

            Assert.AreEqual(1, C.sscanf("0x40", "%f", ref r1));
            Assert.AreEqual((Single)0, r1);

            Assert.AreEqual(1, C.sscanf("123 ", "%2f", ref r1));
            Assert.AreEqual((Single)12, r1);

            Assert.AreEqual(1, C.sscanf("123", "%5f", ref r1));
            Assert.AreEqual((Single)123, r1);

            Assert.AreEqual(1, C.sscanf("12.34.56", "%f", ref r1));
            Assert.AreEqual((Single)12.34, r1);

            Assert.AreEqual(1, C.sscanf("12.34e2", "%f", ref r1));
            Assert.AreEqual((Single)1234, r1);

            Assert.AreEqual(1, C.sscanf("12.34e+2", "%f", ref r1));
            Assert.AreEqual((Single)1234, r1);

            Assert.AreEqual(1, C.sscanf("12.34e-2", "%f", ref r1));
            Assert.AreEqual((Single)0.1234, r1);
        }

        [TestMethod]
        public void TestScanfOctal() {
            uint r1 = 0;

            Assert.AreEqual(0, C.sscanf(" ", "%o", ref r1));
            Assert.AreEqual((uint)0, r1);

            Assert.AreEqual(1, C.sscanf("123", "%o", ref r1));
            Assert.AreEqual((uint)83, r1);

            Assert.AreEqual(1, C.sscanf("545", "%2o", ref r1));
            Assert.AreEqual((uint)44, r1);

            Assert.AreEqual(1, C.sscanf("545", "%4o", ref r1));
            Assert.AreEqual((uint)357, r1);

        }

        [TestMethod]
        public void TestScanfHexa() {
            uint r1 = 0;

            Assert.AreEqual(0, C.sscanf(" ", "%x", ref r1));
            Assert.AreEqual((uint)0, r1);

            Assert.AreEqual(1, C.sscanf("123", "%x", ref r1));
            Assert.AreEqual((uint)291, r1);

            Assert.AreEqual(1, C.sscanf("0x123", "%x", ref r1));
            Assert.AreEqual((uint)291, r1);

            Assert.AreEqual(1, C.sscanf("123", "%2x", ref r1));
            Assert.AreEqual((uint)18, r1);

            Assert.AreEqual(1, C.sscanf("123", "%4x", ref r1));
            Assert.AreEqual((uint)291, r1);

        }

        [TestMethod]
        public void TestScanfString() {
            String r1 = null;

            Assert.AreEqual(0, C.sscanf(" ", "%s", ref r1));
            Assert.AreEqual(null, r1);

            Assert.AreEqual(1, C.sscanf("abc", "%s", ref r1));
            Assert.AreEqual("abc", r1);

            Assert.AreEqual(1, C.sscanf("abc", "%2s", ref r1));
            Assert.AreEqual("ab", r1);

            Assert.AreEqual(1, C.sscanf("abc", "%4s", ref r1));
            Assert.AreEqual("abc", r1);

        }

        [TestMethod]
        public void TestScanfChars() {
            int r1 = 0;

            Assert.AreEqual(0, C.sscanf("123", "[%d]", ref r1));
            Assert.AreEqual(0, r1);

            Assert.AreEqual(1, C.sscanf("[123]", "[%d]", ref r1));
            Assert.AreEqual(123, r1);

        }

        [TestMethod]
        public void TestScanfWidth() {
            int r1 = 0;

            Assert.AreEqual(1, C.sscanf("[123]", "[%2d]", ref r1));
            Assert.AreEqual(12, r1);

        }

        [TestMethod]
        public void TestScanfScanSet() {
            Int32 r1 = 0, r2 = 0;
            String r3 = null, r4 = null;

            Assert.AreEqual(4, C.sscanf("Copyright 2009-2011 CompanyName (Multi-Word message)", "Copyright %d-%d %s (%[^)]", ref r1, ref r2, ref r3, ref r4));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(2011, r2);
            Assert.AreEqual("CompanyName", r3);
            Assert.AreEqual("Multi-Word message", r4);

            r3 = null;

            Assert.AreEqual(0, C.sscanf("( ", "(%[itluM]", ref r3));
            Assert.AreEqual(null, r3);

            Assert.AreEqual(1, C.sscanf("(Multi-Word message)", "(%[itluM]", ref r3));
            Assert.AreEqual("Multi", r3);

            Assert.AreEqual(1, C.sscanf("(Multi-Word message)", "(%2[itluM]", ref r3));
            Assert.AreEqual("Mu", r3);

            Assert.AreEqual(1, C.sscanf("(Multi-Word message)", "(%8[itluM]", ref r3));
            Assert.AreEqual("Multi", r3);

            r3 = null;
            Assert.AreEqual(1, C.sscanf("[Multi-Word] message", "%[^]]", ref r3));
            Assert.AreEqual("[Multi-Word", r3);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestScanfScanSetInvalidCloseBracket() {
            String r3 = null;
            Assert.AreEqual(0, C.sscanf("([Multi-Word] message)", "(%[]", ref r3));
        }

        [TestMethod]
        public void TestScanfPercent() {
            int r1 = 0;

            Assert.AreEqual(1, C.sscanf("%123%", "%%%d%%", ref r1));
            Assert.AreEqual(123, r1);

        }

        [TestMethod]
        public void TestScanfNoStored() {
            int r1 = 0, r2 = 0;

            Assert.AreEqual(1, C.sscanf("123-456", "%*d-%d", ref r1, ref r2));
            Assert.AreEqual(456, r1);
            Assert.AreEqual(0, r2);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestScanfScanSetUnknownTypeSpecifier() {
            String r3 = null;
            Assert.AreEqual(0, C.sscanf("1234", "%z", ref r3));
        }

        [TestMethod]
        public void TestScanf() {
            Int32 r1 = 0, r2 = 0;
            String r3 = null, r4 = null;
            Int32 r5 = 0, r6 = 0;

            String test = "Copyright 2009-2011 CompanyName (Multi-Word message) - 123 - 987";

            r1 = r2 = 0; r3 = r4 = null;
            Assert.AreEqual(1, C.sscanf(test, "Copyright %d", ref r1));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(0, r2);
            Assert.AreEqual(null, r3);
            Assert.AreEqual(null, r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.AreEqual(2, C.sscanf(test, "Copyright %d-%d", ref r1, ref r2));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(2011, r2);
            Assert.AreEqual(null, r3);
            Assert.AreEqual(null, r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.AreEqual(3, C.sscanf(test, "Copyright %d-%d %s", ref r1, ref r2, ref r3));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(2011, r2);
            Assert.AreEqual("CompanyName", r3);
            Assert.AreEqual(null, r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.AreEqual(4, C.sscanf(test, "Copyright %d-%d %s (%[^)]", ref r1, ref r2, ref r3, ref r4));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(2011, r2);
            Assert.AreEqual("CompanyName", r3);
            Assert.AreEqual("Multi-Word message", r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.AreEqual(5, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d", ref r1, ref r2, ref r3, ref r4, ref r5));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(2011, r2);
            Assert.AreEqual("CompanyName", r3);
            Assert.AreEqual("Multi-Word message", r4);
            Assert.AreEqual(123, r5);

            r1 = r2 = r5 = 0; r3 = r4 = null;
            Assert.AreEqual(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d", ref r1, ref r2, ref r3, ref r4, ref r5, ref r6));
            Assert.AreEqual(2009, r1);
            Assert.AreEqual(2011, r2);
            Assert.AreEqual("CompanyName", r3);
            Assert.AreEqual("Multi-Word message", r4);
            Assert.AreEqual(123, r5);
            Assert.AreEqual(987, r6);

        }

    }
}
