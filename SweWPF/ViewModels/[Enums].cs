using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{
    /// <summary>
    /// Type of date selection
    /// </summary>
    public enum EphemerisDateType
    {
        /// <summary>
        /// UT
        /// </summary>
        UniversalTime,
        /// <summary>
        /// ET
        /// </summary>
        EphemerisTime,
        /// <summary>
        /// JD
        /// </summary>
        JulianDay
    }

    /// <summary>
    /// Mode of Day light
    /// </summary>
    public enum DayLightMode
    {
        /// <summary>
        /// Use the .Net time zone daylight information
        /// </summary>
        DotNet,
        /// <summary>
        /// No day light
        /// </summary>
        Off,
        /// <summary>
        /// With day light
        /// </summary>
        On
    }

}
