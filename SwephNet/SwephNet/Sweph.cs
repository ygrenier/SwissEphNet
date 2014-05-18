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
        private IDependencyContainer _Dependencies;

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
                if (_Dependencies != null) {
                    _Dependencies.Dispose();
                }
                _Dependencies = null;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        #endregion

        #region Dependency container

        /// <summary>
        /// Get the current container
        /// </summary>
        protected IDependencyContainer GetDependencies() {
            if (_Dependencies == null) {
                _Dependencies = CreateDependencyContainer();
                BuildDependencies(_Dependencies);
            }
            return _Dependencies;
        }

        /// <summary>
        /// Create a new container
        /// </summary>
        protected virtual IDependencyContainer CreateDependencyContainer() {
            return new Dependency.SimpleContainer();
        }

        /// <summary>
        /// Create all dependencies
        /// </summary>
        protected virtual void BuildDependencies(IDependencyContainer container) {
            container.RegisterInstance(this);
            container.RegisterInstance<IDependencyContainer>(container);
            container.Register<SweDate, SweDate>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Dependency container
        /// </summary>
        public IDependencyContainer Dependencies { get { return GetDependencies(); } }

        /// <summary>
        /// Date engine
        /// </summary>
        public SweDate Date {
            get { return Dependencies.Resolve<SweDate>(); }
        }

        #endregion

    }

}
