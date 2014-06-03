using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// House calculation
    /// </summary>
    public class SweHouse
    {
        Sweph _Sweph;

        #region Helpers

        /// <summary>
        /// Convert an house system from a char
        /// </summary>
        public static HouseSystem HouseSystemFromChar(char c)
        {
            switch (Char.ToUpper(c))
            {
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
        public static Char HouseSystemToChar(HouseSystem hs)
        {
            switch (hs)
            {
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
        public static String GetHouseSystemName(HouseSystem hs)
        {
            switch (hs)
            {
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

        /// <summary>
        /// New SweHouse
        /// </summary>
        public SweHouse(Sweph sweph)
        {
            _Sweph = sweph;
        }

        /// <summary>
        /// Calculate houses positions
        /// </summary>
        /// <param name="day"></param>
        /// <param name="position"></param>
        /// <param name="hsys"></param>
        /// <returns></returns>
        public Houses.HouseResult Houses(JulianDay day, GeoPosition position, HouseSystem hsys)
        {
            throw new NotImplementedException();
            //            int i, retc = 0;
            //            double armc, eps; double[] nutlo = new double[2];
            var jde = _Sweph.EphemerisTime(day);
            var eps = SweLib.Epsiln(jde, 0) * SweLib.RADTODEG;
            //            SE.SwephLib.swi_nutation(tjde, 0, nutlo);
            //            for (i = 0; i < 2; i++)
            //                nutlo[i] *= SwissEph.RADTODEG;
            //            armc = SE.swe_degnorm(SE.swe_sidtime0(tjd_ut, eps + nutlo[1], nutlo[0]) * 15 + geolon);
            //#if TRACE
            //            //swi_open_trace(NULL);
            //            //if (swi_trace_count <= TRACE_COUNT_MAX) {
            //            //    if (swi_fp_trace_c != NULL) {
            //            //        fputs("\n/*SWE_HOUSES*/\n", swi_fp_trace_c);
            //            //        fprintf(swi_fp_trace_c, "#if 0\n");
            //            //        fprintf(swi_fp_trace_c, "  tjd = %.9f;", tjd_ut);
            //            //        fprintf(swi_fp_trace_c, " geolon = %.9f;", geolon);
            //            //        fprintf(swi_fp_trace_c, " geolat = %.9f;", geolat);
            //            //        fprintf(swi_fp_trace_c, " hsys = %d;\n", hsys);
            //            //        fprintf(swi_fp_trace_c, "  retc = swe_houses(tjd, geolat, geolon, hsys, cusp, ascmc);\n");
            //            //        fprintf(swi_fp_trace_c, "  /* swe_houses calls swe_houses_armc as follows: */\n");
            //            //        fprintf(swi_fp_trace_c, "#endif\n");
            //            //        fflush(swi_fp_trace_c);
            //            //    }
            //            //}
            //#endif
            //            retc = swe_houses_armc(armc, geolat, eps + nutlo[1], hsys, cusp, ascmc);
            //            return retc;
            //        }
        }

    }
}
