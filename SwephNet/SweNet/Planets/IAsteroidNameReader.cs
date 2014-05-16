using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Planets
{
    /// <summary>
    /// Interface for asteroid name reader
    /// </summary>
    public interface IAsteroidNameReader : Persit.IDataReader
    {
        /// <summary>
        /// Read the next name
        /// </summary>
        Tuple<int, String> Read();
    }
}
