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

    /// <summary>
    /// House system calculation
    /// </summary>
    public enum HouseSystem : sbyte
    {
        Placidus,
        Koch,
        Porphyrius,
        Regiomontanus,
        Campanus,
        Equal,
        VehlowEqual,
        WholeSign,
        MeridianSystem,
        Horizon,
        PolichPage,
        Alcabitus,
        Morinus,
        KrusinskiPisa,
        GauquelinSector,
        APC
    }

    /// <summary>
    /// Zodiacal sign
    /// </summary>
    public enum ZodiacSign
    {
        Aries,
        Taurus,
        Gemini,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius,
        Capricorn,
        Aquarius,
        Pisces
    }

}
