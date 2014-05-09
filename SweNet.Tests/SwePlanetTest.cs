using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SwissEphNet;

namespace SweNet.Tests
{
    [TestClass]
    public class SwePlanetTest
    {

        [TestMethod]
        public void TestGetPlanetName() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("Sun", swe.Planets.GetPlanetName(Planet.Sun));
                Assert.AreEqual("Moon", swe.Planets.GetPlanetName(Planet.Moon));
                Assert.AreEqual("Mercury", swe.Planets.GetPlanetName(Planet.Mercury));
                Assert.AreEqual("Venus", swe.Planets.GetPlanetName(Planet.Venus));
                Assert.AreEqual("Earth", swe.Planets.GetPlanetName(Planet.Earth));
                Assert.AreEqual("Mars", swe.Planets.GetPlanetName(Planet.Mars));
                Assert.AreEqual("Jupiter", swe.Planets.GetPlanetName(Planet.Jupiter));
                Assert.AreEqual("Saturn", swe.Planets.GetPlanetName(Planet.Saturn));
                Assert.AreEqual("Uranus", swe.Planets.GetPlanetName(Planet.Uranus));
                Assert.AreEqual("Neptune", swe.Planets.GetPlanetName(Planet.Neptune));

                Assert.AreEqual("Pluto", swe.Planets.GetPlanetName(Planet.Pluto));
                Assert.AreEqual("mean Node", swe.Planets.GetPlanetName(Planet.MeanNode));
                Assert.AreEqual("true Node", swe.Planets.GetPlanetName(Planet.TrueNode));
                Assert.AreEqual("mean Apogee", swe.Planets.GetPlanetName(Planet.MeanApog));
                Assert.AreEqual("osc. Apogee", swe.Planets.GetPlanetName(Planet.OscuApog));
                Assert.AreEqual("Chiron", swe.Planets.GetPlanetName(Planet.Chiron));
                Assert.AreEqual("Pholus", swe.Planets.GetPlanetName(Planet.Pholus));
                Assert.AreEqual("Ceres", swe.Planets.GetPlanetName(Planet.Ceres));
                Assert.AreEqual("Pallas", swe.Planets.GetPlanetName(Planet.Pallas));
                Assert.AreEqual("Juno", swe.Planets.GetPlanetName(Planet.Juno));
                Assert.AreEqual("Vesta", swe.Planets.GetPlanetName(Planet.Vesta));
                Assert.AreEqual("intp. Apogee", swe.Planets.GetPlanetName(Planet.IntpApog));
                Assert.AreEqual("intp. Perigee", swe.Planets.GetPlanetName(Planet.IntpPerg));

                Assert.AreEqual("Pluto", swe.Planets.GetPlanetName(Planet.AsteroidPluto));
                Assert.AreEqual("Ceres", swe.Planets.GetPlanetName(Planet.AsteroidCeres));
                Assert.AreEqual("Pallas", swe.Planets.GetPlanetName(Planet.AsteroidPallas));
                Assert.AreEqual("Juno", swe.Planets.GetPlanetName(Planet.AsteroidJuno));
                Assert.AreEqual("Vesta", swe.Planets.GetPlanetName(Planet.AsteroidVesta));
                Assert.AreEqual("Chiron", swe.Planets.GetPlanetName(Planet.AsteroidChiron));
                Assert.AreEqual("Pholus", swe.Planets.GetPlanetName(Planet.AsteroidPholus));

                // Call twice the same planet for code coverage in swe_get_planet_name optimization planet name
                Assert.AreEqual("Pluto", swe.Planets.GetPlanetName(Planet.Pluto));
                Assert.AreEqual("Pluto", swe.Planets.GetPlanetName(Planet.Pluto));

                Assert.AreEqual("9988", swe.Planets.GetPlanetName(Planet.FirstAsteroid - 12));
            }
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestGetPlanetName_Fictitious() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("Cupido", swe.Planets.GetPlanetName(Planet.FirstFictitious));
                Assert.AreEqual("name not found", swe.Planets.GetPlanetName(Planet.FirstFictitious + 200));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("0: not found", swe.Planets.GetPlanetName(Planet.FirstAsteroid));
                Assert.AreEqual("433: not found", swe.Planets.GetPlanetName(Planet.FirstAsteroid + 433));
            }
        }

    }
}
