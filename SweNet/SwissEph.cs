using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Swiss Ephemeris C conversion
    /// </summary>
    public partial class SwissEph : IDisposable
    {
        #region Ctors & Dest

        /// <summary>
        /// Create a new context
        /// </summary>
        public SwissEph() {
            pnoint2jpl = PNOINT2JPL.ToArray();
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing)
                swe_close();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        #endregion

        #region Configuration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geopos"></param>
        public void swe_set_topo(GeoPosition geopos) {
            swe_set_topo(geopos.Longitude, geopos.Latitude, geopos.Altitude);
        }

        #endregion

    }

}
