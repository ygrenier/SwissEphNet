using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet.Planets
{
    /// <summary>
    /// Interface for asteroid name provider
    /// </summary>
    public interface IAsteroidNameProvider
    {
        /// <summary>
        /// Find the name of an asteroid
        /// </summary>
        /// <param name="id">Id of the asteroid</param>
        /// <returns>Name of the asteroid or null if not found</returns>
        String FindAsteroidName(int id);
    }
}
