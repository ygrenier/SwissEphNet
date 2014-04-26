using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Persitent
{

    /// <summary>
    /// Interface for data provider
    /// </summary>
    public interface IDataProvider
    {

        /// <summary>
        /// Open a new record DeltaT reader
        /// </summary>
        Date.IRecordDeltaTReader OpenRecordDeltaTReader();

    }

}
