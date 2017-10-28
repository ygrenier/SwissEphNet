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
    /// Issue: https://github.com/ygrenier/SwissEphNet/issues/29
    /// </summary>
    public class Issue29Test
    {
        [Fact]
        public void Test()
        {
            string jplfolder = @"C:\Temp\ephe\jplfiles";
            string file = "de430.eph";
            string eop_today = "eop_1962_today.txt";
            string eop_finals = "eop_finals.txt";

            // Skip test if the file not exists
            if (!File.Exists(Path.Combine(jplfolder, file)))
                return;

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
                    else if (fn == eop_today || fn == eop_finals)
                    {
                        e.File = ResourceFileHelpers.OpenResourceFile(fn);
                    }
                };
                swe.swe_set_ephe_path(jplfolder);

                // The issue raise a FormatException on this instruction
                swe.swe_set_jpl_file(file);
            }
        }

    }
}
