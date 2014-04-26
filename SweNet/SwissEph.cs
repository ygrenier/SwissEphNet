using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Swiss Ephemeris C conversion
    /// </summary>
    public partial class SwissEph : Sweph
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
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing)
                swe_close();
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
