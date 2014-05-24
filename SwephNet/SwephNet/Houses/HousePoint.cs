using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// The other house points like ascendant
    /// </summary>
    public class HousePoint
    {
        #region Constants

        /// <summary>
        /// Ascendant
        /// </summary>
        public static HousePoint Ascendant { get { return Points[0]; } }
        /// <summary>
        /// MC
        /// </summary>
        public static HousePoint MC { get { return Points[1]; } }
        /// <summary>
        /// ARMC
        /// </summary>
        public static HousePoint ARMC { get { return Points[2]; } }
        /// <summary>
        /// Vertex
        /// </summary>
        public static HousePoint Vertex { get { return Points[3]; } }
        /// <summary>
        /// Equatorial ascendant
        /// </summary>
        public static HousePoint EquatorialAscendant { get { return Points[4]; } }
        /// <summary>
        /// "co-ascendant" (W. Koch)
        /// </summary>
        public static HousePoint CoAscendantKoch { get { return Points[5]; } }
        /// <summary>
        /// "co-ascendant" (M. Munkasey)
        /// </summary>
        public static HousePoint CoAscendantMunkasey { get { return Points[6]; } }
        /// <summary>
        /// "polar ascendant" (M. Munkasey)
        /// </summary>
        public static HousePoint PolarAcsendant { get { return Points[7]; } }

        #endregion

        /// <summary>
        /// Liste of points
        /// </summary>
        public static HousePoint[] Points { get; private set; }

        /// <summary>
        /// Static constructor
        /// </summary>
        static HousePoint()
        {
            Points = new HousePoint[]{
                new HousePoint(0, "Ascendant"),
                new HousePoint(1, "MC"),
                new HousePoint(2, "ARMC"),
                new HousePoint(3, "Vertex"),
                new HousePoint(4, "Equatorial ascendant"),
                new HousePoint(5, "Co-Ascendant Koch (W. Koch)"),
                new HousePoint(6, "Co-Ascendant (M. Munkasey)"),
                new HousePoint(7, "Polar ascendant (M. Munkasey)")
            };
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        HousePoint(int id, String name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// ID of the point
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the point
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Convert to int
        /// </summary>
        public static implicit operator int(HousePoint point)
        {
            return point.Id;
        }

        /// <summary>
        /// Get the housepoint of an id
        /// </summary>
        public static implicit operator HousePoint(int id)
        {
            if (id < 0 || id >= Points.Length) return null;
            return Points[id];
        }

    }

}
