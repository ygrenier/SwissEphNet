﻿using SweNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.Models
{
    /// <summary>
    /// Input values for calculation
    /// </summary>
    public class InputCalculation
    {

        public InputCalculation() {
            Planets = new List<Planet>();
            TimeZone = TimeZoneInfo.Local;
            //Date = new DateUT(DateTime.Now);
            Date = new DateUT(1974, 8, 16, 0, 30, 0);
            Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            HouseSystem = "Placidus";
            Planets.AddRange(new Planet[] { 
                Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
                Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
                Planet.MeanNode, Planet.TrueNode,
                Planet.MeanApog, Planet.OscuApog, Planet.Earth
            });
            Planets.AddRange(new Planet[] { Planet.AsAsteroid(433), Planet.AsAsteroid(3045), Planet.AsAsteroid(7066) });
        }

        public TimeZoneInfo TimeZone { get; set; }

        public DateUT Date { get; set; }

        public DateUT DateUTC {
            get {
                TimeSpan daylight = TimeSpan.Zero;
                if (Date.Year > 0 && TimeZone.SupportsDaylightSavingTime && TimeZone.IsDaylightSavingTime(Date.ToDateTime()))
                    daylight = TimeSpan.FromHours(1);
                return Date - (TimeZone.BaseUtcOffset + daylight);
            }
        }

        /// <summary>
        /// Latitude
        /// </summary>
        public Latitude Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public Longitude Longitude { get; set; }

        /// <summary>
        /// Altitude
        /// </summary>
        public int Altitude { get; set; }

        /// <summary>
        /// House system
        /// </summary>
        public String HouseSystem { get; set; }

        /// <summary>
        /// Planets to calculate
        /// </summary>
        public List<Planet> Planets { get; private set; }

    }
}