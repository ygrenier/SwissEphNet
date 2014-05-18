using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Interface for loading stream
    /// </summary>
    public interface IStreamProvider
    {

        /// <summary>
        /// Load a file
        /// </summary>
        /// <param name="filename">Name of file to load</param>
        /// <returns>The stream or null if the file not exists</returns>
        Stream LoadFile(String filename);

    }
}
