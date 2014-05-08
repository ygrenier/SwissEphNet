using SweWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Services
{
    /// <summary>
    /// Interface of a Sweph calculation service
    /// </summary>
    public interface ICalcService
    {

        /// <summary>
        /// Make calculation
        /// </summary>
        EphemerisResult Calculate(Configuration config, InputCalculation input);

    }
}
