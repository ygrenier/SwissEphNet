using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    /// <summary>
    /// Description résumée pour DependencyTest
    /// </summary>
    [TestClass]
    public class DependencyTest
    {
        class Type1 : IDisposable
        {
            public Type1()
            {
                IsDisposed = false;
            }
            public int Value { get; set; }
            public bool IsDisposed { get; private set; }
            void IDisposable.Dispose()
            {
                IsDisposed = true;
            }
        }
        class Type2 : IDisposable
        {
            public Type2(Type1 value)
            {
                this.Value = value;
            }
            void IDisposable.Dispose()
            {
                ((IDisposable)Value).Dispose();
            }
            public Type1 Value { get; private set; }
        }
        class Type3
        {
            private Type3() { }
        }
        class Type4
        {
            public Type4(Type1 t = null) { }
        }

        [TestMethod]
        public void TestGetDependencies()
        {
            using (var swe = new Sweph())
            {
                Assert.IsNotNull(swe.Dependencies);
            }
        }

        [TestMethod]
        public void TestCanResolve()
        {
            using (var swe = new Sweph())
            {
                Assert.IsFalse(swe.Dependencies.CanResolve(null));
                Assert.IsFalse(DependencyExtensions.CanResolve<Type1>(null));
                Assert.IsFalse(swe.Dependencies.CanResolve(typeof(Type1)));
                Assert.IsFalse(swe.Dependencies.CanResolve<Type1>());
                swe.Dependencies.Register<Type1>(cnt => new Type1 { Value = 123 }, true);
                Assert.IsFalse(swe.Dependencies.CanResolve(null));
                Assert.IsFalse(DependencyExtensions.CanResolve<Type1>(null));
                Assert.IsTrue(swe.Dependencies.CanResolve(typeof(Type1)));
                Assert.IsTrue(swe.Dependencies.CanResolve<Type1>());
            }
        }

        [TestMethod]
        public void TestResolve()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Register<Type1>(cnt => new Type1 { Value = 123 }, true);
                var t = swe.Dependencies.Resolve<Type1>();
                Assert.AreEqual(123, t.Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestResolve_FailedGenericType()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Resolve<Type1>();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestResolve_FailedType()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Resolve(typeof(Type1));
            }
        }

        [TestMethod]
        public void TestCreateBasicType()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Register<Type1>(cnt => new Type1 { Value = 123 });
                var t = swe.Dependencies.Create<Type1>();
                Assert.AreEqual(123, t.Value);
                Assert.AreEqual(false, t.IsDisposed);
            }
        }

        [TestMethod]
        public void TestCreateFromResolver()
        {
            using (var swe = new Sweph())
            {
                var t = swe.Dependencies.Create<Type1>();
                Assert.AreEqual(0, t.Value);
                Assert.AreEqual(false, t.IsDisposed);
            }
        }

        [TestMethod]
        public void TestCreateValueType()
        {
            using (var swe = new Sweph())
            {
                var t = swe.Dependencies.Create<IDisposable>();
                Assert.IsNull(t);
            }
        }

        [TestMethod]
        public void TestCreateTypeWithoutConstructor()
        {
            using (var swe = new Sweph())
            {
                var t = swe.Dependencies.Create<Type3>();
                Assert.IsNull(t);
            }
        }

        [TestMethod]
        public void TestCreateTypeWithInjection()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Register<Type1, Type1>();
                var t = swe.Dependencies.Create<Type2>();
                Assert.IsNotNull(t);
                Assert.AreSame(t.Value, swe.Dependencies.Resolve<Type1>());
            }
            // Test create a type constructor with parameter can't be resolved
            using (var swe = new Sweph())
            {
                var t = swe.Dependencies.Create<Type2>();
                Assert.IsNull(t);
            }
            // Test create a type constructor with optionnal parameter 
            using (var swe = new Sweph())
            {
                var t = swe.Dependencies.Create<Type4>();
                Assert.IsNull(t);
            }
        }

        [TestMethod]
        public void TestTryToResolve()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Register<Type1>(cnt => new Type1 { Value = 123 }, true);
                Type1 t1 = null;
                Assert.IsTrue(swe.Dependencies.TryToResolve(out t1));
                Assert.AreEqual(123, t1.Value);
                Type2 t2 = null;
                Assert.IsFalse(swe.Dependencies.TryToResolve(out t2));
                Assert.IsNotNull(t2);
            }
        }

        [TestMethod]
        public void TestRegisterByCreatorAsSingleton()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Register<Type1>(cnt => new Type1 { Value = 123 }, true);
                var t1 = swe.Dependencies.Resolve<Type1>();
                Assert.IsNotNull(t1);
                var t2 = swe.Dependencies.Resolve<Type1>();
                Assert.AreSame(t1, t2);
            }
        }

        [TestMethod]
        public void TestRegisterByCreatorAsNotSingleton()
        {
            using (var swe = new Sweph())
            {
                swe.Dependencies.Register<Type1>(cnt => new Type1 { Value = 123 }, false);
                var t1 = swe.Dependencies.Resolve<Type1>();
                Assert.IsNotNull(t1);
                var t2 = swe.Dependencies.Resolve<Type1>();
                Assert.AreNotSame(t1, t2);
            }
        }

        [TestMethod]
        public void TestRegisterInstance()
        {
            using (var swe = new Sweph())
            {
                var t1 = new Type1() {
                    Value = 9876
                };
                swe.Dependencies.RegisterInstance(t1);
                var t2 = swe.Dependencies.Resolve<Type1>();
                Assert.AreSame(t1, t2);
            }
        }

    }
}
