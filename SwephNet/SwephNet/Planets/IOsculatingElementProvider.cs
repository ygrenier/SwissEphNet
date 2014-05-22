using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet.Planets
{

    /// <summary>
    /// Interface for reading an osculating element
    /// </summary>
    public interface IOsculatingElementProvider
    {
        /// <summary>
        /// Find an element
        /// </summary>
        OsculatingElement FindElement(int idPlanet, double julianDay, ref int fict_ifl);
    }

}
