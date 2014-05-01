using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SweNet.Tests
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void TestContainsChar() {
            String s = null;
            Assert.IsFalse(s.Contains('a'));

            Assert.IsFalse("".Contains('a'));
            Assert.IsFalse("AbCd".Contains('a'));
            Assert.IsTrue("AbCd".Contains('b'));
            Assert.IsFalse("AbCd".Contains('c'));
            Assert.IsTrue("AbCd".Contains('d'));
            Assert.IsFalse("AbCd".Contains('e'));
            Assert.IsTrue("AbCd".Contains('A'));
            Assert.IsFalse("AbCd".Contains('B'));
            Assert.IsTrue("AbCd".Contains('C'));
            Assert.IsFalse("AbCd".Contains('D'));
            Assert.IsFalse("AbCd".Contains('E'));
        }

        [TestMethod]
        public void TestContainsCharSet() {
            String s = null;
            Char[] charSet = new char[] { 'A', 'c' };

            Assert.IsFalse(s.Contains(charSet));
            Assert.IsFalse(s.Contains((Char[])null));
            Assert.IsFalse("--".Contains(charSet));
            Assert.IsFalse("--".Contains((Char[])null));

            Assert.IsFalse("".Contains(charSet));
            Assert.IsTrue("ABCD".Contains(charSet));
            Assert.IsTrue("abcd".Contains(charSet));
            Assert.IsFalse("xyz".Contains(charSet));

        }

        [TestMethod]
        public void TestIndexOfFirstNot() {
            String s = null;
            Char[] charSet = new char[] { 'A', 'c' };

            Assert.AreEqual(-1, s.IndexOfFirstNot(charSet));
            Assert.AreEqual(-1, s.IndexOfFirstNot());
            Assert.AreEqual(0, "--".IndexOfFirstNot(charSet));
            Assert.AreEqual(-1, "--".IndexOfFirstNot((Char[])null));

            Assert.AreEqual(-1, "".IndexOfFirstNot(charSet));
            Assert.AreEqual(1, "ABCD".IndexOfFirstNot(charSet));
            Assert.AreEqual(0, "abcd".IndexOfFirstNot(charSet));
            Assert.AreEqual(2, "AcEg".IndexOfFirstNot(charSet));
            Assert.AreEqual(4, "AcAcEg".IndexOfFirstNot(charSet));
            Assert.AreEqual(-1, "AcA".IndexOfFirstNot(charSet));
            Assert.AreEqual(0, "xyz".IndexOfFirstNot(charSet));

        }

    }
}
