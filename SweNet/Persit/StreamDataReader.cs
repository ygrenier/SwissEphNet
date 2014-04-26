using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SweNet.Persit
{
    /// <summary>
    /// Abstract for DataReader base on a stream
    /// </summary>
    public abstract class StreamDataReader : IDataReader
    {

        /// <summary>
        /// Create new data reader
        /// </summary>
        /// <param name="stream"></param>
        public StreamDataReader(Stream stream) {
            this.Stream = stream;
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (disposing && Stream != null) {
                Stream.Dispose();
                Stream = null;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Stream
        /// </summary>
        public Stream Stream { get; private set; }
    }

}
