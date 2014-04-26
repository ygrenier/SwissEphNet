using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SweNet.Date
{

    /// <summary>
    /// DeltaT reader from Stream
    /// </summary>
    public class StreamDeltaTReader : Persit.StreamDataReader, IDeltaTReader
    {
        /// <summary>
        /// Create new reader from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public StreamDeltaTReader(Stream stream, Encoding encoding = null)
            : this(new StreamReader(stream, Sweph.CheckEncoding(encoding))) {
        }

        /// <summary>
        /// Create new reader from text reader
        /// </summary>
        /// <param name="reader"></param>
        public StreamDeltaTReader(TextReader reader)
            : base(null) {
            if (reader == null) throw new ArgumentNullException("reader");
            BaseReader = reader;
        }

        /// <summary>
        /// Release resource
        /// </summary>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing && BaseReader != null) {
                BaseReader.Dispose();
                BaseReader = null;
            }
        }

        /// <summary>
        /// Read new record
        /// </summary>
        public Tuple<int, double> Read() {
            if (BaseReader == null) return null;
            // 
            String line;
            Regex reg = new Regex(@"(\d{4})\w+(\d+\.\d+)");
            while ((line = BaseReader.ReadLine()) != null) {
                line = line.Trim(' ', '\t');
                if (String.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;
                var match = reg.Match(line);
                if (!match.Success) continue;
                int y;
                if (!int.TryParse(match.Groups[1].Value, out y))
                    continue;
                double v;
                if (!double.TryParse(match.Groups[2].Value, out v))
                    continue;
                return new Tuple<int, double>(y, v);
            }
            return null;
        }

        /// <summary>
        /// Reader
        /// </summary>
        public System.IO.TextReader BaseReader { get; private set; }

    }

}
