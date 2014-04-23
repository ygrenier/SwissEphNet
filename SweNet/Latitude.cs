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
            var sig = Math.Sign(value);
            value = Math.Abs(value);
            Degrees = (int)value;
            Minutes = ((int)(value * 60.0)) % 60;
            Seconds = ((int)(value * 3600.0)) % 60;
            while (Degrees >= 180) Degrees -= 180;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            if (sig < 0) Value = -Value;
            Polarity = sig < 0 ? LatitudePolarity.South : LatitudePolarity.North;
        }

        /// <summary>
        /// Create a latitude from his components
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public Latitude(int degrees, int minutes, int seconds)
            : this() {
            if (degrees <= -180 || degrees >= 180) throw new ArgumentOutOfRangeException("degrees");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("minutes");
            if (seconds < 0.0 || seconds >= 60.0) throw new ArgumentOutOfRangeException("seconds");
            Degrees = Math.Abs(degrees);
            Minutes = minutes;
            Seconds = seconds;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            if (degrees < 0) Value = -Value;
            Polarity = degrees < 0 ? LatitudePolarity.South : LatitudePolarity.North;
        }

        /// <summary>
        /// Create a latitude from his components
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="polarity"></param>
        public Latitude(int degrees, int minutes, int seconds, LatitudePolarity polarity)
            : this() {
            if (degrees < 0 || degrees >= 180) throw new ArgumentOutOfRangeException("degrees");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("minutes");
            if (seconds < 0.0 || seconds >= 60.0) throw new ArgumentOutOfRangeException("seconds");
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            if (polarity == LatitudePolarity.South) Value = -Value;
            Polarity = polarity;
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString() {
            return String.Format("{0}{3}{1:D2}'{2:D2}\"", Degrees, Minutes, Seconds, Polarity.ToString()[0]);
        }

        /// <summary>
        /// Implicit conversion of Latitude to Double
        /// </summary>
        public static implicit operator Double(Latitude lat) {
            return lat.Value;
        }

        /// <summary>
        /// Implicit conversion of Double to Latitude
        /// </summary>
        public static implicit operator Latitude(Double val) {
            return new Latitude(val);
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
        public int Seconds { get; private set; }

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
