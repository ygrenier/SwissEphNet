using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SwissEphNet;

namespace SweNet.Tests
{
    partial class SwissEphTest
    {

        [TestMethod]
        public void Test_swe_get_planet_name() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual("Sun", swe.swe_get_planet_name(SwissEph.SE_SUN));
                Assert.AreEqual("Moon", swe.swe_get_planet_name(SwissEph.SE_MOON));
                Assert.AreEqual("Mercury", swe.swe_get_planet_name(SwissEph.SE_MERCURY));
                Assert.AreEqual("Venus", swe.swe_get_planet_name(SwissEph.SE_VENUS));
                Assert.AreEqual("Earth", swe.swe_get_planet_name(SwissEph.SE_EARTH));
                Assert.AreEqual("Mars", swe.swe_get_planet_name(SwissEph.SE_MARS));
                Assert.AreEqual("Jupiter", swe.swe_get_planet_name(SwissEph.SE_JUPITER));
                Assert.AreEqual("Saturn", swe.swe_get_planet_name(SwissEph.SE_SATURN));
                Assert.AreEqual("Uranus", swe.swe_get_planet_name(SwissEph.SE_URANUS));
                Assert.AreEqual("Neptune", swe.swe_get_planet_name(SwissEph.SE_NEPTUNE));

                Assert.AreEqual("Pluto", swe.swe_get_planet_name(SwissEph.SE_PLUTO));
                Assert.AreEqual("mean Node", swe.swe_get_planet_name(SwissEph.SE_MEAN_NODE));
                Assert.AreEqual("true Node", swe.swe_get_planet_name(SwissEph.SE_TRUE_NODE));
                Assert.AreEqual("mean Apogee", swe.swe_get_planet_name(SwissEph.SE_MEAN_APOG));
                Assert.AreEqual("osc. Apogee", swe.swe_get_planet_name(SwissEph.SE_OSCU_APOG));
                Assert.AreEqual("Chiron", swe.swe_get_planet_name(SwissEph.SE_CHIRON));
                Assert.AreEqual("Pholus", swe.swe_get_planet_name(SwissEph.SE_PHOLUS));
                Assert.AreEqual("Ceres", swe.swe_get_planet_name(SwissEph.SE_CERES));
                Assert.AreEqual("Pallas", swe.swe_get_planet_name(SwissEph.SE_PALLAS));
                Assert.AreEqual("Juno", swe.swe_get_planet_name(SwissEph.SE_JUNO));
                Assert.AreEqual("Vesta", swe.swe_get_planet_name(SwissEph.SE_VESTA));
                Assert.AreEqual("intp. Apogee", swe.swe_get_planet_name(SwissEph.SE_INTP_APOG));
                Assert.AreEqual("intp. Perigee", swe.swe_get_planet_name(SwissEph.SE_INTP_PERG));

                Assert.AreEqual("Pluto", swe.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 134340));

                // Call twice the same planet for code coverage in swe_get_planet_name optimization planet name
                Assert.AreEqual("Pluto", swe.swe_get_planet_name(SwissEph.SE_PLUTO));
                Assert.AreEqual("Pluto", swe.swe_get_planet_name(SwissEph.SE_PLUTO));
            }
        }

        [TestMethod]
        public void Test_swe_get_planet_name_Trace() {
            using (var swe = new SwissEph()) {
                List<String> trace_out = new List<string>();
                swe.OnTrace += (s, e) => {
                    trace_out.Add(e.Message);
                };
                Assert.AreEqual("Sun", swe.swe_get_planet_name(SwissEph.SE_SUN));

                CollectionAssert.AreEqual(new String[] {
                    "swe_get_planet_name: 0\tSun\t"
                }, trace_out);
            }
        }

        [TestMethod]
        public void Test_swe_get_planet_name_Fictitious() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual("Cupido", swe.swe_get_planet_name(SwissEph.SE_FICT_OFFSET));
                Assert.AreEqual("name not found", swe.swe_get_planet_name(SwissEph.SE_FICT_MAX));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual("10000", swe.swe_get_planet_name(SwissEph.SE_AST_OFFSET));
            }
        }

    }
}
