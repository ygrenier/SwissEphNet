using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Models
{
    public class PlanetValues
    {
        public Planet Planet { get; set; }
        public String PlanetName { get; set; }
        public Double HousePosition { get; set; }
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
        public Double Distance { get; set; }
        public Double LongitudeSpeed { get; set; }
        public Double LatitudeSpeed { get; set; }
        public Double DistanceSpeed { get; set; }
        /// <summary>
        /// Error in calculation
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Warning in calculation
        /// </summary>
        public string WarnMessage { get; set; }
    }
}
