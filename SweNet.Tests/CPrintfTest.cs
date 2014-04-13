using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SweNet.Tests
{
    [TestClass]
    public class CPrintfTest
    {
        private string sepDecimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private string sep1000 = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

        #region Tests
        #region Special Formats
        [TestMethod]
        [TestCategory("Special")]
        [Description("Special formats %% / %n")]
        public void SpecialFormats() {
            //Console.WriteLine("Test special formats %% / %n");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%%]", "[%]"));
            Assert.IsTrue(RunTest("[%n]", "[1]"));
            Assert.IsTrue(RunTest("[%%n shows the number of processed chars so far (%010n)]",
                "[%n shows the number of processed chars so far (0000000048)]"));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region PositiveInteger
        [TestMethod]
        [TestCategory("Integer")]
        [Description("Test positive signed integer format %d / %i")]
        public void PositiveInteger() {
            //Console.WriteLine("Test positive signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%d]", "[42]", 42));
            Assert.IsTrue(RunTest("[%10d]", "[        42]", 42));
            Assert.IsTrue(RunTest("[%-10d]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%010d]", "[0000000042]", 42));
            Assert.IsTrue(RunTest("[%-010d]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%+d]", "[+42]", 42));
            Assert.IsTrue(RunTest("[%+10d]", "[       +42]", 42));
            Assert.IsTrue(RunTest("[%+ 10d]", "[       +42]", 42));
            Assert.IsTrue(RunTest("[%-+10d]", "[+42       ]", 42));
            Assert.IsTrue(RunTest("[%+010d]", "[+000000042]", 42));
            Assert.IsTrue(RunTest("[%-+010d]", "[+42       ]", 42));

            Assert.IsTrue(RunTest("[%d]", "[42]", (byte)42));
            Assert.IsTrue(RunTest("[%d]", "[42]", (sbyte)42));
            Assert.IsTrue(RunTest("[%d]", "[42]", (Int16)42));
            Assert.IsTrue(RunTest("[%d]", "[42]", (UInt16)42));
            Assert.IsTrue(RunTest("[%d]", "[42]", (UInt32)42));
            Assert.IsTrue(RunTest("[%d]", "[42]", (Int64)42));
            Assert.IsTrue(RunTest("[%d]", "[42]", (UInt64)42));
            Assert.IsTrue(RunTest("[%i]", "[42]", (byte)42));
            //Assert.IsTrue(RunTest("[%d]", "[42]", (Char)42));

            Assert.IsTrue(RunTest("[%d]", "[65537]", 65537));
            Assert.IsTrue(RunTest("[%'d]", String.Format("[65{0}537]", sep1000), 65537));
            Assert.IsTrue(RunTest("[%'d]", String.Format("[10{0}065{0}537]", sep1000), 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region ShortInteger
        [TestMethod]
        [TestCategory("Integer")]
        [Description("Test positive short format %d / %i")]
        public void ShortInteger() {
            //Console.WriteLine("Test positive signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%hd]", "[42]", 42));
            Assert.IsTrue(RunTest("[%10hd]", "[        42]", 42));
            Assert.IsTrue(RunTest("[%-10hd]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%010hd]", "[0000000042]", 42));
            Assert.IsTrue(RunTest("[%-010hd]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%+hd]", "[+42]", 42));
            Assert.IsTrue(RunTest("[%+10hd]", "[       +42]", 42));
            Assert.IsTrue(RunTest("[%+ 10hd]", "[       +42]", 42));
            Assert.IsTrue(RunTest("[%-+10hd]", "[+42       ]", 42));
            Assert.IsTrue(RunTest("[%+010hd]", "[+000000042]", 42));
            Assert.IsTrue(RunTest("[%-+010hd]", "[+42       ]", 42));

            Assert.IsTrue(RunTest("[%hd]", "[42]", (byte)42));
            Assert.IsTrue(RunTest("[%hd]", "[42]", (sbyte)42));
            Assert.IsTrue(RunTest("[%hd]", "[42]", (Int16)42));
            Assert.IsTrue(RunTest("[%hd]", "[42]", (UInt16)42));
            Assert.IsTrue(RunTest("[%hd]", "[42]", (UInt32)42));
            Assert.IsTrue(RunTest("[%hd]", "[42]", (Int64)42));
            Assert.IsTrue(RunTest("[%hd]", "[42]", (UInt64)42));
            //Assert.IsTrue(RunTest("[%d]", "[42]", (Char)42));

            Assert.AreEqual("[1]", C.sprintf("[%hd]", 65537));
            Assert.AreEqual("[1]", C.sprintf("[%'hd]", 65537));
            Assert.AreEqual(String.Format("[-27{0}007]", sep1000), C.sprintf("[%'hd]", 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region LongInteger
        [TestMethod]
        [TestCategory("Integer")]
        [Description("Test positive long format %d / %i")]
        public void LongInteger() {
            //Console.WriteLine("Test positive signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%ld]", "[42]", 42));
            Assert.IsTrue(RunTest("[%10ld]", "[        42]", 42));
            Assert.IsTrue(RunTest("[%-10ld]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%010ld]", "[0000000042]", 42));
            Assert.IsTrue(RunTest("[%-010ld]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%+ld]", "[+42]", 42));
            Assert.IsTrue(RunTest("[%+10ld]", "[       +42]", 42));
            Assert.IsTrue(RunTest("[%+ 10ld]", "[       +42]", 42));
            Assert.IsTrue(RunTest("[%-+10ld]", "[+42       ]", 42));
            Assert.IsTrue(RunTest("[%+010ld]", "[+000000042]", 42));
            Assert.IsTrue(RunTest("[%-+010ld]", "[+42       ]", 42));

            Assert.IsTrue(RunTest("[%ld]", "[42]", (byte)42));
            Assert.IsTrue(RunTest("[%ld]", "[42]", (sbyte)42));
            Assert.IsTrue(RunTest("[%ld]", "[42]", (Int16)42));
            Assert.IsTrue(RunTest("[%ld]", "[42]", (UInt16)42));
            Assert.IsTrue(RunTest("[%ld]", "[42]", (UInt32)42));
            Assert.IsTrue(RunTest("[%ld]", "[42]", (Int64)42));
            Assert.IsTrue(RunTest("[%ld]", "[42]", (UInt64)42));
            //Assert.IsTrue(RunTest("[%d]", "[42]", (Char)42));

            Assert.IsTrue(RunTest("[%ld]", "[65537]", 65537));
            Assert.IsTrue(RunTest("[%'ld]", String.Format("[65{0}537]", sep1000), 65537));
            Assert.IsTrue(RunTest("[%'ld]", String.Format("[10{0}065{0}537]", sep1000), 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region NegativeInteger
        [TestMethod]
        [TestCategory("Integer")]
        [Description("Test negative signed integer format %d / %i")]
        public void NegativeInteger() {
            //Console.WriteLine("Test negative signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%d]", "[-42]", -42));
            Assert.IsTrue(RunTest("[%10d]", "[       -42]", -42));
            Assert.IsTrue(RunTest("[%-10d]", "[-42       ]", -42));
            Assert.IsTrue(RunTest("[%010d]", "[-000000042]", -42));
            Assert.IsTrue(RunTest("[%-010d]", "[-42       ]", -42));
            Assert.IsTrue(RunTest("[%+d]", "[-42]", -42));
            Assert.IsTrue(RunTest("[%+10d]", "[       -42]", -42));
            Assert.IsTrue(RunTest("[%-+10d]", "[-42       ]", -42));
            Assert.IsTrue(RunTest("[%+010d]", "[-000000042]", -42));
            Assert.IsTrue(RunTest("[%-+010d]", "[-42       ]", -42));

            Assert.IsTrue(RunTest("[%d]", "[-42]", (sbyte)(-42)));
            Assert.IsTrue(RunTest("[%d]", "[-42]", (Int16)(-42)));
            Assert.IsTrue(RunTest("[%d]", "[-42]", (Int64)(-42)));
            //Assert.IsTrue(RunTest("[%d]", "[-42]", (Char)(-42)));

            Assert.IsTrue(RunTest("[%d]", "[-65537]", -65537));
            Assert.IsTrue(RunTest("[%'d]", String.Format("[-65{0}537]", sep1000), -65537));
            Assert.IsTrue(RunTest("[%'d]", String.Format("[-10{0}065{0}537]", sep1000), -10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region UnsignedInteger
        [TestMethod]
        [TestCategory("Integer")]
        [Description("Test unsigned integer format %u")]
        public void UnsignedInteger() {
            //Console.WriteLine("Test unsigned integer format %u");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%u]", "[42]", 42));
            Assert.IsTrue(RunTest("[%10u]", "[        42]", 42));
            Assert.IsTrue(RunTest("[%-10u]", "[42        ]", 42));
            Assert.IsTrue(RunTest("[%010u]", "[0000000042]", 42));
            Assert.IsTrue(RunTest("[%-010u]", "[42        ]", 42));

            Assert.IsTrue(RunTest("[%u]", "[4294967254]", -42));
            Assert.IsTrue(RunTest("[%20u]", "[          4294967254]", -42));
            Assert.IsTrue(RunTest("[%-20u]", "[4294967254          ]", -42));
            Assert.IsTrue(RunTest("[%020u]", "[00000000004294967254]", -42));
            Assert.IsTrue(RunTest("[%-020u]", "[4294967254          ]", -42));

            Assert.IsTrue(RunTest("[%u]", "[65537]", 65537));
            Assert.IsTrue(RunTest("[%'u]", String.Format("[65{0}537]", sep1000), 65537));
            Assert.IsTrue(RunTest("[%'u]", String.Format("[10{0}065{0}537]", sep1000), 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Float
        [TestMethod]
        [TestCategory("Float")]
        [Description("Test float format %f")]
        public void Floats() {
            //Console.WriteLine("Test float format %f");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.AreEqual(String.Format("[42]", sepDecimal), C.sprintf("[%g]", 42));
            Assert.AreEqual(String.Format("[42]", sepDecimal), C.sprintf("[%G]", 42));

            Assert.IsTrue(RunTest("[%f]", String.Format("[42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%f]", String.Format("[42{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%10f]", String.Format("[ 42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%10f]", String.Format("[ 42{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-10f]", String.Format("[42{0}000000 ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-10f]", String.Format("[42{0}500000 ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%010f]", String.Format("[042{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%010f]", String.Format("[042{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-010f]", String.Format("[42{0}000000 ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-010f]", String.Format("[42{0}500000 ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%+f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+10f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+10f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-10f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-10f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+010f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+010f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-010f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-010f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));

            Assert.IsTrue(RunTest("[%+f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));

            // -----

            Assert.IsTrue(RunTest("[%.2f]", String.Format("[42{0}00]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%.2f]", String.Format("[42{0}50]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%10.2f]", String.Format("[     42{0}00]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%10.2f]", String.Format("[     42{0}50]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-10.2f]", String.Format("[42{0}00     ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-10.2f]", String.Format("[42{0}50     ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%010.2f]", String.Format("[0000042{0}00]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%010.2f]", String.Format("[0000042{0}50]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-010.2f]", String.Format("[42{0}00     ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-010.2f]", String.Format("[42{0}50     ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%+.2f]", String.Format("[+42{0}00]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+.2f]", String.Format("[+42{0}50]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+10.2f]", String.Format("[    +42{0}00]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+10.2f]", String.Format("[    +42{0}50]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-10.2f]", String.Format("[+42{0}00    ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-10.2f]", String.Format("[+42{0}50    ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+010.2f]", String.Format("[+000042{0}00]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+010.2f]", String.Format("[+000042{0}50]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-010.2f]", String.Format("[+42{0}00    ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-010.2f]", String.Format("[+42{0}50    ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%.2f]", String.Format("[-42{0}00]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%.2f]", String.Format("[-42{0}50]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%10.2f]", String.Format("[    -42{0}00]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%10.2f]", String.Format("[    -42{0}50]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-10.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-10.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%010.2f]", String.Format("[-000042{0}00]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%010.2f]", String.Format("[-000042{0}50]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-010.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-010.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));

            Assert.IsTrue(RunTest("[%+.2f]", String.Format("[-42{0}00]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+.2f]", String.Format("[-42{0}50]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+10.2f]", String.Format("[    -42{0}00]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+10.2f]", String.Format("[    -42{0}50]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-10.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-10.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+010.2f]", String.Format("[-000042{0}00]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+010.2f]", String.Format("[-000042{0}50]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-010.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-010.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Exponent
        [TestMethod]
        [TestCategory("Exponent")]
        [Description("Test exponent format %f")]
        public void Exponents() {
            //Console.WriteLine("Test exponent format %f");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%e]", String.Format("[4{0}200000e+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%e]", String.Format("[4{0}250000e+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%20e]", String.Format("[       4{0}200000e+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%20e]", String.Format("[       4{0}250000e+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-20e]", String.Format("[4{0}200000e+001       ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-20e]", String.Format("[4{0}250000e+001       ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%020e]", String.Format("[00000004{0}200000e+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%020e]", String.Format("[00000004{0}250000e+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-020e]", String.Format("[4{0}200000e+001       ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-020e]", String.Format("[4{0}250000e+001       ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%+E]", String.Format("[+4{0}200000E+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+E]", String.Format("[+4{0}250000E+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+20E]", String.Format("[      +4{0}200000E+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+20E]", String.Format("[      +4{0}250000E+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-20E]", String.Format("[+4{0}200000E+001      ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-20E]", String.Format("[+4{0}250000E+001      ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+020E]", String.Format("[+0000004{0}200000E+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+020E]", String.Format("[+0000004{0}250000E+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-020E]", String.Format("[+4{0}200000E+001      ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-020E]", String.Format("[+4{0}250000E+001      ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%e]", String.Format("[-4{0}200000e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%e]", String.Format("[-4{0}250000e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%20e]", String.Format("[      -4{0}200000e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%20e]", String.Format("[      -4{0}250000e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-20e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-20e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%020e]", String.Format("[-0000004{0}200000e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%020e]", String.Format("[-0000004{0}250000e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-020e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-020e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));

            Assert.IsTrue(RunTest("[%+e]", String.Format("[-4{0}200000e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+e]", String.Format("[-4{0}250000e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+20e]", String.Format("[      -4{0}200000e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+20e]", String.Format("[      -4{0}250000e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-20e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-20e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+020e]", String.Format("[-0000004{0}200000e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+020e]", String.Format("[-0000004{0}250000e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-020e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-020e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));

            // -----

            Assert.IsTrue(RunTest("[%.2e]", String.Format("[4{0}20e+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%.2e]", String.Format("[4{0}25e+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%20.2e]", String.Format("[           4{0}20e+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%20.2e]", String.Format("[           4{0}25e+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-20.2e]", String.Format("[4{0}20e+001           ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-20.2e]", String.Format("[4{0}25e+001           ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%020.2e]", String.Format("[000000000004{0}20e+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%020.2e]", String.Format("[000000000004{0}25e+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%-020.2e]", String.Format("[4{0}20e+001           ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%-020.2e]", String.Format("[4{0}25e+001           ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%+.2E]", String.Format("[+4{0}20E+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+.2E]", String.Format("[+4{0}25E+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+20.2E]", String.Format("[          +4{0}20E+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+20.2E]", String.Format("[          +4{0}25E+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-20.2E]", String.Format("[+4{0}20E+001          ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-20.2E]", String.Format("[+4{0}25E+001          ]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+020.2E]", String.Format("[+00000000004{0}20E+001]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+020.2E]", String.Format("[+00000000004{0}25E+001]", sepDecimal), 42.5));
            Assert.IsTrue(RunTest("[%+-020.2E]", String.Format("[+4{0}20E+001          ]", sepDecimal), 42));
            Assert.IsTrue(RunTest("[%+-020.2E]", String.Format("[+4{0}25E+001          ]", sepDecimal), 42.5));

            Assert.IsTrue(RunTest("[%.2e]", String.Format("[-4{0}20e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%.2e]", String.Format("[-4{0}25e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%20.2e]", String.Format("[          -4{0}20e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%20.2e]", String.Format("[          -4{0}25e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-20.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-20.2e]", String.Format("[-4{0}25e+001          ]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%020.2e]", String.Format("[-00000000004{0}20e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%020.2e]", String.Format("[-00000000004{0}25e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%-020.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%-020.2e]", String.Format("[-4{0}25e+001          ]", sepDecimal), -42.5));

            Assert.IsTrue(RunTest("[%+.2e]", String.Format("[-4{0}20e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+.2e]", String.Format("[-4{0}25e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+20.2e]", String.Format("[          -4{0}20e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+20.2e]", String.Format("[          -4{0}25e+001]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+-20.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-20.2e]", String.Format("[-4{0}25e+001          ]", sepDecimal), -42.5));
            Assert.IsTrue(RunTest("[%+020.2e]", String.Format("[-00000000004{0}20e+001]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+020.2e]", String.Format("[-00000000004{0}25e+001]", sepDecimal), -42.545));
            Assert.IsTrue(RunTest("[%+-020.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.IsTrue(RunTest("[%+-020.2e]", String.Format("[-4{0}26e+001          ]", sepDecimal), -42.555));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Character
        [TestMethod]
        [TestCategory("Character")]
        [Description("Character format %c")]
        public void CharacterFormat() {
            //Console.WriteLine("Test character formats %c");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%c]", "[]", null));
            Assert.IsTrue(RunTest("[%c]", "[A]", 'A'));
            Assert.IsTrue(RunTest("[%c]", "[A]", "A Test"));
            Assert.IsTrue(RunTest("[%c]", "[A]", 65));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Pointer
        [TestMethod]
        [TestCategory("Pointer")]
        [Description("Pointer format %p")]
        public void PointerFormat() {
            Assert.AreEqual("[]", C.sprintf("[%p]", null));
            Assert.AreEqual("[0x0]", C.sprintf("[%p]", IntPtr.Zero));
            Assert.AreEqual("[0x7b]", C.sprintf("[%p]", (IntPtr)123));
            Assert.AreEqual("[0xffffffffffffff85]", C.sprintf("[%p]", (IntPtr)(-123)));
            Assert.AreEqual("[%P]", C.sprintf("[%P]", (IntPtr)123));
        }
        #endregion
        #region Strings
        [TestMethod]
        [TestCategory("String")]
        [Description("Test string format %s")]
        public void Strings() {
            //Console.WriteLine("Test string format %s");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%s]", "[This is a test]", "This is a test"));
            Assert.IsTrue(RunTest("[%s]", "[A test with %]", "A test with %"));
            Assert.IsTrue(RunTest("[%s]", "[A test with %s inside]", "A test with %s inside"));
            Assert.IsTrue(RunTest("[%% %s %%]", "[% % Another test % %]", "% Another test %"));
            Assert.IsTrue(RunTest("[%20s]", "[       a long string]", "a long string"));
            Assert.IsTrue(RunTest("[%-20s]", "[a long string       ]", "a long string"));
            Assert.IsTrue(RunTest("[%020s]", "[0000000a long string]", "a long string"));
            Assert.IsTrue(RunTest("[%-020s]", "[a long string       ]", "a long string"));

            Assert.IsTrue(RunTest("[%.10s]", "[This is a ]", "This is a shortened string"));
            Assert.IsTrue(RunTest("[%20.10s]", "[          This is a ]", "This is a shortened string"));
            Assert.IsTrue(RunTest("[%-20.10s]", "[This is a           ]", "This is a shortened string"));
            Assert.IsTrue(RunTest("[%020.10s]", "[0000000000This is a ]", "This is a shortened string"));
            Assert.IsTrue(RunTest("[%-020.10s]", "[This is a           ]", "This is a shortened string"));

            //C.printf("Account balance: %'+20.2f\n", 12345678);

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Hex
        [TestMethod]
        [TestCategory("HEX")]
        [Description("Test hex format %x / %X")]
        public void Hex() {
            //Console.WriteLine("Test hex format %x / %X");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%x]", "[2a]", 42));
            Assert.IsTrue(RunTest("[%X]", "[2A]", 42));
            Assert.IsTrue(RunTest("[%5x]", "[   2a]", 42));
            Assert.IsTrue(RunTest("[%5X]", "[   2A]", 42));
            Assert.IsTrue(RunTest("[%05x]", "[0002a]", 42));
            Assert.IsTrue(RunTest("[%05X]", "[0002A]", 42));
            Assert.IsTrue(RunTest("[%-05x]", "[2a   ]", 42));
            Assert.IsTrue(RunTest("[%-05X]", "[2A   ]", 42));

            Assert.IsTrue(RunTest("[%#x]", "[0x2a]", 42));
            Assert.IsTrue(RunTest("[%#X]", "[0X2A]", 42));
            Assert.IsTrue(RunTest("[%#5x]", "[ 0x2a]", 42));
            Assert.IsTrue(RunTest("[%#5X]", "[ 0X2A]", 42));
            Assert.IsTrue(RunTest("[%#05x]", "[0x02a]", 42));
            Assert.IsTrue(RunTest("[%#05X]", "[0X02A]", 42));
            Assert.IsTrue(RunTest("[%#-05x]", "[0x2a ]", 42));
            Assert.IsTrue(RunTest("[%#-05X]", "[0X2A ]", 42));

            Assert.IsTrue(RunTest("[%.2x]", "[05]", 5));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Octal
        [TestMethod]
        [TestCategory("Octal")]
        [Description("Test octal format %o")]
        public void Octal() {
            //Console.WriteLine("Test octal format %o");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%o]", "[52]", 42));
            Assert.IsTrue(RunTest("[%o]", "[52]", 42));
            Assert.IsTrue(RunTest("[%5o]", "[   52]", 42));
            Assert.IsTrue(RunTest("[%5o]", "[   52]", 42));
            Assert.IsTrue(RunTest("[%05o]", "[00052]", 42));
            Assert.IsTrue(RunTest("[%05o]", "[00052]", 42));
            Assert.IsTrue(RunTest("[%-05o]", "[52   ]", 42));
            Assert.IsTrue(RunTest("[%-05o]", "[52   ]", 42));

            Assert.IsTrue(RunTest("[%#o]", "[052]", 42));
            Assert.IsTrue(RunTest("[%#o]", "[052]", 42));
            Assert.IsTrue(RunTest("[%#5o]", "[  052]", 42));
            Assert.IsTrue(RunTest("[%#5o]", "[  052]", 42));
            Assert.IsTrue(RunTest("[%#05o]", "[00052]", 42));
            Assert.IsTrue(RunTest("[%#05o]", "[00052]", 42));
            Assert.IsTrue(RunTest("[%#-05o]", "[052  ]", 42));
            Assert.IsTrue(RunTest("[%#-05o]", "[052  ]", 42));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region PositionIndex
        [TestMethod]
        [TestCategory("PositionIndex")]
        [Description("Test position index (n$)")]
        public void PositionIndex() {
            //Console.WriteLine("Test position index (n$)");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.IsTrue(RunTest("[%2$d %1$#x %1$d]", "[17 0x10 16]", 16, 17));

            //Console.WriteLine("\n\n");
        }
        #endregion

        [TestMethod]
        public void Testfprintf() {
            StringWriter writer = new StringWriter();
            C.fprintf(writer, "%d %c", 1234, 'X');
            Assert.AreEqual("1234 X", writer.GetStringBuilder().ToString());
        }

        [TestMethod]
        public void TestIsPositive() {

            Assert.IsTrue(C.IsPositive((sbyte)0, true));
            Assert.IsTrue(C.IsPositive((byte)0, true));
            Assert.IsTrue(C.IsPositive((Int16)0, true));
            Assert.IsTrue(C.IsPositive((UInt16)0, true));
            Assert.IsTrue(C.IsPositive((Int32)0, true));
            Assert.IsTrue(C.IsPositive((UInt32)0, true));
            Assert.IsTrue(C.IsPositive((Int64)0, true));
            Assert.IsTrue(C.IsPositive((UInt64)0, true));
            Assert.IsTrue(C.IsPositive((Single)0, true));
            Assert.IsTrue(C.IsPositive((Double)0, true));
            Assert.IsTrue(C.IsPositive((Decimal)0, true));
            Assert.IsTrue(C.IsPositive((Char)0, true));

            Assert.IsFalse(C.IsPositive((sbyte)0, false));
            Assert.IsFalse(C.IsPositive((byte)0, false));
            Assert.IsFalse(C.IsPositive((Int16)0, false));
            Assert.IsFalse(C.IsPositive((UInt16)0, false));
            Assert.IsFalse(C.IsPositive((Int32)0, false));
            Assert.IsFalse(C.IsPositive((UInt32)0, false));
            Assert.IsFalse(C.IsPositive((Int64)0, false));
            Assert.IsFalse(C.IsPositive((UInt64)0, false));
            Assert.IsFalse(C.IsPositive((Single)0, false));
            Assert.IsFalse(C.IsPositive((Double)0, false));
            Assert.IsFalse(C.IsPositive((Decimal)0, false));
            Assert.IsFalse(C.IsPositive((Char)0, false));

            Assert.IsTrue(C.IsPositive((sbyte)10, true));
            Assert.IsTrue(C.IsPositive((byte)10, true));
            Assert.IsTrue(C.IsPositive((Int16)10, true));
            Assert.IsTrue(C.IsPositive((UInt16)10, true));
            Assert.IsTrue(C.IsPositive((Int32)10, true));
            Assert.IsTrue(C.IsPositive((UInt32)10, true));
            Assert.IsTrue(C.IsPositive((Int64)10, true));
            Assert.IsTrue(C.IsPositive((UInt64)10, true));
            Assert.IsTrue(C.IsPositive((Single)10, true));
            Assert.IsTrue(C.IsPositive((Double)10, true));
            Assert.IsTrue(C.IsPositive((Decimal)10, true));
            Assert.IsTrue(C.IsPositive((Char)10, true));

            Assert.IsTrue(C.IsPositive((sbyte)10, false));
            Assert.IsTrue(C.IsPositive((byte)10, false));
            Assert.IsTrue(C.IsPositive((Int16)10, false));
            Assert.IsTrue(C.IsPositive((UInt16)10, false));
            Assert.IsTrue(C.IsPositive((Int32)10, false));
            Assert.IsTrue(C.IsPositive((UInt32)10, false));
            Assert.IsTrue(C.IsPositive((Int64)10, false));
            Assert.IsTrue(C.IsPositive((UInt64)10, false));
            Assert.IsTrue(C.IsPositive((Single)10, false));
            Assert.IsTrue(C.IsPositive((Double)10, false));
            Assert.IsTrue(C.IsPositive((Decimal)10, false));
            Assert.IsTrue(C.IsPositive((Char)10, false));

            Assert.IsFalse(C.IsPositive((sbyte)(-10), true));
            Assert.IsFalse(C.IsPositive((Int16)(-10), true));
            Assert.IsFalse(C.IsPositive((Int32)(-10), true));
            Assert.IsFalse(C.IsPositive((Int64)(-10), true));
            Assert.IsFalse(C.IsPositive((Single)(-10), true));
            Assert.IsFalse(C.IsPositive((Double)(-10), true));
            Assert.IsFalse(C.IsPositive((Decimal)(-10), true));

            Assert.IsFalse(C.IsPositive((sbyte)(-10), false));
            Assert.IsFalse(C.IsPositive((Int16)(-10), false));
            Assert.IsFalse(C.IsPositive((Int32)(-10), false));
            Assert.IsFalse(C.IsPositive((Int64)(-10), false));
            Assert.IsFalse(C.IsPositive((Single)(-10), false));
            Assert.IsFalse(C.IsPositive((Double)(-10), false));
            Assert.IsFalse(C.IsPositive((Decimal)(-10), false));

            Assert.IsFalse(C.IsPositive("", true));
            Assert.IsFalse(C.IsPositive("", false));
        }

        [TestMethod]
        public void TestToInteger() {

            Assert.AreEqual((sbyte)0, C.ToInteger((sbyte)0, true));
            Assert.AreEqual((byte)0, C.ToInteger((byte)0, true));
            Assert.AreEqual((Int16)0, C.ToInteger((Int16)0, true));
            Assert.AreEqual((UInt16)0, C.ToInteger((UInt16)0, true));
            Assert.AreEqual((Int32)0, C.ToInteger((Int32)0, true));
            Assert.AreEqual((UInt32)0, C.ToInteger((UInt32)0, true));
            Assert.AreEqual((Int64)0, C.ToInteger((Int64)0, true));
            Assert.AreEqual((UInt64)0, C.ToInteger((UInt64)0, true));
            Assert.AreEqual((Int32)0, C.ToInteger((Single)0, true));
            Assert.AreEqual((Int64)0, C.ToInteger((Double)0, true));
            Assert.AreEqual((Decimal)0, C.ToInteger((Decimal)0, true));
            Assert.AreEqual((Int32)0, C.ToInteger((Single)0.345, true));
            Assert.AreEqual((Int64)0, C.ToInteger((Double)0.345, true));
            Assert.AreEqual((Decimal)0, C.ToInteger((Decimal)0.345, true));
            Assert.AreEqual(null, C.ToInteger((Char)0, true));

            Assert.AreEqual((sbyte)0, C.ToInteger((sbyte)0, false));
            Assert.AreEqual((byte)0, C.ToInteger((byte)0, false));
            Assert.AreEqual((Int16)0, C.ToInteger((Int16)0, false));
            Assert.AreEqual((UInt16)0, C.ToInteger((UInt16)0, false));
            Assert.AreEqual((Int32)0, C.ToInteger((Int32)0, false));
            Assert.AreEqual((UInt32)0, C.ToInteger((UInt32)0, false));
            Assert.AreEqual((Int64)0, C.ToInteger((Int64)0, false));
            Assert.AreEqual((UInt64)0, C.ToInteger((UInt64)0, false));
            Assert.AreEqual((Int32)0, C.ToInteger((Single)0, false));
            Assert.AreEqual((Int64)0, C.ToInteger((Double)0, false));
            Assert.AreEqual((Decimal)0, C.ToInteger((Decimal)0, false));
            Assert.AreEqual((Int32)0, C.ToInteger((Single)0.345, false));
            Assert.AreEqual((Int64)0, C.ToInteger((Double)0.345, false));
            Assert.AreEqual((Decimal)0.345, C.ToInteger((Decimal)0.345, false));
            Assert.AreEqual(null, C.ToInteger((Char)0, false));

            Assert.AreEqual((sbyte)10, C.ToInteger((sbyte)10, true));
            Assert.AreEqual((byte)10, C.ToInteger((byte)10, true));
            Assert.AreEqual((Int16)10, C.ToInteger((Int16)10, true));
            Assert.AreEqual((UInt16)10, C.ToInteger((UInt16)10, true));
            Assert.AreEqual((Int32)10, C.ToInteger((Int32)10, true));
            Assert.AreEqual((UInt32)10, C.ToInteger((UInt32)10, true));
            Assert.AreEqual((Int64)10, C.ToInteger((Int64)10, true));
            Assert.AreEqual((UInt64)10, C.ToInteger((UInt64)10, true));
            Assert.AreEqual((Int32)10, C.ToInteger((Single)10, true));
            Assert.AreEqual((Int64)10, C.ToInteger((Double)10, true));
            Assert.AreEqual((Decimal)10, C.ToInteger((Decimal)10, true));
            Assert.AreEqual((Int32)10, C.ToInteger((Single)10.345, true));
            Assert.AreEqual((Int64)10, C.ToInteger((Double)10.345, true));
            Assert.AreEqual((Decimal)10, C.ToInteger((Decimal)10.345, true));
            Assert.AreEqual(null, C.ToInteger((Char)10, true));

            Assert.AreEqual((sbyte)10, C.ToInteger((sbyte)10, false));
            Assert.AreEqual((byte)10, C.ToInteger((byte)10, false));
            Assert.AreEqual((Int16)10, C.ToInteger((Int16)10, false));
            Assert.AreEqual((UInt16)10, C.ToInteger((UInt16)10, false));
            Assert.AreEqual((Int32)10, C.ToInteger((Int32)10, false));
            Assert.AreEqual((UInt32)10, C.ToInteger((UInt32)10, false));
            Assert.AreEqual((Int64)10, C.ToInteger((Int64)10, false));
            Assert.AreEqual((UInt64)10, C.ToInteger((UInt64)10, false));
            Assert.AreEqual((Int32)10, C.ToInteger((Single)10, false));
            Assert.AreEqual((Int64)10, C.ToInteger((Double)10, false));
            Assert.AreEqual((Decimal)10, C.ToInteger((Decimal)10, false));
            Assert.AreEqual((Int32)10, C.ToInteger((Single)10.345, false));
            Assert.AreEqual((Int64)10, C.ToInteger((Double)10.345, false));
            Assert.AreEqual((Decimal)10.345, C.ToInteger((Decimal)10.345, false));
            Assert.AreEqual(null, C.ToInteger((Char)10, false));

            Assert.AreEqual((sbyte)(-10), C.ToInteger((sbyte)(-10), true));
            Assert.AreEqual((Int16)(-10), C.ToInteger((Int16)(-10), true));
            Assert.AreEqual((Int32)(-10), C.ToInteger((Int32)(-10), true));
            Assert.AreEqual((Int64)(-10), C.ToInteger((Int64)(-10), true));
            Assert.AreEqual((Int32)(-10), C.ToInteger((Single)(-10), true));
            Assert.AreEqual((Int64)(-10), C.ToInteger((Double)(-10), true));
            Assert.AreEqual((Decimal)(-10), C.ToInteger((Decimal)(-10), true));
            Assert.AreEqual((Int32)(-10), C.ToInteger((Single)(-10.345), true));
            Assert.AreEqual((Int64)(-10), C.ToInteger((Double)(-10.345), true));
            Assert.AreEqual((Decimal)(-10), C.ToInteger((Decimal)(-10.345), true));

            Assert.AreEqual((sbyte)(-10), C.ToInteger((sbyte)(-10), false));
            Assert.AreEqual((Int16)(-10), C.ToInteger((Int16)(-10), false));
            Assert.AreEqual((Int32)(-10), C.ToInteger((Int32)(-10), false));
            Assert.AreEqual((Int64)(-10), C.ToInteger((Int64)(-10), false));
            Assert.AreEqual((Int32)(-10), C.ToInteger((Single)(-10), false));
            Assert.AreEqual((Int64)(-10), C.ToInteger((Double)(-10), false));
            Assert.AreEqual((Decimal)(-10), C.ToInteger((Decimal)(-10), false));
            Assert.AreEqual((Int32)(-10), C.ToInteger((Single)(-10.345), false));
            Assert.AreEqual((Int64)(-10), C.ToInteger((Double)(-10.345), false));
            Assert.AreEqual((Decimal)(-10.345), C.ToInteger((Decimal)(-10.345), false));

            Assert.AreEqual(null, C.ToInteger("", true));
            Assert.AreEqual(null, C.ToInteger("", false));
        }

        [TestMethod]
        public void TestToUnsigned() {

            Assert.AreEqual((byte)0, C.ToUnsigned((sbyte)0));
            Assert.AreEqual((byte)0, C.ToUnsigned((byte)0));
            Assert.AreEqual((UInt16)0, C.ToUnsigned((Int16)0));
            Assert.AreEqual((UInt16)0, C.ToUnsigned((UInt16)0));
            Assert.AreEqual((UInt32)0, C.ToUnsigned((Int32)0));
            Assert.AreEqual((UInt32)0, C.ToUnsigned((UInt32)0));
            Assert.AreEqual((UInt64)0, C.ToUnsigned((Int64)0));
            Assert.AreEqual((UInt64)0, C.ToUnsigned((UInt64)0));
            Assert.AreEqual((UInt32)0, C.ToUnsigned((Single)0));
            Assert.AreEqual((UInt64)0, C.ToUnsigned((Double)0));
            Assert.AreEqual((UInt64)0, C.ToUnsigned((Decimal)0));
            Assert.AreEqual((UInt32)0, C.ToUnsigned((Single)0.345));
            Assert.AreEqual((UInt64)0, C.ToUnsigned((Double)0.345));
            Assert.AreEqual((UInt64)0, C.ToUnsigned((Decimal)0.345));
            Assert.AreEqual(null, C.ToUnsigned((Char)0));

            Assert.AreEqual((byte)10, C.ToUnsigned((sbyte)10));
            Assert.AreEqual((byte)10, C.ToUnsigned((byte)10));
            Assert.AreEqual((UInt16)10, C.ToUnsigned((Int16)10));
            Assert.AreEqual((UInt16)10, C.ToUnsigned((UInt16)10));
            Assert.AreEqual((UInt32)10, C.ToUnsigned((Int32)10));
            Assert.AreEqual((UInt32)10, C.ToUnsigned((UInt32)10));
            Assert.AreEqual((UInt64)10, C.ToUnsigned((Int64)10));
            Assert.AreEqual((UInt64)10, C.ToUnsigned((UInt64)10));
            Assert.AreEqual((UInt32)10, C.ToUnsigned((Single)10));
            Assert.AreEqual((UInt64)10, C.ToUnsigned((Double)10));
            Assert.AreEqual((UInt64)10, C.ToUnsigned((Decimal)10));
            Assert.AreEqual((UInt32)10, C.ToUnsigned((Single)10.345));
            Assert.AreEqual((UInt64)10, C.ToUnsigned((Double)10.345));
            Assert.AreEqual((UInt64)10, C.ToUnsigned((Decimal)10.345));
            Assert.AreEqual(null, C.ToUnsigned((Char)10));

            Assert.AreEqual((byte)(246), C.ToUnsigned((sbyte)(-10)));
            Assert.AreEqual((UInt16)(65526), C.ToUnsigned((Int16)(-10)));
            Assert.AreEqual((UInt32)(4294967286), C.ToUnsigned((Int32)(-10)));
            Assert.AreEqual((UInt64)(18446744073709551606), C.ToUnsigned((Int64)(-10)));
            Assert.AreEqual((UInt32)(4294967286), C.ToUnsigned((Single)(-10)));
            Assert.AreEqual((UInt64)(18446744073709551606), C.ToUnsigned((Double)(-10)));
            //Assert.AreEqual((UInt64)(18446744073709551606), C.ToUnsigned((Decimal)(-10)));
            Assert.AreEqual((UInt32)(4294967286), C.ToUnsigned((Single)(-10.345)));
            Assert.AreEqual((UInt64)(18446744073709551606), C.ToUnsigned((Double)(-10.345)));
            //Assert.AreEqual((UInt64)(18446744073709551606), C.ToUnsigned((Decimal)(-10.345)));

            Assert.AreEqual(null, C.ToUnsigned(""));
            Assert.AreEqual(null, C.ToUnsigned(""));
        }

        [TestMethod]
        public void TestUnboxToLong() {

            Assert.AreEqual((sbyte)0, C.UnboxToLong((sbyte)0, true));
            Assert.AreEqual((byte)0, C.UnboxToLong((byte)0, true));
            Assert.AreEqual((Int16)0, C.UnboxToLong((Int16)0, true));
            Assert.AreEqual((UInt16)0, C.UnboxToLong((UInt16)0, true));
            Assert.AreEqual((Int32)0, C.UnboxToLong((Int32)0, true));
            Assert.AreEqual((UInt32)0, C.UnboxToLong((UInt32)0, true));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Int64)0, true));
            Assert.AreEqual((Int64)0, C.UnboxToLong((UInt64)0, true));
            Assert.AreEqual((Int32)0, C.UnboxToLong((Single)0, true));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Double)0, true));
            Assert.AreEqual((Decimal)0, C.UnboxToLong((Decimal)0, true));
            Assert.AreEqual((Int32)0, C.UnboxToLong((Single)0.345, true));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Double)0.345, true));
            Assert.AreEqual((Decimal)0, C.UnboxToLong((Decimal)0.345, true));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Char)0, true));

            Assert.AreEqual((sbyte)0, C.UnboxToLong((sbyte)0, false));
            Assert.AreEqual((byte)0, C.UnboxToLong((byte)0, false));
            Assert.AreEqual((Int16)0, C.UnboxToLong((Int16)0, false));
            Assert.AreEqual((UInt16)0, C.UnboxToLong((UInt16)0, false));
            Assert.AreEqual((Int32)0, C.UnboxToLong((Int32)0, false));
            Assert.AreEqual((UInt32)0, C.UnboxToLong((UInt32)0, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Int64)0, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((UInt64)0, false));
            Assert.AreEqual((Int32)0, C.UnboxToLong((Single)0, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Double)0, false));
            Assert.AreEqual((Decimal)0, C.UnboxToLong((Decimal)0, false));
            Assert.AreEqual((Int32)0, C.UnboxToLong((Single)0.345, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Double)0.345, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Decimal)0.345, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Char)0, false));

            Assert.AreEqual((sbyte)10, C.UnboxToLong((sbyte)10, true));
            Assert.AreEqual((byte)10, C.UnboxToLong((byte)10, true));
            Assert.AreEqual((Int16)10, C.UnboxToLong((Int16)10, true));
            Assert.AreEqual((UInt16)10, C.UnboxToLong((UInt16)10, true));
            Assert.AreEqual((Int32)10, C.UnboxToLong((Int32)10, true));
            Assert.AreEqual((UInt32)10, C.UnboxToLong((UInt32)10, true));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Int64)10, true));
            Assert.AreEqual((Int64)10, C.UnboxToLong((UInt64)10, true));
            Assert.AreEqual((Int32)10, C.UnboxToLong((Single)10, true));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Double)10, true));
            Assert.AreEqual((Decimal)10, C.UnboxToLong((Decimal)10, true));
            Assert.AreEqual((Int32)10, C.UnboxToLong((Single)10.345, true));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Double)10.345, true));
            Assert.AreEqual((Decimal)10, C.UnboxToLong((Decimal)10.345, true));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Char)10, true));

            Assert.AreEqual((sbyte)10, C.UnboxToLong((sbyte)10, false));
            Assert.AreEqual((byte)10, C.UnboxToLong((byte)10, false));
            Assert.AreEqual((Int16)10, C.UnboxToLong((Int16)10, false));
            Assert.AreEqual((UInt16)10, C.UnboxToLong((UInt16)10, false));
            Assert.AreEqual((Int32)10, C.UnboxToLong((Int32)10, false));
            Assert.AreEqual((UInt32)10, C.UnboxToLong((UInt32)10, false));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Int64)10, false));
            Assert.AreEqual((Int64)10, C.UnboxToLong((UInt64)10, false));
            Assert.AreEqual((Int32)10, C.UnboxToLong((Single)10, false));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Double)10, false));
            Assert.AreEqual((Decimal)10, C.UnboxToLong((Decimal)10, false));
            Assert.AreEqual((Int32)10, C.UnboxToLong((Single)10.345, false));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Double)10.345, false));
            Assert.AreEqual((Int64)10, C.UnboxToLong((Decimal)10.345, false));
            Assert.AreEqual((Int64)0, C.UnboxToLong((Char)10, false));

            Assert.AreEqual((sbyte)(-10), C.UnboxToLong((sbyte)(-10), true));
            Assert.AreEqual((Int16)(-10), C.UnboxToLong((Int16)(-10), true));
            Assert.AreEqual((Int32)(-10), C.UnboxToLong((Int32)(-10), true));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Int64)(-10), true));
            Assert.AreEqual((Int32)(-10), C.UnboxToLong((Single)(-10), true));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Double)(-10), true));
            Assert.AreEqual((Decimal)(-10), C.UnboxToLong((Decimal)(-10), true));
            Assert.AreEqual((Int32)(-10), C.UnboxToLong((Single)(-10.345), true));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Double)(-10.345), true));
            Assert.AreEqual((Decimal)(-10), C.UnboxToLong((Decimal)(-10.345), true));

            Assert.AreEqual((sbyte)(-10), C.UnboxToLong((sbyte)(-10), false));
            Assert.AreEqual((Int16)(-10), C.UnboxToLong((Int16)(-10), false));
            Assert.AreEqual((Int32)(-10), C.UnboxToLong((Int32)(-10), false));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Int64)(-10), false));
            Assert.AreEqual((Int32)(-10), C.UnboxToLong((Single)(-10), false));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Double)(-10), false));
            Assert.AreEqual((Decimal)(-10), C.UnboxToLong((Decimal)(-10), false));
            Assert.AreEqual((Int32)(-10), C.UnboxToLong((Single)(-10.345), false));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Double)(-10.345), false));
            Assert.AreEqual((Int64)(-10), C.UnboxToLong((Decimal)(-10.345), false));

            Assert.AreEqual((Int64)0, C.UnboxToLong("", true));
            Assert.AreEqual((Int64)0, C.UnboxToLong("", false));
        }

        [TestMethod]
        public void TestReplaceMetaChars() {
            Assert.AreEqual("", C.ReplaceMetaChars(""));
            Assert.AreEqual("%%", C.ReplaceMetaChars("%%"));
            Assert.AreEqual(" ", C.ReplaceMetaChars(@"\040"));
            Assert.AreEqual("\0", C.ReplaceMetaChars(@"\0"));
            Assert.AreEqual("\a\b\f\v\r\n\tx", C.ReplaceMetaChars(@"\a\b\f\v\r\n\t\x"));
        }

        #endregion

        #region Private Methods
        #region RunTest
        [Ignore()]
        private bool RunTest(string Format, string Wanted, params object[] Parameters) {
            string result = C.sprintf(Format, Parameters);
            Assert.AreEqual(Wanted, result);
            return true;
            /*
            //Console.WriteLine("Format:\t{0,-30}Parameters: {1}\nWanted:\t{2}\nResult:\t{3}",
            //    Format, ShowParameters(Parameters), Wanted, result);
            if (Wanted == null || Wanted == result) {
                //Console.WriteLine();
                return true;
            } else {
                //Console.WriteLine("*** ERROR ***\n");
                return false;
            }*/
        }
        #endregion
        //#region ShowParameters
        //private string ShowParameters(params object[] Parameters) {
        //    string w = String.Empty;

        //    if (Parameters == null)
        //        return "(null)";

        //    foreach (object o in Parameters)
        //        w += (w.Length > 0 ? ", " : "") + (o == null ? "(null)" : o.ToString());
        //    return w;
        //}
        //#endregion
        #endregion
    }
}
