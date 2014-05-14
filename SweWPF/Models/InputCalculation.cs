using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Models
{
    /// <summary>
    /// Input values for calculation
    /// </summary>
    public class InputCalculation
    {

        public InputCalculation() {
            EphemerisMode = SweNet.EphemerisMode.SwissEphemeris;
            JplFile = SwissEphNet.SwissEph.SE_FNAME_DFT;
            Planets = new List<Planet>();
            DateUT = new DateUT(DateTime.Now);
            Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            HouseSystem = HouseSystem.Placidus;
            Planets.AddRange(new Planet[] { 
                Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
                Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
                Planet.MeanNode, Planet.TrueNode,
                Planet.MeanApog, Planet.OscuApog, Planet.Earth
            });
            Planets.AddRange(new Planet[] { Planet.AsAsteroid(433), Planet.AsAsteroid(3045), Planet.AsAsteroid(7066) });
        }

        public EphemerisMode EphemerisMode { get; set; }

        public String JplFile { get; set; }

        public DateUT? DateUT { get; set; }

        public DateUT? DateET { get; set; }

        public JulianDay? JulianDay { get; set; }

        public PositionCenter PositionCenter { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public Latitude Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public Longitude Longitude { get; set; }

        /// <summary>
        /// Altitude
        /// </summary>
        public int Altitude { get; set; }

        /// <summary>
        /// House system
        /// </summary>
        public HouseSystem HouseSystem { get; set; }

        /// <summary>
        /// Planets to calculate
        /// </summary>
        public List<Planet> Planets { get; private set; }

    }
}
