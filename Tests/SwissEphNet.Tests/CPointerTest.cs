using System;
using Xunit;

namespace SwissEphNet.Tests
{

    public class CPointerTest
    {

        [Fact]
        public void TestCreate() {
            CPointer<int> target = new CPointer<int>();
            Assert.True(target.IsNull);
            Assert.Equal(0, target.Length);

            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            target = new CPointer<int>(array);
            Assert.False(target.IsNull);
            Assert.Equal(10, target.Length);

            target = new CPointer<int>(array, 5);
            Assert.False(target.IsNull);
            Assert.Equal(5, target.Length);
        }

        [Fact]
        public void TestGetItem() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Equal(0, target[0]);

            target = new CPointer<int>(array, 5);
            Assert.Equal(5, target[0]);
        }

        [Fact]
        public void TestGetItemNullReferenceException() {
            CPointer<int> target = new CPointer<int>();
            Assert.Throws<NullReferenceException>(() => target[0]);
        }

        [Fact]
        public void TestGetItemOutOfRange() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Throws<IndexOutOfRangeException>(() => target[100]);
        }

        [Fact]
        public void TestSetItem() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            target[0] = 100;
            target[2] = 102;
            target = new CPointer<int>(array, 5);
            target[0] = 200;
            target[2] = 202;
            Assert.Equal(new int[] { 100, 1, 102, 3, 4, 200, 6, 202, 8, 9 }, array);
        }

        [Fact]
        public void TestSetItemNullReferenceException() {
            CPointer<int> target = new CPointer<int>();
            Assert.Throws<NullReferenceException>(() => target[0] = 12);
        }

        [Fact]
        public void TestSetItemOutOfRange() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Throws<IndexOutOfRangeException>(() => target[100] = 12);
        }

        [Fact]
        public void TestGetHashCode() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Equal(array.GetHashCode() ^ 0, target.GetHashCode());

            target = new CPointer<int>(array, 5);
            Assert.Equal(array.GetHashCode() ^ 5, target.GetHashCode());

            target = new CPointer<int>();
            Assert.Equal(0, target.GetHashCode());

        }

        [Fact]
        public void TestEquals() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.False(target.Equals(null));
            Assert.False(target.Equals(5));
            Assert.True(target.Equals(array));
            Assert.True(target.Equals(new CPointer<int>(array)));
            Assert.False(target.Equals(new CPointer<int>(array, 5)));

        }

        [Fact]
        public void TestAdditionWithInt() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Equal(0, target[0]);
            target = target + 2;
            Assert.Equal(2, target[0]);
        }

        [Fact]
        public void TestSubstractionWithInt() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array, 5);
            Assert.Equal(5, target[0]);
            target = target - 2;
            Assert.Equal(3, target[0]);
        }

        [Fact]
        public void TestIncrement() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Equal(0, target[0]);
            target++;
            Assert.Equal(1, target[0]);
        }

        [Fact]
        public void TestDecrement() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array, 5);
            Assert.Equal(5, target[0]);
            target--;
            Assert.Equal(4, target[0]);
        }

        [Fact]
        public void TestImplicitCastToT() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.Equal(0, (int)target);
            target = new CPointer<int>(array, 5);
            Assert.Equal(5, (int)target);

        }

        [Fact]
        public void TestImplicitCastFromArray() {
            CPointer<int> target = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.Equal(0, target[0]);
            Assert.Equal(10, target.Length);
        }

        [Fact]
        public void TestExplicitCastToArray() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            int[] array2 = (int[])target;
            Assert.Equal(array2, array);

            target = new CPointer<int>(array, 5);
            array2 = (int[])target;
            Assert.Equal(array2, new int[] { 5, 6, 7, 8, 9 });
        }

        [Fact]
        public void TestEqualityWithArray() {
            int[] array1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] array2 = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            CPointer<int> target = new CPointer<int>(array1);
            Assert.True(array1 == target);
            Assert.True(target == array1);
            Assert.False(array2 == target);
            Assert.False(target == array2);
            Assert.False(array1 != target);
            Assert.False(target != array1);
            Assert.True(array2 != target);
            Assert.True(target != array2);
        }

        [Fact]
        public void TestEquality() {
            int[] array1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] array2 = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            CPointer<int> target1 = new CPointer<int>(array1);
            CPointer<int> target2 = new CPointer<int>(array2);
            CPointer<int> target3 = new CPointer<int>(array1, 5);
            CPointer<int> target4 = new CPointer<int>(array2, 5);
            Assert.True(target1 == new CPointer<int>(array1));
            Assert.False(target1 != new CPointer<int>(array1));
            Assert.False(target2 == target1);
            Assert.True(target2 != target1);
            Assert.False(target3 == target1);
            Assert.True(target3 != target1);
            Assert.False(target4 == target1);
            Assert.True(target4 != target1);
        }

    }
}
