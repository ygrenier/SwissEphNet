using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Library
    /// </summary>
    public class SweLib
    {
        #region Embedded types

        /// <summary>
        /// Precession coefficients for remote past and future.
        /// </summary>
        public enum PrecessionCoefficients
        {
            Vondrak2011,
            Williams1994,
            Simon1994,
            Laskar1986,
            Bretagnon2003,
        }

        /// <summary>
        /// IAU precession 1976 or 2003 for recent centuries.
        /// </summary>
        public enum PrecessionIAU
        {
            None,
            IAU_1976,
            IAU_2000,
            /// <summary>
            /// precession model P03
            /// </summary>
            IAU_2006
        }

        #endregion

        #region Constants

        /// <summary>
        /// Value to convert Degrees to Radian
        /// </summary>
        public const double DEGTORAD = 0.0174532925199433;

        /// <summary>
        /// Value to convert Radian to Degrees
        /// </summary>
        public const double RADTODEG = 57.2957795130823;

        /// <summary>
        /// J2000 +/- two centuries
        /// </summary>
        const double PrecessionIAU_1976_Centuries = 2.0;
        /// <summary>
        /// J2000 +/- two centuries
        /// </summary>
        const double PrecessionIAU_2000_Centuries = 2.0;
        /// <summary>
        /// J2000 +/- 75 centuries
        /// </summary>
        /// <remarks>
        /// we use P03 for whole ephemeris
        /// </remarks>
        const double PrecessionIAU_2006_Centuries = 75.0;

        #endregion

        /// <summary>
        /// Reduce x modulo 360 degrees
        /// </summary>
        public static double DegNorm(double x)
        {
            double y;
            y = (x % 360.0);
            if (Math.Abs(y) < 1e-13) y = 0;	// Alois fix 11-dec-1999
            if (y < 0.0) y += 360.0;
            return (y);
        }

        /// <summary>
        /// Convert a degrees value to radians
        /// </summary>
        public static double DegToRad(double x)
        {
            return x * DEGTORAD;
        }

        #region Precession and ecliptic obliquity

        const double AS2R = (DEGTORAD / 3600.0);
        const double D2PI = Math.PI * 2.0;
        const double EPS0 = (84381.406 * AS2R);
        const int NPOL_PEPS = 4;
        const int NPER_PEPS = 10;
        const int NPOL_PECL = 4;
        const int NPER_PECL = 8;
        const int NPOL_PEQU = 4;
        const int NPER_PEQU = 14;

        /// <summary>
        /// for pre_peps(): polynomials
        /// </summary>
        static double[,] pepol = new double[,]{
          {+8134.017132, +84028.206305},
          {+5043.0520035, +0.3624445},
          {-0.00710733, -0.00004039},
          {+0.000000271, -0.000000110}
        };

        /// <summary>
        /// for pre_peps(): periodics 
        /// </summary>
        static double[,] peper = new double[,]{
          {+409.90, +396.15, +537.22, +402.90, +417.15, +288.92, +4043.00, +306.00, +277.00, +203.00},
          {-6908.287473, -3198.706291, +1453.674527, -857.748557, +1173.231614, -156.981465, +371.836550, -216.619040, +193.691479, +11.891524},
          {+753.872780, -247.805823, +379.471484, -53.880558, -90.109153, -353.600190, -63.115353, -28.248187, +17.703387, +38.911307},
          {-2845.175469, +449.844989, -1255.915323, +886.736783, +418.887514, +997.912441, -240.979710, +76.541307, -36.788069, -170.964086},
          {-1704.720302, -862.308358, +447.832178, -889.571909, +190.402846, -56.564991, -296.222622, -75.859952, +67.473503, +3.014055}
        };

        /// <summary>
        /// for pre_pecl(): polynomials 
        /// </summary>
        static double[,] pqpol = new double[,]{
          {+5851.607687, -1600.886300},
          {-0.1189000, +1.1689818},
          {-0.00028913, -0.00000020},
          {+0.000000101, -0.000000437}
        };

        /// <summary>
        /// for pre_pecl(): periodics
        /// </summary>
        static double[,] pqper = new double[,]{
          {708.15, 2309, 1620, 492.2, 1183, 622, 882, 547},
          {-5486.751211, -17.127623, -617.517403, 413.44294, 78.614193, -180.732815, -87.676083, 46.140315},
          {-684.66156, 2446.28388, 399.671049, -356.652376, -186.387003, -316.80007, 198.296701, 101.135679}, /* typo in publication fixed */
          {667.66673, -2354.886252, -428.152441, 376.202861, 184.778874, 335.321713, -185.138669, -120.97283},
          {-5523.863691, -549.74745, -310.998056, 421.535876, -36.776172, -145.278396, -34.74445, 22.885731}
        };

        /// <summary>
        /// for pre_pequ(): polynomials
        /// </summary>
        static double[,] xypol = new double[,]{
          {+5453.282155, -73750.930350},
          {+0.4252841, -0.7675452},
          {-0.00037173, -0.00018725},
          {-0.000000152, +0.000000231}
        };

        /// <summary>
        /// for pre_pequ(): periodics 
        /// </summary>
        static double[,] xyper = new double[,]{
          {256.75, 708.15, 274.2, 241.45, 2309, 492.2, 396.1, 288.9, 231.1, 1610, 620, 157.87, 220.3, 1200},
          {-819.940624, -8444.676815, 2600.009459, 2755.17563, -167.659835, 871.855056, 44.769698, -512.313065, -819.415595, -538.071099, -189.793622, -402.922932, 179.516345, -9.814756},
          {75004.344875, 624.033993, 1251.136893, -1102.212834, -2660.66498, 699.291817, 153.16722, -950.865637, 499.754645, -145.18821, 558.116553, -23.923029, -165.405086, 9.344131},
          {81491.287984, 787.163481, 1251.296102, -1257.950837, -2966.79973, 639.744522, 131.600209, -445.040117, 584.522874, -89.756563, 524.42963, -13.549067, -210.157124, -44.919798},
          {1558.515853, 7774.939698, -2219.534038, -2523.969396, 247.850422, -846.485643, -1393.124055, 368.526116, 749.045012, 444.704518, 235.934465, 374.049623, -171.33018, -22.899655}
        };

        /// <summary>
        /// return dpre, deps
        /// </summary>
        internal static Tuple<double, double> LdpPeps(double tjd)
        {
            int i;
            double w, a, s, c;
            int npol = NPOL_PEPS;
            int nper = NPER_PEPS;
            double t = (tjd - SweDate.J2000) / 36525.0;
            double p = 0;
            double q = 0;
            // periodic terms
            for (i = 0; i < nper; i++)
            {
                w = D2PI * t;
                a = w / peper[0, i];
                s = Math.Sin(a);
                c = Math.Cos(a);
                p += c * peper[1, i] + s * peper[3, i];
                q += c * peper[2, i] + s * peper[4, i];
            }
            // polynomial terms
            w = 1;
            for (i = 0; i < npol; i++)
            {
                p += pepol[i, 0] * w;
                q += pepol[i, 1] * w;
                w *= t;
            }
            // both to radians
            p *= AS2R;
            q *= AS2R;
            // return
            return new Tuple<double, double>(p, q);
        }

        const double OFFSET_EPS_JPLHORIZONS = (35.95);
        const double DCOR_EPS_JPL_TJD0 = 2437846.5;
        const int NDCOR_EPS_JPL = 51;
        static double[] dcor_eps_jpl = new double[]{
            36.726, 36.627, 36.595, 36.578, 36.640, 36.659, 36.731, 36.765,
            36.662, 36.555, 36.335, 36.321, 36.354, 36.227, 36.289, 36.348, 36.257, 36.163,
            35.979, 35.896, 35.842, 35.825, 35.912, 35.950, 36.093, 36.191, 36.009, 35.943,
            35.875, 35.771, 35.788, 35.753, 35.822, 35.866, 35.771, 35.732, 35.543, 35.498,
            35.449, 35.409, 35.497, 35.556, 35.672, 35.760, 35.596, 35.565, 35.510, 35.394,
            35.385, 35.375, 35.415,
        };

        /// <summary>
        /// Obliquity of the ecliptic at Julian date jd
        /// </summary>
        /// <remarks>
        /// IAU Coefficients are from:
        /// J. H. Lieske, T. Lederle, W. Fricke, and B. Morando,
        /// "Expressions for the Precession Quantities Based upon the IAU
        /// (1976) System of Astronomical Constants,"  Astronomy and Astrophysics
        /// 58, 1-16 (1977).
        /// 
        /// Before or after 200 years from J2000, the formula used is from:
        /// J. Laskar, "Secular terms of classical planetary theories
        /// using the results of general theory," Astronomy and Astrophysics
        /// 157, 59070 (1986).
        /// 
        /// Bretagnon, P. et al.: 2003, "Expressions for Precession Consistent with 
        /// the IAU 2000A Model". A&A 400,785
        /// B03  	84381.4088  	-46.836051*t  	-1667*10-7*t2  	+199911*10-8*t3  	-523*10-9*t4  	-248*10-10*t5  	-3*10-11*t6
        /// C03   84381.406  	-46.836769*t  	-1831*10-7*t2  	+20034*10-7*t3  	-576*10-9*t4  	-434*10-10*t5
        /// 
        /// See precess and page B18 of the Astronomical Almanac.
        /// </remarks>
        internal static double Epsiln(double jd, JPL.JplHorizonMode horizons)
        {
            double eps = 0;
            double T = (jd - 2451545.0) / 36525.0;
            if ((horizons & JPL.JplHorizonMode.JplHorizons) != 0 && IncludeCodeForDpsiDepsIAU1980)
            {
                eps = (((1.813e-3 * T - 5.9e-4) * T - 46.8150) * T + 84381.448) * DEGTORAD / 3600;
            }
            else if ((horizons & JPL.JplHorizonMode.JplApproximate) != 0 && !ApproximateHorizonsAstrodienst)
            {
                eps = (((1.813e-3 * T - 5.9e-4) * T - 46.8150) * T + 84381.448) * DEGTORAD / 3600;
            }
            else if (UsePrecessionIAU == PrecessionIAU.IAU_1976 && Math.Abs(T) <= PrecessionIAU_1976_Centuries)
            {
                eps = (((1.813e-3 * T - 5.9e-4) * T - 46.8150) * T + 84381.448) * DEGTORAD / 3600;
            }
            else if (UsePrecessionIAU == PrecessionIAU.IAU_2000 && Math.Abs(T) <= PrecessionIAU_2000_Centuries)
            {
                eps = (((1.813e-3 * T - 5.9e-4) * T - 46.84024) * T + 84381.406) * DEGTORAD / 3600;
            }
            else if (UsePrecessionIAU == PrecessionIAU.IAU_2006 && Math.Abs(T) <= PrecessionIAU_2006_Centuries)
            {
                eps = (((((-4.34e-8 * T - 5.76e-7) * T + 2.0034e-3) * T - 1.831e-4) * T - 46.836769) * T + 84381.406) * DEGTORAD / 3600.0;
            }
            else if (UsePrecessionCoefficient == PrecessionCoefficients.Bretagnon2003)
            {
                eps = ((((((-3e-11 * T - 2.48e-8) * T - 5.23e-7) * T + 1.99911e-3) * T - 1.667e-4) * T - 46.836051) * T + 84381.40880) * DEGTORAD / 3600.0;
            }
            else if (UsePrecessionCoefficient == PrecessionCoefficients.Simon1994)
            {
                eps = (((((2.5e-8 * T - 5.1e-7) * T + 1.9989e-3) * T - 1.52e-4) * T - 46.80927) * T + 84381.412) * DEGTORAD / 3600.0;
            }
            else if (UsePrecessionCoefficient == PrecessionCoefficients.Williams1994)
            {
                eps = ((((-1.0e-6 * T + 2.0e-3) * T - 1.74e-4) * T - 46.833960) * T + 84381.409) * DEGTORAD / 3600.0;/* */
            }
            else if (UsePrecessionCoefficient == PrecessionCoefficients.Laskar1986)
            {
                T /= 10.0;
                eps = (((((((((2.45e-10 * T + 5.79e-9) * T + 2.787e-7) * T
                + 7.12e-7) * T - 3.905e-5) * T - 2.4967e-3) * T
                - 5.138e-3) * T + 1.99925) * T - 0.0155) * T - 468.093) * T
                + 84381.448;
                eps *= DEGTORAD / 3600.0;
            }
            else
            {
                // Vondrak2011
                var tup = LdpPeps(jd);
                eps = tup.Item2;
                if ((horizons & JPL.JplHorizonMode.JplApproximate) != 0 && !ApproximateHorizonsAstrodienst)
                {
                    double tofs = (jd - DCOR_EPS_JPL_TJD0) / 365.25;
                    double dofs = OFFSET_EPS_JPLHORIZONS;
                    if (tofs < 0)
                    {
                        tofs = 0;
                        dofs = dcor_eps_jpl[0];
                    }
                    else if (tofs >= NDCOR_EPS_JPL - 1)
                    {
                        tofs = NDCOR_EPS_JPL;
                        dofs = dcor_eps_jpl[NDCOR_EPS_JPL - 1];
                    }
                    else
                    {
                        double t0 = (int)tofs;
                        double t1 = t0 + 1;
                        dofs = dcor_eps_jpl[(int)t0];
                        dofs = (tofs - t0) * (dcor_eps_jpl[(int)t0] - dcor_eps_jpl[(int)t1]) + dcor_eps_jpl[(int)t0];
                    }
                    dofs /= (1000.0 * 3600.0);
                    eps += dofs * DEGTORAD;
                }
            }
            return eps;
        }

        #endregion

        #region Nutation in longitude and obliquity

        //internal double[] Nutation(double J, JPL.JplHorizonMode jplMode)
        //{
        //    double[] result;
        //    if ((jplMode & JPL.JplHorizonMode.JplHorizons) != 0 && IncludeCodeForDpsiDepsIAU1980)
        //    {
        //        result = NutationIAU1980(J);
        //    }
        //    else if (NUT_IAU_1980)
        //    {
        //        result = NutationIAU1980(J);
        //    }
        //    else if (NUT_IAU_2000A || NUT_IAU_2000B)
        //    {
        //        result = NutationIAU2000ab(J);
        //        /*if ((iflag & SEFLG_JPLHOR_APPROX) && FRAME_BIAS_APPROX_HORIZONS) {*/
        //        if ((jplMode & JPL.JplHorizonMode.JplApproximate) != 0 && !ApproximateHorizonsAstrodienst)
        //        {
        //            result[0] += -41.7750 / 3600.0 / 1000.0 * DEGTORAD;
        //            result[1] += -6.8192 / 3600.0 / 1000.0 * DEGTORAD;
        //        }
        //    }
        //    if (IncludeCodeForDpsiDepsIAU1980)
        //    {
        //        if ((jplMode & JPL.JplHorizonMode.JplHorizons) != 0)
        //        {
        //            double n = (int)(swed.eop_tjd_end - swed.eop_tjd_beg + 0.000001);
        //            double J2 = J;
        //            if (J < swed.eop_tjd_beg_horizons)
        //                J2 = swed.eop_tjd_beg_horizons;
        //            double dpsi = bessel(swed.dpsi, n + 1, J2 - swed.eop_tjd_beg);
        //            double deps = bessel(swed.deps, n + 1, J2 - swed.eop_tjd_beg);
        //            result[0] += dpsi / 3600.0 * DEGTORAD;
        //            result[1] += deps / 3600.0 * DEGTORAD;
        //        }
        //    }
        //    return result;
        //}

        #endregion

        #region Parameters

        /// <summary>
        /// Precession coefficients for remote past and future
        /// </summary>
        public static PrecessionCoefficients UsePrecessionCoefficient = PrecessionCoefficients.Vondrak2011;

        /// <summary>
        /// IAU precession 1976 or 2003 for recent centuries.
        /// </summary>
        public static PrecessionIAU UsePrecessionIAU = PrecessionIAU.None;

        /// <summary>
        /// You can set the latter false if you do not want to compile the
        /// code required to reproduce JPL Horizons.
        /// Keep it TRUE in order to reproduce JPL Horizons following
        /// IERS Conventions 1996 (1992), p. 22. Call swe_calc_ut() with 
        /// iflag|SEFLG_JPLHOR.  This options runs only, if the files 
        /// DPSI_DEPS_IAU1980_FILE_EOPC04 and DPSI_DEPS_IAU1980_FILE_FINALS
        /// are in the ephemeris path.
        /// </summary>
        public static bool IncludeCodeForDpsiDepsIAU1980 = true;

        /// <summary>
        /// If the above define INCLUDE_CODE_FOR_DPSI_DEPS_IAU1980 is FALSE or 
        /// the software does not find the earth orientation files (see above)
        /// in the ephemeris path, then SEFLG_JPLHOR will run as 
        /// SEFLG_JPLHOR_APPROX.
        /// The following define APPROXIMATE_HORIZONS_ASTRODIENST defines 
        /// the handling of SEFLG_JPLHOR_APPROX.
        /// With this flag, planetary positions are always calculated 
        /// using a recent precession/nutation model.  
        /// If APPROXIMATE_HORIZONS_ASTRODIENST is FALSE, then the 
        /// frame bias as recommended by IERS Conventions 2003 and 2010
        /// is *not* applied. Instead, dpsi_bias and deps_bias are added to 
        /// nutation. This procedure is found in some older astronomical software.
        /// Equatorial apparent positions will be close to JPL Horizons 
        /// (within a few mas) beetween 1962 and current years. Ecl. longitude 
        /// will be good, latitude bad.
        /// If APPROXIMATE_HORIZONS_ASTRODIENST is TRUE, the approximation of 
        /// JPL Horizons is even better. Frame bias matrix is applied with
        /// some correction to RA and another correction is added to epsilon.
        /// </summary>
        public static bool ApproximateHorizonsAstrodienst = true;

        /// <summary>
        /// The latter, if combined with SEFLG_JPLHOR provides good agreement 
        /// with JPL Horizons for 1800 - today. However, Horizons uses correct
        /// dpsi and deps only after 20-jan-1962. For all dates before that
        /// it uses dpsi and deps of 20-jan-1962, which provides a continuous 
        /// ephemeris, but does not make sense otherwise. 
        /// Before 1800, even this option does not provide agreement with Horizons,
        /// because Horizons uses a different precession model (Owen 1986) 
        /// before 1800, which is not included in the Swiss Ephemeris.
        /// If this macro is FALSE then the program defaults to SEFLG_JPLHOR_APPROX
        /// outside the time range of correction data dpsi and deps.
        /// Note that this will result in a non-continuous ephemeris near
        /// 20-jan-1962 and current years.
        /// </summary>
        /// <remarks>
        /// Horizons method before 20-jan-1962
        /// </remarks>
        public static bool UseHorizonsMethodBefore1980 = true;

        #endregion

    }
}
