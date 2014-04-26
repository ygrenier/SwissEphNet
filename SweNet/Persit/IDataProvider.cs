using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet.Persit
{

    /// <summary>
    /// Interface for data provider
    /// </summary>
    public interface IDataProvider
    {

        /// <summary>
        /// Open a new DeltaT reader
        /// </summary>
        Date.IDeltaTReader OpenDeltaTReader();

        /// <summary>
        /// Open a new asteroid name reader
        /// </summary>
        Planets.IAsteroidNameReader OpenAsteroidNameReader();

    }

}
