using System;
using Xunit;

namespace SwissEphNet.Tests
{

    public class StringExtensionsTest
    {
        [Fact]
        public void TestContainsChar() {
            String s = null;
            Assert.False(s.Contains('a'));

            Assert.False("".Contains('a'));
            Assert.False("AbCd".Contains('a'));
            Assert.True("AbCd".Contains('b'));
            Assert.False("AbCd".Contains('c'));
            Assert.True("AbCd".Contains('d'));
            Assert.False("AbCd".Contains('e'));
            Assert.True("AbCd".Contains('A'));
            Assert.False("AbCd".Contains('B'));
            Assert.True("AbCd".Contains('C'));
            Assert.False("AbCd".Contains('D'));
            Assert.False("AbCd".Contains('E'));
        }

        [Fact]
        public void TestContainsCharSet() {
            String s = null;
            Char[] charSet = new char[] { 'A', 'c' };

            Assert.False(s.Contains(charSet));
            Assert.False(s.Contains((Char[])null));
            Assert.False("--".Contains(charSet));
            Assert.False("--".Contains((Char[])null));

            Assert.False("".Contains(charSet));
            Assert.True("ABCD".Contains(charSet));
            Assert.True("abcd".Contains(charSet));
            Assert.False("xyz".Contains(charSet));

        }

        [Fact]
        public void TestIndexOfFirstNot() {
            String s = null;
            Char[] charSet = new char[] { 'A', 'c' };

            Assert.Equal(-1, s.IndexOfFirstNot(charSet));
            Assert.Equal(-1, s.IndexOfFirstNot());
            Assert.Equal(0, "--".IndexOfFirstNot(charSet));
            Assert.Equal(-1, "--".IndexOfFirstNot((Char[])null));

            Assert.Equal(-1, "".IndexOfFirstNot(charSet));
            Assert.Equal(1, "ABCD".IndexOfFirstNot(charSet));
            Assert.Equal(0, "abcd".IndexOfFirstNot(charSet));
            Assert.Equal(2, "AcEg".IndexOfFirstNot(charSet));
            Assert.Equal(4, "AcAcEg".IndexOfFirstNot(charSet));
            Assert.Equal(-1, "AcA".IndexOfFirstNot(charSet));
            Assert.Equal(0, "xyz".IndexOfFirstNot(charSet));

        }

    }
}
