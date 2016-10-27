using System;
using System.Collections.Generic;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {

        [Fact]
        public void Test_swe_get_planet_name() {
            using (var swe = new SwissEph()) {
                Assert.Equal("Sun", swe.swe_get_planet_name(SwissEph.SE_SUN));
                Assert.Equal("Moon", swe.swe_get_planet_name(SwissEph.SE_MOON));
                Assert.Equal("Mercury", swe.swe_get_planet_name(SwissEph.SE_MERCURY));
                Assert.Equal("Venus", swe.swe_get_planet_name(SwissEph.SE_VENUS));
                Assert.Equal("Earth", swe.swe_get_planet_name(SwissEph.SE_EARTH));
                Assert.Equal("Mars", swe.swe_get_planet_name(SwissEph.SE_MARS));
                Assert.Equal("Jupiter", swe.swe_get_planet_name(SwissEph.SE_JUPITER));
                Assert.Equal("Saturn", swe.swe_get_planet_name(SwissEph.SE_SATURN));
                Assert.Equal("Uranus", swe.swe_get_planet_name(SwissEph.SE_URANUS));
                Assert.Equal("Neptune", swe.swe_get_planet_name(SwissEph.SE_NEPTUNE));

                Assert.Equal("Pluto", swe.swe_get_planet_name(SwissEph.SE_PLUTO));
                Assert.Equal("mean Node", swe.swe_get_planet_name(SwissEph.SE_MEAN_NODE));
                Assert.Equal("true Node", swe.swe_get_planet_name(SwissEph.SE_TRUE_NODE));
                Assert.Equal("mean Apogee", swe.swe_get_planet_name(SwissEph.SE_MEAN_APOG));
                Assert.Equal("osc. Apogee", swe.swe_get_planet_name(SwissEph.SE_OSCU_APOG));
                Assert.Equal("Chiron", swe.swe_get_planet_name(SwissEph.SE_CHIRON));
                Assert.Equal("Pholus", swe.swe_get_planet_name(SwissEph.SE_PHOLUS));
                Assert.Equal("Ceres", swe.swe_get_planet_name(SwissEph.SE_CERES));
                Assert.Equal("Pallas", swe.swe_get_planet_name(SwissEph.SE_PALLAS));
                Assert.Equal("Juno", swe.swe_get_planet_name(SwissEph.SE_JUNO));
                Assert.Equal("Vesta", swe.swe_get_planet_name(SwissEph.SE_VESTA));
                Assert.Equal("intp. Apogee", swe.swe_get_planet_name(SwissEph.SE_INTP_APOG));
                Assert.Equal("intp. Perigee", swe.swe_get_planet_name(SwissEph.SE_INTP_PERG));

                Assert.Equal("Pluto", swe.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 134340));

                // Call twice the same planet for code coverage in swe_get_planet_name optimization planet name
                Assert.Equal("Pluto", swe.swe_get_planet_name(SwissEph.SE_PLUTO));
                Assert.Equal("Pluto", swe.swe_get_planet_name(SwissEph.SE_PLUTO));
            }
        }

        [Fact]
        public void Test_swe_get_planet_name_Trace() {
            using (var swe = new SwissEph()) {
                List<String> trace_out = new List<string>();
                swe.OnTrace += (s, e) => {
                    trace_out.Add(e.Message);
                };
                Assert.Equal("Sun", swe.swe_get_planet_name(SwissEph.SE_SUN));

                Assert.Equal(new String[] {
                    "swe_get_planet_name: 0\tSun\t"
                }, trace_out);
            }
        }

        [Fact]
        public void Test_swe_get_planet_name_Fictitious() {
            using (var swe = new SwissEph()) {
                Assert.Equal("Cupido", swe.swe_get_planet_name(SwissEph.SE_FICT_OFFSET));
                Assert.Equal("name not found", swe.swe_get_planet_name(SwissEph.SE_FICT_MAX));
            }
        }

        [Fact]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new SwissEph()) {
                Assert.Equal("10000", swe.swe_get_planet_name(SwissEph.SE_AST_OFFSET));
            }
        }

    }
}
