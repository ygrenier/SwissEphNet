using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Planet data management
    /// </summary>
    public class SwePlanet
    {
        private Sweph _Sweph;
        private Dictionary<int, String> _BufferNames = new Dictionary<int, String>();

        // Basic planets names
        static String[] PlanetNames = new String[]{
            "Sun", "Moon", "Mercury", "Venus", "Mars", 
            "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto", 
            "mean Node", "true Node", "mean Apogee", "osc. Apogee", "Earth", 
            "Chiron", "Pholus", "Ceres", "Pallas", "Juno", 
            "Vesta", "intp. Apogee", "intp. Perigee"
        };

        //const string SE_NAME_SUN = "Sun";
        //const string SE_NAME_MOON = "Moon";
        //const string SE_NAME_MERCURY = "Mercury";
        //const string SE_NAME_VENUS = "Venus";
        //const string SE_NAME_MARS = "Mars";
        //const string SE_NAME_JUPITER = "Jupiter";
        //const string SE_NAME_SATURN = "Saturn";
        //const string SE_NAME_URANUS = "Uranus";
        //const string SE_NAME_NEPTUNE = "Neptune";
        //const string SE_NAME_PLUTO = "Pluto";
        //const string SE_NAME_MEAN_NODE = "mean Node";
        //const string SE_NAME_TRUE_NODE = "true Node";
        //const string SE_NAME_MEAN_APOG = "mean Apogee";
        //const string SE_NAME_OSCU_APOG = "osc. Apogee";
        //const string SE_NAME_INTP_APOG = "intp. Apogee";
        //const string SE_NAME_INTP_PERG = "intp. Perigee";
        //const string SE_NAME_EARTH = "Earth";
        //const string SE_NAME_CERES = "Ceres";
        //const string SE_NAME_PALLAS = "Pallas";
        //const string SE_NAME_JUNO = "Juno";
        //const string SE_NAME_VESTA = "Vesta";
        //const string SE_NAME_CHIRON = "Chiron";
        //const string SE_NAME_PHOLUS = "Pholus";

        //const string SE_NAME_CUPIDO = "Cupido";
        //const string SE_NAME_HADES = "Hades";
        //const string SE_NAME_ZEUS = "Zeus";
        //const string SE_NAME_KRONOS = "Kronos";
        //const string SE_NAME_APOLLON = "Apollon";
        //const string SE_NAME_ADMETOS = "Admetos";
        //const string SE_NAME_VULKANUS = "Vulkanus";
        //const string SE_NAME_POSEIDON = "Poseidon";
        //const string SE_NAME_ISIS = "Isis";
        //const string SE_NAME_NIBIRU = "Nibiru";
        //const string SE_NAME_HARRINGTON = "Harrington";
        //const string SE_NAME_NEPTUNE_LEVERRIER = "Leverrier";
        //const string SE_NAME_NEPTUNE_ADAMS = "Adams";
        //const string SE_NAME_PLUTO_LOWELL = "Lowell";
        //const string SE_NAME_PLUTO_PICKERING = "Pickering";
        //const string SE_NAME_VULCAN = "Vulcan";
        //const string SE_NAME_WHITE_MOON = "White Moon";

        /// <summary>
        /// Create a new planet management
        /// </summary>
        /// <param name="sweph"></param>
        public SwePlanet(Sweph sweph) {
            _Sweph = sweph;
        }

        /// <summary>
        /// Returns the name of a fictitious planet
        /// </summary>
        /// <param name="id">Id of the fictitious planet. 0 is the first fictitious id.</param>
        /// <returns>Name of the fictitious planet</returns>
        protected string GetFictitiousName(Int32 id) {
            throw new NotImplementedException("SwePlanet.GetFictitiousName()");
            //string snam = null, serr = null; double dummy = 0; int idummy = 0;
            //if (read_elements_file(ipl, 0, ref dummy, ref dummy,
            //     ref dummy, ref dummy, ref dummy, ref dummy, ref dummy, ref dummy,
            //     ref snam, ref idummy, ref serr) == ERR)
            //    snam = "name not found";
            //return snam;
        }

        /// <summary>
        /// Returns the name of planet <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the planet</param>
        /// <returns>Name of the planet</returns>
        public string GetPlanetName(Planet id) {
            String result = null;
            // Update the id for the asteroid
            if (id == Planet.AsteroidPluto)
                id = Planet.Pluto;
            if (id == Planet.AsteroidCeres)
                id = Planet.Ceres;
            if (id == Planet.AsteroidPallas)
                id = Planet.Pallas;
            if (id == Planet.AsteroidJuno)
                id = Planet.Juno;
            if (id == Planet.AsteroidVesta)
                id = Planet.Vesta;
            if (id == Planet.AsteroidChiron)
                id = Planet.Chiron;
            if (id == Planet.AsteroidPholus)
                id = Planet.Pholus;
            // Check the buffer
            if (_BufferNames.TryGetValue(id, out result))
                return result;
            // Planets names
            if (id >= 0 && id <= Planet.LastPlanet) {
                result = PlanetNames[id];
            } else {
                if (id.IsFictitious) {
                    result = GetFictitiousName(id - Planet.FirstFictitious);
                } else if (id.IsAsteroid) {
                    // TODO Check to implement this from a future 'File Data' ?????
                    ///* if name is already available */
                    //if (ipl == swed.fidat[SEI_FILE_ANY_AST].ipl[0])
                    //    s = swed.fidat[SEI_FILE_ANY_AST].astnam;
                    ///* else try to get it from ephemeris file */
                    //else {
                    //    var retc = sweph(J2000, ipl, SEI_FILE_ANY_AST, 0, null, NO_SAVE, xp, ref sdummy);
                    //    if (retc != ERR && retc != NOT_AVAILABLE)
                    //        s = swed.fidat[SEI_FILE_ANY_AST].astnam;
                    //    else
                    //        s = C.sprintf("%d: not found", ipl - SE_AST_OFFSET);
                    result = String.Format("{0}: not found", id - Planet.FirstAsteroid);
                    //}

                    // If there is a provisional designation only in ephemeris file,
                    // we look for a name in seasnam.txt, which can be updated by
                    // the user.
                    // Some old ephemeris files return a '?' in the first position.
                    // There are still a couple of unnamed bodies that got their
                    // provisional designation before 1925, when the current method
                    // of provisional designations was introduced. They have an 'A'
                    // as the first character, e.g. A924 RC. 
                    // The file seasnam.txt may contain comments starting with '#'.
                    // There must be at least two columns: 
                    // 1. asteroid catalog number
                    // 2. asteroid name
                    // The asteroid number may or may not be in brackets
                    if (!String.IsNullOrEmpty(result) && "?0123456789".Contains(result[0])) {
                        int astId = id - Planet.FirstAsteroid;
                        var reader = _Sweph.DataProvider.OpenAsteroidNameReader();
                        if (reader != null) {
                            Tuple<int,string> aName;
                            while ((aName = reader.Read()) != null) {
                                if (aName.Item1 == id) {
                                    result = aName.Item2;
                                    break;
                                }
                            }
                        }
                    }
                } else {    // If not found return the id
                    result = id.Id.ToString();
                }
            }
            // Save the result in the buffer
            _BufferNames[id] = result;
            return result;
        }
    }

}
