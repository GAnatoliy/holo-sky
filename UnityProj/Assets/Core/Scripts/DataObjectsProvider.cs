using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Scripts.Dtos;
using UnityEngine;


namespace Assets.Core.Scripts
{
    [System.Serializable]
    public class GroundStationsDto
    {
        public List<GroundStationDto> GroundStations;
    }

    [System.Serializable]
    public class GroundStationDto
    {
        public string Id;
        public string Name;
        public GeoCoordinateDto Location;
        public string ImageUrl;
        public string Description;
    }

    [System.Serializable]
    public class GeoCoordinateDto
    {
        public double Latitude;
        public double Longitude;
    }

    [Serializable]
    public class SattelitesDto
    {
        public SatelliteDto[] data;
    }

    [Serializable]
    public class SatelliteDto
    {
        public string CCSDS_OMM_VERS;
        public string COMMENT;
        public DateTime CREATION_DATE;
        public string ORIGINATOR;
        public string OBJECT_NAME;
        public string OBJECT_ID;
        public string CENTER_NAME;
        public string REF_FRAME;
        public string TIME_SYSTEM;
        public string MEAN_ELEMENT_THEORY;
        public DateTime EPOCH;
        public string MEAN_MOTION;
        public string ECCENTRICITY;
        public string INCLINATION;
        public string RA_OF_ASC_NODE;
        public string ARG_OF_PERICENTER;
        public string MEAN_ANOMALY;
        public string EPHEMERIS_TYPE;
        public string CLASSIFICATION_TYPE;
        public string NORAD_CAT_ID;
        public string ELEMENT_SET_NO;
        public string REV_AT_EPOCH;
        public string BSTAR;
        public string MEAN_MOTION_DOT;
        public string MEAN_MOTION_DDOT;
        public string SEMIMAJOR_AXIS;
        public string PERIOD;
        public string APOAPSIS;
        public string PERIAPSIS;
        public string OBJECT_TYPE;
        public string RCS_SIZE;
        public string COUNTRY_CODE;
        public string LAUNCH_DATE;
        public string SITE;
        public string DECAY_DATE;
        public string FILE;
        public string GP_ID;
        public string TLE_LINE0;
        public string TLE_LINE1;
        public string TLE_LINE2;
        public string IMAGE_URL;
        public string DESCRIPTION;
    }

    public class DataObjectsProvider
    {
        private const string GroundStationsResourcePath = "data/groundstations";
        private const string SatellitesResourcePath = "data/spacetrackpayloads";

        private List<GroundStation> _groundStations = null;
        private List<Satellite> _satellites = null;


        public List<GroundStation> GetGroundStations()
        {
            if (_groundStations != null) {
                return _groundStations.ToList();
            }

            var groundStationsTextFile = Resources.Load<TextAsset>(GroundStationsResourcePath);
            string groundStationsJson = groundStationsTextFile.text;

            var groundStations = JsonUtility.FromJson<GroundStationsDto>(groundStationsJson);

            _groundStations = groundStations.GroundStations.Select(dto => new GroundStation {
                Id = dto.Id,
                Name = dto.Name,
                Location = new GeoCoordinate(dto.Location.Latitude, dto.Location.Longitude),
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            }).ToList();

            return _groundStations;
        }

        public List<Satellite> GetSatellites()
        {
            if (_satellites != null) {
                return _satellites.ToList();
            }

            var satellitesTextFile = Resources.Load<TextAsset>(SatellitesResourcePath);
            string satellitesJson = satellitesTextFile.text;

            var satellites = JsonUtility.FromJson<SattelitesDto>(satellitesJson);

            _satellites = satellites.data.Select(dto => new Satellite
            {
                CcsdsOmmVers = dto.CCSDS_OMM_VERS,
                Comment = dto.COMMENT,
                CreationDate = dto.CREATION_DATE,
                Originator = dto.ORIGINATOR,
                ObjectName = dto.OBJECT_NAME,
                ObjectId = dto.OBJECT_ID,
                CenterName = dto.CENTER_NAME,
                RefFrame = dto.REF_FRAME,
                TimeSystem = dto.TIME_SYSTEM,
                MeanElementTheory = dto.MEAN_ELEMENT_THEORY,
                Epoch = dto.EPOCH,
                MeanMotion = dto.MEAN_MOTION,
                Eccentricity = dto.ECCENTRICITY,
                Inclination = dto.INCLINATION,
                RaOfAscNode = dto.RA_OF_ASC_NODE,
                ArgOfPericenter = dto.ARG_OF_PERICENTER,
                MeanAnomaly = dto.MEAN_ANOMALY,
                EphemerisType = dto.EPHEMERIS_TYPE,
                ClassificationType = dto.CLASSIFICATION_TYPE,
                NoradCatId = dto.NORAD_CAT_ID,
                ElementSetNo = dto.ELEMENT_SET_NO,
                RevAtEpoch = dto.REV_AT_EPOCH,
                Bstar = dto.BSTAR,
                MeanMotionDot = dto.MEAN_MOTION_DOT,
                MeanMotionDdot = dto.MEAN_MOTION_DDOT,
                SemimajorAxis = dto.SEMIMAJOR_AXIS,
                Period = dto.PERIOD,
                Apoapsis = dto.APOAPSIS,
                Periapsis = dto.PERIAPSIS,
                ObjectType = dto.OBJECT_TYPE,
                RcsSize = dto.RCS_SIZE,
                CountryCode = dto.COUNTRY_CODE,
                LaunchDate = dto.LAUNCH_DATE,
                Site = dto.SITE,
                DecayDate = dto.DECAY_DATE,
                File = dto.FILE,
                GpId = dto.GP_ID,
                TleLine0 = dto.TLE_LINE0,
                TleLine1 = dto.TLE_LINE1,
                TleLine2 = dto.TLE_LINE2,
                ImageUrl = dto.IMAGE_URL,
                Description = dto.DESCRIPTION
            }).ToList();

            return _satellites;
        }
    }
}

