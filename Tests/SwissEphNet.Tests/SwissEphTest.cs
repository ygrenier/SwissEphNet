using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissEphNet.Tests
{
    [TestClass]
    public partial class SwissEphTest
    {
        [TestMethod]
        public void TestConstructor() {
            using (var target = new SwissEph()) {
                Assert.AreEqual(true, target.ESPENAK_MEEUS_2006);
            }
        }

        [TestMethod]
        public void TestVersion() {
            using (var target = new SwissEph()) {
                Assert.AreEqual("2.00.00", target.swe_version());
                Assert.AreEqual("2.00.00-net-0001", target.swe_dotnet_version());
            }
        }

        [TestMethod]
        public void TestOnLoadFile() {
            // No file loading defined
            using (var target = new SwissEph()) {
                Assert.AreEqual("100: not found", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

            // File loading defined, but file not found
            using (var target = new SwissEph()) {
                target.OnLoadFile += (s, e) => {
                    e.File = null;
                };
                Assert.AreEqual("100: not found", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

            // File loading defined
            using (var target = new SwissEph()) {
                target.OnLoadFile += (s, e) => {
                    if (e.FileName == @"[ephe]\seasnam.txt") {
                        e.File = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(@"
000096  Aegle
000097  Klotho
000098  Ianthe
000099  Dike
000100  Hekate
000101  Helena
000102  Miriam
000103  Hera
"));
                    } else
                        e.File = null;
                };
                Assert.AreEqual("Hekate", target.swe_get_planet_name(SwissEph.SE_AST_OFFSET + 100));
            }

        }

    }
}
