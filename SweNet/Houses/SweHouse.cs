using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// House management
    /// </summary>
    public class SweHouse
    {

        #region Helpers

        /// <summary>
        /// Convert an house system from a char
        /// </summary>
        public static HouseSystem HouseSystemFromChar(char c) {
            switch (Char.ToUpper(c)) {
                case 'A': 
                case 'E': return HouseSystem.Equal;
                case 'B': return HouseSystem.Alcabitus;
                case 'C': return HouseSystem.Campanus;
                case 'G': return HouseSystem.GauquelinSector;
                case 'H': return HouseSystem.Horizon;
                case 'M': return HouseSystem.Morinus;
                case 'O': return HouseSystem.Porphyrius;
                case 'R': return HouseSystem.Regiomontanus;
                case 'T': return HouseSystem.PolichPage;
                case 'U': return HouseSystem.KrusinskiPisa;
                case 'V': return HouseSystem.VehlowEqual;
                case 'W': return HouseSystem.WholeSign;
                case 'X': return HouseSystem.MeridianSystem;
                case 'Y': return HouseSystem.APC;
                default: return HouseSystem.Placidus;
            }
        }

        /// <summary>
        /// Convert a char to an house system
        /// </summary>
        public static Char HouseSystemToChar(HouseSystem hs) {
            switch (hs) {
                case HouseSystem.Koch: return 'K';
                case HouseSystem.Porphyrius: return 'O';
                case HouseSystem.Regiomontanus: return 'R';
                case HouseSystem.Campanus: return 'C';
                case HouseSystem.Equal: return 'E';
                case HouseSystem.VehlowEqual: return 'V';
                case HouseSystem.WholeSign: return 'W';
                case HouseSystem.MeridianSystem: return 'X';
                case HouseSystem.Horizon: return 'H';
                case HouseSystem.PolichPage: return 'T';
                case HouseSystem.Alcabitus: return 'B';
                case HouseSystem.Morinus: return 'M';
                case HouseSystem.KrusinskiPisa: return 'U';
                case HouseSystem.GauquelinSector: return 'G';
                case HouseSystem.APC: return 'Y';
                case HouseSystem.Placidus:
                default: return 'P';
            }
        }

        /// <summary>
        /// Returns the name of an house system
        /// </summary>
        public static String GetHouseSystemName(HouseSystem hs) {
            switch (hs) {
                case HouseSystem.Koch: return "Koch";
                case HouseSystem.Porphyrius: return "Porphyrius";
                case HouseSystem.Regiomontanus: return "Regiomontanus";
                case HouseSystem.Campanus: return "Campanus";
                case HouseSystem.Equal: return "Equal";
                case HouseSystem.VehlowEqual: return "Vehlow equal";
                case HouseSystem.WholeSign: return "Whole sign";
                case HouseSystem.MeridianSystem: return "Axial rotation system / Meridian system / Zariel";
                case HouseSystem.Horizon: return "Azimuthal / Horizontal system";
                case HouseSystem.PolichPage: return "Polich/Page (\"topocentric\" system)";
                case HouseSystem.Alcabitus: return "Alcabitus";
                case HouseSystem.Morinus: return "Morinus";
                case HouseSystem.KrusinskiPisa: return "Krusinski-Pisa";
                case HouseSystem.GauquelinSector: return "Gauquelin sector";
                case HouseSystem.APC: return "APC houses";
                case HouseSystem.Placidus:
                default: return "Placidus";
            }
        }

        #endregion

    }

}
