using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SweNet.Tests
{
    partial class SwissEphTest
    {

        [TestMethod]
        public void TestGetPlanetName() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual("Sun", swe.GetPlanetName(SwissEph.SE_SUN));
                Assert.AreEqual("Moon", swe.GetPlanetName(SwissEph.SE_MOON));
                Assert.AreEqual("Mercury", swe.GetPlanetName(SwissEph.SE_MERCURY));
                Assert.AreEqual("Venus", swe.GetPlanetName(SwissEph.SE_VENUS));
                Assert.AreEqual("Earth", swe.GetPlanetName(SwissEph.SE_EARTH));
                Assert.AreEqual("Mars", swe.GetPlanetName(SwissEph.SE_MARS));
                Assert.AreEqual("Jupiter", swe.GetPlanetName(SwissEph.SE_JUPITER));
                Assert.AreEqual("Saturn", swe.GetPlanetName(SwissEph.SE_SATURN));
                Assert.AreEqual("Uranus", swe.GetPlanetName(SwissEph.SE_URANUS));
                Assert.AreEqual("Neptune", swe.GetPlanetName(SwissEph.SE_NEPTUNE));

                Assert.AreEqual("Pluto", swe.GetPlanetName(SwissEph.SE_PLUTO));
                Assert.AreEqual("mean Node", swe.GetPlanetName(SwissEph.SE_MEAN_NODE));
                Assert.AreEqual("true Node", swe.GetPlanetName(SwissEph.SE_TRUE_NODE));
                Assert.AreEqual("mean Apogee", swe.GetPlanetName(SwissEph.SE_MEAN_APOG));
                Assert.AreEqual("osc. Apogee", swe.GetPlanetName(SwissEph.SE_OSCU_APOG));
                Assert.AreEqual("Chiron", swe.GetPlanetName(SwissEph.SE_CHIRON));
                Assert.AreEqual("Pholus", swe.GetPlanetName(SwissEph.SE_PHOLUS));
                Assert.AreEqual("Ceres", swe.GetPlanetName(SwissEph.SE_CERES));
                Assert.AreEqual("Pallas", swe.GetPlanetName(SwissEph.SE_PALLAS));
                Assert.AreEqual("Juno", swe.GetPlanetName(SwissEph.SE_JUNO));
                Assert.AreEqual("Vesta", swe.GetPlanetName(SwissEph.SE_VESTA));
                Assert.AreEqual("intp. Apogee", swe.GetPlanetName(SwissEph.SE_INTP_APOG));
                Assert.AreEqual("intp. Perigee", swe.GetPlanetName(SwissEph.SE_INTP_PERG));

                Assert.AreEqual("Pluto", swe.GetPlanetName(SwissEph.SE_AST_OFFSET + 134340));

                // Call twice the same planet for code coverage in swe_get_planet_name optimization planet name
                Assert.AreEqual("Pluto", swe.GetPlanetName(SwissEph.SE_PLUTO));
                Assert.AreEqual("Pluto", swe.GetPlanetName(SwissEph.SE_PLUTO));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Trace() {
            using (var swe = new SwissEph()) {
                List<String> trace_out = new List<string>(), trace_c = new List<string>();
                swe.OnTrace += (s, e) => {
                    if (e.IsCTrace)
                        trace_c.Add(e.Message);
                    else
                        trace_out.Add(e.Message);
                };
                Assert.AreEqual("Sun", swe.GetPlanetName(SwissEph.SE_SUN));

                CollectionAssert.AreEqual(new String[] {
                    "swe_get_planet_name: 0\tSun\t"
                }, trace_out);
                CollectionAssert.AreEqual(new String[] { 
                    "\n/*SWE_GET_PLANET_NAME*/",
                    "  ipl = 0;",
                    "  swe_get_planet_name(ipl, s);   /* s =  */",
                    "  printf(\"swe_get_planet_name: 0\\tSun\\t\\n\", ipl, s);"
                }, trace_c);
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Fictitious() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual("Cupido", swe.GetPlanetName(SwissEph.SE_FICT_OFFSET));
                Assert.AreEqual("name not found", swe.GetPlanetName(SwissEph.SE_FICT_MAX));
            }
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new SwissEph()) {
                Assert.AreEqual("Sun", swe.GetPlanetName(SwissEph.SE_AST_OFFSET));
            }
        }

    }
}
