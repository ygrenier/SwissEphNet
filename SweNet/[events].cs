using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Arguments for trace event
    /// </summary>
    public class TraceEventArgs : EventArgs
    {
        /// <summary>
        /// Create new arguments
        /// </summary>
        public TraceEventArgs(String message, bool isCTrace = false) {
            this.Message = message;
            this.IsCTrace = isCTrace;
        }

        /// <summary>
        /// Message
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// Is a C trace ?
        /// </summary>
        public bool IsCTrace { get; private set; }
    }

    /// <summary>
    /// Arguments for print event
    /// </summary>
    public class PrintEventArgs : EventArgs
    {
        /// <summary>
        /// Create new arguments
        /// </summary>
        public PrintEventArgs(String line) {
            this.Line = line;
        }

        /// <summary>
        /// Line printed
        /// </summary>
        public String Line { get; private set; }
    }

    /// <summary>
    /// Arguments for load file event
    /// </summary>
    public class LoadFileEventArgs : EventArgs
    {
        /// <summary>
        /// Create new arguments
        /// </summary>
        public LoadFileEventArgs(String file, String path) {
            this.FileName = file;
            this.Path = path;
            this.File = null;
        }

        /// <summary>
        /// File to load
        /// </summary>
        public String FileName { get; private set; }

        /// <summary>
        /// Path where search file
        /// </summary>
        public String Path { get; private set; }

        /// <summary>
        /// File
        /// </summary>
        public TextReader File { get; set; }

    }

}
