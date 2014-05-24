using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet.Data
{

    /// <summary>
    /// Planet position
    /// </summary>
    public struct PlanetPosition
    {
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Distance
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Speed longitude
        /// </summary>
        public double LongitudeSpeed { get; set; }
        /// <summary>
        /// Speed latitude
        /// </summary>
        public double LatitudeSpeed { get; set; }
        /// <summary>
        /// Speed distance
        /// </summary>
        public double DistanceSpeed { get; set; }
    }

}
