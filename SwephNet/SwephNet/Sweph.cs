using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Swiss Ephmeris context
    /// </summary>
    public class Sweph :
        IDisposable,
        IStreamProvider,
        ITracer
    {
        private IDependencyContainer _Dependencies;

        #region Ctors & Dest

        /// <summary>
        /// Create a new engine
        /// </summary>
        public Sweph()
        {
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_Dependencies != null)
                {
                    _Dependencies.Dispose();
                }
                _Dependencies = null;
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region Dependency container

        /// <summary>
        /// Get the current container
        /// </summary>
        protected IDependencyContainer GetDependencies()
        {
            if (_Dependencies == null)
            {
                _Dependencies = CreateDependencyContainer();
                BuildDependencies(_Dependencies);
            }
            return _Dependencies;
        }

        /// <summary>
        /// Create a new container
        /// </summary>
        protected virtual IDependencyContainer CreateDependencyContainer()
        {
            return new Dependency.SimpleContainer();
        }

        /// <summary>
        /// Create all dependencies
        /// </summary>
        protected virtual void BuildDependencies(IDependencyContainer container)
        {
            // Register default type
            container.RegisterInstance(this);
            container.RegisterInstance<IStreamProvider>(this);
            container.RegisterInstance<ITracer>(this);
            container.RegisterInstance<IDependencyContainer>(container);
            // Register engines types
            container.Register<SweDate, SweDate>();
            container.Register<SwePlanet, SwePlanet>();
            container.Register<SweHouse, SweHouse>();
        }

        #endregion

        #region File management

        /// <summary>
        /// Load a file
        /// </summary>
        public System.IO.Stream LoadFile(string filename)
        {
            var h = OnLoadFile;
            if (h != null)
            {
                var e = new LoadFileEventArgs(filename);
                h(this, e);
                return e.File;
            }
            return null;
        }

        #endregion

        #region Trace

        /// <summary>
        /// Trace a message
        /// </summary>
        public void Trace(String message)
        {
            var h = OnTrace;
            if (h != null)
            {
                h(this, new TraceEventArgs(message));
            }
        }

        #endregion

        #region Date management

        /// <summary>
        /// Create a Julian Day
        /// </summary>
        public JulianDay JulianDay(int year, int month, int day, double hour, DateCalendar? calendar = null)
        {
            return JulianDay(new UniversalTime(year, month, day, hour), calendar);
        }

        /// <summary>
        /// Create a Julian Day
        /// </summary>
        public JulianDay JulianDay(int year, int month, int day, int hour, int minute, int second, DateCalendar? calendar = null)
        {
            return JulianDay(new UniversalTime(year, month, day, hour, minute, second), calendar);
        }

        /// <summary>
        /// Create a Julian Day
        /// </summary>
        public JulianDay JulianDay(UniversalTime date, DateCalendar? calendar = null)
        {
            return new JulianDay(date, calendar);
        }

        /// <summary>
        /// Get Date UT from Julian Day
        /// </summary>
        public UniversalTime DateUT(JulianDay jd)
        {
            return SweDate.JulianDayToDate(jd.Value, jd.Calendar);
        }

        /// <summary>
        /// Get Date UT from Ephemeris Time
        /// </summary>
        public UniversalTime DateUT(EphemerisTime et)
        {
            return SweDate.JulianDayToDate(et.JulianDay.Value, et.JulianDay.Calendar);
        }

        /// <summary>
        /// Create an Ephemeris Time
        /// </summary>
        public EphemerisTime EphemerisTime(JulianDay day)
        {
            return new EphemerisTime(day, Date.DeltaT(day));
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
        public SweDate Date
        {
            get { return Dependencies.Resolve<SweDate>(); }
        }

        /// <summary>
        /// Planet engine
        /// </summary>
        public SwePlanet Planet
        {
            get { return Dependencies.Resolve<SwePlanet>(); }
        }

        /// <summary>
        /// House engine
        /// </summary>
        public SweHouse House
        {
            get { return Dependencies.Resolve<SweHouse>(); }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when a file is required
        /// </summary>
        public event EventHandler<LoadFileEventArgs> OnLoadFile;

        /// <summary>
        /// Event raised when a message is traced
        /// </summary>
        public event EventHandler<TraceEventArgs> OnTrace;

        #endregion

    }

}
