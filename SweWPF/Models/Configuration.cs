using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Models
{

    /// <summary>
    /// Sweph configuration
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// New configuration
        /// </summary>
        public Configuration() {
            SearchPaths = new List<string>();
        }
        /// <summary>
        /// Liste of file search folders
        /// </summary>
        public List<String> SearchPaths { get; private set; }
    }

}
