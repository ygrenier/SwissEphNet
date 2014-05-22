using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet.Planets
{
    /// <summary>
    /// Osculating element informations
    /// </summary>
    public class OsculatingElement
    {
        public String Name { get; set; }
        public double Epoch { get; set; }
        public double Equinox { get; set; }
        public double MeanAnomaly { get; set; }
        public double SemiAxis { get; set; }
        public double Eccentricity { get; set; }
        public double Perihelion { get; set; }
        public double AscendingNode { get; set; }
        public double Inclination { get; set; }
    }
}
