using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Calendar type
    /// </summary>
    public enum DateCalendar
    {
        /// <summary>
        /// Julian
        /// </summary>
        Julian = 0,
        /// <summary>
        /// Gregorian
        /// </summary>
        Gregorian = 1
    }

    /// <summary>
    /// Ephemeris modes
    /// </summary>
    public enum EphemerisMode
    {
        SwissEphemeris,
        Moshier,
        JPL,
    }

    /// <summary>
    /// Position center
    /// </summary>
    public enum PositionCenter
    {
        Geocentric,
        Topocentric,
        Heliocentric,
        Barycentric,
        SiderealFagan,
        SiderealLahiri
    }

}
