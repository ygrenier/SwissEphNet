using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Arguments for load file event
    /// </summary>
    public class LoadFileEventArgs : EventArgs
    {
        /// <summary>
        /// Create new arguments
        /// </summary>
        public LoadFileEventArgs(String fileName)
        {
            this.FileName = fileName;
            this.File = null;
        }

        /// <summary>
        /// File to load
        /// </summary>
        public String FileName { get; private set; }

        /// <summary>
        /// Stream
        /// </summary>
        public Stream File { get; set; }

    }

}
