using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Swiss Ephemeris context
    /// </summary>
    public partial class SwissEph : IDisposable
    {
        #region Ctors & Dest

        /// <summary>
        /// Create a new context
        /// </summary>
        public SwissEph() {
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
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
