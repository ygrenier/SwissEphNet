using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SwephNet.Planets
{

    /// <summary>
    /// Osculating element provider from a stream
    /// </summary>
    public class OsculatingElementFile : IOsculatingElementProvider
    {

        /// <summary>
        /// Name of the fictitious data file
        /// </summary>
        public const string FictitiousFile = "seorbel.txt";

        const int FictitiousGeo = 1;

        /// <summary>
        /// Create a new OsculatingElementFile 
        /// </summary>
        public OsculatingElementFile(IStreamProvider streamProvider)
        {
            this.StreamProvider = streamProvider;
        }

        /// <summary>
        /// Find an element
        /// </summary>
        public OsculatingElement FindElement(int idPlanet, double julianDay, ref int fict_ifl)
        {
            OsculatingElement result = null;
            var file = StreamProvider.LoadFile(FictitiousFile);
            if (file != null)
            {
                using (var reader = new StreamReader(file))
                {
                    // Find the element in the file
                    int iLine = 0, iPlanet = -1;
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        iLine++;
                        // Pass unused lines
                        line = line.Trim(' ', '\t');
                        if (String.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;
                        // Remove comments ending the line
                        int iTmp = line.IndexOf('#');
                        if (iTmp >= 0) line = line.Substring(0, iTmp);
                        // Split parts
                        var parts = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //            serri = C.sprintf("error in file %s, line %7.0f:", SwissEph.SE_FICTFILE, (double)iline);
                        if (parts.Length < 9)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorNineElementsRequired);
                        iPlanet++;
                        if (iPlanet != idPlanet)
                            continue;

                        result = new OsculatingElement();

                        // epoch of elements
                        String sp = parts[0].ToLower();
                        if (sp.StartsWith("j2000"))
                            result.Epoch = SweDate.J2000;
                        else if (sp.StartsWith("b1950"))
                            result.Epoch = SweDate.B1950;
                        else if (sp.StartsWith("j1900"))
                            result.Epoch = SweDate.J1900;
                        else if (sp.StartsWith("j") || sp.StartsWith("b"))
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorInvalidEpoch);
                        else
                            result.Epoch = double.Parse(sp, CultureInfo.InvariantCulture);
                        var tt = julianDay - result.Epoch;

                        // equinox
                        sp = parts[1].TrimStart(' ', '\t').ToLower();
                        if (sp.StartsWith("j2000"))
                            result.Equinox = SweDate.J2000;
                        else if (sp.StartsWith("b1950"))
                            result.Equinox = SweDate.B1950;
                        else if (sp.StartsWith("j1900"))
                            result.Equinox = SweDate.J1900;
                        else if (sp.StartsWith("jdate"))
                            result.Equinox = julianDay;
                        else if (sp.StartsWith("j") || sp.StartsWith("b"))
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorInvalidEquinox);
                        else
                            result.Equinox = double.Parse(sp, CultureInfo.InvariantCulture);

                        // mean anomaly t0
                        double dTmp;
                        var retc = CheckTTerms(tt, parts[2], out dTmp);
                        if (retc <0)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorMeanAnomalyValueInvalid);
                        result.MeanAnomaly = SweLib.DegNorm(dTmp);
                        // if mean anomaly has t terms (which happens with fictitious planet Vulcan), 
                        // we set epoch = tjd, so that no motion will be added anymore equinox = tjd 
                        if (retc == 1)
                        {
                            result.Epoch= julianDay;
                        }
                        result.MeanAnomaly *= SweLib.DEGTORAD;

                        // semi-axis
                        retc = CheckTTerms(tt, parts[3], out dTmp);
                        if (dTmp <= 0 || retc < 0)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorSemiAxisValueInvalid);
                        result.SemiAxis = dTmp;

                        // eccentricity
                        retc = CheckTTerms(tt, parts[4], out dTmp);
                        if (dTmp >= 1 || dTmp < 0 || retc < 0)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorEccentricityValueInvalid);
                        result.Eccentricity = dTmp;

                        // perihelion argument
                        retc = CheckTTerms(tt, parts[5], out dTmp);
                        if (retc < 0)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorPerihelionArgumentValueInvalid);
                        result.Perihelion = SweLib.DegNorm(dTmp);
                        result.Perihelion *= SweLib.DEGTORAD;

                        // node
                        retc = CheckTTerms(tt, parts[6], out dTmp);
                        if (retc < 0)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorNodeValueInvalid);
                        result.AscendingNode = SweLib.DegNorm(dTmp);
                        result.AscendingNode *= SweLib.DEGTORAD;

                        // Inclination
                        retc = CheckTTerms(tt, parts[7], out dTmp);
                        if (retc < 0)
                            throw new SweError(Locales.LSR.Error_ReadingFile, FictitiousFile, iLine, Locales.LSR.Fictitious_ErrorInclinationValueInvalid);
                        result.Inclination = SweLib.DegNorm(dTmp);
                        result.Inclination *= SweLib.DEGTORAD;

                        // planet name
                        result.Name = parts[8].Trim(' ', '\t');

                        // geocentric
                        if (parts.Length > 9)
                        {
                            parts[9] = parts[9].ToLower();
                            if (parts[9].Contains("geo"))
                                fict_ifl |= FictitiousGeo;
                        }
                        break;
                    }
                    if (result == null)
                        throw new SweError(
                            Locales.LSR.Error_ReadingFile, FictitiousFile, iLine,
                            String.Format(Locales.LSR.Fictitious_ErrorElementsNotFound, idPlanet)
                            );
                }
            }
            return result;
        }

        int CheckTTerms(double t, string sinp, out double doutp)
        {
            int i, isgn = 1, z;
            int retc = 0;
            string sp;
            double[] tt = new double[5]; double fac;
            tt[0] = t / 36525;
            tt[1] = tt[0];
            tt[2] = tt[1] * tt[1];
            tt[3] = tt[2] * tt[1];
            tt[4] = tt[3] * tt[1];
            if (sinp.Contains("+") || sinp.Contains("-"))
                retc = 1; // with additional terms
            sp = sinp;
            doutp = 0;
            fac = 1;
            z = 0;
            while (true)
            {
                sp = sp.TrimStart(' ', '\t');
                if (String.IsNullOrWhiteSpace(sp) || sp.StartsWith("+") || sp.StartsWith("-"))
                {
                    if (z > 0)
                        doutp += fac;
                    isgn = 1;
                    if (sp != null && sp.StartsWith("-"))
                        isgn = -1;
                    fac = 1 * isgn;
                    if (String.IsNullOrWhiteSpace(sp))
                        return retc;
                    sp = sp.Substring(1);
                }
                else
                {
                    sp = sp.TrimStart('*', ' ', '\t');
                    if (sp != null && sp.StartsWith("t", StringComparison.OrdinalIgnoreCase))
                    {
                        /* a T */
                        sp = sp.Substring(1);
                        if (sp != null && (sp.StartsWith("+") || sp.StartsWith("-")))
                            fac *= tt[0];
                        else
                        {
                            if (!int.TryParse(sp, out i)) i = 0;
                            if (i <= 4 && i >= 0)
                                fac *= tt[i];
                        }
                    }
                    else
                    {
                        /* a number */
                        int cnt = 0;
                        while (cnt < sp.Length && "0123456789.".IndexOf(sp[cnt++]) >= 0) ;
                        String sval = cnt < 0 ? sp : sp.Substring(0, cnt);
                        var val = double.Parse(sval, CultureInfo.InvariantCulture);
                        if (val != 0 || sp.StartsWith("0"))
                            fac *= val;
                    }
                    if (sp != null)
                    {
                        sp = sp.TrimStart('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.');
                    }
                }
                z++;
            }
            //return retc;	// there have been additional terms
        }

        /// <summary>
        /// Stream provider
        /// </summary>
        public IStreamProvider StreamProvider { get; private set; }

    }
}
