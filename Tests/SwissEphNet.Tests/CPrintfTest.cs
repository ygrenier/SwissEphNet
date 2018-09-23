using System;
using System.IO;
using Xunit;

namespace SwissEphNet.Tests
{
    
    public class CPrintfTest
    {
        private readonly string sepDecimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private readonly string sep1000 = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

        #region Tests
        #region Special Formats
        [Fact]
        //[TestCategory("Special")]
        //[Description("Special formats %% / %n")]
        public void SpecialFormats() {
            //Console.WriteLine("Test special formats %% / %n");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%%]", "[%]"));
            Assert.True(RunTest("[%n]", "[1]"));
            Assert.True(RunTest("[%%n shows the number of processed chars so far (%010n)]",
                "[%n shows the number of processed chars so far (0000000048)]"));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region PositiveInteger
        [Fact]
        //[TestCategory("Integer")]
        //[Description("Test positive signed integer format %d / %i")]
        public void PositiveInteger() {
            //Console.WriteLine("Test positive signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%d]", "[42]", 42));
            Assert.True(RunTest("[%10d]", "[        42]", 42));
            Assert.True(RunTest("[%-10d]", "[42        ]", 42));
            Assert.True(RunTest("[%010d]", "[0000000042]", 42));
            Assert.True(RunTest("[%-010d]", "[42        ]", 42));
            Assert.True(RunTest("[%+d]", "[+42]", 42));
            Assert.True(RunTest("[%+10d]", "[       +42]", 42));
            Assert.True(RunTest("[%+ 10d]", "[       +42]", 42));
            Assert.True(RunTest("[%-+10d]", "[+42       ]", 42));
            Assert.True(RunTest("[%+010d]", "[+000000042]", 42));
            Assert.True(RunTest("[%-+010d]", "[+42       ]", 42));

            Assert.True(RunTest("[%d]", "[42]", (byte)42));
            Assert.True(RunTest("[%d]", "[42]", (sbyte)42));
            Assert.True(RunTest("[%d]", "[42]", (Int16)42));
            Assert.True(RunTest("[%d]", "[42]", (UInt16)42));
            Assert.True(RunTest("[%d]", "[42]", (UInt32)42));
            Assert.True(RunTest("[%d]", "[42]", (Int64)42));
            Assert.True(RunTest("[%d]", "[42]", (UInt64)42));
            Assert.True(RunTest("[%i]", "[42]", (byte)42));
            //Assert.True(RunTest("[%d]", "[42]", (Char)42));

            Assert.True(RunTest("[%d]", "[65537]", 65537));
            Assert.True(RunTest("[%'d]", String.Format("[65{0}537]", sep1000), 65537));
            Assert.True(RunTest("[%'d]", String.Format("[10{0}065{0}537]", sep1000), 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region ShortInteger
        [Fact]
        public void ShortInteger() {
            //Console.WriteLine("Test positive signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%hd]", "[42]", 42));
            Assert.True(RunTest("[%10hd]", "[        42]", 42));
            Assert.True(RunTest("[%-10hd]", "[42        ]", 42));
            Assert.True(RunTest("[%010hd]", "[0000000042]", 42));
            Assert.True(RunTest("[%-010hd]", "[42        ]", 42));
            Assert.True(RunTest("[%+hd]", "[+42]", 42));
            Assert.True(RunTest("[%+10hd]", "[       +42]", 42));
            Assert.True(RunTest("[%+ 10hd]", "[       +42]", 42));
            Assert.True(RunTest("[%-+10hd]", "[+42       ]", 42));
            Assert.True(RunTest("[%+010hd]", "[+000000042]", 42));
            Assert.True(RunTest("[%-+010hd]", "[+42       ]", 42));

            Assert.True(RunTest("[%hd]", "[42]", (byte)42));
            Assert.True(RunTest("[%hd]", "[42]", (sbyte)42));
            Assert.True(RunTest("[%hd]", "[42]", (Int16)42));
            Assert.True(RunTest("[%hd]", "[42]", (UInt16)42));
            Assert.True(RunTest("[%hd]", "[42]", (UInt32)42));
            Assert.True(RunTest("[%hd]", "[42]", (Int64)42));
            Assert.True(RunTest("[%hd]", "[42]", (UInt64)42));
            //Assert.True(RunTest("[%d]", "[42]", (Char)42));

            Assert.Equal("[1]", C.sprintf("[%hd]", 65537));
            Assert.Equal("[1]", C.sprintf("[%'hd]", 65537));
            Assert.Equal(String.Format("[-27{0}007]", sep1000), C.sprintf("[%'hd]", 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region LongInteger
        [Fact]
        //[TestCategory("Integer")]
        //[Description("Test positive long format %d / %i")]
        public void LongInteger() {
            //Console.WriteLine("Test positive signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%ld]", "[42]", 42));
            Assert.True(RunTest("[%10ld]", "[        42]", 42));
            Assert.True(RunTest("[%-10ld]", "[42        ]", 42));
            Assert.True(RunTest("[%010ld]", "[0000000042]", 42));
            Assert.True(RunTest("[%-010ld]", "[42        ]", 42));
            Assert.True(RunTest("[%+ld]", "[+42]", 42));
            Assert.True(RunTest("[%+10ld]", "[       +42]", 42));
            Assert.True(RunTest("[%+ 10ld]", "[       +42]", 42));
            Assert.True(RunTest("[%-+10ld]", "[+42       ]", 42));
            Assert.True(RunTest("[%+010ld]", "[+000000042]", 42));
            Assert.True(RunTest("[%-+010ld]", "[+42       ]", 42));

            Assert.True(RunTest("[%2ld]", "[42]", 42));
            Assert.True(RunTest("[%-2ld]", "[42]", 42));
            Assert.True(RunTest("[% 2ld]", "[ 42]", 42));
            Assert.True(RunTest("[%02ld]", "[42]", 42));
            Assert.True(RunTest("[%-02ld]", "[42]", 42));
            Assert.True(RunTest("[%+ld]", "[+42]", 42));
            Assert.True(RunTest("[%+2ld]", "[+42]", 42));
            Assert.True(RunTest("[%+ 2ld]", "[+42]", 42));
            Assert.True(RunTest("[%-+2ld]", "[+42]", 42));
            Assert.True(RunTest("[%+02ld]", "[+42]", 42));
            Assert.True(RunTest("[%-+02ld]", "[+42]", 42));

            Assert.True(RunTest("[%ld]", "[42]", (byte)42));
            Assert.True(RunTest("[%ld]", "[42]", (sbyte)42));
            Assert.True(RunTest("[%ld]", "[42]", (Int16)42));
            Assert.True(RunTest("[%ld]", "[42]", (UInt16)42));
            Assert.True(RunTest("[%ld]", "[42]", (UInt32)42));
            Assert.True(RunTest("[%ld]", "[42]", (Int64)42));
            Assert.True(RunTest("[%ld]", "[42]", (UInt64)42));
            //Assert.True(RunTest("[%d]", "[42]", (Char)42));

            Assert.True(RunTest("[%ld]", "[65537]", 65537));
            Assert.True(RunTest("[%'ld]", String.Format("[65{0}537]", sep1000), 65537));
            Assert.True(RunTest("[%'ld]", String.Format("[10{0}065{0}537]", sep1000), 10065537));

            Assert.True(RunTest("[.%04ld]", "[.0042]", 42));
            Assert.True(RunTest("[.%04ld]", "[.0002]", 2));
            Assert.True(RunTest("[.%04ld]", "[.0000]", 0));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region NegativeInteger
        [Fact]
        //[TestCategory("Integer")]
        //[Description("Test negative signed integer format %d / %i")]
        public void NegativeInteger() {
            //Console.WriteLine("Test negative signed integer format %d / %i");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%d]", "[-42]", -42));
            Assert.True(RunTest("[%10d]", "[       -42]", -42));
            Assert.True(RunTest("[%-10d]", "[-42       ]", -42));
            Assert.True(RunTest("[%010d]", "[-000000042]", -42));
            Assert.True(RunTest("[%-010d]", "[-42       ]", -42));
            Assert.True(RunTest("[%+d]", "[-42]", -42));
            Assert.True(RunTest("[%+10d]", "[       -42]", -42));
            Assert.True(RunTest("[%-+10d]", "[-42       ]", -42));
            Assert.True(RunTest("[%+010d]", "[-000000042]", -42));
            Assert.True(RunTest("[%-+010d]", "[-42       ]", -42));

            Assert.True(RunTest("[%d]", "[-42]", (sbyte)(-42)));
            Assert.True(RunTest("[%d]", "[-42]", (Int16)(-42)));
            Assert.True(RunTest("[%d]", "[-42]", (Int64)(-42)));
            //Assert.True(RunTest("[%d]", "[-42]", (Char)(-42)));

            Assert.True(RunTest("[%d]", "[-65537]", -65537));
            Assert.True(RunTest("[%'d]", String.Format("[-65{0}537]", sep1000), -65537));
            Assert.True(RunTest("[%'d]", String.Format("[-10{0}065{0}537]", sep1000), -10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region UnsignedInteger
        [Fact]
        //[TestCategory("Integer")]
        //[Description("Test unsigned integer format %u")]
        public void UnsignedInteger() {
            //Console.WriteLine("Test unsigned integer format %u");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%u]", "[42]", 42));
            Assert.True(RunTest("[%10u]", "[        42]", 42));
            Assert.True(RunTest("[%-10u]", "[42        ]", 42));
            Assert.True(RunTest("[%010u]", "[0000000042]", 42));
            Assert.True(RunTest("[%-010u]", "[42        ]", 42));

            Assert.True(RunTest("[%u]", "[4294967254]", -42));
            Assert.True(RunTest("[%20u]", "[          4294967254]", -42));
            Assert.True(RunTest("[%-20u]", "[4294967254          ]", -42));
            Assert.True(RunTest("[%020u]", "[00000000004294967254]", -42));
            Assert.True(RunTest("[%-020u]", "[4294967254          ]", -42));

            Assert.True(RunTest("[%u]", "[65537]", 65537));
            Assert.True(RunTest("[%'u]", String.Format("[65{0}537]", sep1000), 65537));
            Assert.True(RunTest("[%'u]", String.Format("[10{0}065{0}537]", sep1000), 10065537));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Float
        [Fact]
        //[TestCategory("Float")]
        //[Description("Test float format %f")]
        public void Floats() {
            //Console.WriteLine("Test float format %f");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.Equal("[42]", C.sprintf("[%g]", 42));
            Assert.Equal("[42]", C.sprintf("[%G]", 42));

            Assert.True(RunTest("[%f]", String.Format("[42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%f]", String.Format("[42{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%10f]", String.Format("[ 42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%10f]", String.Format("[ 42{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-10f]", String.Format("[42{0}000000 ]", sepDecimal), 42));
            Assert.True(RunTest("[%-10f]", String.Format("[42{0}500000 ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%010f]", String.Format("[042{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%010f]", String.Format("[042{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-010f]", String.Format("[42{0}000000 ]", sepDecimal), 42));
            Assert.True(RunTest("[%-010f]", String.Format("[42{0}500000 ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%+f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%+f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+10f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%+10f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-10f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%+-10f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+010f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%+010f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-010f]", String.Format("[+42{0}000000]", sepDecimal), 42));
            Assert.True(RunTest("[%+-010f]", String.Format("[+42{0}500000]", sepDecimal), 42.5));

            Assert.True(RunTest("[%f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%-10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%-010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));

            Assert.True(RunTest("[%+f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%+f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%+10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-10f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%+-10f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%+010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-010f]", String.Format("[-42{0}000000]", sepDecimal), -42));
            Assert.True(RunTest("[%+-010f]", String.Format("[-42{0}500000]", sepDecimal), -42.5));

            // -----

            Assert.True(RunTest("[%.2f]", String.Format("[42{0}00]", sepDecimal), 42));
            Assert.True(RunTest("[%.2f]", String.Format("[42{0}50]", sepDecimal), 42.5));
            Assert.True(RunTest("[%10.2f]", String.Format("[     42{0}00]", sepDecimal), 42));
            Assert.True(RunTest("[%10.2f]", String.Format("[     42{0}50]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-10.2f]", String.Format("[42{0}00     ]", sepDecimal), 42));
            Assert.True(RunTest("[%-10.2f]", String.Format("[42{0}50     ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%010.2f]", String.Format("[0000042{0}00]", sepDecimal), 42));
            Assert.True(RunTest("[%010.2f]", String.Format("[0000042{0}50]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-010.2f]", String.Format("[42{0}00     ]", sepDecimal), 42));
            Assert.True(RunTest("[%-010.2f]", String.Format("[42{0}50     ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%+.2f]", String.Format("[+42{0}00]", sepDecimal), 42));
            Assert.True(RunTest("[%+.2f]", String.Format("[+42{0}50]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+10.2f]", String.Format("[    +42{0}00]", sepDecimal), 42));
            Assert.True(RunTest("[%+10.2f]", String.Format("[    +42{0}50]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-10.2f]", String.Format("[+42{0}00    ]", sepDecimal), 42));
            Assert.True(RunTest("[%+-10.2f]", String.Format("[+42{0}50    ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+010.2f]", String.Format("[+000042{0}00]", sepDecimal), 42));
            Assert.True(RunTest("[%+010.2f]", String.Format("[+000042{0}50]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-010.2f]", String.Format("[+42{0}00    ]", sepDecimal), 42));
            Assert.True(RunTest("[%+-010.2f]", String.Format("[+42{0}50    ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%.2f]", String.Format("[-42{0}00]", sepDecimal), -42));
            Assert.True(RunTest("[%.2f]", String.Format("[-42{0}50]", sepDecimal), -42.5));
            Assert.True(RunTest("[%10.2f]", String.Format("[    -42{0}00]", sepDecimal), -42));
            Assert.True(RunTest("[%10.2f]", String.Format("[    -42{0}50]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-10.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.True(RunTest("[%-10.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));
            Assert.True(RunTest("[%010.2f]", String.Format("[-000042{0}00]", sepDecimal), -42));
            Assert.True(RunTest("[%010.2f]", String.Format("[-000042{0}50]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-010.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.True(RunTest("[%-010.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));

            Assert.True(RunTest("[%+.2f]", String.Format("[-42{0}00]", sepDecimal), -42));
            Assert.True(RunTest("[%+.2f]", String.Format("[-42{0}50]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+10.2f]", String.Format("[    -42{0}00]", sepDecimal), -42));
            Assert.True(RunTest("[%+10.2f]", String.Format("[    -42{0}50]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-10.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.True(RunTest("[%+-10.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+010.2f]", String.Format("[-000042{0}00]", sepDecimal), -42));
            Assert.True(RunTest("[%+010.2f]", String.Format("[-000042{0}50]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-010.2f]", String.Format("[-42{0}00    ]", sepDecimal), -42));
            Assert.True(RunTest("[%+-010.2f]", String.Format("[-42{0}50    ]", sepDecimal), -42.5));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Exponent
        [Fact]
        //[TestCategory("Exponent")]
        //[Description("Test exponent format %f")]
        public void Exponents() {
            //Console.WriteLine("Test exponent format %f");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%e]", String.Format("[4{0}200000e+001]", sepDecimal), 42));
            Assert.True(RunTest("[%e]", String.Format("[4{0}250000e+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%20e]", String.Format("[       4{0}200000e+001]", sepDecimal), 42));
            Assert.True(RunTest("[%20e]", String.Format("[       4{0}250000e+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-20e]", String.Format("[4{0}200000e+001       ]", sepDecimal), 42));
            Assert.True(RunTest("[%-20e]", String.Format("[4{0}250000e+001       ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%020e]", String.Format("[00000004{0}200000e+001]", sepDecimal), 42));
            Assert.True(RunTest("[%020e]", String.Format("[00000004{0}250000e+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-020e]", String.Format("[4{0}200000e+001       ]", sepDecimal), 42));
            Assert.True(RunTest("[%-020e]", String.Format("[4{0}250000e+001       ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%+E]", String.Format("[+4{0}200000E+001]", sepDecimal), 42));
            Assert.True(RunTest("[%+E]", String.Format("[+4{0}250000E+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+20E]", String.Format("[      +4{0}200000E+001]", sepDecimal), 42));
            Assert.True(RunTest("[%+20E]", String.Format("[      +4{0}250000E+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-20E]", String.Format("[+4{0}200000E+001      ]", sepDecimal), 42));
            Assert.True(RunTest("[%+-20E]", String.Format("[+4{0}250000E+001      ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+020E]", String.Format("[+0000004{0}200000E+001]", sepDecimal), 42));
            Assert.True(RunTest("[%+020E]", String.Format("[+0000004{0}250000E+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-020E]", String.Format("[+4{0}200000E+001      ]", sepDecimal), 42));
            Assert.True(RunTest("[%+-020E]", String.Format("[+4{0}250000E+001      ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%e]", String.Format("[-4{0}200000e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%e]", String.Format("[-4{0}250000e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%20e]", String.Format("[      -4{0}200000e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%20e]", String.Format("[      -4{0}250000e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-20e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.True(RunTest("[%-20e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));
            Assert.True(RunTest("[%020e]", String.Format("[-0000004{0}200000e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%020e]", String.Format("[-0000004{0}250000e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-020e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.True(RunTest("[%-020e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));

            Assert.True(RunTest("[%+e]", String.Format("[-4{0}200000e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%+e]", String.Format("[-4{0}250000e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+20e]", String.Format("[      -4{0}200000e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%+20e]", String.Format("[      -4{0}250000e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-20e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.True(RunTest("[%+-20e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+020e]", String.Format("[-0000004{0}200000e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%+020e]", String.Format("[-0000004{0}250000e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-020e]", String.Format("[-4{0}200000e+001      ]", sepDecimal), -42));
            Assert.True(RunTest("[%+-020e]", String.Format("[-4{0}250000e+001      ]", sepDecimal), -42.5));

            // -----

            Assert.True(RunTest("[%.2e]", String.Format("[4{0}20e+001]", sepDecimal), 42));
            Assert.True(RunTest("[%.2e]", String.Format("[4{0}25e+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%20.2e]", String.Format("[           4{0}20e+001]", sepDecimal), 42));
            Assert.True(RunTest("[%20.2e]", String.Format("[           4{0}25e+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-20.2e]", String.Format("[4{0}20e+001           ]", sepDecimal), 42));
            Assert.True(RunTest("[%-20.2e]", String.Format("[4{0}25e+001           ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%020.2e]", String.Format("[000000000004{0}20e+001]", sepDecimal), 42));
            Assert.True(RunTest("[%020.2e]", String.Format("[000000000004{0}25e+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%-020.2e]", String.Format("[4{0}20e+001           ]", sepDecimal), 42));
            Assert.True(RunTest("[%-020.2e]", String.Format("[4{0}25e+001           ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%+.2E]", String.Format("[+4{0}20E+001]", sepDecimal), 42));
            Assert.True(RunTest("[%+.2E]", String.Format("[+4{0}25E+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+20.2E]", String.Format("[          +4{0}20E+001]", sepDecimal), 42));
            Assert.True(RunTest("[%+20.2E]", String.Format("[          +4{0}25E+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-20.2E]", String.Format("[+4{0}20E+001          ]", sepDecimal), 42));
            Assert.True(RunTest("[%+-20.2E]", String.Format("[+4{0}25E+001          ]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+020.2E]", String.Format("[+00000000004{0}20E+001]", sepDecimal), 42));
            Assert.True(RunTest("[%+020.2E]", String.Format("[+00000000004{0}25E+001]", sepDecimal), 42.5));
            Assert.True(RunTest("[%+-020.2E]", String.Format("[+4{0}20E+001          ]", sepDecimal), 42));
            Assert.True(RunTest("[%+-020.2E]", String.Format("[+4{0}25E+001          ]", sepDecimal), 42.5));

            Assert.True(RunTest("[%.2e]", String.Format("[-4{0}20e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%.2e]", String.Format("[-4{0}25e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%20.2e]", String.Format("[          -4{0}20e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%20.2e]", String.Format("[          -4{0}25e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-20.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.True(RunTest("[%-20.2e]", String.Format("[-4{0}25e+001          ]", sepDecimal), -42.5));
            Assert.True(RunTest("[%020.2e]", String.Format("[-00000000004{0}20e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%020.2e]", String.Format("[-00000000004{0}25e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%-020.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.True(RunTest("[%-020.2e]", String.Format("[-4{0}25e+001          ]", sepDecimal), -42.5));

            Assert.True(RunTest("[%+.2e]", String.Format("[-4{0}20e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%+.2e]", String.Format("[-4{0}25e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+20.2e]", String.Format("[          -4{0}20e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%+20.2e]", String.Format("[          -4{0}25e+001]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+-20.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.True(RunTest("[%+-20.2e]", String.Format("[-4{0}25e+001          ]", sepDecimal), -42.5));
            Assert.True(RunTest("[%+020.2e]", String.Format("[-00000000004{0}20e+001]", sepDecimal), -42));
            Assert.True(RunTest("[%+020.2e]", String.Format("[-00000000004{0}25e+001]", sepDecimal), -42.545));
            Assert.True(RunTest("[%+-020.2e]", String.Format("[-4{0}20e+001          ]", sepDecimal), -42));
            Assert.True(RunTest("[%+-020.2e]", String.Format("[-4{0}26e+001          ]", sepDecimal), -42.555));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Character
        [Fact]
        //[TestCategory("Character")]
        //[Description("Character format %c")]
        public void CharacterFormat() {
            //Console.WriteLine("Test character formats %c");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%c]", "[]", null));
            Assert.True(RunTest("[%c]", "[A]", 'A'));
            Assert.True(RunTest("[%c]", "[A]", "A Test"));
            Assert.True(RunTest("[%c]", "[A]", 65));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Pointer
        [Fact]
        //[TestCategory("Pointer")]
        //[Description("Pointer format %p")]
        public void PointerFormat() {
            Assert.Equal("[]", C.sprintf("[%p]", null));
            Assert.Equal("[0x0]", C.sprintf("[%p]", IntPtr.Zero));
            Assert.Equal("[0x7b]", C.sprintf("[%p]", (IntPtr)123));
            Assert.Equal("[0xffffffffffffff85]", C.sprintf("[%p]", (IntPtr)(-123)));
            Assert.Equal("[%P]", C.sprintf("[%P]", (IntPtr)123));
        }
        #endregion
        #region Strings
        [Fact]
        //[TestCategory("String")]
        //[Description("Test string format %s")]
        public void Strings() {
            //Console.WriteLine("Test string format %s");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%s]", "[This is a test]", "This is a test"));
            Assert.True(RunTest("[%s]", "[A test with %]", "A test with %"));
            Assert.True(RunTest("[%s]", "[A test with %s inside]", "A test with %s inside"));
            Assert.True(RunTest("[%% %s %%]", "[% % Another test % %]", "% Another test %"));
            Assert.True(RunTest("[%20s]", "[       a long string]", "a long string"));
            Assert.True(RunTest("[%-20s]", "[a long string       ]", "a long string"));
            Assert.True(RunTest("[%020s]", "[0000000a long string]", "a long string"));
            Assert.True(RunTest("[%-020s]", "[a long string       ]", "a long string"));

            Assert.True(RunTest("[%.10s]", "[This is a ]", "This is a shortened string"));
            Assert.True(RunTest("[%20.10s]", "[          This is a ]", "This is a shortened string"));
            Assert.True(RunTest("[%-20.10s]", "[This is a           ]", "This is a shortened string"));
            Assert.True(RunTest("[%020.10s]", "[0000000000This is a ]", "This is a shortened string"));
            Assert.True(RunTest("[%-020.10s]", "[This is a           ]", "This is a shortened string"));

            //C.printf("Account balance: %'+20.2f\n", 12345678);

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Hex
        [Fact]
        //[TestCategory("HEX")]
        //[Description("Test hex format %x / %X")]
        public void Hex() {
            //Console.WriteLine("Test hex format %x / %X");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%x]", "[2a]", 42));
            Assert.True(RunTest("[%X]", "[2A]", 42));
            Assert.True(RunTest("[%5x]", "[   2a]", 42));
            Assert.True(RunTest("[%5X]", "[   2A]", 42));
            Assert.True(RunTest("[%05x]", "[0002a]", 42));
            Assert.True(RunTest("[%05X]", "[0002A]", 42));
            Assert.True(RunTest("[%-05x]", "[2a   ]", 42));
            Assert.True(RunTest("[%-05X]", "[2A   ]", 42));

            Assert.True(RunTest("[%#x]", "[0x2a]", 42));
            Assert.True(RunTest("[%#X]", "[0X2A]", 42));
            Assert.True(RunTest("[%#5x]", "[ 0x2a]", 42));
            Assert.True(RunTest("[%#5X]", "[ 0X2A]", 42));
            Assert.True(RunTest("[%#05x]", "[0x02a]", 42));
            Assert.True(RunTest("[%#05X]", "[0X02A]", 42));
            Assert.True(RunTest("[%#-05x]", "[0x2a ]", 42));
            Assert.True(RunTest("[%#-05X]", "[0X2A ]", 42));

            Assert.True(RunTest("[%.2x]", "[05]", 5));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region Octal
        [Fact]
        //[TestCategory("Octal")]
        //[Description("Test octal format %o")]
        public void Octal() {
            //Console.WriteLine("Test octal format %o");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%o]", "[52]", 42));
            Assert.True(RunTest("[%o]", "[52]", 42));
            Assert.True(RunTest("[%5o]", "[   52]", 42));
            Assert.True(RunTest("[%5o]", "[   52]", 42));
            Assert.True(RunTest("[%05o]", "[00052]", 42));
            Assert.True(RunTest("[%05o]", "[00052]", 42));
            Assert.True(RunTest("[%-05o]", "[52   ]", 42));
            Assert.True(RunTest("[%-05o]", "[52   ]", 42));

            Assert.True(RunTest("[%#o]", "[052]", 42));
            Assert.True(RunTest("[%#o]", "[052]", 42));
            Assert.True(RunTest("[%#5o]", "[  052]", 42));
            Assert.True(RunTest("[%#5o]", "[  052]", 42));
            Assert.True(RunTest("[%#05o]", "[00052]", 42));
            Assert.True(RunTest("[%#05o]", "[00052]", 42));
            Assert.True(RunTest("[%#-05o]", "[052  ]", 42));
            Assert.True(RunTest("[%#-05o]", "[052  ]", 42));

            //Console.WriteLine("\n\n");
        }
        #endregion
        #region PositionIndex
        [Fact]
        //[TestCategory("PositionIndex")]
        //[Description("Test position index (n$)")]
        public void PositionIndex() {
            //Console.WriteLine("Test position index (n$)");
            //Console.WriteLine("--------------------------------------------------------------------------------");

            Assert.True(RunTest("[%2$d %1$#x %1$d]", "[17 0x10 16]", 16, 17));

            //Console.WriteLine("\n\n");
        }
        #endregion

        [Fact]
        public void Testfprintf() {
            StringWriter writer = new StringWriter();
            C.fprintf(writer, "%d %c", 1234, 'X');
            Assert.Equal("1234 X", writer.GetStringBuilder().ToString());
        }

        [Fact]
        public void TestIsPositive() {

            Assert.True(C.IsPositive((sbyte)0, true));
            Assert.True(C.IsPositive((byte)0, true));
            Assert.True(C.IsPositive((Int16)0, true));
            Assert.True(C.IsPositive((UInt16)0, true));
            Assert.True(C.IsPositive((Int32)0, true));
            Assert.True(C.IsPositive((UInt32)0, true));
            Assert.True(C.IsPositive((Int64)0, true));
            Assert.True(C.IsPositive((UInt64)0, true));
            Assert.True(C.IsPositive((Single)0, true));
            Assert.True(C.IsPositive((Double)0, true));
            Assert.True(C.IsPositive((Decimal)0, true));
            Assert.True(C.IsPositive((Char)0, true));

            Assert.False(C.IsPositive((sbyte)0, false));
            Assert.False(C.IsPositive((byte)0, false));
            Assert.False(C.IsPositive((Int16)0, false));
            Assert.False(C.IsPositive((UInt16)0, false));
            Assert.False(C.IsPositive((Int32)0, false));
            Assert.False(C.IsPositive((UInt32)0, false));
            Assert.False(C.IsPositive((Int64)0, false));
            Assert.False(C.IsPositive((UInt64)0, false));
            Assert.False(C.IsPositive((Single)0, false));
            Assert.False(C.IsPositive((Double)0, false));
            Assert.False(C.IsPositive((Decimal)0, false));
            Assert.False(C.IsPositive((Char)0, false));

            Assert.True(C.IsPositive((sbyte)10, true));
            Assert.True(C.IsPositive((byte)10, true));
            Assert.True(C.IsPositive((Int16)10, true));
            Assert.True(C.IsPositive((UInt16)10, true));
            Assert.True(C.IsPositive((Int32)10, true));
            Assert.True(C.IsPositive((UInt32)10, true));
            Assert.True(C.IsPositive((Int64)10, true));
            Assert.True(C.IsPositive((UInt64)10, true));
            Assert.True(C.IsPositive((Single)10, true));
            Assert.True(C.IsPositive((Double)10, true));
            Assert.True(C.IsPositive((Decimal)10, true));
            Assert.True(C.IsPositive((Char)10, true));

            Assert.True(C.IsPositive((sbyte)10, false));
            Assert.True(C.IsPositive((byte)10, false));
            Assert.True(C.IsPositive((Int16)10, false));
            Assert.True(C.IsPositive((UInt16)10, false));
            Assert.True(C.IsPositive((Int32)10, false));
            Assert.True(C.IsPositive((UInt32)10, false));
            Assert.True(C.IsPositive((Int64)10, false));
            Assert.True(C.IsPositive((UInt64)10, false));
            Assert.True(C.IsPositive((Single)10, false));
            Assert.True(C.IsPositive((Double)10, false));
            Assert.True(C.IsPositive((Decimal)10, false));
            Assert.True(C.IsPositive((Char)10, false));

            Assert.False(C.IsPositive((sbyte)(-10), true));
            Assert.False(C.IsPositive((Int16)(-10), true));
            Assert.False(C.IsPositive((Int32)(-10), true));
            Assert.False(C.IsPositive((Int64)(-10), true));
            Assert.False(C.IsPositive((Single)(-10), true));
            Assert.False(C.IsPositive((Double)(-10), true));
            Assert.False(C.IsPositive((Decimal)(-10), true));

            Assert.False(C.IsPositive((sbyte)(-10), false));
            Assert.False(C.IsPositive((Int16)(-10), false));
            Assert.False(C.IsPositive((Int32)(-10), false));
            Assert.False(C.IsPositive((Int64)(-10), false));
            Assert.False(C.IsPositive((Single)(-10), false));
            Assert.False(C.IsPositive((Double)(-10), false));
            Assert.False(C.IsPositive((Decimal)(-10), false));

            Assert.False(C.IsPositive("", true));
            Assert.False(C.IsPositive("", false));
        }

        [Fact]
        public void TestToInteger() {

            Assert.Equal((sbyte)0, C.ToInteger((sbyte)0, true));
            Assert.Equal((byte)0, C.ToInteger((byte)0, true));
            Assert.Equal((Int16)0, C.ToInteger((Int16)0, true));
            Assert.Equal((UInt16)0, C.ToInteger((UInt16)0, true));
            Assert.Equal((Int32)0, C.ToInteger((Int32)0, true));
            Assert.Equal((UInt32)0, C.ToInteger((UInt32)0, true));
            Assert.Equal((Int64)0, C.ToInteger((Int64)0, true));
            Assert.Equal((UInt64)0, C.ToInteger((UInt64)0, true));
            Assert.Equal((Int32)0, C.ToInteger((Single)0, true));
            Assert.Equal((Int64)0, C.ToInteger((Double)0, true));
            Assert.Equal((Decimal)0, C.ToInteger((Decimal)0, true));
            Assert.Equal((Int32)0, C.ToInteger((Single)0.345, true));
            Assert.Equal((Int64)0, C.ToInteger((Double)0.345, true));
            Assert.Equal((Decimal)0, C.ToInteger((Decimal)0.345, true));
            Assert.Null(C.ToInteger((Char)0, true));

            Assert.Equal((sbyte)0, C.ToInteger((sbyte)0, false));
            Assert.Equal((byte)0, C.ToInteger((byte)0, false));
            Assert.Equal((Int16)0, C.ToInteger((Int16)0, false));
            Assert.Equal((UInt16)0, C.ToInteger((UInt16)0, false));
            Assert.Equal((Int32)0, C.ToInteger((Int32)0, false));
            Assert.Equal((UInt32)0, C.ToInteger((UInt32)0, false));
            Assert.Equal((Int64)0, C.ToInteger((Int64)0, false));
            Assert.Equal((UInt64)0, C.ToInteger((UInt64)0, false));
            Assert.Equal((Int32)0, C.ToInteger((Single)0, false));
            Assert.Equal((Int64)0, C.ToInteger((Double)0, false));
            Assert.Equal((Decimal)0, C.ToInteger((Decimal)0, false));
            Assert.Equal((Int32)0, C.ToInteger((Single)0.345, false));
            Assert.Equal((Int64)0, C.ToInteger((Double)0.345, false));
            Assert.Equal((Decimal)0.345, C.ToInteger((Decimal)0.345, false));
            Assert.Null(C.ToInteger((Char)0, false));

            Assert.Equal((sbyte)10, C.ToInteger((sbyte)10, true));
            Assert.Equal((byte)10, C.ToInteger((byte)10, true));
            Assert.Equal((Int16)10, C.ToInteger((Int16)10, true));
            Assert.Equal((UInt16)10, C.ToInteger((UInt16)10, true));
            Assert.Equal((Int32)10, C.ToInteger((Int32)10, true));
            Assert.Equal((UInt32)10, C.ToInteger((UInt32)10, true));
            Assert.Equal((Int64)10, C.ToInteger((Int64)10, true));
            Assert.Equal((UInt64)10, C.ToInteger((UInt64)10, true));
            Assert.Equal((Int32)10, C.ToInteger((Single)10, true));
            Assert.Equal((Int64)10, C.ToInteger((Double)10, true));
            Assert.Equal((Decimal)10, C.ToInteger((Decimal)10, true));
            Assert.Equal((Int32)10, C.ToInteger((Single)10.345, true));
            Assert.Equal((Int64)10, C.ToInteger((Double)10.345, true));
            Assert.Equal((Decimal)10, C.ToInteger((Decimal)10.345, true));
            Assert.Null(C.ToInteger((Char)10, true));

            Assert.Equal((sbyte)10, C.ToInteger((sbyte)10, false));
            Assert.Equal((byte)10, C.ToInteger((byte)10, false));
            Assert.Equal((Int16)10, C.ToInteger((Int16)10, false));
            Assert.Equal((UInt16)10, C.ToInteger((UInt16)10, false));
            Assert.Equal((Int32)10, C.ToInteger((Int32)10, false));
            Assert.Equal((UInt32)10, C.ToInteger((UInt32)10, false));
            Assert.Equal((Int64)10, C.ToInteger((Int64)10, false));
            Assert.Equal((UInt64)10, C.ToInteger((UInt64)10, false));
            Assert.Equal((Int32)10, C.ToInteger((Single)10, false));
            Assert.Equal((Int64)10, C.ToInteger((Double)10, false));
            Assert.Equal((Decimal)10, C.ToInteger((Decimal)10, false));
            Assert.Equal((Int32)10, C.ToInteger((Single)10.345, false));
            Assert.Equal((Int64)10, C.ToInteger((Double)10.345, false));
            Assert.Equal((Decimal)10.345, C.ToInteger((Decimal)10.345, false));
            Assert.Null(C.ToInteger((Char)10, false));

            Assert.Equal((sbyte)(-10), C.ToInteger((sbyte)(-10), true));
            Assert.Equal((Int16)(-10), C.ToInteger((Int16)(-10), true));
            Assert.Equal((Int32)(-10), C.ToInteger((Int32)(-10), true));
            Assert.Equal((Int64)(-10), C.ToInteger((Int64)(-10), true));
            Assert.Equal((Int32)(-10), C.ToInteger((Single)(-10), true));
            Assert.Equal((Int64)(-10), C.ToInteger((Double)(-10), true));
            Assert.Equal((Decimal)(-10), C.ToInteger((Decimal)(-10), true));
            Assert.Equal((Int32)(-10), C.ToInteger((Single)(-10.345), true));
            Assert.Equal((Int64)(-10), C.ToInteger((Double)(-10.345), true));
            Assert.Equal((Decimal)(-10), C.ToInteger((Decimal)(-10.345), true));

            Assert.Equal((sbyte)(-10), C.ToInteger((sbyte)(-10), false));
            Assert.Equal((Int16)(-10), C.ToInteger((Int16)(-10), false));
            Assert.Equal((Int32)(-10), C.ToInteger((Int32)(-10), false));
            Assert.Equal((Int64)(-10), C.ToInteger((Int64)(-10), false));
            Assert.Equal((Int32)(-10), C.ToInteger((Single)(-10), false));
            Assert.Equal((Int64)(-10), C.ToInteger((Double)(-10), false));
            Assert.Equal((Decimal)(-10), C.ToInteger((Decimal)(-10), false));
            Assert.Equal((Int32)(-10), C.ToInteger((Single)(-10.345), false));
            Assert.Equal((Int64)(-10), C.ToInteger((Double)(-10.345), false));
            Assert.Equal((Decimal)(-10.345), C.ToInteger((Decimal)(-10.345), false));

            Assert.Null(C.ToInteger("", true));
            Assert.Null(C.ToInteger("", false));
        }

        [Fact]
        public void TestToUnsigned() {

            Assert.Equal((byte)0, C.ToUnsigned((sbyte)0));
            Assert.Equal((byte)0, C.ToUnsigned((byte)0));
            Assert.Equal((UInt16)0, C.ToUnsigned((Int16)0));
            Assert.Equal((UInt16)0, C.ToUnsigned((UInt16)0));
            Assert.Equal((UInt32)0, C.ToUnsigned((Int32)0));
            Assert.Equal((UInt32)0, C.ToUnsigned((UInt32)0));
            Assert.Equal((UInt64)0, C.ToUnsigned((Int64)0));
            Assert.Equal((UInt64)0, C.ToUnsigned((UInt64)0));
            Assert.Equal((UInt32)0, C.ToUnsigned((Single)0));
            Assert.Equal((UInt64)0, C.ToUnsigned((Double)0));
            Assert.Equal((UInt64)0, C.ToUnsigned((Decimal)0));
            Assert.Equal((UInt32)0, C.ToUnsigned((Single)0.345));
            Assert.Equal((UInt64)0, C.ToUnsigned((Double)0.345));
            Assert.Equal((UInt64)0, C.ToUnsigned((Decimal)0.345));
            Assert.Null(C.ToUnsigned((Char)0));

            Assert.Equal((byte)10, C.ToUnsigned((sbyte)10));
            Assert.Equal((byte)10, C.ToUnsigned((byte)10));
            Assert.Equal((UInt16)10, C.ToUnsigned((Int16)10));
            Assert.Equal((UInt16)10, C.ToUnsigned((UInt16)10));
            Assert.Equal((UInt32)10, C.ToUnsigned((Int32)10));
            Assert.Equal((UInt32)10, C.ToUnsigned((UInt32)10));
            Assert.Equal((UInt64)10, C.ToUnsigned((Int64)10));
            Assert.Equal((UInt64)10, C.ToUnsigned((UInt64)10));
            Assert.Equal((UInt32)10, C.ToUnsigned((Single)10));
            Assert.Equal((UInt64)10, C.ToUnsigned((Double)10));
            Assert.Equal((UInt64)10, C.ToUnsigned((Decimal)10));
            Assert.Equal((UInt32)10, C.ToUnsigned((Single)10.345));
            Assert.Equal((UInt64)10, C.ToUnsigned((Double)10.345));
            Assert.Equal((UInt64)10, C.ToUnsigned((Decimal)10.345));
            Assert.Null(C.ToUnsigned((Char)10));

            Assert.Equal((byte)(246), C.ToUnsigned((sbyte)(-10)));
            Assert.Equal((UInt16)(65526), C.ToUnsigned((Int16)(-10)));
            Assert.Equal((UInt32)(4294967286), C.ToUnsigned((Int32)(-10)));
            Assert.Equal((UInt64)(18446744073709551606), C.ToUnsigned((Int64)(-10)));
            Assert.Equal((UInt32)(4294967286), C.ToUnsigned((Single)(-10)));
            Assert.Equal((UInt64)(18446744073709551606), C.ToUnsigned((Double)(-10)));
            //Assert.Equal((UInt64)(18446744073709551606), C.ToUnsigned((Decimal)(-10)));
            Assert.Equal((UInt32)(4294967286), C.ToUnsigned((Single)(-10.345)));
            Assert.Equal((UInt64)(18446744073709551606), C.ToUnsigned((Double)(-10.345)));
            //Assert.Equal((UInt64)(18446744073709551606), C.ToUnsigned((Decimal)(-10.345)));

            Assert.Null(C.ToUnsigned(""));
            Assert.Null(C.ToUnsigned(""));
        }

        [Fact]
        public void TestUnboxToLong() {

            Assert.Equal((sbyte)0, C.UnboxToLong((sbyte)0, true));
            Assert.Equal((byte)0, C.UnboxToLong((byte)0, true));
            Assert.Equal((Int16)0, C.UnboxToLong((Int16)0, true));
            Assert.Equal((UInt16)0, C.UnboxToLong((UInt16)0, true));
            Assert.Equal((Int32)0, C.UnboxToLong((Int32)0, true));
            Assert.Equal((UInt32)0, C.UnboxToLong((UInt32)0, true));
            Assert.Equal((Int64)0, C.UnboxToLong((Int64)0, true));
            Assert.Equal((Int64)0, C.UnboxToLong((UInt64)0, true));
            Assert.Equal((Int32)0, C.UnboxToLong((Single)0, true));
            Assert.Equal((Int64)0, C.UnboxToLong((Double)0, true));
            Assert.Equal((Decimal)0, C.UnboxToLong((Decimal)0, true));
            Assert.Equal((Int32)0, C.UnboxToLong((Single)0.345, true));
            Assert.Equal((Int64)0, C.UnboxToLong((Double)0.345, true));
            Assert.Equal((Decimal)0, C.UnboxToLong((Decimal)0.345, true));
            Assert.Equal((Int64)0, C.UnboxToLong((Char)0, true));

            Assert.Equal((sbyte)0, C.UnboxToLong((sbyte)0, false));
            Assert.Equal((byte)0, C.UnboxToLong((byte)0, false));
            Assert.Equal((Int16)0, C.UnboxToLong((Int16)0, false));
            Assert.Equal((UInt16)0, C.UnboxToLong((UInt16)0, false));
            Assert.Equal((Int32)0, C.UnboxToLong((Int32)0, false));
            Assert.Equal((UInt32)0, C.UnboxToLong((UInt32)0, false));
            Assert.Equal((Int64)0, C.UnboxToLong((Int64)0, false));
            Assert.Equal((Int64)0, C.UnboxToLong((UInt64)0, false));
            Assert.Equal((Int32)0, C.UnboxToLong((Single)0, false));
            Assert.Equal((Int64)0, C.UnboxToLong((Double)0, false));
            Assert.Equal((Decimal)0, C.UnboxToLong((Decimal)0, false));
            Assert.Equal((Int32)0, C.UnboxToLong((Single)0.345, false));
            Assert.Equal((Int64)0, C.UnboxToLong((Double)0.345, false));
            Assert.Equal((Int64)0, C.UnboxToLong((Decimal)0.345, false));
            Assert.Equal((Int64)0, C.UnboxToLong((Char)0, false));

            Assert.Equal((sbyte)10, C.UnboxToLong((sbyte)10, true));
            Assert.Equal((byte)10, C.UnboxToLong((byte)10, true));
            Assert.Equal((Int16)10, C.UnboxToLong((Int16)10, true));
            Assert.Equal((UInt16)10, C.UnboxToLong((UInt16)10, true));
            Assert.Equal((Int32)10, C.UnboxToLong((Int32)10, true));
            Assert.Equal((UInt32)10, C.UnboxToLong((UInt32)10, true));
            Assert.Equal((Int64)10, C.UnboxToLong((Int64)10, true));
            Assert.Equal((Int64)10, C.UnboxToLong((UInt64)10, true));
            Assert.Equal((Int32)10, C.UnboxToLong((Single)10, true));
            Assert.Equal((Int64)10, C.UnboxToLong((Double)10, true));
            Assert.Equal((Decimal)10, C.UnboxToLong((Decimal)10, true));
            Assert.Equal((Int32)10, C.UnboxToLong((Single)10.345, true));
            Assert.Equal((Int64)10, C.UnboxToLong((Double)10.345, true));
            Assert.Equal((Decimal)10, C.UnboxToLong((Decimal)10.345, true));
            Assert.Equal((Int64)0, C.UnboxToLong((Char)10, true));

            Assert.Equal((sbyte)10, C.UnboxToLong((sbyte)10, false));
            Assert.Equal((byte)10, C.UnboxToLong((byte)10, false));
            Assert.Equal((Int16)10, C.UnboxToLong((Int16)10, false));
            Assert.Equal((UInt16)10, C.UnboxToLong((UInt16)10, false));
            Assert.Equal((Int32)10, C.UnboxToLong((Int32)10, false));
            Assert.Equal((UInt32)10, C.UnboxToLong((UInt32)10, false));
            Assert.Equal((Int64)10, C.UnboxToLong((Int64)10, false));
            Assert.Equal((Int64)10, C.UnboxToLong((UInt64)10, false));
            Assert.Equal((Int32)10, C.UnboxToLong((Single)10, false));
            Assert.Equal((Int64)10, C.UnboxToLong((Double)10, false));
            Assert.Equal((Decimal)10, C.UnboxToLong((Decimal)10, false));
            Assert.Equal((Int32)10, C.UnboxToLong((Single)10.345, false));
            Assert.Equal((Int64)10, C.UnboxToLong((Double)10.345, false));
            Assert.Equal((Int64)10, C.UnboxToLong((Decimal)10.345, false));
            Assert.Equal((Int64)0, C.UnboxToLong((Char)10, false));

            Assert.Equal((sbyte)(-10), C.UnboxToLong((sbyte)(-10), true));
            Assert.Equal((Int16)(-10), C.UnboxToLong((Int16)(-10), true));
            Assert.Equal((Int32)(-10), C.UnboxToLong((Int32)(-10), true));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Int64)(-10), true));
            Assert.Equal((Int32)(-10), C.UnboxToLong((Single)(-10), true));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Double)(-10), true));
            Assert.Equal((Decimal)(-10), C.UnboxToLong((Decimal)(-10), true));
            Assert.Equal((Int32)(-10), C.UnboxToLong((Single)(-10.345), true));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Double)(-10.345), true));
            Assert.Equal((Decimal)(-10), C.UnboxToLong((Decimal)(-10.345), true));

            Assert.Equal((sbyte)(-10), C.UnboxToLong((sbyte)(-10), false));
            Assert.Equal((Int16)(-10), C.UnboxToLong((Int16)(-10), false));
            Assert.Equal((Int32)(-10), C.UnboxToLong((Int32)(-10), false));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Int64)(-10), false));
            Assert.Equal((Int32)(-10), C.UnboxToLong((Single)(-10), false));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Double)(-10), false));
            Assert.Equal((Decimal)(-10), C.UnboxToLong((Decimal)(-10), false));
            Assert.Equal((Int32)(-10), C.UnboxToLong((Single)(-10.345), false));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Double)(-10.345), false));
            Assert.Equal((Int64)(-10), C.UnboxToLong((Decimal)(-10.345), false));

            Assert.Equal((Int64)0, C.UnboxToLong("", true));
            Assert.Equal((Int64)0, C.UnboxToLong("", false));
        }

        [Fact]
        public void TestReplaceMetaChars() {
            Assert.Equal("", C.ReplaceMetaChars(""));
            Assert.Equal("%%", C.ReplaceMetaChars("%%"));
            Assert.Equal(" ", C.ReplaceMetaChars(@"\040"));
            Assert.Equal("\0", C.ReplaceMetaChars(@"\0"));
            Assert.Equal("\a\b\f\v\r\n\tx", C.ReplaceMetaChars(@"\a\b\f\v\r\n\t\x"));
        }

        #endregion

        #region Private Methods
        #region RunTest
        //[Ignore()]
        private bool RunTest(string Format, string Wanted, params object[] Parameters) {
            string result = C.sprintf(Format, Parameters);
            Assert.Equal(Wanted, result);
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
