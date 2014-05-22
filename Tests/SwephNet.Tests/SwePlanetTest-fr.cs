using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SwephNet.Tests
{
    [TestClass]
    public class SwePlanetTest_fr
    {
        System.Globalization.CultureInfo _SaveCulture, _SaveUICulture;

        [TestInitialize]
        public void Init()
        {
            _SaveCulture = System.Globalization.CultureInfo.DefaultThreadCurrentCulture;
            _SaveUICulture = System.Globalization.CultureInfo.DefaultThreadCurrentUICulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("fr");
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("fr");
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
                Assert.AreEqual("Soleil", swe.Planet.GetPlanetName(Planet.Sun));
                Assert.AreEqual("Lune", swe.Planet.GetPlanetName(Planet.Moon));
                Assert.AreEqual("Mercure", swe.Planet.GetPlanetName(Planet.Mercury));
                Assert.AreEqual("Vénus", swe.Planet.GetPlanetName(Planet.Venus));
                Assert.AreEqual("Terre", swe.Planet.GetPlanetName(Planet.Earth));
                Assert.AreEqual("Mars", swe.Planet.GetPlanetName(Planet.Mars));
                Assert.AreEqual("Jupiter", swe.Planet.GetPlanetName(Planet.Jupiter));
                Assert.AreEqual("Saturne", swe.Planet.GetPlanetName(Planet.Saturn));
                Assert.AreEqual("Uranus", swe.Planet.GetPlanetName(Planet.Uranus));
                Assert.AreEqual("Neptune", swe.Planet.GetPlanetName(Planet.Neptune));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Fictitious() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("Cupido", swe.Planet.GetPlanetName(Planet.FirstFictitious));
                Assert.AreEqual("Nom inconnu", swe.Planet.GetPlanetName(Planet.FirstFictitious + 200));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("0: nom inconnu", swe.Planet.GetPlanetName(Planet.FirstAsteroid));
                Assert.AreEqual("433: nom inconnu", swe.Planet.GetPlanetName(Planet.FirstAsteroid + 433));
            }
        }

    }
}
