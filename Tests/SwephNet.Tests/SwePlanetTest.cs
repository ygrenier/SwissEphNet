using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SwephNet.Tests
{
    [TestClass]
    public class SwePlanetTest
    {
        System.Globalization.CultureInfo _SaveCulture, _SaveUICulture;

        [TestInitialize]
        public void Init()
        {
            _SaveCulture = System.Globalization.CultureInfo.DefaultThreadCurrentCulture;
            _SaveUICulture = System.Globalization.CultureInfo.DefaultThreadCurrentUICulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
        }

        [TestCleanup]
        public void Restore()
        {
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = _SaveCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = _SaveUICulture;
        }

        [TestMethod]
        public void TestGetPlanetName() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("Sun", swe.Planet.GetPlanetName(Planet.Sun));
                Assert.AreEqual("Moon", swe.Planet.GetPlanetName(Planet.Moon));
                Assert.AreEqual("Mercury", swe.Planet.GetPlanetName(Planet.Mercury));
                Assert.AreEqual("Venus", swe.Planet.GetPlanetName(Planet.Venus));
                Assert.AreEqual("Earth", swe.Planet.GetPlanetName(Planet.Earth));
                Assert.AreEqual("Mars", swe.Planet.GetPlanetName(Planet.Mars));
                Assert.AreEqual("Jupiter", swe.Planet.GetPlanetName(Planet.Jupiter));
                Assert.AreEqual("Saturn", swe.Planet.GetPlanetName(Planet.Saturn));
                Assert.AreEqual("Uranus", swe.Planet.GetPlanetName(Planet.Uranus));
                Assert.AreEqual("Neptune", swe.Planet.GetPlanetName(Planet.Neptune));

                Assert.AreEqual("Pluto", swe.Planet.GetPlanetName(Planet.Pluto));
                Assert.AreEqual("mean Node", swe.Planet.GetPlanetName(Planet.MeanNode));
                Assert.AreEqual("true Node", swe.Planet.GetPlanetName(Planet.TrueNode));
                Assert.AreEqual("mean Apogee", swe.Planet.GetPlanetName(Planet.MeanApog));
                Assert.AreEqual("osc. Apogee", swe.Planet.GetPlanetName(Planet.OscuApog));
                Assert.AreEqual("Chiron", swe.Planet.GetPlanetName(Planet.Chiron));
                Assert.AreEqual("Pholus", swe.Planet.GetPlanetName(Planet.Pholus));
                Assert.AreEqual("Ceres", swe.Planet.GetPlanetName(Planet.Ceres));
                Assert.AreEqual("Pallas", swe.Planet.GetPlanetName(Planet.Pallas));
                Assert.AreEqual("Juno", swe.Planet.GetPlanetName(Planet.Juno));
                Assert.AreEqual("Vesta", swe.Planet.GetPlanetName(Planet.Vesta));
                Assert.AreEqual("intp. Apogee", swe.Planet.GetPlanetName(Planet.IntpApog));
                Assert.AreEqual("intp. Perigee", swe.Planet.GetPlanetName(Planet.IntpPerg));

                Assert.AreEqual("Pluto", swe.Planet.GetPlanetName(Planet.AsteroidPluto));
                Assert.AreEqual("Ceres", swe.Planet.GetPlanetName(Planet.AsteroidCeres));
                Assert.AreEqual("Pallas", swe.Planet.GetPlanetName(Planet.AsteroidPallas));
                Assert.AreEqual("Juno", swe.Planet.GetPlanetName(Planet.AsteroidJuno));
                Assert.AreEqual("Vesta", swe.Planet.GetPlanetName(Planet.AsteroidVesta));
                Assert.AreEqual("Chiron", swe.Planet.GetPlanetName(Planet.AsteroidChiron));
                Assert.AreEqual("Pholus", swe.Planet.GetPlanetName(Planet.AsteroidPholus));

                // Call twice the same planet for code coverage in GetPlanetName optimization planet name
                Assert.AreEqual("Pluto", swe.Planet.GetPlanetName(Planet.Pluto));
                Assert.AreEqual("Pluto", swe.Planet.GetPlanetName(Planet.Pluto));

                Assert.AreEqual("9988", swe.Planet.GetPlanetName(Planet.FirstAsteroid - 12));
            }
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestGetPlanetName_Fictitious() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("Cupido", swe.Planet.GetPlanetName(Planet.FirstFictitious));
                Assert.AreEqual("name not found", swe.Planet.GetPlanetName(Planet.FirstFictitious + 200));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("0: not found", swe.Planet.GetPlanetName(Planet.FirstAsteroid));
                Assert.AreEqual("433: not found", swe.Planet.GetPlanetName(Planet.FirstAsteroid + 433));
            }
        }

    }
}
