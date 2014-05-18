using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    [TestClass]
    public class PlanetTest
    {
        [TestMethod]
        public void TestCreate() {
            var planet = new Planet();
            Assert.AreEqual(0, planet.Id);
            planet = new Planet(1234);
            Assert.AreEqual(1234, planet.Id);
        }

        [TestMethod]
        public void TestIntCast() {
            Planet planet = 1234;
            Assert.AreEqual(1234, planet.Id);

            int i = planet;
            Assert.AreEqual(1234, i);
        }

        [TestMethod]
        public void TestPlanetType() {
            Planet planet = 0;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(true, planet.IsPlanet);

            planet = -100;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 10;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(true, planet.IsPlanet);

            planet = 25;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 50;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(true, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 50;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(true, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 1000;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(true, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 2000;
            Assert.AreEqual(false, planet.IsAsteroid);
            Assert.AreEqual(true, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 10000;
            Assert.AreEqual(true, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);

            planet = 12000;
            Assert.AreEqual(true, planet.IsAsteroid);
            Assert.AreEqual(false, planet.IsComet);
            Assert.AreEqual(false, planet.IsFictitious);
            Assert.AreEqual(false, planet.IsPlanet);
        }

        [TestMethod]
        public void TestAsAsteroid() {
            Planet planet = Planet.AsAsteroid(12);
            Assert.AreEqual(Planet.FirstAsteroid + 12, planet.Id);
        }

        [TestMethod]
        public void TestToString() {
            Planet planet = Planet.Venus;
            Assert.AreEqual("3", planet.ToString());
        }

    }
}
