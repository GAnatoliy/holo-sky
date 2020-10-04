using Assets.Core.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class SatelitesManager : MonoBehaviour
{
    public GameObject SatelitePrefab;
    public Transform Container;
    public Transform Origin;
    public SphereCollider EarthCollider;

    public const float EARTH_RADIUS_IN_METERS = 6378137;

    private List<GeoObject> _satelites = new List<GeoObject>();

    // Temporary satelites coordinates stub
    private List<Vector2> _stubSatelites = new List<Vector2>
    {
        new Vector2(47.583204f, 29.588764f),
        new Vector2(32.769229f, 42.884609f),
        new Vector2(51.163797f, 71.396299f)
    };

    public void ShowSatelitesInPoint(Vector3 point)
    {
        RemoveAllSatelites();

        var local = Origin.transform.InverseTransformPoint(point);
        var latLon = ToSpherical(local);

        foreach (var sat in _stubSatelites)
        {
            var satelite = CreateSatelite($"satelite_{_satelites.Count + 1}");

            var sateliteCoordinate = new GeoCoordinate(sat.x, sat.y);
            var convertedSpherical = new GeoCoordinate(latLon.x, latLon.y);

            var distance = GeoCoordinate.Distance(sateliteCoordinate, convertedSpherical);
            var altitude = Mathf.Clamp((float)distance / 1000 / 5000, 1, 20);

            satelite.SetCoordinates(sat.x, sat.y, altitude);
        }
    }

    private GeoObject CreateSatelite(string name)
    {
        var satelite = Instantiate(SatelitePrefab);
        satelite.transform.SetParent(Container, false);
        satelite.name = !string.IsNullOrEmpty(name) ? name : "Satelite";
        var sateliteObj = satelite.AddComponent<GeoObject>();
        sateliteObj.EarthObjectRadius = EarthCollider.radius;
        _satelites.Add(sateliteObj);

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