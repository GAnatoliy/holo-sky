using Assets.Core.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SatelitesManager : MonoBehaviour
{
    public GameObject SatelitePointPrefab;
    public GameObject SateliteWorldPrefab;
    public Transform Container;
    public Transform SceneContainer;
    public Transform Origin;
    public SphereCollider EarthCollider;

    public const float EARTH_RADIUS_IN_METERS = 6378137;

    private List<GameObject> _satelites = new List<GameObject>();
    

    private void Start()
    {
        ShowAllSatelites();
    }

    public void ShowSatelitesInPoint(Vector3 point)
    {
        RemoveAllSatelites();

        var local = Origin.transform.InverseTransformPoint(point);
        var latLon = ToSpherical(local);
        var sphericalGeocoordinate = new GeoCoordinate(latLon.x, latLon.y);

        var gsProvider = new DataObjectsProvider();
        var satelites = gsProvider.GetSatellites().Where(sat => sat.IsVisibleFromPointNow(sphericalGeocoordinate));

        foreach(var satelite in satelites)
        {
            var worldSatelite = CreateWorldSatelite($"{satelite.ObjectName}");

            var sateliteCoord = satelite.GetGeodeticCoordinateNow();

            var lat = Mathf.Clamp((float)sateliteCoord.Latitude, -90, 90);
            var lon = Mathf.Clamp((float)sateliteCoord.Longitude, -180, 180);
            var localPos = Quaternion.Euler(new Vector3(-lat, -lon)) * new Vector3(0, 0, EarthCollider.radius);

            worldSatelite.transform.position = localPos;
        }
    }

    private void ShowAllSatelites()
    {
        var gsProvider = new DataObjectsProvider();
        var satelites = gsProvider.GetSatellites();

        foreach (var sat in satelites)
        {
            var satelite = CreateSatelite($"{sat.ObjectName}");
            var sateliteData = sat.GetGeodeticCoordinateNow();

            var altitude = sateliteData.Altitude / EarthCollider.radius / EARTH_RADIUS_IN_METERS;
            satelite.SetCoordinates(sateliteData.Latitude, sateliteData.Longitude, altitude);
        }
    }

    private GameObject CreateWorldSatelite(string name)
    {
        var satelite = Instantiate(SateliteWorldPrefab);
        satelite.transform.SetParent(SceneContainer, false);
        satelite.name = !string.IsNullOrEmpty(name) ? name + "_World" : "Satelite_World";
        _satelites.Add(satelite);
        return satelite;
    }


    private GeoObject CreateSatelite(string name)
    {
        var satelite = Instantiate(SatelitePointPrefab);
        satelite.transform.SetParent(Container, false);
        satelite.name = !string.IsNullOrEmpty(name) ? name : "Satelite";
        var sateliteObj = satelite.AddComponent<GeoObject>();
        sateliteObj.EarthObjectRadius = EarthCollider.radius;
        return sateliteObj;
    }



    private void RemoveAllSatelites()
    {
        foreach(var sat in _satelites)
        {
            Destroy(sat);
        }

        _satelites.Clear();
    }


    //https://gamedev.stackexchange.com/questions/149109/calculating-the-latitude-longitude-of-a-raycast-hit-point-on-a-sphere
    public static Vector2 ToSpherical(Vector3 position)
    {
        // Convert to a unit vector so our y coordinate is in the range -1...1.
        position = Vector3.Normalize(position);

        // The vertical coordinate (y) varies as the sine of latitude, not the cosine.
        float lat = Mathf.Asin(position.y) * Mathf.Rad2Deg;

        // Use the 2-argument arctangent, which will correctly handle all four quadrants.
        float lon = Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg;

        // I usually put longitude first because I associate vector.x with "horizontal."
        return new Vector2(lat, -lon);
    }
}