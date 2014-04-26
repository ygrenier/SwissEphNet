using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Persit
{
    
    /// <summary>
    /// Empty data provider
    /// </summary>
    public class EmptyDataProvider : IDataProvider
    {

        /// <summary>
        /// Open a record DeltaT reader
        /// </summary>
        public Date.IRecordDeltaTReader OpenRecordDeltaTReader() {
            return null;
        }

    }

}
