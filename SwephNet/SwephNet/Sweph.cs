using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Swiss Ephmeris context
    /// </summary>
    public class Sweph : IDisposable
    {
        private SweDate _Date;

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

        #region Embedded engines

        /// <summary>
        /// Create new SweDate
        /// </summary>
        protected virtual SweDate CreateSweDate() { return new SweDate(); }

        /// <summary>
        /// Date engine
        /// </summary>
        public SweDate Date {
            get { return _Date ?? (_Date = CreateSweDate()); }
        }

        #endregion

    }

}
