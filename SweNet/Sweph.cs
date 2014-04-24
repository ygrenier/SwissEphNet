using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Swiss Ephemeris context
    /// </summary>
    public partial class Sweph : IDisposable
    {

        #region Public constants

        /// <summary>
        /// Current Swiss Ephemeris version
        /// </summary>
        public const String Version = "2.00.00";

        #endregion

        #region Ctors & Dest

        /// <summary>
        /// Create a new context
        /// </summary>
        public Sweph() {
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // TODO Release all resources
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        #endregion

    }

}
