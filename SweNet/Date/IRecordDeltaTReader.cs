using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Date
{
    /// <summary>
    /// Interface for read DeltaT records
    /// </summary>
    public interface IRecordDeltaTReader : Persit.IDataReader
    {
        /// <summary>
        /// Read the next RecordDeltaT
        /// </summary>
        /// <returns>Returns the next record or null if it's end of records list</returns>
        RecordDeltaT Read();
    }
}
