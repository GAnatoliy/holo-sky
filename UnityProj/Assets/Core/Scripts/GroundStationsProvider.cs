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

    public class GroundStationsProvider
    {
        private const string ResourcePath = "data/groundstations";

        public List<GroundStation> GetGroundStations()
        {
            var groundStationsTextFile = Resources.Load<TextAsset>(ResourcePath);
            string groundStationsJson = groundStationsTextFile.text;
            Debug.Log(groundStationsJson);

            var groundStations = JsonUtility.FromJson<GroundStationsDto>(groundStationsJson);
            Debug.Log(groundStations);


            return groundStations.GroundStations.Select(dto => new GroundStation {
                Id = dto.Id,
                Name = dto.Name,
                Location = new GeoCoordinate(dto.Location.Latitude, dto.Location.Longitude),
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            }).ToList();
        }
    }

}

