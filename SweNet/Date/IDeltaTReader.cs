using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Date
{

    /// <summary>
    /// Interface for read DeltaT records
    /// </summary>
    public interface IDeltaTReader : Persit.IDataReader
    {
        /// <summary>
        /// Read the next record
        /// </summary>
        /// <returns>Returns the next record or null if it's end of records list</returns>
        Tuple<int, double> Read();
    }

}
