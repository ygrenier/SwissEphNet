using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Swiss Ephmeris engine
    /// </summary>
    public class Sweph : IDisposable
    {

        #region Ctors & Dest

        /// <summary>
        /// Create a new engine
        /// </summary>
        public Sweph() {
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // TODO Dispose resources
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
