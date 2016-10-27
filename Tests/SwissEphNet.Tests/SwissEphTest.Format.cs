using System;
using Xunit;

namespace SwissEphNet.Tests
{
    partial class SwissEphTest
    {
        [Fact]
        public void TestDMS() {
            using (var sweph=new SwissEph()) {
                Assert.Equal("nan", sweph.DMS(double.NaN, 0, false));

                Assert.Equal("   0° 0' 0.0000", sweph.DMS(0, 0, false));
                Assert.Equal("   0° 0' 0.00000", sweph.DMS(0, 0, true));

                Assert.Equal("  12° 0' 0.0000", sweph.DMS(12, 0, false));
                Assert.Equal("  12° 0' 0.00000", sweph.DMS(12, 0, true));

                Assert.Equal(" -12° 0' 0.0000", sweph.DMS(-12, 0, false));
                Assert.Equal(" -12° 0' 0.00000", sweph.DMS(-12, 0, true));

                Assert.Equal(" 123° 0' 0.0000", sweph.DMS(123, 0, false));
                Assert.Equal(" 123° 0' 0.00000", sweph.DMS(123, 0, true));

                Assert.Equal("-123° 0' 0.0000", sweph.DMS(-123, 0, false));
                Assert.Equal("-123° 0' 0.00000", sweph.DMS(-123, 0, true));

                Assert.Equal("  12° 5'59.10000", sweph.DMS(12.1, 0, false));
                Assert.Equal("  12° 9' 0.0000", sweph.DMS(12.15, 0, false));
                Assert.Equal("  12°30' 0.00000", sweph.DMS(12.5, 0, true));

                Assert.Equal("  98°45'55.5556", sweph.DMS(98.7654321, 0, false));
                Assert.Equal("  98°45'55.55556", sweph.DMS(98.7654321, 0, true));

                Assert.Equal("  98h45'55.5556", sweph.DMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, false));
                Assert.Equal("  98h45'55.55556", sweph.DMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, true));

                Assert.Equal("   8 cn 45'55.5556", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC, false));
                Assert.Equal("   8 cn 45'55.55556", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC, true));

                Assert.Equal("  12°09'17.10000", sweph.DMS(12.155, SwissEph.BIT_LZEROES, false));
                Assert.Equal("  12°09'17.100000", sweph.DMS(12.155, SwissEph.BIT_LZEROES, true));

                Assert.Equal("  98°45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_SEC, false));
                Assert.Equal("  98°45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_SEC, true));

                Assert.Equal("  98°46'", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_MIN, false));
                Assert.Equal("  98°46'", sweph.DMS(98.7654321, SwissEph.BIT_ROUND_MIN, true));

                Assert.Equal("  120ar009'17.10000", sweph.DMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, false));
                Assert.Equal("  120ar009'17.100000", sweph.DMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, true));

                Assert.Equal("   8 cn 45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, false));
                Assert.Equal("   8 cn 45'56\"", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, true));

                Assert.Equal("   8 cn 46", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, false));
                Assert.Equal("   8 cn 46", sweph.DMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, true));

            }
        }

        [Fact]
        public void TestHMS() {
            using (var sweph = new SwissEph()) {
                Assert.Equal("nan", sweph.HMS(double.NaN, 0, false));

                Assert.Equal("   0: 0: 0.0", sweph.HMS(0, 0, false));
                Assert.Equal("   0: 0: 0.0", sweph.HMS(0, 0, true));

                Assert.Equal("  12: 0: 0.0", sweph.HMS(12, 0, false));
                Assert.Equal("  12: 0: 0.0", sweph.HMS(12, 0, true));
                                     
                Assert.Equal(" -12: 0: 0.0", sweph.HMS(-12, 0, false));
                Assert.Equal(" -12: 0: 0.0", sweph.HMS(-12, 0, true));
                                        
                Assert.Equal(" 123: 0: 0.0", sweph.HMS(123, 0, false));
                Assert.Equal(" 123: 0: 0.0", sweph.HMS(123, 0, true));
                                        
                Assert.Equal("-123: 0: 0.0", sweph.HMS(-123, 0, false));
                Assert.Equal("-123: 0: 0.0", sweph.HMS(-123, 0, true));
                                        
                Assert.Equal("  12: 5:59.1", sweph.HMS(12.1, 0, false));
                Assert.Equal("  12: 9: 0.0", sweph.HMS(12.15, 0, false));
                Assert.Equal("  12:30: 0.0", sweph.HMS(12.5, 0, true));
                                     
                Assert.Equal("  98:45:55.5", sweph.HMS(98.7654321, 0, false));
                Assert.Equal("  98:45:55.5", sweph.HMS(98.7654321, 0, true));

                Assert.Equal("  98h45'55.5556", sweph.HMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, false));
                Assert.Equal("  98h45'55.55556", sweph.HMS(98.7654321, SwissEph.SEFLG_EQUATORIAL, true));

                Assert.Equal("   8 cn 45'55.5556", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC, false));
                Assert.Equal("   8 cn 45'55.55556", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC, true));

                Assert.Equal("  12:09:17.1", sweph.HMS(12.155, SwissEph.BIT_LZEROES, false));
                Assert.Equal("  12:09:17.1", sweph.HMS(12.155, SwissEph.BIT_LZEROES, true));

                Assert.Equal("  98:45:56", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_SEC, false));
                Assert.Equal("  98:45:56", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_SEC, true));

                Assert.Equal("  98:46", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_MIN, false));
                Assert.Equal("  98:46", sweph.HMS(98.7654321, SwissEph.BIT_ROUND_MIN, true));

                Assert.Equal("  120ar009'17.10000", sweph.HMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, false));
                Assert.Equal("  120ar009'17.100000", sweph.HMS(12.155, SwissEph.BIT_ZODIAC | SwissEph.BIT_LZEROES, true));

                Assert.Equal("   8 cn 45'56\"", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, false));
                Assert.Equal("   8 cn 45'56\"", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_SEC, true));

                Assert.Equal("   8 cn 46", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, false));
                Assert.Equal("   8 cn 46", sweph.HMS(98.7654321, SwissEph.BIT_ZODIAC | SwissEph.BIT_ROUND_MIN, true));

            }
        }

        [Fact]
        public void TestFormatToDegreeMinuteSecond() {

            Assert.Equal("nan", SwissEph.FormatToDegreeMinuteSecond(double.NaN));

            Assert.Equal("   0° 0' 0.0000", SwissEph.FormatToDegreeMinuteSecond(0));
            Assert.Equal("  12° 9'18.0000", SwissEph.FormatToDegreeMinuteSecond(12.155));
            Assert.Equal("  98°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(98.7654321));
            Assert.Equal(" -98°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(-98.7654321));
            Assert.Equal(" 198°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(198.7654321));
            Assert.Equal("-198°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(-198.7654321));

            String format = "D1";
            Assert.Equal("   0° 0' 0.0000", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("  12° 9'18.0000", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("  98°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal(" -98°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal(" 198°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("-198°45'55.5556", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "D2";
            Assert.Equal("   0° 0' 0\"", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("  12° 9'18\"", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("  98°45'55\"", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal(" -98°45'55\"", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal(" 198°45'55\"", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("-198°45'55\"", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "Z1";
            Assert.Equal(" 0 ar  0' 0.0000", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("12 ar  9'18.0000", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal(" 8 cn 45'55.5556", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal(" 8 cn 45'55.5556", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("18 li 45'55.5556", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("18 li 45'55.5556", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "Z2";
            Assert.Equal(" 0 ar  0' 0\"", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("12 ar  9'18\"", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal(" 8 cn 45'55\"", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal(" 8 cn 45'55\"", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("18 li 45'55\"", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("18 li 45'55\"", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[d|dd|ddd|dddd|ddddd]";
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[12|12| 12|  12|   12]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[98|98| 98|  98|   98]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[-98|-98|-98| -98|  -98]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[198|198|198| 198|  198]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[-198|-198|-198|-198| -198]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[D|DD|DDD|DDDD|DDDDD]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[12|12|012|0012|00012]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[98|98|098|0098|00098]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[-98|-98|-98|-098|-0098]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[198|198|198|0198|00198]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[-198|-198|-198|-198|-0198]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[a|aa|aaa|aaaa|aaaaa]";
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[12|12| 12|  12|   12]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[98|98| 98|  98|   98]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[98|98| 98|  98|   98]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[198|198|198| 198|  198]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[198|198|198| 198|  198]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[A|AA|AAA|AAAA|AAAAA]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[12|12|012|0012|00012]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[98|98|098|0098|00098]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[98|98|098|0098|00098]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[198|198|198|0198|00198]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[198|198|198|0198|00198]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[n|nn|nnn|nnnn|nnnnn]";
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[3| 3|  3|   3|    3]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[3| 3|  3|   3|    3]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[6| 6|  6|   6|    6]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[6| 6|  6|   6|    6]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[N|NN|NNN|NNNN|NNNNN]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[3|03|003|0003|00003]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[3|03|003|0003|00003]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[6|06|006|0006|00006]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[6|06|006|0006|00006]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[g|gg|ggg|gggg|ggggg]";
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[12|12| 12|  12|   12]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[8| 8|  8|   8|    8]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[8| 8|  8|   8|    8]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[18|18| 18|  18|   18]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[18|18| 18|  18|   18]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[G|GG|GGG|GGGG|GGGGG]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[12|12|012|0012|00012]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[8|08|008|0008|00008]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[8|08|008|0008|00008]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[18|18|018|0018|00018]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[18|18|018|0018|00018]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[m|mm|mmm|mmmm|mmmmm]";
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[9| 9|  9|   9|    9]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[45|45| 45|  45|   45]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[45|45| 45|  45|   45]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[45|45| 45|  45|   45]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[45|45| 45|  45|   45]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[M|MM|MMM|MMMM|MMMMM]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[9|09|009|0009|00009]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[45|45|045|0045|00045]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[45|45|045|0045|00045]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[45|45|045|0045|00045]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[45|45|045|0045|00045]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[s|ss|sss|ssss|sssss]";
            Assert.Equal("[0| 0|  0|   0|    0]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[18|18| 18|  18|   18]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[55|55| 55|  55|   55]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[55|55| 55|  55|   55]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[55|55| 55|  55|   55]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[55|55| 55|  55|   55]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[S|SS|SSS|SSSS|SSSSS]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[18|18|018|0018|00018]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[55|55|055|0055|00055]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[55|55|055|0055|00055]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[55|55|055|0055|00055]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[55|55|055|0055|00055]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[p|pp|ppp|pppp|ppppp]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[P|PP|PPP|PPPP|PPPPP]";
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[0|00|000|0000|00000]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[6|56|556|5556|55556]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[z|zz|zzz|zzzz|zzzzz]";
            Assert.Equal("[♈|ar|Aries|Aries|Aries]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[♈|ar|Aries|Aries|Aries]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[♋|cn|Cancer|Cancer|Cancer]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[♋|cn|Cancer|Cancer|Cancer]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[♎|li|Libra|Libra|Libra]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[♎|li|Libra|Libra|Libra]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[Z|ZZ|ZZZ|ZZZZ|ZZZZZ]";
            Assert.Equal("[♈|ar|Aries|Aries|Aries]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[♈|ar|Aries|Aries|Aries]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[♋|cn|Cancer|Cancer|Cancer]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[♋|cn|Cancer|Cancer|Cancer]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[♎|li|Libra|Libra|Libra]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[♎|li|Libra|Libra|Libra]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

            format = "[-|+]";
            Assert.Equal("[| ]", SwissEph.FormatToDegreeMinuteSecond(0, format));
            Assert.Equal("[| ]", SwissEph.FormatToDegreeMinuteSecond(12.155, format));
            Assert.Equal("[| ]", SwissEph.FormatToDegreeMinuteSecond(98.7654321, format));
            Assert.Equal("[-|-]", SwissEph.FormatToDegreeMinuteSecond(-98.7654321, format));
            Assert.Equal("[| ]", SwissEph.FormatToDegreeMinuteSecond(198.7654321, format));
            Assert.Equal("[-|-]", SwissEph.FormatToDegreeMinuteSecond(-198.7654321, format));

        }

    }
}
