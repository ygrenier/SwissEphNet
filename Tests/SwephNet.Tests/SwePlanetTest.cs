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

        [TestMethod]
        public void TestGetPlanetName_Fictitious() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("Cupido", swe.Planet.GetPlanetName(Planet.FirstFictitious));
                Assert.AreEqual("Name not found", swe.Planet.GetPlanetName(Planet.FirstFictitious + 200));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Fictitious_WithFile()
        {
            using (var swe = new Sweph())
            {
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == "seorbel.txt")
                    {
                        String fileContent = @"
    # Orbital elements of ficticious planets
#2456200.5, J2000, 143.49291, 1.4579341, 0.2225740, 178.78899, 304.33810, 10.82816, Eros   # 1
J1900, J1900, 163.7409, 40.99837, 0.00460, 171.4333, 129.8325, 1.0833, Cupido   # 1
J1900, J1900,  27.6496, 50.66744, 0.00245, 148.1796, 161.3339, 1.0500, Hades    # 2
J1900, J1900, 165.1232, 59.21436, 0.00120, 299.0440,   0.0000, 0.0000, Zeus     # 3
J1900, J1900, 169.0193, 64.81960, 0.00305, 208.8801,   0.0000, 0.0000, Kronos   # 4
J1900, J1900, 138.0533, 70.29949, 0.00000,   0.0000,   0.0000, 0.0000, Apollon  # 5
J1900, J1900, 351.3350, 73.62765, 0.00000,   0.0000,   0.0000, 0.0000, Admetos  # 6
J1900, J1900,  55.8983, 77.25568, 0.00000,   0.0000,   0.0000, 0.0000, Vulcanus # 7
J1900, J1900, 165.5163, 83.66907, 0.00000,   0.0000,   0.0000, 0.0000, Poseidon # 8
    #
2368547.66, 2431456.5, 0.0, 77.775, 0.3, 0.7, 0, 0, Isis-Transpluto             # 9
1856113.380954, 1856113.380954, 0.0, 234.8921, 0.981092, 103.966, -44.567, 158.708, Nibiru # 10
2374696.5, J2000, 0.0, 101.2, 0.411, 208.5, 275.4, 32.4, Harrington             # 11
2395662.5, 2395662.5, 34.05, 36.15, 0.10761, 284.75, 0, 0, Leverrier (Neptune)  # 12
2395662.5, 2395662.5, 24.28, 37.25, 0.12062, 299.11, 0, 0, Adams (Neptune)      # 13
2425977.5, 2425977.5, 281, 43.0, 0.202, 204.9, 0, 0, Lowell (Pluto)             # 14
2425977.5, 2425977.5, 48.95, 55.1, 0.31, 280.1, 100, 15, Pickering (Pluto)      # 15
J1900,JDATE, 252.8987988 + 707550.7341 * T, 0.13744, 0.019, 322.212069+1670.056*T, 47.787931-1670.056*T, 7.5, Vulcan # 16
J2000,JDATE, 242.2205555 + 5143.5418158 * T, 0.05280098949, 0.0, 0.0, 0.0, 0.0, Selena/White Moon, geo # 17
J1900,JDATE, 170.73, 79.225630, 0, 0, 0, 0, Proserpina #18
2414290.95827875,2414290.95827875, 70.3407215 + 109023.2634989 * T, 0.0068400705250028, 0.1587, 8.14049594 + 2393.47417444 * T, 136.24878256 - 1131.71719709 * T, 2.5, Waldemath, geo # 19
J2000,JDATE, 242.2205555, 0.05279142865925, 0.0, 0.0, 0.0, 0.0, Selena/White Moon, geo # 17
J2000,JDATE, 242.2205555 + 5143.5418158 * T, 0.05279142865925, 0.0, 0.0, 0.0, 0.0, Selena/White Moon with T Terms, geo # 17
J2000, JDATE, 174.794787 + 149472.5157715 * T, 0.38709831, 0.20563175 + 0.000020406 * T, 29.125226 + 0.3702885 * T, 48.330893 + 1.186189 * T, 7.004986 + 0.0018215 * T, Mercury elem. for equ. of date # 18
J2000, J2000, 174.794787 + 149472.5157715 * T, 0.38709831, 0.20563175 + 0.000020406 * T, 29.125226 + 0.2842872 * T, 48.330893 - 0.1254229 * T, 7.004986 - 0.0059516 * T, Mercury Test J2000 Elements# 18
";
                        e.File = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(fileContent));
                    }
                };

                Assert.AreEqual("Cupido", swe.Planet.GetPlanetName(Planet.FirstFictitious));
                Assert.AreEqual("Mercury Test J2000 Elements", swe.Planet.GetPlanetName(Planet.FirstFictitious + 22));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Asteroid() {
            using (var swe = new Sweph()) {
                Assert.AreEqual("0: not found", swe.Planet.GetPlanetName(Planet.FirstAsteroid));
                Assert.AreEqual("433: not found", swe.Planet.GetPlanetName(Planet.FirstAsteroid + 433));
            }
        }

        [TestMethod]
        public void TestGetPlanetName_Asteroid_WithFile()
        {
            using (var swe = new Sweph())
            {
                swe.OnLoadFile += (s, e) => {
                    if (e.FileName == "seasnam.txt")
                    {
                        string fileContent = @"
# Asteroid names
000001  Ceres
(000002)  Pallas
 [3]  Juno
Invalid line
{ 004  } Vesta
000430  Hybris
000431  Nephele
000432  Pythia
000433  Eros
   487  Venetia
";
                        e.File = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(fileContent));
                    }
                };

                Assert.AreEqual("0: not found", swe.Planet.GetPlanetName(Planet.FirstAsteroid));
                Assert.AreEqual("Ceres", swe.Planet.GetPlanetName(Planet.Ceres));
                Assert.AreEqual("Pallas", swe.Planet.GetPlanetName(Planet.Pallas));
                Assert.AreEqual("Juno", swe.Planet.GetPlanetName(Planet.Juno));
                Assert.AreEqual("Vesta", swe.Planet.GetPlanetName(Planet.Vesta));
                Assert.AreEqual("Hybris", swe.Planet.GetPlanetName(Planet.FirstAsteroid + 430));
                Assert.AreEqual("Eros", swe.Planet.GetPlanetName(Planet.FirstAsteroid + 433));
                Assert.AreEqual("450: not found", swe.Planet.GetPlanetName(Planet.FirstAsteroid + 450));
            }
        }

    }
}
