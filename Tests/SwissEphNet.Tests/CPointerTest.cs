using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public class CPointerTest
    {

        [TestMethod]
        public void TestCreate() {
            CPointer<int> target = new CPointer<int>();
            Assert.AreEqual(true, target.IsNull);
            Assert.AreEqual(0, target.Length);

            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            target = new CPointer<int>(array);
            Assert.AreEqual(false, target.IsNull);
            Assert.AreEqual(10, target.Length);

            target = new CPointer<int>(array, 5);
            Assert.AreEqual(false, target.IsNull);
            Assert.AreEqual(5, target.Length);
        }

        [TestMethod]
        public void TestGetItem() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(0, target[0]);

            target = new CPointer<int>(array, 5);
            Assert.AreEqual(5, target[0]);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void TestGetItemNullReferenceException() {
            CPointer<int> target = new CPointer<int>();
            Assert.AreEqual(0.0, target[0]);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetItemOutOfRange() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(0, target[100]);
        }

        [TestMethod]
        public void TestSetItem() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            target[0] = 100;
            target[2] = 102;
            target = new CPointer<int>(array, 5);
            target[0] = 200;
            target[2] = 202;
            CollectionAssert.AreEqual(new int[] { 100, 1, 102, 3, 4, 200, 6, 202, 8, 9 }, array);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void TestSetItemNullReferenceException() {
            CPointer<int> target = new CPointer<int>();
            target[0] = 12;
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSetItemOutOfRange() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            target[100] = 12;
        }

        [TestMethod]
        public void TestGetHashCode() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(array.GetHashCode() ^ 0, target.GetHashCode());

            target = new CPointer<int>(array, 5);
            Assert.AreEqual(array.GetHashCode() ^ 5, target.GetHashCode());

            target = new CPointer<int>();
            Assert.AreEqual(0, target.GetHashCode());

        }

        [TestMethod]
        public void TestEquals() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(false, target.Equals(null));
            Assert.AreEqual(false, target.Equals(5));
            Assert.AreEqual(true, target.Equals(array));
            Assert.AreEqual(true, target.Equals(new CPointer<int>(array)));
            Assert.AreEqual(false, target.Equals(new CPointer<int>(array, 5)));

        }

        [TestMethod]
        public void TestAdditionWithInt() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(0, target[0]);
            target = target + 2;
            Assert.AreEqual(2, target[0]);
        }

        [TestMethod]
        public void TestSubstractionWithInt() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array, 5);
            Assert.AreEqual(5, target[0]);
            target = target - 2;
            Assert.AreEqual(3, target[0]);
        }

        [TestMethod]
        public void TestIncrement() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(0, target[0]);
            target++;
            Assert.AreEqual(1, target[0]);
        }

        [TestMethod]
        public void TestDecrement() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array, 5);
            Assert.AreEqual(5, target[0]);
            target--;
            Assert.AreEqual(4, target[0]);
        }

        [TestMethod]
        public void TestImplicitCastToT() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            Assert.AreEqual(0, (int)target);
            target = new CPointer<int>(array, 5);
            Assert.AreEqual(5, (int)target);

        }

        [TestMethod]
        public void TestImplicitCastFromArray() {
            CPointer<int> target = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.AreEqual(0, target[0]);
            Assert.AreEqual(10, target.Length);
        }

        [TestMethod]
        public void TestExplicitCastToArray() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CPointer<int> target = new CPointer<int>(array);
            int[] array2 = (int[])target;
            CollectionAssert.AreEqual(array2, array);

            target = new CPointer<int>(array, 5);
            array2 = (int[])target;
            CollectionAssert.AreEqual(array2, new int[] { 5, 6, 7, 8, 9 });
        }

        [TestMethod]
        public void TestEqualityWithArray() {
            int[] array1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] array2 = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            CPointer<int> target = new CPointer<int>(array1);
            Assert.IsTrue(array1 == target);
            Assert.IsTrue(target == array1);
            Assert.IsFalse(array2 == target);
            Assert.IsFalse(target == array2);
            Assert.IsFalse(array1 != target);
            Assert.IsFalse(target != array1);
            Assert.IsTrue(array2 != target);
            Assert.IsTrue(target != array2);
        }

        [TestMethod]
        public void TestEquality() {
            int[] array1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] array2 = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            CPointer<int> target1 = new CPointer<int>(array1);
            CPointer<int> target2 = new CPointer<int>(array2);
            CPointer<int> target3 = new CPointer<int>(array1, 5);
            CPointer<int> target4 = new CPointer<int>(array2, 5);
            Assert.IsTrue(target1 == new CPointer<int>(array1));
            Assert.IsFalse(target1 != new CPointer<int>(array1));
            Assert.IsFalse(target2 == target1);
            Assert.IsTrue(target2 != target1);
            Assert.IsFalse(target3 == target1);
            Assert.IsTrue(target3 != target1);
            Assert.IsFalse(target4 == target1);
            Assert.IsTrue(target4 != target1);
        }

    }
}
