using System;
using System.Globalization;


namespace Assets.Core.Scripts
{
    /// <summary>
    /// Represents geo coordinate.
    /// </summary>
    [Serializable]
    public struct GeoCoordinate
    {
        /// <summary>
        /// Precision that is used for equality, is selected according to this post http://gis.stackexchange.com/questions/8650/measuring-accuracy-of-latitude-and-longitude
        /// </summary>
        private const double COORDINATE_DELTA_PER_METER = 0.00001;
        /// <summary>
        /// Default precision is point within 1 sm.
        /// </summary>
        private const double DEFAULT_PRECISION = 0.01 * COORDINATE_DELTA_PER_METER;

        private const double MIN_LATITUDE = -90;
        private const double MAX_LATITUDE = 90;
        private const double MIN_LONGITUDE = -180;
        private const double MAX_LONGITUDE = 180;

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double Altitude { get; private set; }
        public bool HasAltitude { get; private set; }

        public GeoCoordinate(double latitude, double longitude, double? altitude = null)
        {
            if (latitude < MIN_LATITUDE || latitude > MAX_LATITUDE)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude should be in range [-90;90]");
            }

            if (longitude < MIN_LONGITUDE || longitude > MAX_LONGITUDE)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "Latitude should be in range [-180;180]");
            }

            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude ?? 0.0;
            HasAltitude = altitude.HasValue;
        }

        public override string ToString()
        {
            return $"{Latitude.ToString(CultureInfo.InvariantCulture)},{Longitude.ToString(CultureInfo.InvariantCulture)},{Altitude}";
        }

        // TODO: write unittests.
        public override bool Equals(object other)
        {
            return other is GeoCoordinate coordinate && EqualsWithPrecision(coordinate, DEFAULT_PRECISION);
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }

        /// <summary>
        /// Returns new geo coordinate without altitude.
        /// </summary>
        /// <returns></returns>
        public GeoCoordinate IgnoreAltitude()
        {
            return new GeoCoordinate(Latitude, Longitude);
        }

        /// <summary>
        /// Compares two coordinates with give precision
        /// </summary>
        /// <param name="other">Other object for comparison.</param>
        /// <param name="precision">Precision in meters.</param>
        /// <returns></returns>
        // TODO: write unittests.
        public bool Equals(GeoCoordinate other, double precision)
        {
            return EqualsWithPrecision(other, precision * COORDINATE_DELTA_PER_METER);
        }

        // https://stackoverflow.com/a/24712129/952023
        public static double Distance(GeoCoordinate from, GeoCoordinate to)
        {
            var baseRad = Math.PI * from.Latitude / 180;
            var targetRad = Math.PI * to.Latitude / 180;
            var theta = from.Longitude - to.Longitude;
            var thetaRad = Math.PI * theta / 180;

            var dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = dist * 1609.344;
            return dist;
        }

        /// <summary>
        /// Make subtraction of two geo coordinates (by analogy of two vectors).
        /// </summary>
        /// <param name="lhs">Left coordinate.</param>
        /// <param name="rhs">Right coordinate.</param>
        /// <returns></returns>
        public static GeoCoordinate operator -(GeoCoordinate lhs, GeoCoordinate rhs)
        {
            // Handle move over north/south pole.
            var latitude = lhs.Latitude - rhs.Latitude;
            if (latitude > MAX_LATITUDE)
            {
                latitude = 180 - latitude;
            }
            else if (latitude < MIN_LATITUDE)
            {
                latitude = -180 - latitude;
            }

            // Handle move over anti-meridian.
            var longitude = lhs.Longitude - rhs.Longitude;
            if (longitude > MAX_LONGITUDE)
            {
                longitude = MIN_LONGITUDE + (longitude % MAX_LONGITUDE);
            }
            else if (longitude < MIN_LONGITUDE)
            {
                longitude = MAX_LONGITUDE + (longitude % MAX_LONGITUDE);
            }

            return new GeoCoordinate(
                latitude,
                longitude,
                lhs.HasAltitude ? lhs.Altitude - rhs.Altitude : (double?)null);
        }

        /// <summary>
        /// Make addition of two geo coordinates (by analogy of two vectors).
        /// </summary>
        /// <param name="lhs">Left coordinate.</param>
        /// <param name="rhs">Right coordinate.</param>
        /// <returns></returns>
        public static GeoCoordinate operator +(GeoCoordinate lhs, GeoCoordinate rhs)
        {
            // TODO:36686 write unittests.
            // Handle move over north/south pole.
            var latitude = lhs.Latitude + rhs.Latitude;
            if (latitude > MAX_LATITUDE)
            {
                latitude = 180 - latitude;
            }
            else if (latitude < MIN_LATITUDE)
            {
                latitude = -180 - latitude;
            }

            // Handle move over anti-meridian.
            var longitude = lhs.Longitude + rhs.Longitude;
            if (longitude > MAX_LONGITUDE)
            {
                longitude = MIN_LONGITUDE + (longitude % MAX_LONGITUDE);
            }
            else if (longitude < MIN_LONGITUDE)
            {
                longitude = MAX_LONGITUDE + (longitude % MAX_LONGITUDE);
            }

            return new GeoCoordinate(
                latitude,
                longitude,
                lhs.HasAltitude ? lhs.Altitude + rhs.Altitude : (double?)null);
        }

        private bool EqualsWithPrecision(GeoCoordinate other, double precision)
        {
            // NOTE: points are equal if they are placed within specified square, will
            // be more precise to calculate distance, but current method is enough for us.
            return Math.Abs(Latitude - other.Latitude) < precision
                && Math.Abs(Longitude - other.Longitude) < precision
                && Math.Abs(Altitude - other.Altitude) < precision;
        }
    }
}