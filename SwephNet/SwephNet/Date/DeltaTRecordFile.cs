using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SwephNet.Date
{

    /// <summary>
    /// DeltaT record provider from a stream
    /// </summary>
    public class DeltaTRecordFile : IDeltaTRecordProvider
    {
        /// <summary>
        /// Create a new DeltaTRecordFile
        /// </summary>
        /// <param name="streamProvider"></param>
        public DeltaTRecordFile(IStreamProvider streamProvider)
        {
            this.StreamProvider = streamProvider;
        }

        /// <summary>
        /// Read records
        /// </summary>
        public IEnumerable<DeltaTRecord> GetRecords()
        {
            var file = StreamProvider.LoadFile("swe_deltat.txt")
                ?? StreamProvider.LoadFile("sedeltat.txt");
            if (file != null)
            {
                using (var reader = new StreamReader(file))
                {
                    String line;
                    Regex reg = new Regex(@"^(\d{4})\s+(\d+\.\d+)$");
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim(' ', '\t');
                        if (String.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;
                        var match = reg.Match(line);
                        if (!match.Success) continue;
                        int y;
                        if (!int.TryParse(match.Groups[1].Value, out y))
                            continue;
                        double v;
                        if (!double.TryParse(match.Groups[2].Value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                            continue;
                        yield return new DeltaTRecord { Year = y, Value = v };
                    }
                }
            }
        }

        /// <summary>
        /// Stream provider
        /// </summary>
        public IStreamProvider StreamProvider { get; private set; }

    }

}
