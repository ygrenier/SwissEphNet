using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SwephNet.Planets
{

    /// <summary>
    /// Provide asteroid name from file
    /// </summary>
    public class AsteroidNameFile : IAsteroidNameProvider
    {
        /// <summary>
        /// Name of the asteroid names file 
        /// </summary>
        public const string AsteroidFileName = "seasnam.txt";

        /// <summary>
        /// Create a new AsteroidNameFile
        /// </summary>
        public AsteroidNameFile(IStreamProvider streamProvider)
        {
            this.StreamProvider = streamProvider;
        }

        /// <summary>
        /// Find the name of an asteroid
        /// </summary>
        /// <remarks>
        /// The file seasnam.txt may contain comments starting with '#'.
        /// There must be at least two columns: 
        /// 1. asteroid catalog number
        /// 2. asteroid name
        /// The asteroid number may or may not be in brackets
        /// </remarks>
        /// <param name="id">Id of the asteroid</param>
        /// <returns>Name of the asteroid or null if not found</returns>
        public String FindAsteroidName(int id)
        {
            var file = StreamProvider.LoadFile(AsteroidFileName);
            if (file != null)
            {
                using (var reader = new StreamReader(file))
                {
                    Regex reg = new Regex(@"^[\s\(\{\[]*(\d+)[\s\)\}\]]*(.+)$");
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.TrimStart(' ', '\t', '(', '[', '{');
                        if (String.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;

                        // Parse line
                        var match = reg.Match(line);
                        if (!match.Success) continue;

                        // Read id planet
                        int idPlan = int.Parse(match.Groups[1].Value);

                        // Retourne name if match
                        if (idPlan == id)
                        {
                            return match.Groups[2].Value;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Stream provider
        /// </summary>
        public IStreamProvider StreamProvider { get; private set; }

    }

}
