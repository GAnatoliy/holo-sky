using System;
using One_Sgp4;


namespace Assets.Core.Scripts
{
    public class Satellite
    {
        // Earth angular velocity, rad/sec
        const double OMEGA_E = 7.2921151e-5;

        public string CcsdsOmmVers { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public string Originator { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string CenterName { get; set; }
        public string RefFrame { get; set; }
        public string TimeSystem { get; set; }
        public string MeanElementTheory { get; set; }
        public DateTime Epoch { get; set; }
        public string MeanMotion { get; set; }
        public string Eccentricity { get; set; }
        public string Inclination { get; set; }
        public string RaOfAscNode { get; set; }
        public string ArgOfPericenter { get; set; }
        public string MeanAnomaly { get; set; }
        public string EphemerisType { get; set; }
        public string ClassificationType { get; set; }
        public string NoradCatId { get; set; }
        public string ElementSetNo { get; set; }
        public string RevAtEpoch { get; set; }
        public string Bstar { get; set; }
        public string MeanMotionDot { get; set; }
        public string MeanMotionDdot { get; set; }
        public string SemimajorAxis { get; set; }
        public string Period { get; set; }
        public string Apoapsis { get; set; }
        public string Periapsis { get; set; }
        public string ObjectType { get; set; }
        public string RcsSize { get; set; }
        public string CountryCode { get; set; }
        public string LaunchDate { get; set; }
        public string Site { get; set; }
        public string DecayDate { get; set; }
        public string File { get; set; }
        public string GpId { get; set; }
        public string TleLine0 { get; set; }
        public string TleLine1 { get; set; }
        public string TleLine2 { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public GeoCoordinate GetGeodeticCoordinateNow()
        {
            var tleISS = ParserTLE.parseTle(TleLine1, TleLine2, TleLine0);
            var currentTime = new EpochTime(DateTime.UtcNow);
            var data = SatFunctions.getSatPositionAtTime(tleISS, currentTime, Sgp4.wgsConstant.WGS_84);
            var secondsFromStart = (currentTime.getEpoch() - Math.Truncate(currentTime.getEpoch())) * 24 * 60 * 60;
            var omega = OMEGA_E * secondsFromStart;

            var C = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(
                new double[,] {
                    {Math.Cos(omega), Math.Sin(omega), 0},
                    {-Math.Sin(omega), Math.Cos(omega), 0},
                    {0, 0, 1}});

            var p = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(
                new double[,] {
                    { data.getX() * 1000},
                    { data.getY() * 1000},
                    { data.getZ() * 1000}});

            var ecr = C * p;
            GpsUtils.EcefToGeodetic(ecr[0, 0], ecr[1, 0], ecr[2, 0], out var lat, out var lon, out var h);

            return new GeoCoordinate(lat, lon, h);
        }

        public bool IsVisibleFromPointNow(GeoCoordinate observer)
        {
            var tleISS = ParserTLE.parseTle(TleLine1, TleLine2, TleLine0);
            var currentTime = new EpochTime(DateTime.UtcNow);
            var data = One_Sgp4.SatFunctions.getSatPositionAtTime(tleISS, currentTime, Sgp4.wgsConstant.WGS_84);
            return SatFunctions.isSatVisible(new Coordinate(observer.Latitude, observer.Longitude, observer.Altitude), 0, currentTime, data);
        }
    }
}