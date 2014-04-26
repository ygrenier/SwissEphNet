using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    /// <summary>
    /// Planet number
    /// </summary>
    public struct SwePlanet
    {

        #region Planets

        /// <summary>
        /// Sun
        /// </summary>
        public static readonly SwePlanet Sun = 0;
        /// <summary>
        /// Moon
        /// </summary>
        public static readonly SwePlanet Moon = 1;
        /// <summary>
        /// Mercury
        /// </summary>
        public static readonly SwePlanet Mercury = 2;
        /// <summary>
        /// Venus
        /// </summary>
        public static readonly SwePlanet Venus = 3;
        /// <summary>
        /// Mars
        /// </summary>
        public static readonly SwePlanet Mars = 4;
        /// <summary>
        /// Jupiter
        /// </summary>
        public static readonly SwePlanet Jupiter = 5;
        /// <summary>
        /// Saturn
        /// </summary>
        public static readonly SwePlanet Saturn = 6;
        /// <summary>
        /// Uranus
        /// </summary>
        public static readonly SwePlanet Uranus = 7;
        /// <summary>
        /// Neptune
        /// </summary>
        public static readonly SwePlanet Neptune = 8;
        /// <summary>
        /// Pluto
        /// </summary>
        public static readonly SwePlanet Pluto = 9;
        /// <summary>
        /// MeanNode
        /// </summary>
        public static readonly SwePlanet MeanNode = 10;
        /// <summary>
        /// TrueNode
        /// </summary>
        public static readonly SwePlanet TrueNode = 11;
        /// <summary>
        /// MeanApog
        /// </summary>
        public static readonly SwePlanet MeanApog = 12;
        /// <summary>
        /// OscuApog
        /// </summary>
        public static readonly SwePlanet OscuApog = 13;
        /// <summary>
        /// Earth
        /// </summary>
        public static readonly SwePlanet Earth = 14;
        /// <summary>
        /// Chiron
        /// </summary>
        public static readonly SwePlanet Chiron = 15;
        /// <summary>
        /// Pholus
        /// </summary>
        public static readonly SwePlanet Pholus = 16;
        /// <summary>
        /// Ceres
        /// </summary>
        public static readonly SwePlanet Ceres = 17;
        /// <summary>
        /// Pallas
        /// </summary>
        public static readonly SwePlanet Pallas = 18;
        /// <summary>
        /// Juno
        /// </summary>
        public static readonly SwePlanet Juno = 19;
        /// <summary>
        /// Vesta
        /// </summary>
        public static readonly SwePlanet Vesta = 20;
        /// <summary>
        /// IntpApog
        /// </summary>
        public static readonly SwePlanet IntpApog = 21;
        /// <summary>
        /// IntpPerg
        /// </summary>
        public static readonly SwePlanet IntpPerg = 22;

        #endregion

        #region Fictitious

        #region Hamburger or Uranian "planets"
        public static readonly SwePlanet Cupido = 40;
        public static readonly SwePlanet Hades = 41;
        public static readonly SwePlanet Zeus = 42;
        public static readonly SwePlanet Kronos = 43;
        public static readonly SwePlanet Apollon = 44;
        public static readonly SwePlanet Admetos = 45;
        public static readonly SwePlanet Vulkanus = 46;
        public static readonly SwePlanet Poseidon = 47;
        #endregion

        #region Other fictitious bodies
        public static readonly SwePlanet Isis = 48;
        public static readonly SwePlanet Nibiru = 49;
        public static readonly SwePlanet Harrington = 50;
        public static readonly SwePlanet NeptuneLeverrier = 51;
        public static readonly SwePlanet NeptuneAdams = 52;
        public static readonly SwePlanet PlutoLowell = 53;
        public static readonly SwePlanet PlutoPickering = 54;
        public static readonly SwePlanet Vulcan = 55;
        public static readonly SwePlanet WhiteMoon = 56;
        public static readonly SwePlanet Proserpina = 57;
        public static readonly SwePlanet Waldemath = 58;
        #endregion

        #endregion

        #region Asteroids

        public static readonly SwePlanet AsteroidCeres = FirstAsteroid + 1;
        public static readonly SwePlanet AsteroidPallas = FirstAsteroid + 2;
        public static readonly SwePlanet AsteroidJuno = FirstAsteroid + 3;
        public static readonly SwePlanet AsteroidVesta = FirstAsteroid + 4;
        public static readonly SwePlanet AsteroidChiron = FirstAsteroid + 2060;
        public static readonly SwePlanet AsteroidPholus = FirstAsteroid + 5145;

        /// <summary>
        /// Pluto as Asteroid
        /// </summary>
        public static readonly SwePlanet AsteroidPluto = 134340;

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
        /// New planet
        /// </summary>
        public SwePlanet(Int32 id)
            : this() {
            this.Id = id;
        }

        /// <summary>
        /// Implicit casting SwePlanet to Int32
        /// </summary>
        public static implicit operator Int32(SwePlanet planet) { return planet.Id; }

        /// <summary>
        /// Implicit casting Int32 to SwePlanet 
        /// </summary>
        public static implicit operator SwePlanet(Int32 id) { return new SwePlanet(id); }

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
