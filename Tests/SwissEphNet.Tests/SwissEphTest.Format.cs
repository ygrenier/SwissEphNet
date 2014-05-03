using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [TestMethod]
        public void TestDMS() {
            using (var sweph=new SwissEph()) {
                Assert.AreEqual("nan", sweph.DMS(double.NaN, 0, false));

                Assert.AreEqual("   0° 0' 0.0000", sweph.DMS(0, 0, false));
                Assert.AreEqual("   0° 0' 0.00000", sweph.DMS(0, 0, true));

                Assert.AreEqual("  12° 0' 0.0000", sweph.DMS(12, 0, false));
                Assert.AreEqual("  12° 0' 0.00000", sweph.DMS(12, 0, true));

                Assert.AreEqual(" -12° 0' 0.0000", sweph.DMS(-12, 0, false));
                Assert.AreEqual(" -12° 0' 0.00000", sweph.DMS(-12, 0, true));

                Assert.AreEqual(" 123° 0' 0.0000", sweph.DMS(123, 0, false));
                Assert.AreEqual(" 123° 0' 0.00000", sweph.DMS(123, 0, true));

                Assert.AreEqual("-123° 0' 0.0000", sweph.DMS(-123, 0, false));
                Assert.AreEqual("-123° 0' 0.00000", sweph.DMS(-123, 0, true));

                Assert.AreEqual("  12° 5'59.10000", sweph.DMS(12.1, 0, false));
                Assert.AreEqual("  12° 9' 0.0000", sweph.DMS(12.15, 0, false));
                Assert.AreEqual("  12°30' 0.00000", sweph.DMS(12.5, 0, true));

                Assert.AreEqual("  98°45'55.5556", sweph.DMS(98.7654321, 0, false));
                Assert.AreEqual("  98°45'55.55556", sweph.DMS(98.7654321, 0, true));

                Assert.AreEqual("  98h45'55.5556", sweph.DMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, false));
                Assert.AreEqual("  98h45'55.55556", sweph.DMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, true));

                Assert.AreEqual("   8 cn 45'55.5556", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC, false));
                Assert.AreEqual("   8 cn 45'55.55556", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC, true));

                Assert.AreEqual("  12°09'17.10000", sweph.DMS(12.155, SwissEph.BIT_LZEROES, false));
                Assert.AreEqual("  12°09'17.100000", sweph.DMS(12.155, SwissEph.BIT_LZEROES, true));

                Assert.AreEqual("  98°45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_SEC, false));
                Assert.AreEqual("  98°45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_SEC, true));

                Assert.AreEqual("  98°46'", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_MIN, false));
                Assert.AreEqual("  98°46'", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_MIN, true));

                Assert.AreEqual("  120ar009'17.10000", sweph.DMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, false));
                Assert.AreEqual("  120ar009'17.100000", sweph.DMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, true));

                Assert.AreEqual("   8 cn 45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, false));
                Assert.AreEqual("   8 cn 45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, true));

                Assert.AreEqual("   8 cn 46", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, false));
                Assert.AreEqual("   8 cn 46", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, true));

            }
        }

        [TestMethod]
        public void TestHMS() {
            using (var sweph = new SwissEph()) {
                Assert.AreEqual("nan", sweph.HMS(double.NaN, 0, false));

                Assert.AreEqual("   0: 0: 0.0", sweph.HMS(0, 0, false));
                Assert.AreEqual("   0: 0: 0.0", sweph.HMS(0, 0, true));

                Assert.AreEqual("  12: 0: 0.0", sweph.HMS(12, 0, false));
                Assert.AreEqual("  12: 0: 0.0", sweph.HMS(12, 0, true));
                                     
                Assert.AreEqual(" -12: 0: 0.0", sweph.HMS(-12, 0, false));
                Assert.AreEqual(" -12: 0: 0.0", sweph.HMS(-12, 0, true));
                                        
                Assert.AreEqual(" 123: 0: 0.0", sweph.HMS(123, 0, false));
                Assert.AreEqual(" 123: 0: 0.0", sweph.HMS(123, 0, true));
                                        
                Assert.AreEqual("-123: 0: 0.0", sweph.HMS(-123, 0, false));
                Assert.AreEqual("-123: 0: 0.0", sweph.HMS(-123, 0, true));
                                        
                Assert.AreEqual("  12: 5:59.1", sweph.HMS(12.1, 0, false));
                Assert.AreEqual("  12: 9: 0.0", sweph.HMS(12.15, 0, false));
                Assert.AreEqual("  12:30: 0.0", sweph.HMS(12.5, 0, true));
                                     
                Assert.AreEqual("  98:45:55.5", sweph.HMS(98.7654321, 0, false));
                Assert.AreEqual("  98:45:55.5", sweph.HMS(98.7654321, 0, true));

                Assert.AreEqual("  98h45'55.5556", sweph.HMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, false));
                Assert.AreEqual("  98h45'55.55556", sweph.HMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, true));

                Assert.AreEqual("   8 cn 45'55.5556", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC, false));
                Assert.AreEqual("   8 cn 45'55.55556", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC, true));

                Assert.AreEqual("  12:09:17.1", sweph.HMS(12.155, SwissEph.BIT_LZEROES, false));
                Assert.AreEqual("  12:09:17.1", sweph.HMS(12.155, SwissEph.BIT_LZEROES, true));

                Assert.AreEqual("  98:45:56", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_SEC, false));
                Assert.AreEqual("  98:45:56", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_SEC, true));

                Assert.AreEqual("  98:46", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_MIN, false));
                Assert.AreEqual("  98:46", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_MIN, true));

                Assert.AreEqual("  120ar009'17.10000", sweph.HMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, false));
                Assert.AreEqual("  120ar009'17.100000", sweph.HMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, true));

                Assert.AreEqual("   8 cn 45'56\"", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, false));
                Assert.AreEqual("   8 cn 45'56\"", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, true));

                Assert.AreEqual("   8 cn 46", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, false));
                Assert.AreEqual("   8 cn 46", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, true));

            }
        }

        [TestMethod]
        public void TestFormatToDegreeMinuteSecond() {
            using (var sweph = new SwissEph()) {
                Assert.AreEqual("nan", sweph.FormatToDegreeMinuteSecond(double.NaN));

                Assert.AreEqual("   0° 0' 0.0000", sweph.FormatToDegreeMinuteSecond(0));
                Assert.AreEqual("  12° 9'18.0000", sweph.FormatToDegreeMinuteSecond(12.155));
                Assert.AreEqual("  98°45'55.5556", sweph.FormatToDegreeMinuteSecond(98.7654321));
                Assert.AreEqual(" -98°45'55.5556", sweph.FormatToDegreeMinuteSecond(-98.7654321));
                Assert.AreEqual(" 198°45'55.5556", sweph.FormatToDegreeMinuteSecond(198.7654321));
                Assert.AreEqual("-198°45'55.5556", sweph.FormatToDegreeMinuteSecond(-198.7654321));

                String format = "[d|dd|ddd|dddd|ddddd]";
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[12|12| 12|  12|   12]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[98|98| 98|  98|   98]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[-98|-98|-98| -98|  -98]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[198|198|198| 198|  198]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[-198|-198|-198|-198| -198]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[D|DD|DDD|DDDD|DDDDD]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[12|12|012|0012|00012]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[98|98|098|0098|00098]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[-98|-98|-98|-098|-0098]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[198|198|198|0198|00198]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[-198|-198|-198|-198|-0198]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[a|aa|aaa|aaaa|aaaaa]";
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[12|12| 12|  12|   12]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[98|98| 98|  98|   98]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[98|98| 98|  98|   98]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[198|198|198| 198|  198]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[198|198|198| 198|  198]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));
                
                format = "[A|AA|AAA|AAAA|AAAAA]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[12|12|012|0012|00012]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[98|98|098|0098|00098]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[98|98|098|0098|00098]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[198|198|198|0198|00198]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[198|198|198|0198|00198]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[n|nn|nnn|nnnn|nnnnn]";
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[3| 3|  3|   3|    3]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[3| 3|  3|   3|    3]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[6| 6|  6|   6|    6]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[6| 6|  6|   6|    6]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[N|NN|NNN|NNNN|NNNNN]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[3|03|003|0003|00003]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[3|03|003|0003|00003]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[6|06|006|0006|00006]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[6|06|006|0006|00006]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[g|gg|ggg|gggg|ggggg]";
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[12|12| 12|  12|   12]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[8| 8|  8|   8|    8]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[8| 8|  8|   8|    8]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[18|18| 18|  18|   18]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[18|18| 18|  18|   18]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[G|GG|GGG|GGGG|GGGGG]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[12|12|012|0012|00012]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[8|08|008|0008|00008]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[8|08|008|0008|00008]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[18|18|018|0018|00018]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[18|18|018|0018|00018]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[m|mm|mmm|mmmm|mmmmm]";
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[9| 9|  9|   9|    9]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[45|45| 45|  45|   45]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[45|45| 45|  45|   45]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[45|45| 45|  45|   45]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[45|45| 45|  45|   45]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[M|MM|MMM|MMMM|MMMMM]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[9|09|009|0009|00009]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[45|45|045|0045|00045]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[45|45|045|0045|00045]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[45|45|045|0045|00045]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[45|45|045|0045|00045]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[s|ss|sss|ssss|sssss]";
                Assert.AreEqual("[0| 0|  0|   0|    0]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[18|18| 18|  18|   18]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[55|55| 55|  55|   55]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[55|55| 55|  55|   55]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[55|55| 55|  55|   55]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[55|55| 55|  55|   55]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[S|SS|SSS|SSSS|SSSSS]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[18|18|018|0018|00018]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[55|55|055|0055|00055]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[55|55|055|0055|00055]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[55|55|055|0055|00055]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[55|55|055|0055|00055]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[p|pp|ppp|pppp|ppppp]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[P|PP|PPP|PPPP|PPPPP]";
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[0|00|000|0000|00000]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[6|56|556|5556|55556]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[z|zz|zzz|zzzz|zzzzz]";
                Assert.AreEqual("[♈|ar|Aries|Aries|Aries]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[♈|ar|Aries|Aries|Aries]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[♋|cn|Cancer|Cancer|Cancer]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[♋|cn|Cancer|Cancer|Cancer]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[♎|li|Libra|Libra|Libra]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[♎|li|Libra|Libra|Libra]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[Z|ZZ|ZZZ|ZZZZ|ZZZZZ]";
                Assert.AreEqual("[♈|ar|Aries|Aries|Aries]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[♈|ar|Aries|Aries|Aries]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[♋|cn|Cancer|Cancer|Cancer]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[♋|cn|Cancer|Cancer|Cancer]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[♎|li|Libra|Libra|Libra]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[♎|li|Libra|Libra|Libra]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

                format = "[-|+]";
                Assert.AreEqual("[| ]", sweph.FormatToDegreeMinuteSecond(0, format));
                Assert.AreEqual("[| ]", sweph.FormatToDegreeMinuteSecond(12.155, format));
                Assert.AreEqual("[| ]", sweph.FormatToDegreeMinuteSecond(98.7654321, format));
                Assert.AreEqual("[-|-]", sweph.FormatToDegreeMinuteSecond(-98.7654321, format));
                Assert.AreEqual("[| ]", sweph.FormatToDegreeMinuteSecond(198.7654321, format));
                Assert.AreEqual("[-|-]", sweph.FormatToDegreeMinuteSecond(-198.7654321, format));

            }

        }

    }
}
