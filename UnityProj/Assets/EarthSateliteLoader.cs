using Assets.Core.Scripts;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EarthSateliteLoader : MonoBehaviour
{
    public GameObject SatelitePointPrefab;
    public Transform Container;
    public SphereCollider EarthCollider;
    public EllipseOrbitFactory OrbitFactory;

    public const float EARTH_RADIUS_IN_METERS = 6378137;

    private SatellitedSelectEvent _sputnikSelectedEvent = new SatellitedSelectEvent();

    private Dictionary<string, SatelliteObject> _statellits = new Dictionary<string, SatelliteObject>();

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

            satelite.GetComponent<ClipableHandler>().OrbitFactory = OrbitFactory;

            var satellite = satelite.gameObject.AddComponent<SatelliteObject>();
            satellite.Init(sat);
            satellite.OnStationSelected(OnSatelliteSelected);


            _statellits.Add(sat.ObjectId, satellite);


            var altitude = sateliteData.Altitude / (EARTH_RADIUS_IN_METERS / EarthCollider.radius);
            satelite.SetCoordinates(sateliteData.Latitude, sateliteData.Longitude, altitude);
        }
    }

    private void OnSatelliteSelected(Satellite satellite)
    {
        _sputnikSelectedEvent.Invoke(satellite);
    }

    public void SatelliteSelected(UnityAction<Satellite> handler)
    {
        _sputnikSelectedEvent.AddListener(handler);
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
