using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{
    /// <summary>
    /// Latitude
    /// </summary>
    public struct Latitude
    {

        /// <summary>
        /// Create a latitude from a value
        /// </summary>
        /// <param name="value"></param>
        public Latitude(Double value)
            : this() {
            Degrees = (int)value;
            Minutes = ((int)(value * 60.0)) % 60;
            Seconds = ((int)(value * 3600.0)) % 3600;
            while (Degrees <= -180) Degrees += 180;
            while (Degrees >= 180) Degrees -= 180;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            Polarity = Degrees < 0 ? LatitudePolarity.South : LatitudePolarity.North;
        }

        /// <summary>
        /// Create a latitude from his components
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public Latitude(int degrees, uint minutes, double seconds)
            : this() {
            if (degrees <= -180 || degrees >= 180) throw new ArgumentOutOfRangeException("degrees");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("minutes");
            if (seconds < 0.0 || seconds >= 60.0) throw new ArgumentOutOfRangeException("seconds");
            Degrees = degrees;
            Minutes = (int)minutes;
            Seconds = seconds;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            Polarity = Degrees < 0 ? LatitudePolarity.South : LatitudePolarity.North;
        }

        /// <summary>
        /// Create a latitude from his components
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="polarity"></param>
        public Latitude(uint degrees, int minutes, double seconds, LatitudePolarity polarity)
            : this() {
            if (degrees >= 180) throw new ArgumentOutOfRangeException("degrees");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("minutes");
            if (seconds < 0.0 || seconds >= 60.0) throw new ArgumentOutOfRangeException("seconds");
            Degrees = (int)degrees * (polarity == LatitudePolarity.South ? -1 : 1);
            Minutes = minutes;
            Seconds = seconds;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            Polarity = polarity;
        }

        /// <summary>
        /// Degrees
        /// </summary>
        public int Degrees { get; private set; }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; private set; }

        /// <summary>
        /// Seconds
        /// </summary>
        public Double Seconds { get; private set; }

        /// <summary>
        /// Polarity
        /// </summary>
        public LatitudePolarity Polarity { get; private set; }

        /// <summary>
        /// Numeric value
        /// </summary>
        public Double Value { get; private set; }

    }

    /// <summary>
    /// Latitude polarity
    /// </summary>
    public enum LatitudePolarity
    {
        /// <summary>
        /// North (Positive)
        /// </summary>
        North,
        /// <summary>
        /// South (Negative)
        /// </summary>
        South
    }

}
