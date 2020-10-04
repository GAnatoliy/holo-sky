using Assets.Core.Scripts;
using UnityEngine;

public class EarthSateliteLoader : MonoBehaviour
{
    public GameObject SatelitePointPrefab;
    public Transform Container;
    public SphereCollider EarthCollider;

    public const float EARTH_RADIUS_IN_METERS = 6378137;

    private void Start()
    {
        ShowAllSatelites();
    }


    private void ShowAllSatelites()
    {
        var gsProvider = new DataObjectsProvider();
        var satelites = gsProvider.GetSatellites();

        foreach (var sat in satelites)
        {
            var satelite = CreateSatelite($"{sat.ObjectName}");
            var sateliteData = sat.GetGeodeticCoordinateNow();

            var altitude = sateliteData.Altitude / (EARTH_RADIUS_IN_METERS / EarthCollider.radius);
            satelite.SetCoordinates(sateliteData.Latitude, sateliteData.Longitude, altitude);
        }
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
}
