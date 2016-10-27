using System;
using Xunit;

namespace SwissEphNet.Tests
{

    public class ArrayExtensionsTest
    {
        [Fact]
        public void TestGetPointer() {
            var array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var target = array.GetPointer();
            Assert.Equal(10, target.Length);
            Assert.Equal(0, target[0]);

            target = array.GetPointer(5);
            Assert.Equal(5, target.Length);
            Assert.Equal(5, target[0]);

        }
    }
}
