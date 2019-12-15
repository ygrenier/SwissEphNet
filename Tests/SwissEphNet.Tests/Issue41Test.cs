using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwissEphNet.Tests
{
    /// <summary>
    /// Issue #41 : https://github.com/ygrenier/SwissEphNet/issues/41
    /// </summary>
    public class Issue41Test
    {
        public static IEnumerable<object[]> TestDataFixstar()
        {
            yield return new object[] { "1", 4, "Aldebaran,alTau", null };
            yield return new object[] { "5", 4, "Regulus,alLeo", null };
            yield return new object[] { "10", 4, "Gal. Center,SgrA*", null };
            yield return new object[] { "25", 4, "Mirach,beAnd", null };
            yield return new object[] { "1000", 4, "Samakah,bePsc", null };
            yield return new object[] { "10000", -1, "", "star 10000 not found" };
            yield return new object[] { "aldeb", 4, "Aldebaran,alTau", null };
            yield return new object[] { ",alTau", 4, "Aldebaran,alTau", null };
            yield return new object[] { "aldeb%", -1, "", "star aldeb% not found" };
            yield return new object[] { "Spica", 4, "Spica,alVir", null };
            yield return new object[] { "alVir", -1, "", "star alVir not found" };
            yield return new object[] { ",alVir", 4, "Spica,alVir", null };
        }

        [Theory]
        [MemberData(nameof(TestDataFixstar))]
        public void TestFixstar(string search, int eres, string estar, string error)
        {
            int day = 16, month = 8, year = 1974;
            double time = 0.05;

            using (var swe = new SwissEph())
            {
                swe.OnLoadFile += (s, e) =>
                {
                    string f = e.FileName;
                    string fn = Path.GetFileName(f);
                    if (File.Exists(f))
                    {
                        e.File = new FileStream(f, FileMode.Open, FileAccess.Read);
                    }
                    else
                    {
                        e.File = ResourceFileHelpers.OpenResourceFile(fn);
                    }
                };

                double[] xx = new double[6];

                double tjd = swe.swe_julday(year, month, day, time, SwissEph.SE_GREG_CAL);
                double te = tjd + swe.swe_deltat(tjd);

                string star = search, serr = null;
                int res = swe.swe_fixstar(ref star, te, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.Equal(eres, res);
                if (res == SwissEph.ERR)
                {
                    Assert.Equal(error, serr);
                }
                else
                {
                    Assert.Equal(estar, star);
                }
            }
        }

        public static IEnumerable<object[]> TestDataFixstar2()
        {
            yield return new object[] { "1", 4, ",109Vir", null };
            yield return new object[] { "5", 4, ",13Mon", null };
            yield return new object[] { "10", 4, "Electra,17Tau", null };
            yield return new object[] { "25", 4, ",26UMa", null };
            yield return new object[] { "1000", 4, "Rukbalgethi Genubi,thHer", null };
            yield return new object[] { "10000", -1, "", "error, swe_fixstar(): sequential fixed star number 10000 is not available" };
            yield return new object[] { "aldebaran", 4, "Aldebaran,alTau", null };
            yield return new object[] { "aldeb", -1, "", "error, swe_fixstar(): could not find star name aldeb" };
            yield return new object[] { ",alTau", 4, "Aldebaran,alTau", null };
            yield return new object[] { "aldeb%", 4, "Aldebaran,alTau", null };
            yield return new object[] { "Spica", 4, "Spica,alVir", null };
            yield return new object[] { "alVir", -1, "", "error, swe_fixstar(): could not find star name alvir" };
            yield return new object[] { ",alVir", 4, "Spica,alVir", null };
        }

        [Theory]
        [MemberData(nameof(TestDataFixstar2))]
        public void TestFixstar2(string search, int eres, string estar, string error)
        {
            int day = 16, month = 8, year = 1974;
            double time = 0.05;

            using (var swe = new SwissEph())
            {
                swe.OnLoadFile += (s, e) =>
                {
                    string f = e.FileName;
                    string fn = Path.GetFileName(f);
                    if (File.Exists(f))
                    {
                        e.File = new FileStream(f, FileMode.Open, FileAccess.Read);
                    }
                    else
                    {
                        e.File = ResourceFileHelpers.OpenResourceFile(fn);
                    }
                };

                double[] xx = new double[6];

                double tjd = swe.swe_julday(year, month, day, time, SwissEph.SE_GREG_CAL);
                double te = tjd + swe.swe_deltat(tjd);

                string star = search, serr = null;
                int res = swe.swe_fixstar2(ref star, te, SwissEph.SEFLG_MOSEPH, xx, ref serr);
                Assert.Equal(eres, res);
                if (res == SwissEph.ERR)
                {
                    Assert.Equal(error, serr);
                }
                else
                {
                    Assert.Equal(estar, star);
                }
            }
        }

    }
}
