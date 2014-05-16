using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Configuration
    /// </summary>
    public class SweConfig
    {
        /// <summary>
        /// New configuration
        /// </summary>
        public SweConfig() {
            UseEspenakMeeusDeltaT = true;
        }

        /// <summary>
        /// Clone this configuration
        /// </summary>
        /// <returns></returns>
        public SweConfig Clone() {
            return new SweConfig() {
                UseEspenakMeeusDeltaT = this.UseEspenakMeeusDeltaT
            };
        }

        /// <summary>
        /// Get or set if we use Espenak Meeus in DeltaT calculation
        /// </summary>
        public bool UseEspenakMeeusDeltaT { get; set; }

    }

}
