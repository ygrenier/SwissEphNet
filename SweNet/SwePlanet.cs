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

        #region Default planets
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

    }

}
