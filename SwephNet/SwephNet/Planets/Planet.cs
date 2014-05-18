using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Planet number
    /// </summary>
    public struct Planet
    {

        #region Planets

        /// <summary>
        /// Sun
        /// </summary>
        public static readonly Planet Sun = 0;
        /// <summary>
        /// Moon
        /// </summary>
        public static readonly Planet Moon = 1;
        /// <summary>
        /// Mercury
        /// </summary>
        public static readonly Planet Mercury = 2;
        /// <summary>
        /// Venus
        /// </summary>
        public static readonly Planet Venus = 3;
        /// <summary>
        /// Mars
        /// </summary>
        public static readonly Planet Mars = 4;
        /// <summary>
        /// Jupiter
        /// </summary>
        public static readonly Planet Jupiter = 5;
        /// <summary>
        /// Saturn
        /// </summary>
        public static readonly Planet Saturn = 6;
        /// <summary>
        /// Uranus
        /// </summary>
        public static readonly Planet Uranus = 7;
        /// <summary>
        /// Neptune
        /// </summary>
        public static readonly Planet Neptune = 8;
        /// <summary>
        /// Pluto
        /// </summary>
        public static readonly Planet Pluto = 9;
        /// <summary>
        /// MeanNode
        /// </summary>
        public static readonly Planet MeanNode = 10;
        /// <summary>
        /// TrueNode
        /// </summary>
        public static readonly Planet TrueNode = 11;
        /// <summary>
        /// MeanApog
        /// </summary>
        public static readonly Planet MeanApog = 12;
        /// <summary>
        /// OscuApog
        /// </summary>
        public static readonly Planet OscuApog = 13;
        /// <summary>
        /// Earth
        /// </summary>
        public static readonly Planet Earth = 14;
        /// <summary>
        /// Chiron
        /// </summary>
        public static readonly Planet Chiron = 15;
        /// <summary>
        /// Pholus
        /// </summary>
        public static readonly Planet Pholus = 16;
        /// <summary>
        /// Ceres
        /// </summary>
        public static readonly Planet Ceres = 17;
        /// <summary>
        /// Pallas
        /// </summary>
        public static readonly Planet Pallas = 18;
        /// <summary>
        /// Juno
        /// </summary>
        public static readonly Planet Juno = 19;
        /// <summary>
        /// Vesta
        /// </summary>
        public static readonly Planet Vesta = 20;
        /// <summary>
        /// IntpApog
        /// </summary>
        public static readonly Planet IntpApog = 21;
        /// <summary>
        /// IntpPerg
        /// </summary>
        public static readonly Planet IntpPerg = 22;

        #endregion

        #region Specials

        /// <summary>
        /// Ecliptic/Nutation
        /// </summary>
        public static readonly Planet EclipticNutation = -1;

        /// <summary>
        /// Fixed star
        /// </summary>
        public static readonly Planet FixedStar = -10;

        #endregion

        #region Fictitious

        #region Hamburger or Uranian "planets"
        public static readonly Planet Cupido = 40;
        public static readonly Planet Hades = 41;
        public static readonly Planet Zeus = 42;
        public static readonly Planet Kronos = 43;
        public static readonly Planet Apollon = 44;
        public static readonly Planet Admetos = 45;
        public static readonly Planet Vulkanus = 46;
        public static readonly Planet Poseidon = 47;
        #endregion

        #region Other fictitious bodies
        public static readonly Planet Isis = 48;
        public static readonly Planet Nibiru = 49;
        public static readonly Planet Harrington = 50;
        public static readonly Planet NeptuneLeverrier = 51;
        public static readonly Planet NeptuneAdams = 52;
        public static readonly Planet PlutoLowell = 53;
        public static readonly Planet PlutoPickering = 54;
        public static readonly Planet Vulcan = 55;
        public static readonly Planet WhiteMoon = 56;
        public static readonly Planet Proserpina = 57;
        public static readonly Planet Waldemath = 58;
        #endregion

        #endregion

        #region Asteroids

        public static readonly Planet AsteroidCeres = FirstAsteroid + 1;
        public static readonly Planet AsteroidPallas = FirstAsteroid + 2;
        public static readonly Planet AsteroidJuno = FirstAsteroid + 3;
        public static readonly Planet AsteroidVesta = FirstAsteroid + 4;
        public static readonly Planet AsteroidChiron = FirstAsteroid + 2060;
        public static readonly Planet AsteroidPholus = FirstAsteroid + 5145;

        /// <summary>
        /// Pluto as Asteroid
        /// </summary>
        public static readonly Planet AsteroidPluto = 134340;

        #endregion

        /// <summary>
        /// Last id of 'planet'
        /// </summary>
        public const int LastPlanet = 23;

        /// <summary>
        /// First id of fictitious
        /// </summary>
        public const int FirstFictitious = 40;

        /// <summary>
        /// First id of comets
        /// </summary>
        public const int FirstComet = 1000;

        /// <summary>
        /// First id of asteroid
        /// </summary>
        public const int FirstAsteroid = 10000;

        /// <summary>
        /// Create a new planet as asteroid
        /// </summary>
        /// <param name="id">Id of the asteroid</param>
        /// <returns>The asteroid</returns>
        public static Planet AsAsteroid(Int32 id) { return new Planet(FirstAsteroid + id); }

        /// <summary>
        /// New planet
        /// </summary>
        public Planet(Int32 id)
            : this() {
            this.Id = id;
        }

        /// <summary>
        /// String value
        /// </summary>
        public override string ToString() {
            return Id.ToString();
        }

        /// <summary>
        /// Implicit casting SwePlanet to Int32
        /// </summary>
        public static implicit operator Int32(Planet planet) { return planet.Id; }

        /// <summary>
        /// Implicit casting Int32 to SwePlanet 
        /// </summary>
        public static implicit operator Planet(Int32 id) { return new Planet(id); }

        /// <summary>
        /// Planet id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// This id is a planet ?
        /// </summary>
        public bool IsPlanet { get { return Id >= 0 && Id <= LastPlanet; } }

        /// <summary>
        /// This id is a fictitious ?
        /// </summary>
        public bool IsFictitious { get { return Id >= FirstFictitious && Id < FirstComet; } }

        /// <summary>
        /// This id is a comet ?
        /// </summary>
        public bool IsComet { get { return Id >= FirstComet && Id < FirstAsteroid; } }

        /// <summary>
        /// This id is an asteroid ?
        /// </summary>
        public bool IsAsteroid { get { return Id >= FirstAsteroid; } }

    }

}
